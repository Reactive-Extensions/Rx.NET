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
        /// Computes the sum of a sequence of <see cref="int" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="int" /> values to calculate the sum of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<int> SumAsync(this IAsyncEnumerable<int> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<int> Core(IAsyncEnumerable<int> source, CancellationToken cancellationToken)
            {
                var sum = 0;

                await foreach (int value in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    checked
                    {
                        sum += value;
                    }
                }

                return sum;
            }
        }

        /// <summary>
        /// Computes the sum of a sequence of <see cref="int" /> values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<int> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<int> Core(IAsyncEnumerable<TSource> source, Func<TSource, int> selector, CancellationToken cancellationToken)
            {
                var sum = 0;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = selector(item);

                    checked
                    {
                        sum += value;
                    }
                }

                return sum;
            }
        }

        /// <summary>
        /// Computes the sum of a sequence of <see cref="int"/> values that are obtained by invoking a transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">An asynchronous transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<int> SumAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<int> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int>> selector, CancellationToken cancellationToken)
            {
                var sum = 0;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = await selector(item).ConfigureAwait(false);

                    checked
                    {
                        sum += value;
                    }
                }

                return sum;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<int> SumAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<int> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int>> selector, CancellationToken cancellationToken)
            {
                var sum = 0;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = await selector(item, cancellationToken).ConfigureAwait(false);

                    checked
                    {
                        sum += value;
                    }
                }

                return sum;
            }
        }
#endif

        /// <summary>
        /// Computes the sum of a sequence of <see cref="long" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="long" /> values to calculate the sum of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<long> SumAsync(this IAsyncEnumerable<long> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<long> Core(IAsyncEnumerable<long> source, CancellationToken cancellationToken)
            {
                var sum = 0L;

                await foreach (long value in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    checked
                    {
                        sum += value;
                    }
                }

                return sum;
            }
        }

        /// <summary>
        /// Computes the sum of a sequence of <see cref="long" /> values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<long> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, long> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<long> Core(IAsyncEnumerable<TSource> source, Func<TSource, long> selector, CancellationToken cancellationToken)
            {
                var sum = 0L;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = selector(item);

                    checked
                    {
                        sum += value;
                    }
                }

                return sum;
            }
        }

        /// <summary>
        /// Computes the sum of a sequence of <see cref="long"/> values that are obtained by invoking a transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">An asynchronous transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<long> SumAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<long> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long>> selector, CancellationToken cancellationToken)
            {
                var sum = 0L;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = await selector(item).ConfigureAwait(false);

                    checked
                    {
                        sum += value;
                    }
                }

                return sum;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<long> SumAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<long> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long>> selector, CancellationToken cancellationToken)
            {
                var sum = 0L;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = await selector(item, cancellationToken).ConfigureAwait(false);

                    checked
                    {
                        sum += value;
                    }
                }

                return sum;
            }
        }
#endif

        /// <summary>
        /// Computes the sum of a sequence of <see cref="float" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="float" /> values to calculate the sum of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<float> SumAsync(this IAsyncEnumerable<float> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<float> Core(IAsyncEnumerable<float> source, CancellationToken cancellationToken)
            {
                var sum = 0.0f;

                await foreach (float value in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    sum += value;
                }

                return sum;
            }
        }

        /// <summary>
        /// Computes the sum of a sequence of <see cref="float" /> values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<float> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, float> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<float> Core(IAsyncEnumerable<TSource> source, Func<TSource, float> selector, CancellationToken cancellationToken)
            {
                var sum = 0.0f;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = selector(item);

                    sum += value;
                }

                return sum;
            }
        }

        /// <summary>
        /// Computes the sum of a sequence of <see cref="float"/> values that are obtained by invoking a transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">An asynchronous transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<float> SumAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<float> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float>> selector, CancellationToken cancellationToken)
            {
                var sum = 0.0f;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = await selector(item).ConfigureAwait(false);

                    sum += value;
                }

                return sum;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<float> SumAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<float> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float>> selector, CancellationToken cancellationToken)
            {
                var sum = 0.0f;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = await selector(item, cancellationToken).ConfigureAwait(false);

                    sum += value;
                }

                return sum;
            }
        }
#endif

        /// <summary>
        /// Computes the sum of a sequence of <see cref="double" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="double" /> values to calculate the sum of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<double> SumAsync(this IAsyncEnumerable<double> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<double> Core(IAsyncEnumerable<double> source, CancellationToken cancellationToken)
            {
                var sum = 0.0;

                await foreach (double value in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    sum += value;
                }

                return sum;
            }
        }

        /// <summary>
        /// Computes the sum of a sequence of <see cref="double" /> values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<double> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, double> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double> Core(IAsyncEnumerable<TSource> source, Func<TSource, double> selector, CancellationToken cancellationToken)
            {
                var sum = 0.0;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = selector(item);

                    sum += value;
                }

                return sum;
            }
        }

        /// <summary>
        /// Computes the sum of a sequence of <see cref="double"/> values that are obtained by invoking a transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">An asynchronous transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<double> SumAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double>> selector, CancellationToken cancellationToken)
            {
                var sum = 0.0;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = await selector(item).ConfigureAwait(false);

                    sum += value;
                }

                return sum;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<double> SumAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double>> selector, CancellationToken cancellationToken)
            {
                var sum = 0.0;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = await selector(item, cancellationToken).ConfigureAwait(false);

                    sum += value;
                }

                return sum;
            }
        }
