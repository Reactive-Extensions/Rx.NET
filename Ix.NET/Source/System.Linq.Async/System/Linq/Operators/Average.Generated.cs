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
        /// Computes the average of an async-enumerable sequence of <see cref="int" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="int" /> values to calculate the average of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the average of the sequence of values.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) The source sequence is empty.</exception>
        public static ValueTask<double> AverageAsync(this IAsyncEnumerable<int> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<double> Core(IAsyncEnumerable<int> source, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    long sum = e.Current;
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += e.Current;
                            ++count;
                        }
                    }

                    return (double)sum / count;
                }
            }
        }

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="int" /> values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) The source sequence is empty.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<double> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double> Core(IAsyncEnumerable<TSource> source, Func<TSource, int> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    long sum = selector(e.Current);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += selector(e.Current);
                            ++count;
                        }
                    }

                    return (double)sum / count;
                }
            }
        }

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="int"/> values that are obtained by invoking an asynchronous transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to compute the average of.</param>
        /// <param name="selector">A transform function to invoke and await on each element of the source sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the average of the sequence of values.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">The source sequence is empty.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<double> AverageAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int>> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    long sum = await selector(e.Current).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += await selector(e.Current).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return (double)sum / count;
                }
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<double> AverageAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int>> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    long sum = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += await selector(e.Current, cancellationToken).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return (double)sum / count;
                }
            }
        }
#endif

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="long" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="long" /> values to calculate the average of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the average of the sequence of values.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) The source sequence is empty.</exception>
        public static ValueTask<double> AverageAsync(this IAsyncEnumerable<long> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<double> Core(IAsyncEnumerable<long> source, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    long sum = e.Current;
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += e.Current;
                            ++count;
                        }
                    }

                    return (double)sum / count;
                }
            }
        }

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="long" /> values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) The source sequence is empty.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<double> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, long> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double> Core(IAsyncEnumerable<TSource> source, Func<TSource, long> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    long sum = selector(e.Current);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += selector(e.Current);
                            ++count;
                        }
                    }

                    return (double)sum / count;
                }
            }
        }

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="long"/> values that are obtained by invoking an asynchronous transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to compute the average of.</param>
        /// <param name="selector">A transform function to invoke and await on each element of the source sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the average of the sequence of values.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">The source sequence is empty.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<double> AverageAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long>> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    long sum = await selector(e.Current).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += await selector(e.Current).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return (double)sum / count;
                }
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<double> AverageAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long>> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    long sum = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += await selector(e.Current, cancellationToken).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return (double)sum / count;
                }
            }
        }
#endif

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="float" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="float" /> values to calculate the average of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the average of the sequence of values.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) The source sequence is empty.</exception>
        public static ValueTask<float> AverageAsync(this IAsyncEnumerable<float> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<float> Core(IAsyncEnumerable<float> source, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    double sum = e.Current;
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += e.Current;
                            ++count;
                        }
                    }

                    return (float)(sum / count);
                }
            }
        }

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="float" /> values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) The source sequence is empty.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<float> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, float> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<float> Core(IAsyncEnumerable<TSource> source, Func<TSource, float> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    double sum = selector(e.Current);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += selector(e.Current);
                            ++count;
                        }
                    }

                    return (float)(sum / count);
                }
            }
        }

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="float"/> values that are obtained by invoking an asynchronous transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to compute the average of.</param>
        /// <param name="selector">A transform function to invoke and await on each element of the source sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the average of the sequence of values.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">The source sequence is empty.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<float> AverageAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<float> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float>> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    double sum = await selector(e.Current).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += await selector(e.Current).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return (float)(sum / count);
                }
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<float> AverageAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<float> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float>> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    double sum = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += await selector(e.Current, cancellationToken).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return (float)(sum / count);
                }
            }
        }
