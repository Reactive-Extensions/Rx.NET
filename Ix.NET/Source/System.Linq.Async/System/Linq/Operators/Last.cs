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
        /// Returns the last element of an async-enumerable sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">Source async-enumerable sequence.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>ValueTask containing the last element in the async-enumerable sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) The source sequence is empty.</exception>
        public static ValueTask<TSource> LastAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<TSource> Core(IAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
            {
                var last = await TryGetLast(source, cancellationToken).ConfigureAwait(false);

                return last.HasValue ? last.Value : throw Error.NoElements();
            }
        }

        /// <summary>
        /// Returns the last element of an async-enumerable sequence that satisfies the condition in the predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">Source async-enumerable sequence.</param>
        /// <param name="predicate">A predicate function to evaluate for elements in the source sequence.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>ValueTask containing the last element in the async-enumerable sequence that satisfies the condition in the predicate.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) No element satisfies the condition in the predicate. -or- The source sequence is empty.</exception>
        public static ValueTask<TSource> LastAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (predicate == null)
                throw Error.ArgumentNull(nameof(predicate));

            return Core(source, predicate, cancellationToken);

            static async ValueTask<TSource> Core(IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken cancellationToken)
            {
                var last = await TryGetLast(source, predicate, cancellationToken).ConfigureAwait(false);

                return last.HasValue ? last.Value : throw Error.NoElements();
            }
        }

        /// <summary>
        /// Returns the last element of an async-enumerable sequence that satisfies the condition in the predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">Source async-enumerable sequence.</param>
        /// <param name="predicate">An asynchronous predicate function to evaluate for elements in the source sequence.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>ValueTask containing the last element in the async-enumerable sequence that satisfies the condition in the predicate.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        /// <exception cref="InvalidOperationException">(Asynchronous) No element satisfies the condition in the predicate. -or- The source sequence is empty.</exception>
        [GenerateAsyncOverload]
        private static ValueTask<TSource> LastAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (predicate == null)
                throw Error.ArgumentNull(nameof(predicate));

            return Core(source, predicate, cancellationToken);

            static async ValueTask<TSource> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<bool>> predicate, CancellationToken cancellationToken)
            {
                var last = await TryGetLast(source, predicate, cancellationToken).ConfigureAwait(false);

                return last.HasValue ? last.Value : throw Error.NoElements();
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<TSource> LastAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (predicate == null)
                throw Error.ArgumentNull(nameof(predicate));

            return Core(source, predicate, cancellationToken);

            static async ValueTask<TSource> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<bool>> predicate, CancellationToken cancellationToken)
            {
                var last = await TryGetLast(source, predicate, cancellationToken).ConfigureAwait(false);

                return last.HasValue ? last.Value : throw Error.NoElements();
            }
        }
#endif
    }
}