#endif

        /// <summary>
        /// Computes the sum of a sequence of <see cref="decimal" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="decimal" /> values to calculate the sum of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<decimal> SumAsync(this IAsyncEnumerable<decimal> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<decimal> Core(IAsyncEnumerable<decimal> source, CancellationToken cancellationToken)
            {
                var sum = 0m;

                await foreach (decimal value in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    sum += value;
                }

                return sum;
            }
        }

        /// <summary>
        /// Computes the sum of a sequence of <see cref="decimal" /> values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<decimal> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, decimal> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<decimal> Core(IAsyncEnumerable<TSource> source, Func<TSource, decimal> selector, CancellationToken cancellationToken)
            {
                var sum = 0m;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = selector(item);

                    sum += value;
                }

                return sum;
            }
        }

        /// <summary>
        /// Computes the sum of a sequence of <see cref="decimal"/> values that are obtained by invoking a transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">An asynchronous transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<decimal> SumAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<decimal> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal>> selector, CancellationToken cancellationToken)
            {
                var sum = 0m;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = await selector(item).ConfigureAwait(false);

                    sum += value;
                }

                return sum;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<decimal> SumAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<decimal> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal>> selector, CancellationToken cancellationToken)
            {
                var sum = 0m;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = await selector(item, cancellationToken).ConfigureAwait(false);

                    sum += value;
                }

                return sum;
            }
        }
#endif

        /// <summary>
        /// Computes the sum of a sequence of <see cref="Nullable{Int}" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="Nullable{Int}" /> values to calculate the sum of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<int?> SumAsync(this IAsyncEnumerable<int?> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<int?> Core(IAsyncEnumerable<int?> source, CancellationToken cancellationToken)
            {
                var sum = 0;

                await foreach (int? value in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    checked
                    {
                        sum += value.GetValueOrDefault();
                    }
                }

                return sum;
            }
        }

        /// <summary>
        /// Computes the sum of a sequence of <see cref="Nullable{Int}" /> values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<int?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int?> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<int?> Core(IAsyncEnumerable<TSource> source, Func<TSource, int?> selector, CancellationToken cancellationToken)
            {
                var sum = 0;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = selector(item);

                    checked
                    {
                        sum += value.GetValueOrDefault();
                    }
                }

                return sum;
            }
        }

        /// <summary>
        /// Computes the sum of a sequence of <see cref="Nullable{Int}"/> values that are obtained by invoking a transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">An asynchronous transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<int?> SumAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<int?> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int?>> selector, CancellationToken cancellationToken)
            {
                var sum = 0;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = await selector(item).ConfigureAwait(false);

                    checked
                    {
                        sum += value.GetValueOrDefault();
                    }
                }

                return sum;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<int?> SumAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<int?> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int?>> selector, CancellationToken cancellationToken)
            {
                var sum = 0;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = await selector(item, cancellationToken).ConfigureAwait(false);

                    checked
                    {
                        sum += value.GetValueOrDefault();
                    }
                }

                return sum;
            }
        }
#endif

        /// <summary>
        /// Computes the sum of a sequence of <see cref="Nullable{Long}" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="Nullable{Long}" /> values to calculate the sum of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<long?> SumAsync(this IAsyncEnumerable<long?> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<long?> Core(IAsyncEnumerable<long?> source, CancellationToken cancellationToken)
            {
                var sum = 0L;

                await foreach (long? value in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    checked
                    {
                        sum += value.GetValueOrDefault();
                    }
                }

                return sum;
            }
        }

        /// <summary>
        /// Computes the sum of a sequence of <see cref="Nullable{Long}" /> values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<long?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, long?> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<long?> Core(IAsyncEnumerable<TSource> source, Func<TSource, long?> selector, CancellationToken cancellationToken)
            {
                var sum = 0L;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = selector(item);

                    checked
                    {
                        sum += value.GetValueOrDefault();
                    }
                }

                return sum;
            }
        }

        /// <summary>
        /// Computes the sum of a sequence of <see cref="Nullable{Long}"/> values that are obtained by invoking a transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">An asynchronous transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<long?> SumAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<long?> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long?>> selector, CancellationToken cancellationToken)
            {
                var sum = 0L;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = await selector(item).ConfigureAwait(false);

                    checked
                    {
                        sum += value.GetValueOrDefault();
                    }
                }

                return sum;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<long?> SumAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<long?> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long?>> selector, CancellationToken cancellationToken)
            {
                var sum = 0L;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = await selector(item, cancellationToken).ConfigureAwait(false);

                    checked
                    {
                        sum += value.GetValueOrDefault();
                    }
                }

                return sum;
            }
        }
