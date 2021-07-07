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
        /// Applies an accumulator function over an async-enumerable sequence, returning the result of the aggregation as a single element in the result sequence.
        /// For aggregation behavior with incremental intermediate results, see System.Interactive.Async.AsyncEnumerableEx.Scan{TSource}.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence and the result of the aggregation.</typeparam>
        /// <param name="source">An async-enumerable sequence to aggregate over.</param>
        /// <param name="accumulator">An accumulator function to be invoked on each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the final accumulator value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="accumulator"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) The source sequence is empty.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<TSource> AggregateAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, TSource, TSource> accumulator, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (accumulator == null)
                throw Error.ArgumentNull(nameof(accumulator));

            return Core(source, accumulator, cancellationToken);

            static async ValueTask<TSource> Core(IAsyncEnumerable<TSource> source, Func<TSource, TSource, TSource> accumulator, CancellationToken cancellationToken)
            {
                await using var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false);

                if (!await e.MoveNextAsync())
                {
                    throw Error.NoElements();
                }

                var acc = e.Current;

                while (await e.MoveNextAsync())
                {
                    acc = accumulator(acc, e.Current);
                }

                return acc;
            }
        }

        /// <summary>
        /// Applies an accumulator function over an async-enumerable sequence, returning the result of the aggregation as a single element in the result sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to aggregate over.</param>
        /// <param name="accumulator">An asynchronous accumulator function to be invoked and awaited on each element.</param>
        /// <param name="cancellationToken">An optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the final accumulator value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="accumulator"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">The source sequence is empty.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<TSource> AggregateAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, TSource, ValueTask<TSource>> accumulator, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (accumulator == null)
                throw Error.ArgumentNull(nameof(accumulator));

            return Core(source, accumulator, cancellationToken);

            static async ValueTask<TSource> Core(IAsyncEnumerable<TSource> source, Func<TSource, TSource, ValueTask<TSource>> accumulator, CancellationToken cancellationToken)
            {
                await using var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false);

                if (!await e.MoveNextAsync())
                {
                    throw Error.NoElements();
                }

                var acc = e.Current;

                while (await e.MoveNextAsync())
                {
                    acc = await accumulator(acc, e.Current).ConfigureAwait(false);
                }

                return acc;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<TSource> AggregateAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, TSource, CancellationToken, ValueTask<TSource>> accumulator, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (accumulator == null)
                throw Error.ArgumentNull(nameof(accumulator));

            return Core(source, accumulator, cancellationToken);

            static async ValueTask<TSource> Core(IAsyncEnumerable<TSource> source, Func<TSource, TSource, CancellationToken, ValueTask<TSource>> accumulator, CancellationToken cancellationToken)
            {
                await using var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false);

                if (!await e.MoveNextAsync())
                {
                    throw Error.NoElements();
                }

                var acc = e.Current;

                while (await e.MoveNextAsync())
                {
                    acc = await accumulator(acc, e.Current, cancellationToken).ConfigureAwait(false);
                }

                return acc;
            }
        }
#endif

        /// <summary>
        /// Applies an accumulator function over an async-enumerable sequence, returning the result of the aggregation as a single element in the result sequence. The specified seed value is used as the initial accumulator value.
        /// For aggregation behavior with incremental intermediate results, see System.Interactive.Async.AsyncEnumerableEx.Scan{TSource, Accumulate}".
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TAccumulate">The type of the result of the aggregation.</typeparam>
        /// <param name="source">An async-enumerable sequence to aggregate over.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="accumulator">An accumulator function to be invoked on each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the final accumulator value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="accumulator"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<TAccumulate> AggregateAsync<TSource, TAccumulate>(this IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> accumulator, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (accumulator == null)
                throw Error.ArgumentNull(nameof(accumulator));

            return Core(source, seed, accumulator, cancellationToken);

            static async ValueTask<TAccumulate> Core(IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> accumulator, CancellationToken cancellationToken)
            {
                var acc = seed;

                await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    acc = accumulator(acc, item);
                }

                return acc;
            }
        }

        /// <summary>
        /// Applies an accumulator function over an async-enumerable sequence, returning the result of the aggregation as a single element in the result sequence. The specified seed value is used as the initial accumulator value.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <typeparam name="TAccumulate">The type of the result of aggregation.</typeparam>
        /// <param name="source">An async-enumerable sequence to aggregate over.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="accumulator">An asynchronous accumulator function to be invoked and awaited on each element.</param>
        /// <param name="cancellationToken">An optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the final accumulator value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="accumulator"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<TAccumulate> AggregateAwaitAsyncCore<TSource, TAccumulate>(this IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, ValueTask<TAccumulate>> accumulator, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (accumulator == null)
                throw Error.ArgumentNull(nameof(accumulator));

            return Core(source, seed, accumulator, cancellationToken);

            static async ValueTask<TAccumulate> Core(IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, ValueTask<TAccumulate>> accumulator, CancellationToken cancellationToken)
            {
                var acc = seed;

                await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    acc = await accumulator(acc, item).ConfigureAwait(false);
                }

                return acc;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<TAccumulate> AggregateAwaitWithCancellationAsyncCore<TSource, TAccumulate>(this IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, CancellationToken, ValueTask<TAccumulate>> accumulator, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (accumulator == null)
                throw Error.ArgumentNull(nameof(accumulator));

            return Core(source, seed, accumulator, cancellationToken);

            static async ValueTask<TAccumulate> Core(IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, CancellationToken, ValueTask<TAccumulate>> accumulator, CancellationToken cancellationToken)
            {
                var acc = seed;

                await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    acc = await accumulator(acc, item, cancellationToken).ConfigureAwait(false);
                }

                return acc;
            }
        }
