// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT License.
// See the LICENSE file in the project root for more information. 

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq
{
    public static partial class AsyncEnumerable
    {
        /// <summary>
        /// Bypasses elements in an async-enumerable sequence as long as a specified condition is true and then returns the remaining elements.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to return elements from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An async-enumerable sequence that contains the elements from the input sequence starting at the first element in the linear series that does not pass the test specified by predicate.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        public static IAsyncEnumerable<TSource> SkipWhile<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (predicate == null)
                throw Error.ArgumentNull(nameof(predicate));

            return Core(source, predicate);

            static async IAsyncEnumerable<TSource> Core(IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
            {
                await using var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false);

                while (await e.MoveNextAsync())
                {
                    var element = e.Current;

                    if (!predicate(element))
                    {
                        yield return element;

                        while (await e.MoveNextAsync())
                        {
                            yield return e.Current;
                        }

                        yield break;
                    }
                }
            }
        }

        /// <summary>
        /// Bypasses elements in an async-enumerable sequence as long as a specified condition is true and then returns the remaining elements.
        /// The element's index is used in the logic of the predicate function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to return elements from.</param>
        /// <param name="predicate">A function to test each element for a condition; the second parameter of the function represents the index of the source element.</param>
        /// <returns>An async-enumerable sequence that contains the elements from the input sequence starting at the first element in the linear series that does not pass the test specified by predicate.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        public static IAsyncEnumerable<TSource> SkipWhile<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (predicate == null)
                throw Error.ArgumentNull(nameof(predicate));

            return Core(source, predicate);

            static async IAsyncEnumerable<TSource> Core(IAsyncEnumerable<TSource> source, Func<TSource, int, bool> predicate, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
            {
                await using var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false);
                var index = -1;

                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        index++;
                    }

                    var element = e.Current;

                    if (!predicate(element, index))
                    {
                        yield return element;

                        while (await e.MoveNextAsync())
                        {
                            yield return e.Current;
                        }

                        yield break;
                    }
                }
            }
        }

        /// <summary>
        /// Bypasses elements in an async-enumerable sequence as long as a condition is true, and then returns the remaining elements.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to return elements from.</param>
        /// <param name="predicate">An asynchronous function to test each element for a condition.</param>
        /// <returns>An async-enumerable sequence containing the elements in the source sequence starting at the first element that does not pass the test specified by the predicate.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<TSource> SkipWhileAwaitCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (predicate == null)
                throw Error.ArgumentNull(nameof(predicate));

            return Core(source, predicate);

            static async IAsyncEnumerable<TSource> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
            {
                await using var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false);

                while (await e.MoveNextAsync())
                {
                    var element = e.Current;

                    if (!await predicate(element).ConfigureAwait(false))
                    {
                        yield return element;

                        while (await e.MoveNextAsync())
                        {
                            yield return e.Current;
                        }

                        yield break;
                    }
                }
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<TSource> SkipWhileAwaitWithCancellationCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (predicate == null)
                throw Error.ArgumentNull(nameof(predicate));

            return Core(source, predicate);

            static async IAsyncEnumerable<TSource> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
            {
                await using var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false);

                while (await e.MoveNextAsync())
                {
                    var element = e.Current;

                    if (!await predicate(element, cancellationToken).ConfigureAwait(false))
                    {
                        yield return element;

                        while (await e.MoveNextAsync())
                        {
                            yield return e.Current;
                        }

                        yield break;
                    }
                }
            }
        }
#endif

        /// <summary>
        /// Bypasses elements in an async-enumerable sequence as long as a condition is true, and then returns the remaining elements.
        /// The index of the element is used by the predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to return elements from.</param>
        /// <param name="predicate">An asynchronous function to test each element for a condition; the second parameter of the function represents the index of the element.</param>
        /// <returns>An async-enumerable sequence containing the elements in the source sequence starting at the first element that does not pass the test specified by the predicate.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<TSource> SkipWhileAwaitCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, ValueTask<bool>> predicate)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (predicate == null)
                throw Error.ArgumentNull(nameof(predicate));

            return Core(source, predicate);

            static async IAsyncEnumerable<TSource> Core(IAsyncEnumerable<TSource> source, Func<TSource, int, ValueTask<bool>> predicate, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
            {
                await using var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false);
                var index = -1;

                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        index++;
                    }

                    var element = e.Current;

                    if (!await predicate(element, index).ConfigureAwait(false))
                    {
                        yield return element;

                        while (await e.MoveNextAsync())
                        {
                            yield return e.Current;
                        }

                        yield break;
                    }
                }
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<TSource> SkipWhileAwaitWithCancellationCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, ValueTask<bool>> predicate)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (predicate == null)
                throw Error.ArgumentNull(nameof(predicate));

            return Core(source, predicate);

            static async IAsyncEnumerable<TSource> Core(IAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, ValueTask<bool>> predicate, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
            {
                await using var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false);
                var index = -1;

                while (await e.MoveNextAsync())
                {
                    checked
                    {
                        index++;
                    }

                    var element = e.Current;

                    if (!await predicate(element, index, cancellationToken).ConfigureAwait(false))
                    {
                        yield return element;

                        while (await e.MoveNextAsync())
                        {
                            yield return e.Current;
                        }

                        yield break;
                    }
                }
            }
        }
#endif
    }
}
