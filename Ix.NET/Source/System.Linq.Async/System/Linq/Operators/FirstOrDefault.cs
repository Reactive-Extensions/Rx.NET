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
        /// Returns the first element of an async-enumerable sequence, or a default value if no such element exists.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">Source async-enumerable sequence.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>ValueTask containing the first element in the async-enumerable sequence, or a default value if no such element exists.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ValueTask<TSource?> FirstOrDefaultAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<TSource?> Core(IAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
            {
                var first = await TryGetFirst(source, cancellationToken).ConfigureAwait(false);

                return first.HasValue ? first.Value : default;
            }
        }

        /// <summary>
        /// Returns the first element of an async-enumerable sequence that satisfies the condition in the predicate, or a default value if no such element exists.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">Source async-enumerable sequence.</param>
        /// <param name="predicate">A predicate function to evaluate for elements in the source sequence.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>ValueTask containing the first element in the async-enumerable sequence that satisfies the condition in the predicate, or a default value if no such element exists.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        public static ValueTask<TSource?> FirstOrDefaultAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (predicate == null)
                throw Error.ArgumentNull(nameof(predicate));

            return Core(source, predicate, cancellationToken);

            static async ValueTask<TSource?> Core(IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)
            {
                var first = await TryGetFirst(source, predicate, cancellationToken).ConfigureAwait(false);

                return first.HasValue ? first.Value : default;
            }
        }

        /// <summary>
        /// Returns the first element of an async-enumerable sequence that satisfies the condition in the predicate, or a default value if no element satisfies the condition in the predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of element in the sequence.</typeparam>
        /// <param name="source">Source async-enumerable sequence.</param>
        /// <param name="predicate">An asynchronous predicate to invoke and await on each element of the sequence.</param>
        /// <param name="cancellationToken">An optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the first element in the sequence that satisfies the predicate, or a default value if no element satisfies the predicate.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static ValueTask<TSource?> FirstOrDefaultAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (predicate == null)
                throw Error.ArgumentNull(nameof(predicate));

            return Core(source, predicate, cancellationToken);

            static async ValueTask<TSource?> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken)
            {
                var first = await TryGetFirst(source, predicate, cancellationToken).ConfigureAwait(false);

                return first.HasValue ? first.Value : default;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<TSource?> FirstOrDefaultAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (predicate == null)
                throw Error.ArgumentNull(nameof(predicate));

            return Core(source, predicate, cancellationToken);

            static async ValueTask<TSource?> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken)
            {
                var first = await TryGetFirst(source, predicate, cancellationToken).ConfigureAwait(false);

                return first.HasValue ? first.Value : default;
            }
        }
#endif

        private static ValueTask<Maybe<TSource>> TryGetFirst<TSource>(IAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
        {
            if (source is IList<TSource> list)
            {
                if (list.Count > 0)
                {
                    return new ValueTask<Maybe<TSource>>(new Maybe<TSource>(list[0]));
                }
            }
            else if (source is IAsyncPartition<TSource> p)
            {
                return p.TryGetFirstAsync(cancellationToken);
            }
            else
            {
                return Core(source, cancellationToken);

                static async ValueTask<Maybe<TSource>> Core(IAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
                {
                    await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                    {
                        if (await e.MoveNextAsync())
                        {
                            return new Maybe<TSource>(e.Current);
                        }
                    }

                    return new Maybe<TSource>();
                }
            }

            return new ValueTask<Maybe<TSource>>(new Maybe<TSource>());
        }

        private static async ValueTask<Maybe<TSource>> TryGetFirst<TSource>(IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)
        {
            await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
            {
                while (await e.MoveNextAsync())
                {
                    var value = e.Current;

                    if (predicate(value))
                    {
                        return new Maybe<TSource>(value);
                    }
                }
            }

            return new Maybe<TSource>();
        }

        private static async ValueTask<Maybe<TSource>> TryGetFirst<TSource>(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken)
        {
            await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
            {
                while (await e.MoveNextAsync())
                {
                    var value = e.Current;

                    if (await predicate(value).ConfigureAwait(false))
                    {
                        return new Maybe<TSource>(value);
                    }
                }
            }

            return new Maybe<TSource>();
        }

#if !NO_DEEP_CANCELLATION
        private static async ValueTask<Maybe<TSource>> TryGetFirst<TSource>(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken)
        {
            await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
            {
                while (await e.MoveNextAsync())
                {
                    var value = e.Current;

                    if (await predicate(value, cancellationToken).ConfigureAwait(false))
                    {
                        return new Maybe<TSource>(value);
                    }
                }
            }

            return new Maybe<TSource>();
        }
#endif
    }
}
