﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq
{
    public static partial class AsyncEnumerable
    {
        public static Task<double> AverageAsync(this IAsyncEnumerable<int> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            async Task<double> Core(IAsyncEnumerable<int> _source, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
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
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    if (!await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        throw Error.NoElements();
                    }

                    long sum = e.Current;
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            sum += e.Current;
                            ++count;
                        }
                    }

                    return (double)sum / count;
                }
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif
            }
        }

        public static Task<double> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<double> Core(IAsyncEnumerable<TSource> _source, Func<TSource, int> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    long sum = _selector(e.Current);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += _selector(e.Current);
                            ++count;
                        }
                    }

                    return (double)sum / count;
                }
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    if (!await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        throw Error.NoElements();
                    }

                    long sum = _selector(e.Current);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            sum += _selector(e.Current);
                            ++count;
                        }
                    }

                    return (double)sum / count;
                }
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif
            }
        }

        public static Task<double> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<double> Core(IAsyncEnumerable<TSource> _source, Func<TSource, ValueTask<int>> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    long sum = await _selector(e.Current).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += await _selector(e.Current).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return (double)sum / count;
                }
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    if (!await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        throw Error.NoElements();
                    }

                    long sum = await _selector(e.Current).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            sum += await _selector(e.Current).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return (double)sum / count;
                }
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif
            }
        }

#if !NO_DEEP_CANCELLATION
        public static Task<double> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<double> Core(IAsyncEnumerable<TSource> _source, Func<TSource, CancellationToken, ValueTask<int>> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    long sum = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return (double)sum / count;
                }
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    if (!await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        throw Error.NoElements();
                    }

                    long sum = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            sum += await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return (double)sum / count;
                }
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif
            }
        }
#endif

        public static Task<double> AverageAsync(this IAsyncEnumerable<long> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            async Task<double> Core(IAsyncEnumerable<long> _source, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
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
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    if (!await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        throw Error.NoElements();
                    }

                    long sum = e.Current;
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            sum += e.Current;
                            ++count;
                        }
                    }

                    return (double)sum / count;
                }
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif
            }
        }

        public static Task<double> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, long> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<double> Core(IAsyncEnumerable<TSource> _source, Func<TSource, long> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    long sum = _selector(e.Current);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += _selector(e.Current);
                            ++count;
                        }
                    }

                    return (double)sum / count;
                }
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    if (!await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        throw Error.NoElements();
                    }

                    long sum = _selector(e.Current);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            sum += _selector(e.Current);
                            ++count;
                        }
                    }

                    return (double)sum / count;
                }
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif
            }
        }

        public static Task<double> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<double> Core(IAsyncEnumerable<TSource> _source, Func<TSource, ValueTask<long>> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    long sum = await _selector(e.Current).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += await _selector(e.Current).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return (double)sum / count;
                }
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    if (!await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        throw Error.NoElements();
                    }

                    long sum = await _selector(e.Current).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            sum += await _selector(e.Current).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return (double)sum / count;
                }
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif
            }
        }

#if !NO_DEEP_CANCELLATION
        public static Task<double> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<double> Core(IAsyncEnumerable<TSource> _source, Func<TSource, CancellationToken, ValueTask<long>> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    long sum = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return (double)sum / count;
                }
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    if (!await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        throw Error.NoElements();
                    }

                    long sum = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            sum += await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return (double)sum / count;
                }
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif
            }
        }
#endif

        public static Task<float> AverageAsync(this IAsyncEnumerable<float> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            async Task<float> Core(IAsyncEnumerable<float> _source, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
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
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    if (!await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        throw Error.NoElements();
                    }

                    double sum = e.Current;
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            sum += e.Current;
                            ++count;
                        }
                    }

                    return (float)(sum / count);
                }
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif
            }
        }

        public static Task<float> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, float> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<float> Core(IAsyncEnumerable<TSource> _source, Func<TSource, float> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    double sum = _selector(e.Current);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += _selector(e.Current);
                            ++count;
                        }
                    }

                    return (float)(sum / count);
                }
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    if (!await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        throw Error.NoElements();
                    }

                    double sum = _selector(e.Current);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            sum += _selector(e.Current);
                            ++count;
                        }
                    }

                    return (float)(sum / count);
                }
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif
            }
        }

        public static Task<float> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<float> Core(IAsyncEnumerable<TSource> _source, Func<TSource, ValueTask<float>> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    double sum = await _selector(e.Current).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += await _selector(e.Current).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return (float)(sum / count);
                }
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    if (!await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        throw Error.NoElements();
                    }

                    double sum = await _selector(e.Current).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            sum += await _selector(e.Current).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return (float)(sum / count);
                }
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif
            }
        }

