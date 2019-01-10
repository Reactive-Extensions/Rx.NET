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
        public static Task<TSource> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            if (default(TSource) == null)
            {
                return Core(source, cancellationToken);

                async Task<TSource> Core(IAsyncEnumerable<TSource> _source, CancellationToken _cancellationToken)
                {
                    var comparer = Comparer<TSource>.Default;

                    var value = default(TSource);

#if CSHARP8
                    await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                    {
                        do
                        {
                            if (!await e.MoveNextAsync())
                            {
                                return value;
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
#else
                    var e = _source.GetAsyncEnumerator(_cancellationToken);

                    try
                    {
                        do
                        {
                            if (!await e.MoveNextAsync().ConfigureAwait(false))
                            {
                                return value;
                            }

                            value = e.Current;
                        }
                        while (value == null);

                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            var x = e.Current;
                            if (x != null && comparer.Compare(x, value) < 0)
                            {
                                value = x;
                            }
                        }
                    }
                    finally
                    {
                        await e.DisposeAsync().ConfigureAwait(false);
                    }
#endif

                    return value;
                }
            }
            else
            {
                return Core(source, cancellationToken);

                async Task<TSource> Core(IAsyncEnumerable<TSource> _source, CancellationToken _cancellationToken)
                {
                    var comparer = Comparer<TSource>.Default;

                    var value = default(TSource);

#if CSHARP8
                    await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
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
#else
                    var e = _source.GetAsyncEnumerator(_cancellationToken);

                    try
                    {
                        if (!await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            throw Error.NoElements();
                        }

                        value = e.Current;
                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            var x = e.Current;
                            if (comparer.Compare(x, value) < 0)
                            {
                                value = x;
                            }
                        }
                    }
                    finally
                    {
                        await e.DisposeAsync().ConfigureAwait(false);
                    }
#endif

                    return value;
                }
            }
        }

        public static Task<TResult> MinAsync<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, TResult> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            if (default(TResult) == null)
            {
                return Core(source, selector, cancellationToken);

                async Task<TResult> Core(IAsyncEnumerable<TSource> _source, Func<TSource, TResult> _selector, CancellationToken _cancellationToken)
                {
                    var comparer = Comparer<TResult>.Default;

                    var value = default(TResult);

#if CSHARP8
                    await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                    {
                        do
                        {
                            if (!await e.MoveNextAsync())
                            {
                                return value;
                            }

                            value = _selector(e.Current);
                        }
                        while (value == null);

                        while (await e.MoveNextAsync())
                        {
                            var x = _selector(e.Current);

                            if (x != null && comparer.Compare(x, value) < 0)
                            {
                                value = x;
                            }
                        }
                    }
#else
                    var e = _source.GetAsyncEnumerator(_cancellationToken);

                    try
                    {
                        do
                        {
                            if (!await e.MoveNextAsync().ConfigureAwait(false))
                            {
                                return value;
                            }

                            value = _selector(e.Current);
                        }
                        while (value == null);

                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            var x = _selector(e.Current);
                            if (x != null && comparer.Compare(x, value) < 0)
                            {
                                value = x;
                            }
                        }
                    }
                    finally
                    {
                        await e.DisposeAsync().ConfigureAwait(false);
                    }
#endif

                    return value;
                }
            }
            else
            {
                return Core(source, selector, cancellationToken);

                async Task<TResult> Core(IAsyncEnumerable<TSource> _source, Func<TSource, TResult> _selector, CancellationToken _cancellationToken)
                {
                    var comparer = Comparer<TResult>.Default;

                    var value = default(TResult);

#if CSHARP8
                    await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                    {
                        if (!await e.MoveNextAsync())
                        {
                            throw Error.NoElements();
                        }

                        value = _selector(e.Current);

                        while (await e.MoveNextAsync())
                        {
                            var x = _selector(e.Current);

                            if (comparer.Compare(x, value) < 0)
                            {
                                value = x;
                            }
                        }
                    }
#else
                    var e = _source.GetAsyncEnumerator(_cancellationToken);

                    try
                    {
                        if (!await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            throw Error.NoElements();
                        }

                        value = _selector(e.Current);
                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            var x = _selector(e.Current);
                            if (comparer.Compare(x, value) < 0)
                            {
                                value = x;
                            }
                        }
                    }
                    finally
                    {
                        await e.DisposeAsync().ConfigureAwait(false);
                    }
#endif

                    return value;
                }
            }
        }

        public static Task<TResult> MinAsync<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TResult>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            if (default(TResult) == null)
            {
                return Core(source, selector, cancellationToken);

                async Task<TResult> Core(IAsyncEnumerable<TSource> _source, Func<TSource, ValueTask<TResult>> _selector, CancellationToken _cancellationToken)
                {
                    var comparer = Comparer<TResult>.Default;

                    var value = default(TResult);

#if CSHARP8
                    await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                    {
                        do
                        {
                            if (!await e.MoveNextAsync())
                            {
                                return value;
                            }

                            value = await _selector(e.Current).ConfigureAwait(false);
                        }
                        while (value == null);

                        while (await e.MoveNextAsync())
                        {
                            var x = await _selector(e.Current).ConfigureAwait(false);

                            if (x != null && comparer.Compare(x, value) < 0)
                            {
                                value = x;
                            }
                        }
                    }
#else
                    var e = _source.GetAsyncEnumerator(_cancellationToken);

                    try
                    {
                        do
                        {
                            if (!await e.MoveNextAsync().ConfigureAwait(false))
                            {
                                return value;
                            }

                            value = await _selector(e.Current).ConfigureAwait(false);
                        }
                        while (value == null);

                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            var x = await _selector(e.Current).ConfigureAwait(false);
                            if (x != null && comparer.Compare(x, value) < 0)
                            {
                                value = x;
                            }
                        }
                    }
                    finally
                    {
                        await e.DisposeAsync().ConfigureAwait(false);
                    }
#endif

                    return value;
                }
            }
            else
            {
                return Core(source, selector, cancellationToken);

                async Task<TResult> Core(IAsyncEnumerable<TSource> _source, Func<TSource, ValueTask<TResult>> _selector, CancellationToken _cancellationToken)
                {
                    var comparer = Comparer<TResult>.Default;

                    var value = default(TResult);

#if CSHARP8
                    await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                    {
                        if (!await e.MoveNextAsync())
                        {
                            throw Error.NoElements();
                        }

                        value = await _selector(e.Current).ConfigureAwait(false);

                        while (await e.MoveNextAsync())
                        {
                            var x = await _selector(e.Current).ConfigureAwait(false);

                            if (comparer.Compare(x, value) < 0)
                            {
                                value = x;
                            }
                        }
                    }
#else
                    var e = _source.GetAsyncEnumerator(_cancellationToken);

                    try
                    {
                        if (!await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            throw Error.NoElements();
                        }

                        value = await _selector(e.Current).ConfigureAwait(false);
                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            var x = await _selector(e.Current).ConfigureAwait(false);
                            if (comparer.Compare(x, value) < 0)
                            {
                                value = x;
                            }
                        }
                    }
                    finally
                    {
                        await e.DisposeAsync().ConfigureAwait(false);
                    }
#endif

                    return value;
                }
            }
        }

