// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq
{
    public static partial class AsyncEnumerable
    {
        /// <summary>
        /// Creates an immutable sorted set from an async-enumerable sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">The source async-enumerable sequence to get a hash set of elements for.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with a sorted set containing all the elements of the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<ImmutableSortedSet<TSource>> ToImmutableSortedSetAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken cancellationToken = default)
            => ToImmutableSortedSetAsync(source, comparer: null, cancellationToken);

        /// <summary>
        /// Creates an immutable sorted set from an async-enumerable sequence according to a specified comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">The source async-enumerable sequence to get a hash set of elements for.</param>
        /// <param name="comparer">A comparer to compare elements of the sequence.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with a hash set containing all the elements of the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<ImmutableSortedSet<TSource>> ToImmutableSortedSetAsync<TSource>(this IAsyncEnumerable<TSource> source, IComparer<TSource>? comparer, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, comparer, cancellationToken);

            static async ValueTask<ImmutableSortedSet<TSource>> Core(IAsyncEnumerable<TSource> source, IComparer<TSource>? comparer, CancellationToken cancellationToken)
            {
                var builder = ImmutableSortedSet.CreateBuilder(comparer);

                await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    builder.Add(item);
                }

                return builder.ToImmutable();
            }
        }
    }
}