#if !NO_DEEP_CANCELLATION
        public static Task<float> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<float> Core(IAsyncEnumerable<TSource> _source, Func<TSource, CancellationToken, ValueTask<float>> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    double sum = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return (float)(sum / count);
                }
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    if (!await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        throw Error.NoElements();
                    }

                    double sum = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            sum += await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return (float)(sum / count);
                }
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif
            }
        }
#endif

        public static Task<double> AverageAsync(this IAsyncEnumerable<double> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            async Task<double> Core(IAsyncEnumerable<double> _source, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
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
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    if (!await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        throw Error.NoElements();
                    }

                    double sum = e.Current;
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            sum += e.Current;
                            ++count;
                        }
                    }

                    return sum / count;
                }
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif
            }
        }

        public static Task<double> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, double> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<double> Core(IAsyncEnumerable<TSource> _source, Func<TSource, double> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    double sum = _selector(e.Current);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += _selector(e.Current);
                            ++count;
                        }
                    }

                    return sum / count;
                }
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    if (!await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        throw Error.NoElements();
                    }

                    double sum = _selector(e.Current);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            sum += _selector(e.Current);
                            ++count;
                        }
                    }

                    return sum / count;
                }
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif
            }
        }

        public static Task<double> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<double> Core(IAsyncEnumerable<TSource> _source, Func<TSource, ValueTask<double>> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    double sum = await _selector(e.Current).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += await _selector(e.Current).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return sum / count;
                }
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    if (!await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        throw Error.NoElements();
                    }

                    double sum = await _selector(e.Current).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            sum += await _selector(e.Current).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return sum / count;
                }
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif
            }
        }

#if !NO_DEEP_CANCELLATION
        public static Task<double> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<double> Core(IAsyncEnumerable<TSource> _source, Func<TSource, CancellationToken, ValueTask<double>> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    double sum = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return sum / count;
                }
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    if (!await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        throw Error.NoElements();
                    }

                    double sum = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            sum += await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return sum / count;
                }
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif
            }
        }
#endif

        public static Task<decimal> AverageAsync(this IAsyncEnumerable<decimal> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            async Task<decimal> Core(IAsyncEnumerable<decimal> _source, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
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
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    if (!await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        throw Error.NoElements();
                    }

                    decimal sum = e.Current;
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            sum += e.Current;
                            ++count;
                        }
                    }

                    return sum / count;
                }
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif
            }
        }

        public static Task<decimal> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, decimal> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<decimal> Core(IAsyncEnumerable<TSource> _source, Func<TSource, decimal> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    decimal sum = _selector(e.Current);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += _selector(e.Current);
                            ++count;
                        }
                    }

                    return sum / count;
                }
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    if (!await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        throw Error.NoElements();
                    }

                    decimal sum = _selector(e.Current);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            sum += _selector(e.Current);
                            ++count;
                        }
                    }

                    return sum / count;
                }
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif
            }
        }

        public static Task<decimal> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<decimal> Core(IAsyncEnumerable<TSource> _source, Func<TSource, ValueTask<decimal>> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    decimal sum = await _selector(e.Current).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += await _selector(e.Current).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return sum / count;
                }
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    if (!await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        throw Error.NoElements();
                    }

                    decimal sum = await _selector(e.Current).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            sum += await _selector(e.Current).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return sum / count;
                }
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif
            }
        }

#if !NO_DEEP_CANCELLATION
        public static Task<decimal> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<decimal> Core(IAsyncEnumerable<TSource> _source, Func<TSource, CancellationToken, ValueTask<decimal>> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    decimal sum = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync())
                        {
                            sum += await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return sum / count;
                }
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    if (!await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        throw Error.NoElements();
                    }

                    decimal sum = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                    long count = 1;
                    checked
                    {
                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            sum += await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                            ++count;
                        }
                    }

                    return sum / count;
                }
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif
            }
        }