#if !NO_DEEP_CANCELLATION
        public static Task<TResult> MinAsync<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TResult>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            if (default(TResult) == null)
            {
                return Core(source, selector, cancellationToken);

                async Task<TResult> Core(IAsyncEnumerable<TSource> _source, Func<TSource, CancellationToken, ValueTask<TResult>> _selector, CancellationToken _cancellationToken)
                {
                    var comparer = Comparer<TResult>.Default;

                    var value = default(TResult);

#if CSHARP8
                    await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                    {
                        do
                        {
                            if (!await e.MoveNextAsync())
                            {
                                return value;
                            }

                            value = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                        }
                        while (value == null);

                        while (await e.MoveNextAsync())
                        {
                            var x = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);

                            if (x != null && comparer.Compare(x, value) < 0)
                            {
                                value = x;
                            }
                        }
                    }
#else
                    var e = _source.GetAsyncEnumerator(_cancellationToken);

                    try
                    {
                        do
                        {
                            if (!await e.MoveNextAsync().ConfigureAwait(false))
                            {
                                return value;
                            }

                            value = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                        }
                        while (value == null);

                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            var x = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                            if (x != null && comparer.Compare(x, value) < 0)
                            {
                                value = x;
                            }
                        }
                    }
                    finally
                    {
                        await e.DisposeAsync().ConfigureAwait(false);
                    }
#endif

                    return value;
                }
            }
            else
            {
                return Core(source, selector, cancellationToken);

                async Task<TResult> Core(IAsyncEnumerable<TSource> _source, Func<TSource, CancellationToken, ValueTask<TResult>> _selector, CancellationToken _cancellationToken)
                {
                    var comparer = Comparer<TResult>.Default;

                    var value = default(TResult);

#if CSHARP8
                    await using (var e = _source.GetAsyncEnumerator(_cancellationToken).ConfigureAwait(false))
                    {
                        if (!await e.MoveNextAsync())
                        {
                            throw Error.NoElements();
                        }

                        value = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                        while (await e.MoveNextAsync())
                        {
                            var x = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                            if (comparer.Compare(x, value) < 0)
                            {
                                value = x;
                            }
                        }
                    }
#else
                    var e = _source.GetAsyncEnumerator(_cancellationToken);

                    try
                    {
                        if (!await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            throw Error.NoElements();
                        }

                        value = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                        while (await e.MoveNextAsync().ConfigureAwait(false))
                        {
                            var x = await _selector(e.Current, _cancellationToken).ConfigureAwait(false);
                            if (comparer.Compare(x, value) < 0)
                            {
                                value = x;
                            }
                        }
                    }
                    finally
                    {
                        await e.DisposeAsync().ConfigureAwait(false);
                    }
#endif

                    return value;
                }
            }
        }
#endif
    }
}