#endif

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="double" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="double" /> values to calculate the average of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the average of the sequence of values.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) The source sequence is empty.</exception>
        public static ValueTask<double> AverageAsync(this IAsyncEnumerable<double> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<double> Core(IAsyncEnumerable<double> source, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    double sum = e.Current;
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += e.Current;
                            ++count;
                        }
                    }

                    return sum / count;
                }
            }
        }

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="double" /> values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) The source sequence is empty.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<double> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, double> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double> Core(IAsyncEnumerable<TSource> source, Func<TSource, double> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    double sum = selector(e.Current);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += selector(e.Current);
                            ++count;
                        }
                    }

                    return sum / count;
                }
            }
        }

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="double"/> values that are obtained by invoking an asynchronous transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to compute the average of.</param>
        /// <param name="selector">A transform function to invoke and await on each element of the source sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the average of the sequence of values.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">The source sequence is empty.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<double> AverageAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double>> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    double sum = await selector(e.Current).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += await selector(e.Current).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return sum / count;
                }
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<double> AverageAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double>> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    double sum = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += await selector(e.Current, cancellationToken).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return sum / count;
                }
            }
        }
#endif

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="decimal" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="decimal" /> values to calculate the average of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the average of the sequence of values.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) The source sequence is empty.</exception>
        public static ValueTask<decimal> AverageAsync(this IAsyncEnumerable<decimal> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<decimal> Core(IAsyncEnumerable<decimal> source, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    decimal sum = e.Current;
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += e.Current;
                            ++count;
                        }
                    }

                    return sum / count;
                }
            }
        }

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="decimal" /> values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) The source sequence is empty.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<decimal> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, decimal> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<decimal> Core(IAsyncEnumerable<TSource> source, Func<TSource, decimal> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    decimal sum = selector(e.Current);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += selector(e.Current);
                            ++count;
                        }
                    }

                    return sum / count;
                }
            }
        }

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="decimal"/> values that are obtained by invoking an asynchronous transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to compute the average of.</param>
        /// <param name="selector">A transform function to invoke and await on each element of the source sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the average of the sequence of values.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">The source sequence is empty.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<decimal> AverageAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<decimal> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal>> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    decimal sum = await selector(e.Current).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += await selector(e.Current).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return sum / count;
                }
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<decimal> AverageAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<decimal> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal>> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    decimal sum = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += await selector(e.Current, cancellationToken).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return sum / count;
                }
            }
        }
#endif

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="Nullable{Int}" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="Nullable{Int}" /> values to calculate the average of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the average of the sequence of values.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) The source sequence is empty.</exception>
        public static ValueTask<double?> AverageAsync(this IAsyncEnumerable<int?> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<double?> Core(IAsyncEnumerable<int?> source, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = e.Current;
                        if (v.HasValue)
                        {
                            long sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = e.Current;
                                    if (v.HasValue)
                                    {
                                        sum += v.GetValueOrDefault();
                                        ++count;
                                    }
                                }
                            }

                            return (double)sum / count;
                        }
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="Nullable{Int}" /> values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) The source sequence is empty.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<double?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int?> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double?> Core(IAsyncEnumerable<TSource> source, Func<TSource, int?> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = selector(e.Current);
                        if (v.HasValue)
                        {
                            long sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = selector(e.Current);
                                    if (v.HasValue)
                                    {
                                        sum += v.GetValueOrDefault();
                                        ++count;
                                    }
                                }
                            }

                            return (double)sum / count;
                        }
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="Nullable{Int}"/> values that are obtained by invoking an asynchronous transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to compute the average of.</param>
        /// <param name="selector">A transform function to invoke and await on each element of the source sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the average of the sequence of values, or <see langword="null"/> if the source sequence is empty.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<double?> AverageAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double?> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int?>> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = await selector(e.Current).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            long sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = await selector(e.Current).ConfigureAwait(false);
                                    if (v.HasValue)
                                    {
                                        sum += v.GetValueOrDefault();
                                        ++count;
                                    }
                                }
                            }

                            return (double)sum / count;
                        }
                    }
                }

                return null;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<double?> AverageAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double?> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int?>> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            long sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                                    if (v.HasValue)
                                    {
                                        sum += v.GetValueOrDefault();
                                        ++count;
                                    }
                                }
                            }

                            return (double)sum / count;
                        }
                    }
                }

                return null;
            }
        }
