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
        /// Returns the minimum element in an async-enumerable sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to determine the minimum element of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a single element with the minimum element in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<TSource> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            if (default(TSource)! == null) // NB: Null value is desired; JIT-time check.
            {
                return Core(source, cancellationToken);

                static async ValueTask<TSource> Core(IAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
                {
                    var comparer = Comparer<TSource>.Default;

                    TSource value;

                    await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                    {
                        do
                        {
                            if (!await e.MoveNextAsync())
                            {
                                return default!;
                            }

                            value = e.Current;
                        }
                        while (value == null);

                        while (await e.MoveNextAsync())
                        {
                            var x = e.Current;

                            if (x != null && comparer.Compare(x, value) < 0)
                            {
                                value = x;
                            }
                        }
                    }

                    return value;
                }
            }
            else
            {
                return Core(source, cancellationToken);

                static async ValueTask<TSource> Core(IAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
                {
                    var comparer = Comparer<TSource>.Default;

                    TSource value;

                    await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                    {
                        if (!await e.MoveNextAsync())
                        {
                            throw Error.NoElements();
                        }

                        value = e.Current;

                        while (await e.MoveNextAsync())
                        {
                            var x = e.Current;

                            if (comparer.Compare(x, value) < 0)
                            {
                                value = x;
                            }
                        }
                    }

                    return value;
                }
            }
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TResult">The type of the objects derived from the elements in the source sequence to determine the minimum of.</typeparam>
        /// <param name="source">An async-enumerable sequence to determine the minimum element of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask sequence containing a single element with the value that corresponds to the minimum element in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<TResult> MinAsync<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, TResult> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            if (default(TResult)! == null) // NB: Null value is desired; JIT-time check.
            {
                return Core(source, selector, cancellationToken);

                static async ValueTask<TResult> Core(IAsyncEnumerable<TSource> source, Func<TSource, TResult> selector, CancellationToken cancellationToken)
                {
                    var comparer = Comparer<TResult>.Default;

                    TResult value;

                    await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                    {
                        do
                        {
                            if (!await e.MoveNextAsync())
                            {
                                return default!;
                            }

                            value = selector(e.Current);
                        }
                        while (value == null);

                        while (await e.MoveNextAsync())
                        {
                            var x = selector(e.Current);

                            if (x != null && comparer.Compare(x, value) < 0)
                            {
                                value = x;
                            }
                        }
                    }

                    return value;
                }
            }
            else
            {
                return Core(source, selector, cancellationToken);

                static async ValueTask<TResult> Core(IAsyncEnumerable<TSource> source, Func<TSource, TResult> selector, CancellationToken cancellationToken)
                {
                    var comparer = Comparer<TResult>.Default;

                    TResult value;

                    await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                    {
                        if (!await e.MoveNextAsync())
                        {
                            throw Error.NoElements();
                        }

                        value = selector(e.Current);

                        while (await e.MoveNextAsync())
                        {
                            var x = selector(e.Current);

                            if (comparer.Compare(x, value) < 0)
                            {
                                value = x;
                            }
                        }
                    }

                    return value;
                }
            }
        }

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the minimum value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TResult">The type of the objects derived from the elements in the source sequence to determine the minimum of.</typeparam>
        /// <param name="source">An async-enumerable sequence to determine the minimum element of.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask sequence containing a single element with the value that corresponds to the minimum element in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<TResult> MinAwaitAsyncCore<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TResult>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            if (default(TResult)! == null) // NB: Null value is desired; JIT-time check.
            {
                return Core(source, selector, cancellationToken);

                static async ValueTask<TResult> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TResult>> selector, CancellationToken cancellationToken)
                {
                    var comparer = Comparer<TResult>.Default;

                    TResult value;

                    await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                    {
                        do
                        {
                            if (!await e.MoveNextAsync())
                            {
                                return default!;
                            }

                            value = await selector(e.Current).ConfigureAwait(false);
                        }
                        while (value == null);

                        while (await e.MoveNextAsync())
                        {
                            var x = await selector(e.Current).ConfigureAwait(false);

                            if (x != null && comparer.Compare(x, value) < 0)
                            {
                                value = x;
                            }
                        }
                    }

                    return value;
                }
            }
            else
            {
                return Core(source, selector, cancellationToken);

                static async ValueTask<TResult> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TResult>> selector, CancellationToken cancellationToken)
                {
                    var comparer = Comparer<TResult>.Default;

                    TResult value;

                    await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                    {
                        if (!await e.MoveNextAsync())
                        {
                            throw Error.NoElements();
                        }

                        value = await selector(e.Current).ConfigureAwait(false);

                        while (await e.MoveNextAsync())
                        {
                            var x = await selector(e.Current).ConfigureAwait(false);

                            if (comparer.Compare(x, value) < 0)
                            {
                                value = x;
                            }
                        }
                    }

                    return value;
                }
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<TResult> MinAwaitWithCancellationAsyncCore<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TResult>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            if (default(TResult)! == null) // NB: Null value is desired; JIT-time check.
            {
                return Core(source, selector, cancellationToken);

                static async ValueTask<TResult> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TResult>> selector, CancellationToken cancellationToken)
                {
                    var comparer = Comparer<TResult>.Default;

                    TResult value;

                    await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                    {
                        do
                        {
                            if (!await e.MoveNextAsync())
                            {
                                return default!;
                            }

                            value = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                        }
                        while (value == null);

                        while (await e.MoveNextAsync())
                        {
                            var x = await selector(e.Current, cancellationToken).ConfigureAwait(false);

                            if (x != null && comparer.Compare(x, value) < 0)
                            {
                                value = x;
                            }
                        }
                    }

                    return value;
                }
            }
            else
            {
                return Core(source, selector, cancellationToken);

                static async ValueTask<TResult> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TResult>> selector, CancellationToken cancellationToken)
                {
                    var comparer = Comparer<TResult>.Default;

                    TResult value;

                    await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                    {
                        if (!await e.MoveNextAsync())
                        {
                            throw Error.NoElements();
                        }

                        value = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                        while (await e.MoveNextAsync())
                        {
                            var x = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                            if (comparer.Compare(x, value) < 0)
                            {
                                value = x;
                            }
                        }
                    }

                    return value;
                }
            }
        }
#endif
    }
}
