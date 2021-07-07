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
        /// Returns the last element of an async-enumerable sequence, or a default value if no such element exists.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">Source async-enumerable sequence.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>ValueTask containing the last element in the async-enumerable sequence, or a default value if no such element exists.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ValueTask<TSource?> LastOrDefaultAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<TSource?> Core(IAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
            {
                var last = await TryGetLast(source, cancellationToken).ConfigureAwait(false);

                return last.HasValue ? last.Value : default;
            }
        }

        /// <summary>
        /// Returns the last element of an async-enumerable sequence that satisfies the condition in the predicate, or a default value if no such element exists.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">Source async-enumerable sequence.</param>
        /// <param name="predicate">A predicate function to evaluate for elements in the source sequence.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>ValueTask containing the last element in the async-enumerable sequence that satisfies the condition in the predicate, or a default value if no such element exists.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        public static ValueTask<TSource?> LastOrDefaultAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (predicate == null)
                throw Error.ArgumentNull(nameof(predicate));

            return Core(source, predicate, cancellationToken);

            static async ValueTask<TSource?> Core(IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)
            {
                var last = await TryGetLast(source, predicate, cancellationToken).ConfigureAwait(false);

                return last.HasValue ? last.Value : default;
            }
        }

        /// <summary>
        /// Returns the last element of an async-enumerable sequence that satisfies the condition in the predicate, or a default value if no such element exists.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">Source async-enumerable sequence.</param>
        /// <param name="predicate">An asynchronous predicate function to evaluate for elements in the source sequence.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>ValueTask containing the last element in the async-enumerable sequence that satisfies the condition in the predicate, or a default value if no such element exists.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        [GenerateAsyncOverload]
        private static ValueTask<TSource?> LastOrDefaultAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (predicate == null)
                throw Error.ArgumentNull(nameof(predicate));

            return Core(source, predicate, cancellationToken);

            static async ValueTask<TSource?> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken)
            {
                var last = await TryGetLast(source, predicate, cancellationToken).ConfigureAwait(false);

                return last.HasValue ? last.Value : default;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<TSource?> LastOrDefaultAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (predicate == null)
                throw Error.ArgumentNull(nameof(predicate));

            return Core(source, predicate, cancellationToken);

            static async ValueTask<TSource?> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken)
            {
                var last = await TryGetLast(source, predicate, cancellationToken).ConfigureAwait(false);

                return last.HasValue ? last.Value : default;
            }
        }
#endif

        private static ValueTask<Maybe<TSource>> TryGetLast<TSource>(IAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
        {
            if (source is IList<TSource> list)
            {
                var count = list.Count;
                if (count > 0)
                {
                    return new ValueTask<Maybe<TSource>>(new Maybe<TSource>(list[count - 1]));
                }
            }
            else if (source is IAsyncPartition<TSource> p)
            {
                return p.TryGetLastAsync(cancellationToken);
            }
            else
            {
                return Core(source, cancellationToken);

                static async ValueTask<Maybe<TSource>> Core(IAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
                {
                    var last = default(TSource)!; // NB: Only matters when hasLast is set to true.
                    var hasLast = false;

                    await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                    {
                        hasLast = true;
                        last = item;
                    }

                    return hasLast ? new Maybe<TSource>(last!) : new Maybe<TSource>();
                }
            }

            return new ValueTask<Maybe<TSource>>(new Maybe<TSource>());
        }

        private static async ValueTask<Maybe<TSource>> TryGetLast<TSource>(IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)
        {
            var last = default(TSource)!; // NB: Only matters when hasLast is set to true.
            var hasLast = false;

            await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            {
                if (predicate(item))
                {
                    hasLast = true;
                    last = item;
                }
            }

            return hasLast ? new Maybe<TSource>(last!) : new Maybe<TSource>();
        }

        private static async ValueTask<Maybe<TSource>> TryGetLast<TSource>(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken)
        {
            var last = default(TSource)!; // NB: Only matters when hasLast is set to true.
            var hasLast = false;

            await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            {
                if (await predicate(item).ConfigureAwait(false))
                {
                    hasLast = true;
                    last = item;
                }
            }

            return hasLast ? new Maybe<TSource>(last!) : new Maybe<TSource>();
        }

#if !NO_DEEP_CANCELLATION
        private static async ValueTask<Maybe<TSource>> TryGetLast<TSource>(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken)
        {
            var last = default(TSource)!; // NB: Only matters when hasLast is set to true.
            var hasLast = false;

            await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
            {
                if (await predicate(item, cancellationToken).ConfigureAwait(false))
                {
                    hasLast = true;
                    last = item;
                }
            }

            return hasLast ? new Maybe<TSource>(last!) : new Maybe<TSource>();
        }
#endif
    }
}