#endif

        public static Task<double?> AverageAsync(this IAsyncEnumerable<int?> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            async Task<double?> Core(IAsyncEnumerable<int?> _source, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
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
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    while (await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        var v = e.Current;
                        if (v.HasValue)
                        {
                            long sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync().ConfigureAwait(false))
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
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif

                return null;
            }
        }

        public static Task<double?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int?> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<double?> Core(IAsyncEnumerable<TSource> _source, Func<TSource, int?> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = _selector(e.Current);
                        if (v.HasValue)
                        {
                            long sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = _selector(e.Current);
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
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    while (await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        var v = _selector(e.Current);
                        if (v.HasValue)
                        {
                            long sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync().ConfigureAwait(false))
                                {
                                    v = _selector(e.Current);
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
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif

                return null;
            }
        }

        public static Task<double?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<double?> Core(IAsyncEnumerable<TSource> _source, Func<TSource, ValueTask<int?>> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = await _selector(e.Current).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            long sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = await _selector(e.Current).ConfigureAwait(false);
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
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    while (await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        var v = await _selector(e.Current).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            long sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync().ConfigureAwait(false))
                                {
                                    v = await _selector(e.Current).ConfigureAwait(false);
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
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif

                return null;
            }
        }

#if !NO_DEEP_CANCELLATION
        public static Task<double?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<double?> Core(IAsyncEnumerable<TSource> _source, Func<TSource, CancellationToken, ValueTask<int?>> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            long sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
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
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    while (await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        var v = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            long sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync().ConfigureAwait(false))
                                {
                                    v = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
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
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif

                return null;
            }
        }
#endif

        public static Task<double?> AverageAsync(this IAsyncEnumerable<long?> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            async Task<double?> Core(IAsyncEnumerable<long?> _source, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
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
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    while (await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        var v = e.Current;
                        if (v.HasValue)
                        {
                            long sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync().ConfigureAwait(false))
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
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif

                return null;
            }
        }

        public static Task<double?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, long?> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<double?> Core(IAsyncEnumerable<TSource> _source, Func<TSource, long?> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = _selector(e.Current);
                        if (v.HasValue)
                        {
                            long sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = _selector(e.Current);
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
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    while (await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        var v = _selector(e.Current);
                        if (v.HasValue)
                        {
                            long sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync().ConfigureAwait(false))
                                {
                                    v = _selector(e.Current);
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
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif

                return null;
            }
        }

        public static Task<double?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<double?> Core(IAsyncEnumerable<TSource> _source, Func<TSource, ValueTask<long?>> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = await _selector(e.Current).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            long sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = await _selector(e.Current).ConfigureAwait(false);
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
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    while (await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        var v = await _selector(e.Current).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            long sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync().ConfigureAwait(false))
                                {
                                    v = await _selector(e.Current).ConfigureAwait(false);
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
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif

                return null;
            }
        }

#if !NO_DEEP_CANCELLATION
        public static Task<double?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<double?> Core(IAsyncEnumerable<TSource> _source, Func<TSource, CancellationToken, ValueTask<long?>> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            long sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
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
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    while (await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        var v = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            long sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync().ConfigureAwait(false))
                                {
                                    v = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
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
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif

                return null;
            }
        }
#endif

        public static Task<float?> AverageAsync(this IAsyncEnumerable<float?> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            async Task<float?> Core(IAsyncEnumerable<float?> _source, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
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
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    while (await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        var v = e.Current;
                        if (v.HasValue)
                        {
                            double sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync().ConfigureAwait(false))
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
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif

                return null;
            }
        }

        public static Task<float?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, float?> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<float?> Core(IAsyncEnumerable<TSource> _source, Func<TSource, float?> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = _selector(e.Current);
                        if (v.HasValue)
                        {
                            double sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = _selector(e.Current);
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
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    while (await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        var v = _selector(e.Current);
                        if (v.HasValue)
                        {
                            double sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync().ConfigureAwait(false))
                                {
                                    v = _selector(e.Current);
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
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif

                return null;
            }
        }

        public static Task<float?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<float?> Core(IAsyncEnumerable<TSource> _source, Func<TSource, ValueTask<float?>> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = await _selector(e.Current).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            double sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = await _selector(e.Current).ConfigureAwait(false);
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
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    while (await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        var v = await _selector(e.Current).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            double sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync().ConfigureAwait(false))
                                {
                                    v = await _selector(e.Current).ConfigureAwait(false);
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
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif

                return null;
            }
        }

#if !NO_DEEP_CANCELLATION
        public static Task<float?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<float?> Core(IAsyncEnumerable<TSource> _source, Func<TSource, CancellationToken, ValueTask<float?>> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            double sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
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
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    while (await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        var v = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            double sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync().ConfigureAwait(false))
                                {
                                    v = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
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
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif

                return null;
            }
        }
#endif

        public static Task<double?> AverageAsync(this IAsyncEnumerable<double?> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            async Task<double?> Core(IAsyncEnumerable<double?> _source, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
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
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    while (await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        var v = e.Current;
                        if (v.HasValue)
                        {
                            double sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync().ConfigureAwait(false))
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
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif

                return null;
            }
        }

        public static Task<double?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, double?> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<double?> Core(IAsyncEnumerable<TSource> _source, Func<TSource, double?> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = _selector(e.Current);
                        if (v.HasValue)
                        {
                            double sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = _selector(e.Current);
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
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    while (await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        var v = _selector(e.Current);
                        if (v.HasValue)
                        {
                            double sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync().ConfigureAwait(false))
                                {
                                    v = _selector(e.Current);
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
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif

                return null;
            }
        }

        public static Task<double?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<double?> Core(IAsyncEnumerable<TSource> _source, Func<TSource, ValueTask<double?>> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = await _selector(e.Current).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            double sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = await _selector(e.Current).ConfigureAwait(false);
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
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    while (await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        var v = await _selector(e.Current).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            double sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync().ConfigureAwait(false))
                                {
                                    v = await _selector(e.Current).ConfigureAwait(false);
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
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif

                return null;
            }
        }

#if !NO_DEEP_CANCELLATION
        public static Task<double?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<double?> Core(IAsyncEnumerable<TSource> _source, Func<TSource, CancellationToken, ValueTask<double?>> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            double sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
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
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    while (await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        var v = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            double sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync().ConfigureAwait(false))
                                {
                                    v = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
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
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif

                return null;
            }
        }
#endif

        public static Task<decimal?> AverageAsync(this IAsyncEnumerable<decimal?> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            async Task<decimal?> Core(IAsyncEnumerable<decimal?> _source, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
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
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    while (await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        var v = e.Current;
                        if (v.HasValue)
                        {
                            decimal sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync().ConfigureAwait(false))
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
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif

                return null;
            }
        }

        public static Task<decimal?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, decimal?> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<decimal?> Core(IAsyncEnumerable<TSource> _source, Func<TSource, decimal?> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = _selector(e.Current);
                        if (v.HasValue)
                        {
                            decimal sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = _selector(e.Current);
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
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    while (await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        var v = _selector(e.Current);
                        if (v.HasValue)
                        {
                            decimal sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync().ConfigureAwait(false))
                                {
                                    v = _selector(e.Current);
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
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif

                return null;
            }
        }

        public static Task<decimal?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<decimal?> Core(IAsyncEnumerable<TSource> _source, Func<TSource, ValueTask<decimal?>> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = await _selector(e.Current).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            decimal sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = await _selector(e.Current).ConfigureAwait(false);
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
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    while (await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        var v = await _selector(e.Current).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            decimal sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync().ConfigureAwait(false))
                                {
                                    v = await _selector(e.Current).ConfigureAwait(false);
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
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif

                return null;
            }
        }

#if !NO_DEEP_CANCELLATION
        public static Task<decimal?> AverageAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            async Task<decimal?> Core(IAsyncEnumerable<TSource> _source, Func<TSource, CancellationToken, ValueTask<decimal?>> _selector, CancellationToken _cancellationToken)
            {
#if CSHARP8
                await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                {
                    while (await e.MoveNextAsync())
                    {
                        var v = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            decimal sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync())
                                {
                                    v = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
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
#else
                var e = _source.GetAsyncEnumerator(_cancellationToken);

                try
                {
                    while (await e.MoveNextAsync().ConfigureAwait(false))
                    {
                        var v = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                        if (v.HasValue)
                        {
                            decimal sum = v.GetValueOrDefault();
                            long count = 1;
                            checked
                            {
                                while (await e.MoveNextAsync().ConfigureAwait(false))
                                {
                                    v = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
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
                finally
                {
                    await e.DisposeAsync().ConfigureAwait(false);
                }
#endif

                return null;
            }
        }
#endif

    }
}