#endif

        /// <summary>
        /// Computes the sum of a sequence of <see cref="Nullable{Float}" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="Nullable{Float}" /> values to calculate the sum of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<float?> SumAsync(this IAsyncEnumerable<float?> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<float?> Core(IAsyncEnumerable<float?> source, CancellationToken cancellationToken)
            {
                var sum = 0.0f;

                await foreach (float? value in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    sum += value.GetValueOrDefault();
                }

                return sum;
            }
        }

        /// <summary>
        /// Computes the sum of a sequence of <see cref="Nullable{Float}" /> values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<float?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, float?> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<float?> Core(IAsyncEnumerable<TSource> source, Func<TSource, float?> selector, CancellationToken cancellationToken)
            {
                var sum = 0.0f;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = selector(item);

                    sum += value.GetValueOrDefault();
                }

                return sum;
            }
        }

        /// <summary>
        /// Computes the sum of a sequence of <see cref="Nullable{Float}"/> values that are obtained by invoking a transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">An asynchronous transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<float?> SumAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<float?> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float?>> selector, CancellationToken cancellationToken)
            {
                var sum = 0.0f;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = await selector(item).ConfigureAwait(false);

                    sum += value.GetValueOrDefault();
                }

                return sum;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<float?> SumAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<float?> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float?>> selector, CancellationToken cancellationToken)
            {
                var sum = 0.0f;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = await selector(item, cancellationToken).ConfigureAwait(false);

                    sum += value.GetValueOrDefault();
                }

                return sum;
            }
        }
#endif

        /// <summary>
        /// Computes the sum of a sequence of <see cref="Nullable{Double}" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="Nullable{Double}" /> values to calculate the sum of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<double?> SumAsync(this IAsyncEnumerable<double?> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<double?> Core(IAsyncEnumerable<double?> source, CancellationToken cancellationToken)
            {
                var sum = 0.0;

                await foreach (double? value in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    sum += value.GetValueOrDefault();
                }

                return sum;
            }
        }

        /// <summary>
        /// Computes the sum of a sequence of <see cref="Nullable{Double}" /> values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<double?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, double?> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double?> Core(IAsyncEnumerable<TSource> source, Func<TSource, double?> selector, CancellationToken cancellationToken)
            {
                var sum = 0.0;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = selector(item);

                    sum += value.GetValueOrDefault();
                }

                return sum;
            }
        }

        /// <summary>
        /// Computes the sum of a sequence of <see cref="Nullable{Double}"/> values that are obtained by invoking a transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">An asynchronous transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<double?> SumAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double?> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double?>> selector, CancellationToken cancellationToken)
            {
                var sum = 0.0;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = await selector(item).ConfigureAwait(false);

                    sum += value.GetValueOrDefault();
                }

                return sum;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<double?> SumAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double?> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double?>> selector, CancellationToken cancellationToken)
            {
                var sum = 0.0;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = await selector(item, cancellationToken).ConfigureAwait(false);

                    sum += value.GetValueOrDefault();
                }

                return sum;
            }
        }
#endif

        /// <summary>
        /// Computes the sum of a sequence of <see cref="Nullable{Decimal}" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="Nullable{Decimal}" /> values to calculate the sum of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<decimal?> SumAsync(this IAsyncEnumerable<decimal?> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<decimal?> Core(IAsyncEnumerable<decimal?> source, CancellationToken cancellationToken)
            {
                var sum = 0m;

                await foreach (decimal? value in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    sum += value.GetValueOrDefault();
                }

                return sum;
            }
        }

        /// <summary>
        /// Computes the sum of a sequence of <see cref="Nullable{Decimal}" /> values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<decimal?> SumAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, decimal?> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<decimal?> Core(IAsyncEnumerable<TSource> source, Func<TSource, decimal?> selector, CancellationToken cancellationToken)
            {
                var sum = 0m;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = selector(item);

                    sum += value.GetValueOrDefault();
                }

                return sum;
            }
        }

        /// <summary>
        /// Computes the sum of a sequence of <see cref="Nullable{Decimal}"/> values that are obtained by invoking a transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">An asynchronous transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the sum of the values in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<decimal?> SumAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<decimal?> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal?>> selector, CancellationToken cancellationToken)
            {
                var sum = 0m;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = await selector(item).ConfigureAwait(false);

                    sum += value.GetValueOrDefault();
                }

                return sum;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<decimal?> SumAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<decimal?> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal?>> selector, CancellationToken cancellationToken)
            {
                var sum = 0m;

                await foreach (TSource item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var value = await selector(item, cancellationToken).ConfigureAwait(false);

                    sum += value.GetValueOrDefault();
                }

                return sum;
            }
        }
#endif

    }
}
