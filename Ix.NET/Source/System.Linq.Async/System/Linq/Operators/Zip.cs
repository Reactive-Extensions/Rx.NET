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
#if HAS_VALUETUPLE
        public static IAsyncEnumerable<(TFirst First, TSecond Second)> Zip<TFirst, TSecond>(this IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second)
        {
            if (first == null)
                throw Error.ArgumentNull(nameof(first));
            if (second == null)
                throw Error.ArgumentNull(nameof(second));

            return Core(first, second);

            static async IAsyncEnumerable<(TFirst, TSecond)> Core(IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, [System.Runtime.CompilerServices.EnumeratorCancellation]CancellationToken cancellationToken = default)
            {
                await using var e1 = first.GetConfiguredAsyncEnumerator(cancellationToken, false);
                await using var e2 = second.GetConfiguredAsyncEnumerator(cancellationToken, false);
                
                while (await e1.MoveNextAsync() && await e2.MoveNextAsync())
                {
                    yield return (e1.Current, e2.Current);
                }
            }
        }
#endif

        /// <summary>
        /// Merges two async-enumerable sequences into one async-enumerable sequence by combining their elements in a pairwise fashion.
        /// </summary>
        /// <typeparam name="TFirst">The type of the elements in the first source sequence.</typeparam>
        /// <typeparam name="TSecond">The type of the elements in the second source sequence.</typeparam>
        /// <typeparam name="TResult">The type of the elements in the result sequence, returned by the selector function.</typeparam>
        /// <param name="first">First async-enumerable source.</param>
        /// <param name="second">Second async-enumerable source.</param>
        /// <param name="selector">Function to invoke for each consecutive pair of elements from the first and second source.</param>
        /// <returns>An async-enumerable sequence containing the result of pairwise combining the elements of the first and second source using the specified result selector function.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="first"/> or <paramref name="second"/> or <paramref name="selector"/> is null.</exception>
        public static IAsyncEnumerable<TResult> Zip<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> selector)
        {
            if (first == null)
                throw Error.ArgumentNull(nameof(first));
            if (second == null)
                throw Error.ArgumentNull(nameof(second));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(first, second, selector);

            static async IAsyncEnumerable<TResult> Core(IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> selector, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
            {
                await using var e1 = first.GetConfiguredAsyncEnumerator(cancellationToken, false);
                await using var e2 = second.GetConfiguredAsyncEnumerator(cancellationToken, false);

                while (await e1.MoveNextAsync() && await e2.MoveNextAsync())
                {
                    yield return selector(e1.Current, e2.Current);
                }
            }
        }

        /// <summary>
        /// Merges two async-enumerable sequences into one async-enumerable sequence by combining their elements in a pairwise fashion.
        /// </summary>
        /// <typeparam name="TFirst">The type of the elements in the first source sequence.</typeparam>
        /// <typeparam name="TSecond">The type of the elements in the second source sequence.</typeparam>
        /// <typeparam name="TResult">The type of the elements in the result sequence, returned by the selector function.</typeparam>
        /// <param name="first">First async-enumerable source.</param>
        /// <param name="second">Second async-enumerable source.</param>
        /// <param name="selector">An asynchronous function to invoke and await for each consecutive pair of elements from the first and second source.</param>
        /// <returns>An async-enumerable sequence containing the result of pairwise combining the elements of the first and second source using the specified result selector function.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="first"/> or <paramref name="second"/> or <paramref name="selector"/> is null.</exception>
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<TResult> ZipAwaitCore<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, ValueTask<TResult>> selector)
        {
            if (first == null)
                throw Error.ArgumentNull(nameof(first));
            if (second == null)
                throw Error.ArgumentNull(nameof(second));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(first, second, selector);

            static async IAsyncEnumerable<TResult> Core(IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, ValueTask<TResult>> selector, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
            {
                await using var e1 = first.GetConfiguredAsyncEnumerator(cancellationToken, false);
                await using var e2 = second.GetConfiguredAsyncEnumerator(cancellationToken, false);

                while (await e1.MoveNextAsync() && await e2.MoveNextAsync())
                {
                    yield return await selector(e1.Current, e2.Current).ConfigureAwait(false);
                }
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<TResult> ZipAwaitWithCancellationCore<TFirst, TSecond, TResult>(this IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, CancellationToken, ValueTask<TResult>> selector)
        {
            if (first == null)
                throw Error.ArgumentNull(nameof(first));
            if (second == null)
                throw Error.ArgumentNull(nameof(second));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(first, second, selector);

            static async IAsyncEnumerable<TResult> Core(IAsyncEnumerable<TFirst> first, IAsyncEnumerable<TSecond> second, Func<TFirst, TSecond, CancellationToken, ValueTask<TResult>> selector, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
            {
                await using var e1 = first.GetConfiguredAsyncEnumerator(cancellationToken, false);
                await using var e2 = second.GetConfiguredAsyncEnumerator(cancellationToken, false);

                while (await e1.MoveNextAsync() && await e2.MoveNextAsync())
                {
                    yield return await selector(e1.Current, e2.Current, cancellationToken).ConfigureAwait(false);
                }
            }
        }
#endif
    }
}
