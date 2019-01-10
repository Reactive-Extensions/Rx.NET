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
        public static Task<TSource> ElementAtAsync<TSource>(this IAsyncEnumerable<TSource> source, int index, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, index, cancellationToken);

            async Task<TSource> Core(IAsyncEnumerable<TSource> _source, int _index, CancellationToken _cancellationToken)
            {
                if (_source is IAsyncPartition<TSource> p)
                {
                    var first = await p.TryGetElementAtAsync(_index, _cancellationToken).ConfigureAwait(false);

                    if (first.HasValue)
                    {
                        return first.Value;
                    }
                }
                else
                {
                    if (_source is IList<TSource> list)
                    {
                        return list[_index];
                    }

                    if (_index >= 0)
                    {
#if CSHARP8 && AETOR_HAS_CT // CS0656 Missing compiler required member 'System.Collections.Generic.IAsyncEnumerable`1.GetAsyncEnumerator'
                        await foreach (TSource item in _source.WithCancellation(_cancellationToken).ConfigureAwait(false))
                        {
                            if (_index == 0)
                            {
                                return item;
                            }

                            _index--;
                        }
#else
                        var e = _source.GetAsyncEnumerator(_cancellationToken);

                        try
                        {
                            while (await e.MoveNextAsync().ConfigureAwait(false))
                            {
                                if (_index == 0)
                                {
                                    return e.Current;
                                }

                                _index--;
                            }
                        }
                        finally
                        {
                            await e.DisposeAsync().ConfigureAwait(false);
                        }
#endif
                    }
                }

                // NB: Even though index is captured, no closure is created.
                //     The nameof expression is lowered to a literal prior to creating closures.

                throw Error.ArgumentOutOfRange(nameof(index));
            }
        }
    }
}