#endif

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="Nullable{Long}" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="Nullable{Long}" /> values to calculate the average of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the average of the sequence of values.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) The source sequence is empty.</exception>
        public static ValueTask<double?> AverageAsync(this IAsyncEnumerable<long?> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<double?> Core(IAsyncEnumerable<long?> source, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = e.Current;
                        if (v.HasValue)
                        {
                            long sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = e.Current;
                                    if (v.HasValue)
                                    {
                                        sum += v.GetValueOrDefault();
                                        ++count;
                                    }
                                }
                            }

                            return (double)sum / count;
                        }
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="Nullable{Long}" /> values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) The source sequence is empty.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<double?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, long?> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double?> Core(IAsyncEnumerable<TSource> source, Func<TSource, long?> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = selector(e.Current);
                        if (v.HasValue)
                        {
                            long sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = selector(e.Current);
                                    if (v.HasValue)
                                    {
                                        sum += v.GetValueOrDefault();
                                        ++count;
                                    }
                                }
                            }

                            return (double)sum / count;
                        }
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="Nullable{Long}"/> values that are obtained by invoking an asynchronous transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to compute the average of.</param>
        /// <param name="selector">A transform function to invoke and await on each element of the source sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the average of the sequence of values, or <see langword="null"/> if the source sequence is empty.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<double?> AverageAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double?> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long?>> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = await selector(e.Current).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            long sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = await selector(e.Current).ConfigureAwait(false);
                                    if (v.HasValue)
                                    {
                                        sum += v.GetValueOrDefault();
                                        ++count;
                                    }
                                }
                            }

                            return (double)sum / count;
                        }
                    }
                }

                return null;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<double?> AverageAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double?> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long?>> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            long sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                                    if (v.HasValue)
                                    {
                                        sum += v.GetValueOrDefault();
                                        ++count;
                                    }
                                }
                            }

                            return (double)sum / count;
                        }
                    }
                }

                return null;
            }
        }
#endif

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="Nullable{Float}" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="Nullable{Float}" /> values to calculate the average of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the average of the sequence of values.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) The source sequence is empty.</exception>
        public static ValueTask<float?> AverageAsync(this IAsyncEnumerable<float?> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<float?> Core(IAsyncEnumerable<float?> source, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = e.Current;
                        if (v.HasValue)
                        {
                            double sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = e.Current;
                                    if (v.HasValue)
                                    {
                                        sum += v.GetValueOrDefault();
                                        ++count;
                                    }
                                }
                            }

                            return (float)(sum / count);
                        }
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="Nullable{Float}" /> values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) The source sequence is empty.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<float?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, float?> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<float?> Core(IAsyncEnumerable<TSource> source, Func<TSource, float?> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = selector(e.Current);
                        if (v.HasValue)
                        {
                            double sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = selector(e.Current);
                                    if (v.HasValue)
                                    {
                                        sum += v.GetValueOrDefault();
                                        ++count;
                                    }
                                }
                            }

                            return (float)(sum / count);
                        }
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="Nullable{Float}"/> values that are obtained by invoking an asynchronous transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to compute the average of.</param>
        /// <param name="selector">A transform function to invoke and await on each element of the source sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the average of the sequence of values, or <see langword="null"/> if the source sequence is empty.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<float?> AverageAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<float?> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float?>> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = await selector(e.Current).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            double sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = await selector(e.Current).ConfigureAwait(false);
                                    if (v.HasValue)
                                    {
                                        sum += v.GetValueOrDefault();
                                        ++count;
                                    }
                                }
                            }

                            return (float)(sum / count);
                        }
                    }
                }

                return null;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<float?> AverageAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<float?> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float?>> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            double sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                                    if (v.HasValue)
                                    {
                                        sum += v.GetValueOrDefault();
                                        ++count;
                                    }
                                }
                            }

                            return (float)(sum / count);
                        }
                    }
                }

                return null;
            }
        }
