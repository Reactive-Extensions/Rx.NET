// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT License.
// See the LICENSE file in the project root for more information. 

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
    // Based on https://github.com/dotnet/corefx/blob/ec2685715b01d12f16b08d0dfa326649b12db8ec/src/Common/src/System/Collections/Generic/EnumerableHelpers.cs
    internal static class AsyncEnumerableHelpers
    {
        internal static async ValueTask<T[]> ToArray<T>(IAsyncEnumerable<T> source, CancellationToken cancellationToken)
        {
            var result = await ToArrayWithLength(source, cancellationToken).ConfigureAwait(false);
            Array.Resize(ref result.Array, result.Length);
            return result.Array;
        }

        internal static async ValueTask<ArrayWithLength<T>> ToArrayWithLength<T>(IAsyncEnumerable<T> source, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = new ArrayWithLength<T>();
            // Check for short circuit optimizations. This one is very unlikely
            // but could be here as a group
            if (source is ICollection<T> ic)
            {
                var count = ic.Count;
                if (count != 0)
                {
                    // Allocate an array of the desired size, then copy the elements into it. Note that this has the same 
                    // issue regarding concurrency as other existing collections like List<T>. If the collection size 
                    // concurrently changes between the array allocation and the CopyTo, we could end up either getting an 
                    // exception from overrunning the array (if the size went up) or we could end up not filling as many 
                    // items as 'count' suggests (if the size went down).  This is only an issue for concurrent collections 
                    // that implement ICollection<T>, which as of .NET 4.6 is just ConcurrentDictionary<TKey, TValue>.
                    result.Array = new T[count];
                    ic.CopyTo(result.Array, 0);
                    result.Length = count;
                    return result;
                }
            }
            else
            {
                await using var en = source.GetConfiguredAsyncEnumerator(cancellationToken, false);

                if (await en.MoveNextAsync())
                {
                    const int DefaultCapacity = 4;
                    var arr = new T[DefaultCapacity];
                    arr[0] = en.Current;
                    var count = 1;

                    while (await en.MoveNextAsync())
                    {
                        if (count == arr.Length)
                        {
                            // MaxArrayLength is defined in Array.MaxArrayLength and in gchelpers in CoreCLR.
                            // It represents the maximum number of elements that can be in an array where
                            // the size of the element is greater than one byte; a separate, slightly larger constant,
                            // is used when the size of the element is one.
                            const int MaxArrayLength = 0x7FEFFFFF;

                            // This is the same growth logic as in List<T>:
                            // If the array is currently empty, we make it a default size.  Otherwise, we attempt to 
                            // double the size of the array.  Doubling will overflow once the size of the array reaches
                            // 2^30, since doubling to 2^31 is 1 larger than Int32.MaxValue.  In that case, we instead 
                            // constrain the length to be MaxArrayLength (this overflow check works because of the 
                            // cast to uint).  Because a slightly larger constant is used when T is one byte in size, we 
                            // could then end up in a situation where arr.Length is MaxArrayLength or slightly larger, such 
                            // that we constrain newLength to be MaxArrayLength but the needed number of elements is actually 
                            // larger than that.  For that case, we then ensure that the newLength is large enough to hold 
                            // the desired capacity.  This does mean that in the very rare case where we've grown to such a 
                            // large size, each new element added after MaxArrayLength will end up doing a resize.
                            var newLength = count << 1;
                            if ((uint)newLength > MaxArrayLength)
                            {
                                newLength = MaxArrayLength <= count ? count + 1 : MaxArrayLength;
                            }

                            Array.Resize(ref arr, newLength);
                        }

                        arr[count++] = en.Current;
                    }

                    result.Length = count;
                    result.Array = arr;
                    return result;
                }
            }

            result.Length = 0;
#if NO_ARRAY_EMPTY
            result.Array = EmptyArray<T>.Value;
#else
            result.Array = Array.Empty<T>();
#endif
            return result;
        }

        internal static async Task<Set<T>> ToSet<T>(IAsyncEnumerable<T> source, IEqualityComparer<T>? comparer, CancellationToken cancellationToken)
        {
            var set = new Set<T>(comparer);

            await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            {
                set.Add(item);
            }

            return set;
        }

        internal static async ValueTask<TResult> ToCollection<TSource, TCollection, TResult>(
            this IAsyncEnumerable<TSource> source,
            TCollection collection,
            Func<TCollection, TResult> resultSelector,
            CancellationToken cancellationToken)
            where TCollection : ICollection<TSource>
        {
            await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            {
                collection.Add(item);
            }

            return resultSelector(collection);
        }

        internal struct ArrayWithLength<T>
        {
            public T[] Array;
            public int Length;
        }
    }
}