#endif

        /// <summary>
        /// Applies an accumulator function over an async-enumerable sequence, returning the result of the aggregation as a single element in the result sequence. The specified seed value is used as the initial accumulator value,
        /// and the specified result selector function is used to select the result value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">An async-enumerable sequence to aggregate over.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="accumulator">An accumulator function to be invoked on each element.</param>
        /// <param name="resultSelector">A function to transform the final accumulator value into the result value.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the final accumulator value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="accumulator"/> or <paramref name="resultSelector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<TResult> AggregateAsync<TSource, TAccumulate, TResult>(this IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> accumulator, Func<TAccumulate, TResult> resultSelector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (accumulator == null)
                throw Error.ArgumentNull(nameof(accumulator));
            if (resultSelector == null)
                throw Error.ArgumentNull(nameof(resultSelector));

            return Core(source, seed, accumulator, resultSelector, cancellationToken);

            static async ValueTask<TResult> Core(IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> accumulator, Func<TAccumulate, TResult> resultSelector, CancellationToken cancellationToken)
            {
                var acc = seed;

                await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    acc = accumulator(acc, item);
                }

                return resultSelector(acc);
            }
        }

        /// <summary>
        /// Applies an accumulator function over an async-enumerable sequence, returning the result of the aggregation as a single element in the result sequence. The specified seed value is used as the initial accumulator value,
        /// and the specified result selector is used to select the result value.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <param name="source">An async-enumerable sequence to aggregate over.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="accumulator">An asynchronous accumulator function to be invoked and awaited on each element.</param>
        /// <param name="resultSelector">An asynchronous transform function to transform the final accumulator value into the result value.</param>
        /// <param name="cancellationToken">An optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the value obtained by applying the result selector to the final accumulator value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="accumulator"/> or <paramref name="resultSelector"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static ValueTask<TResult> AggregateAwaitAsyncCore<TSource, TAccumulate, TResult>(this IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, ValueTask<TAccumulate>> accumulator, Func<TAccumulate, ValueTask<TResult>> resultSelector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (accumulator == null)
                throw Error.ArgumentNull(nameof(accumulator));
            if (resultSelector == null)
                throw Error.ArgumentNull(nameof(resultSelector));

            return Core(source, seed, accumulator, resultSelector, cancellationToken);

            static async ValueTask<TResult> Core(IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, ValueTask<TAccumulate>> accumulator, Func<TAccumulate, ValueTask<TResult>> resultSelector, CancellationToken cancellationToken)
            {
                var acc = seed;

                await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    acc = await accumulator(acc, item).ConfigureAwait(false);
                }

                return await resultSelector(acc).ConfigureAwait(false);
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<TResult> AggregateAwaitWithCancellationAsyncCore<TSource, TAccumulate, TResult>(this IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, CancellationToken, ValueTask<TAccumulate>> accumulator, Func<TAccumulate, CancellationToken, ValueTask<TResult>> resultSelector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (accumulator == null)
                throw Error.ArgumentNull(nameof(accumulator));
            if (resultSelector == null)
                throw Error.ArgumentNull(nameof(resultSelector));

            return Core(source, seed, accumulator, resultSelector, cancellationToken);

            static async ValueTask<TResult> Core(IAsyncEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, CancellationToken, ValueTask<TAccumulate>> accumulator, Func<TAccumulate, CancellationToken, ValueTask<TResult>> resultSelector, CancellationToken cancellationToken)
            {
                var acc = seed;

                await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    acc = await accumulator(acc, item, cancellationToken).ConfigureAwait(false);
                }

                return await resultSelector(acc, cancellationToken).ConfigureAwait(false);
            }
        }
#endif
    }
}
