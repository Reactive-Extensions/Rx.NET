// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT License.
// See the LICENSE file in the project root for more information. 

#if !HAS_AWAIT_FOREACH

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq
{
    public static partial class AsyncEnumerable
    {
        // REVIEW: Once we have C# 8.0 language support, we may want to do away with these methods. An open question is how to
        //         provide support for cancellation, which could be offered through WithCancellation on the source. If we still
        //         want to keep these methods, they may be a candidate for System.Interactive.Async if we consider them to be
        //         non-standard (i.e. IEnumerable<T> doesn't have a ForEach extension method either).

        /// <summary>
        /// Invokes an action for each element in the async-enumerable sequence, and returns a Task object that will get signaled when the sequence terminates.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">Source sequence.</param>
        /// <param name="action">Action to invoke for each element in the async-enumerable sequence.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>Task that signals the termination of the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="action"/> is null.</exception>
        /// <remarks>This operator is especially useful in conjunction with the asynchronous programming features introduced in C# 5.0 and Visual Basic 11.</remarks>
        public static Task ForEachAsync<TSource>(this IAsyncEnumerable<TSource> source, Action<TSource> action, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (action == null)
                throw Error.ArgumentNull(nameof(action));

            return Core(source, action, cancellationToken);

            static async Task Core(IAsyncEnumerable<TSource> source, Action<TSource> action, CancellationToken cancellationToken)
            {
                await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    action(item);
                }
            }
        }

        /// <summary>
        /// Invokes an action for each element in the async-enumerable sequence, incorporating the element's index, and returns a Task object that will get signaled when the sequence terminates.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">Source sequence.</param>
        /// <param name="action">Action to invoke for each element in the async-enumerable sequence.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>Task that signals the termination of the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="action"/> is null.</exception>
        /// <remarks>This operator is especially useful in conjunction with the asynchronous programming features introduced in C# 5.0 and Visual Basic 11.</remarks>
        public static Task ForEachAsync<TSource>(this IAsyncEnumerable<TSource> source, Action<TSource, int> action, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (action == null)
                throw Error.ArgumentNull(nameof(action));

            return Core(source, action, cancellationToken);

            static async Task Core(IAsyncEnumerable<TSource> source, Action<TSource, int> action, CancellationToken cancellationToken)
            {
                var index = 0;

                await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    action(item, checked(index++));
                }
            }
        }

        /// <summary>
        /// Invokes and awaits an asynchronous action on each element in the source sequence, and returns a task that is signaled when the sequence terminates.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the sequence.</typeparam>
        /// <param name="source">Source sequence.</param>
        /// <param name="action">Asynchronous action to invoke and await for each element in the source sequence.</param>
        /// <param name="cancellationToken">Optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>Task that signals the termination of the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static Task ForEachAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, Task> action, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (action == null)
                throw Error.ArgumentNull(nameof(action));

            return Core(source, action, cancellationToken);

            static async Task Core(IAsyncEnumerable<TSource> source, Func<TSource, Task> action, CancellationToken cancellationToken)
            {
                await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    await action(item).ConfigureAwait(false);
                }
            }
        }

        #if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        internal static Task ForEachAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, Task> action, CancellationToken cancellationToken)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (action == null)
                throw Error.ArgumentNull(nameof(action));

            return Core(source, action, cancellationToken);

            static async Task Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, Task> action, CancellationToken cancellationToken)
            {
                await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    await action(item, cancellationToken).ConfigureAwait(false);
                }
            }
        }
        #endif

        /// <summary>
        /// Invokes and awaits an asynchronous action on each element in the source sequence, incorporating the element's index, and returns a task that is signaled when the sequence terminates.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the sequence.</typeparam>
        /// <param name="source">Source sequence.</param>
        /// <param name="action">Asynchronous action to invoke and await for each element in the source sequence; the second parameter represents the index of the element.</param>
        /// <param name="cancellationToken">Optional cancellation token for cancelling the sequence at any time.</param>
        /// <returns>Task that signals the termination of the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static Task ForEachAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, Task> action, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (action == null)
                throw Error.ArgumentNull(nameof(action));

            return Core(source, action, cancellationToken);

            static async Task Core(IAsyncEnumerable<TSource> source, Func<TSource, int, Task> action, CancellationToken cancellationToken)
            {
                var index = 0;

                await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    await action(item, checked(index++)).ConfigureAwait(false);
                }
            }
        }

        #if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        internal static Task ForEachAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, Task> action, CancellationToken cancellationToken)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (action == null)
                throw Error.ArgumentNull(nameof(action));

            return Core(source, action, cancellationToken);

            static async Task Core(IAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, Task> action, CancellationToken cancellationToken)
            {
                var index = 0;

                await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    await action(item, checked(index++), cancellationToken).ConfigureAwait(false);
                }
            }
        }
        #endif
    }
}

#endif