#endif

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="Nullable{Double}" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="Nullable{Double}" /> values to calculate the average of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the average of the sequence of values.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) The source sequence is empty.</exception>
        public static ValueTask<double?> AverageAsync(this IAsyncEnumerable<double?> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<double?> Core(IAsyncEnumerable<double?> source, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = e.Current;
                        if (v.HasValue)
                        {
                            double sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = e.Current;
                                    if (v.HasValue)
                                    {
                                        sum += v.GetValueOrDefault();
                                        ++count;
                                    }
                                }
                            }

                            return sum / count;
                        }
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="Nullable{Double}" /> values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) The source sequence is empty.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<double?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, double?> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double?> Core(IAsyncEnumerable<TSource> source, Func<TSource, double?> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = selector(e.Current);
                        if (v.HasValue)
                        {
                            double sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = selector(e.Current);
                                    if (v.HasValue)
                                    {
                                        sum += v.GetValueOrDefault();
                                        ++count;
                                    }
                                }
                            }

                            return sum / count;
                        }
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="Nullable{Double}"/> values that are obtained by invoking an asynchronous transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to compute the average of.</param>
        /// <param name="selector">A transform function to invoke and await on each element of the source sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the average of the sequence of values, or <see langword="null"/> if the source sequence is empty.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<double?> AverageAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double?> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double?>> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = await selector(e.Current).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            double sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = await selector(e.Current).ConfigureAwait(false);
                                    if (v.HasValue)
                                    {
                                        sum += v.GetValueOrDefault();
                                        ++count;
                                    }
                                }
                            }

                            return sum / count;
                        }
                    }
                }

                return null;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<double?> AverageAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double?> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double?>> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            double sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                                    if (v.HasValue)
                                    {
                                        sum += v.GetValueOrDefault();
                                        ++count;
                                    }
                                }
                            }

                            return sum / count;
                        }
                    }
                }

                return null;
            }
        }
#endif

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="Nullable{Decimal}" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="Nullable{Decimal}" /> values to calculate the average of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the average of the sequence of values.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) The source sequence is empty.</exception>
        public static ValueTask<decimal?> AverageAsync(this IAsyncEnumerable<decimal?> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<decimal?> Core(IAsyncEnumerable<decimal?> source, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = e.Current;
                        if (v.HasValue)
                        {
                            decimal sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = e.Current;
                                    if (v.HasValue)
                                    {
                                        sum += v.GetValueOrDefault();
                                        ++count;
                                    }
                                }
                            }

                            return sum / count;
                        }
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="Nullable{Decimal}" /> values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>An async-enumerable sequence containing a single element with the average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) The source sequence is empty.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        public static ValueTask<decimal?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, decimal?> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<decimal?> Core(IAsyncEnumerable<TSource> source, Func<TSource, decimal?> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = selector(e.Current);
                        if (v.HasValue)
                        {
                            decimal sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = selector(e.Current);
                                    if (v.HasValue)
                                    {
                                        sum += v.GetValueOrDefault();
                                        ++count;
                                    }
                                }
                            }

                            return sum / count;
                        }
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Computes the average of an async-enumerable sequence of <see cref="Nullable{Decimal}"/> values that are obtained by invoking an asynchronous transform function on each element of the source sequence and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to compute the average of.</param>
        /// <param name="selector">A transform function to invoke and await on each element of the source sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the average of the sequence of values, or <see langword="null"/> if the source sequence is empty.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<decimal?> AverageAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<decimal?> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal?>> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = await selector(e.Current).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            decimal sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = await selector(e.Current).ConfigureAwait(false);
                                    if (v.HasValue)
                                    {
                                        sum += v.GetValueOrDefault();
                                        ++count;
                                    }
                                }
                            }

                            return sum / count;
                        }
                    }
                }

                return null;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<decimal?> AverageAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<decimal?> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal?>> selector, CancellationToken cancellationToken)
            {
                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            decimal sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                                    if (v.HasValue)
                                    {
                                        sum += v.GetValueOrDefault();
                                        ++count;
                                    }
                                }
                            }

                            return sum / count;
                        }
                    }
                }

                return null;
            }
        }
#endif

    }
}
