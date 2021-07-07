// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq
{
    public static partial class AsyncEnumerable
    {
        /// <summary>
        /// Returns the maximum value in an async-enumerable sequence of <see cref="int" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="int" /> values to determine the maximum value of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a single element with the maximum value in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ValueTask<int> MaxAsync(this IAsyncEnumerable<int> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<int> Core(IAsyncEnumerable<int> source, CancellationToken cancellationToken)
            {
                int value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = e.Current;

                    while (await e.MoveNextAsync())
                    {
                        var x = e.Current;
                        if (x > value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

        public static ValueTask<int> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<int> Core(IAsyncEnumerable<TSource> source, Func<TSource, int> selector, CancellationToken cancellationToken)
            {
                int value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = selector(e.Current);

                    while (await e.MoveNextAsync())
                    {
                        var x = selector(e.Current);
                        if (x > value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

        /// <summary>
        /// Returns the maximum <see cref="int"/> value in an async-enumerable sequence.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element of the source.</param>
        /// <param name="cancellationToken">The optional cancellation token to be usef for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static ValueTask<int> MaxAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<int> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int>> selector, CancellationToken cancellationToken)
            {
                int value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = await selector(e.Current).ConfigureAwait(false);

                    while (await e.MoveNextAsync())
                    {
                        var x = await selector(e.Current).ConfigureAwait(false);
                        if (x > value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<int> MaxAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<int> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int>> selector, CancellationToken cancellationToken)
            {
                int value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = await selector(e.Current, cancellationToken).ConfigureAwait(false);

                    while (await e.MoveNextAsync())
                    {
                        var x = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                        if (x > value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

#endif
        /// <summary>
        /// Returns the maximum value in an async-enumerable sequence of <see cref="Nullable{Int}" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="Nullable{Int}" /> values to determine the maximum value of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a single element with the maximum value in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ValueTask<int?> MaxAsync(this IAsyncEnumerable<int?> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<int?> Core(IAsyncEnumerable<int?> source, CancellationToken cancellationToken)
            {
                int? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = e.Current;
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    if (valueVal >= 0)
                    {
                        // We can fast-path this case where we know HasValue will
                        // never affect the outcome, without constantly checking
                        // if we're in such a state. Similar fast-paths could
                        // be done for other cases, but as all-positive or mostly-
                        // positive integer values are quite common in real-world
                        // uses, it's only been done for int? and long?.

                        while (await e.MoveNextAsync())
                        {
                            var cur = e.Current;
                            var x = cur.GetValueOrDefault();

                            if (x > valueVal)
                            {
                                valueVal = x;
                                value = cur;
                            }
                        }
                    }
                    else
                    {
                        while (await e.MoveNextAsync())
                        {
                            var cur = e.Current;
                            var x = cur.GetValueOrDefault();

                            // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                            // unless nulls either never happen or always happen.
                            if (cur.HasValue & x > valueVal)
                            {
                                valueVal = x;
                                value = cur;
                            }
                        }
                    }
                }

                return value;
            }
        }

        public static ValueTask<int?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int?> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<int?> Core(IAsyncEnumerable<TSource> source, Func<TSource, int?> selector, CancellationToken cancellationToken)
            {
                int? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = selector(e.Current);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    if (valueVal >= 0)
                    {
                        // We can fast-path this case where we know HasValue will
                        // never affect the outcome, without constantly checking
                        // if we're in such a state. Similar fast-paths could
                        // be done for other cases, but as all-positive or mostly-
                        // positive integer values are quite common in real-world
                        // uses, it's only been done for int? and long?.

                        while (await e.MoveNextAsync())
                        {
                            var cur = selector(e.Current);
                            var x = cur.GetValueOrDefault();

                            if (x > valueVal)
                            {
                                valueVal = x;
                                value = cur;
                            }
                        }
                    }
                    else
                    {
                        while (await e.MoveNextAsync())
                        {
                            var cur = selector(e.Current);
                            var x = cur.GetValueOrDefault();

                            // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                            // unless nulls either never happen or always happen.
                            if (cur.HasValue & x > valueVal)
                            {
                                valueVal = x;
                                value = cur;
                            }
                        }
                    }
                }

                return value;
            }
        }

        /// <summary>
        /// Returns the maximum <see cref="Nullable{Int}"/> value in an async-enumerable sequence.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element of the source.</param>
        /// <param name="cancellationToken">The optional cancellation token to be usef for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static ValueTask<int?> MaxAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<int?> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int?>> selector, CancellationToken cancellationToken)
            {
                int? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = await selector(e.Current).ConfigureAwait(false);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    if (valueVal >= 0)
                    {
                        // We can fast-path this case where we know HasValue will
                        // never affect the outcome, without constantly checking
                        // if we're in such a state. Similar fast-paths could
                        // be done for other cases, but as all-positive or mostly-
                        // positive integer values are quite common in real-world
                        // uses, it's only been done for int? and long?.

                        while (await e.MoveNextAsync())
                        {
                            var cur = await selector(e.Current).ConfigureAwait(false);
                            var x = cur.GetValueOrDefault();

                            if (x > valueVal)
                            {
                                valueVal = x;
                                value = cur;
                            }
                        }
                    }
                    else
                    {
                        while (await e.MoveNextAsync())
                        {
                            var cur = await selector(e.Current).ConfigureAwait(false);
                            var x = cur.GetValueOrDefault();

                            // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                            // unless nulls either never happen or always happen.
                            if (cur.HasValue & x > valueVal)
                            {
                                valueVal = x;
                                value = cur;
                            }
                        }
                    }
                }

                return value;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<int?> MaxAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<int?> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int?>> selector, CancellationToken cancellationToken)
            {
                int? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    if (valueVal >= 0)
                    {
                        // We can fast-path this case where we know HasValue will
                        // never affect the outcome, without constantly checking
                        // if we're in such a state. Similar fast-paths could
                        // be done for other cases, but as all-positive or mostly-
                        // positive integer values are quite common in real-world
                        // uses, it's only been done for int? and long?.

                        while (await e.MoveNextAsync())
                        {
                            var cur = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                            var x = cur.GetValueOrDefault();

                            if (x > valueVal)
                            {
                                valueVal = x;
                                value = cur;
                            }
                        }
                    }
                    else
                    {
                        while (await e.MoveNextAsync())
                        {
                            var cur = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                            var x = cur.GetValueOrDefault();

                            // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                            // unless nulls either never happen or always happen.
                            if (cur.HasValue & x > valueVal)
                            {
                                valueVal = x;
                                value = cur;
                            }
                        }
                    }
                }

                return value;
            }
        }

#endif
        /// <summary>
        /// Returns the maximum value in an async-enumerable sequence of <see cref="long" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="long" /> values to determine the maximum value of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a single element with the maximum value in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ValueTask<long> MaxAsync(this IAsyncEnumerable<long> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<long> Core(IAsyncEnumerable<long> source, CancellationToken cancellationToken)
            {
                long value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = e.Current;

                    while (await e.MoveNextAsync())
                    {
                        var x = e.Current;
                        if (x > value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

        public static ValueTask<long> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, long> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<long> Core(IAsyncEnumerable<TSource> source, Func<TSource, long> selector, CancellationToken cancellationToken)
            {
                long value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = selector(e.Current);

                    while (await e.MoveNextAsync())
                    {
                        var x = selector(e.Current);
                        if (x > value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the maximum <see cref="long"/> value.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element of the source.</param>
        /// <param name="cancellationToken">The optional cancellation token to be usef for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static ValueTask<long> MaxAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<long> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long>> selector, CancellationToken cancellationToken)
            {
                long value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = await selector(e.Current).ConfigureAwait(false);

                    while (await e.MoveNextAsync())
                    {
                        var x = await selector(e.Current).ConfigureAwait(false);
                        if (x > value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<long> MaxAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<long> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long>> selector, CancellationToken cancellationToken)
            {
                long value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = await selector(e.Current, cancellationToken).ConfigureAwait(false);

                    while (await e.MoveNextAsync())
                    {
                        var x = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                        if (x > value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

#endif
        /// <summary>
        /// Returns the maximum value in an async-enumerable sequence of <see cref="Nullable{Long}" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="Nullable{Long}" /> values to determine the maximum value of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a single element with the maximum value in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ValueTask<long?> MaxAsync(this IAsyncEnumerable<long?> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<long?> Core(IAsyncEnumerable<long?> source, CancellationToken cancellationToken)
            {
                long? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = e.Current;
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    if (valueVal >= 0)
                    {
                        // We can fast-path this case where we know HasValue will
                        // never affect the outcome, without constantly checking
                        // if we're in such a state. Similar fast-paths could
                        // be done for other cases, but as all-positive or mostly-
                        // positive integer values are quite common in real-world
                        // uses, it's only been done for int? and long?.

                        while (await e.MoveNextAsync())
                        {
                            var cur = e.Current;
                            var x = cur.GetValueOrDefault();

                            if (x > valueVal)
                            {
                                valueVal = x;
                                value = cur;
                            }
                        }
                    }
                    else
                    {
                        while (await e.MoveNextAsync())
                        {
                            var cur = e.Current;
                            var x = cur.GetValueOrDefault();

                            // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                            // unless nulls either never happen or always happen.
                            if (cur.HasValue & x > valueVal)
                            {
                                valueVal = x;
                                value = cur;
                            }
                        }
                    }
                }

                return value;
            }
        }

        public static ValueTask<long?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, long?> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<long?> Core(IAsyncEnumerable<TSource> source, Func<TSource, long?> selector, CancellationToken cancellationToken)
            {
                long? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = selector(e.Current);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    if (valueVal >= 0)
                    {
                        // We can fast-path this case where we know HasValue will
                        // never affect the outcome, without constantly checking
                        // if we're in such a state. Similar fast-paths could
                        // be done for other cases, but as all-positive or mostly-
                        // positive integer values are quite common in real-world
                        // uses, it's only been done for int? and long?.

                        while (await e.MoveNextAsync())
                        {
                            var cur = selector(e.Current);
                            var x = cur.GetValueOrDefault();

                            if (x > valueVal)
                            {
                                valueVal = x;
                                value = cur;
                            }
                        }
                    }
                    else
                    {
                        while (await e.MoveNextAsync())
                        {
                            var cur = selector(e.Current);
                            var x = cur.GetValueOrDefault();

                            // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                            // unless nulls either never happen or always happen.
                            if (cur.HasValue & x > valueVal)
                            {
                                valueVal = x;
                                value = cur;
                            }
                        }
                    }
                }

                return value;
            }
        }

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the maximum <see cref="Nullable{Long}"/> value.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element of the source.</param>
        /// <param name="cancellationToken">The optional cancellation token to be usef for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static ValueTask<long?> MaxAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<long?> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long?>> selector, CancellationToken cancellationToken)
            {
                long? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = await selector(e.Current).ConfigureAwait(false);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    if (valueVal >= 0)
                    {
                        // We can fast-path this case where we know HasValue will
                        // never affect the outcome, without constantly checking
                        // if we're in such a state. Similar fast-paths could
                        // be done for other cases, but as all-positive or mostly-
                        // positive integer values are quite common in real-world
                        // uses, it's only been done for int? and long?.

                        while (await e.MoveNextAsync())
                        {
                            var cur = await selector(e.Current).ConfigureAwait(false);
                            var x = cur.GetValueOrDefault();

                            if (x > valueVal)
                            {
                                valueVal = x;
                                value = cur;
                            }
                        }
                    }
                    else
                    {
                        while (await e.MoveNextAsync())
                        {
                            var cur = await selector(e.Current).ConfigureAwait(false);
                            var x = cur.GetValueOrDefault();

                            // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                            // unless nulls either never happen or always happen.
                            if (cur.HasValue & x > valueVal)
                            {
                                valueVal = x;
                                value = cur;
                            }
                        }
                    }
                }

                return value;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<long?> MaxAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<long?> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long?>> selector, CancellationToken cancellationToken)
            {
                long? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    if (valueVal >= 0)
                    {
                        // We can fast-path this case where we know HasValue will
                        // never affect the outcome, without constantly checking
                        // if we're in such a state. Similar fast-paths could
                        // be done for other cases, but as all-positive or mostly-
                        // positive integer values are quite common in real-world
                        // uses, it's only been done for int? and long?.

                        while (await e.MoveNextAsync())
                        {
                            var cur = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                            var x = cur.GetValueOrDefault();

                            if (x > valueVal)
                            {
                                valueVal = x;
                                value = cur;
                            }
                        }
                    }
                    else
                    {
                        while (await e.MoveNextAsync())
                        {
                            var cur = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                            var x = cur.GetValueOrDefault();

                            // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                            // unless nulls either never happen or always happen.
                            if (cur.HasValue & x > valueVal)
                            {
                                valueVal = x;
                                value = cur;
                            }
                        }
                    }
                }

                return value;
            }
        }

#endif
        /// <summary>
        /// Returns the maximum value in an async-enumerable sequence of <see cref="float" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="float" /> values to determine the maximum value of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a single element with the maximum value in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ValueTask<float> MaxAsync(this IAsyncEnumerable<float> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<float> Core(IAsyncEnumerable<float> source, CancellationToken cancellationToken)
            {
                float value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = e.Current;

                    // NaN is ordered less than all other values. We need to do explicit checks
                    // to ensure this, but once we've found a value that is not NaN we need no
                    // longer worry about it, so first loop until such a value is found (or not,
                    // as the case may be).

                    while (float.IsNaN(value))
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = e.Current;
                    }

                    while (await e.MoveNextAsync())
                    {
                        var x = e.Current;
                        if (x > value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

        public static ValueTask<float> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, float> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<float> Core(IAsyncEnumerable<TSource> source, Func<TSource, float> selector, CancellationToken cancellationToken)
            {
                float value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = selector(e.Current);

                    // NaN is ordered less than all other values. We need to do explicit checks
                    // to ensure this, but once we've found a value that is not NaN we need no
                    // longer worry about it, so first loop until such a value is found (or not,
                    // as the case may be).

                    while (float.IsNaN(value))
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = selector(e.Current);
                    }

                    while (await e.MoveNextAsync())
                    {
                        var x = selector(e.Current);
                        if (x > value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the maximum <see cref="float"/> value.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element of the source.</param>
        /// <param name="cancellationToken">The optional cancellation token to be usef for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static ValueTask<float> MaxAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<float> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float>> selector, CancellationToken cancellationToken)
            {
                float value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = await selector(e.Current).ConfigureAwait(false);

                    // NaN is ordered less than all other values. We need to do explicit checks
                    // to ensure this, but once we've found a value that is not NaN we need no
                    // longer worry about it, so first loop until such a value is found (or not,
                    // as the case may be).

                    while (float.IsNaN(value))
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = await selector(e.Current).ConfigureAwait(false);
                    }

                    while (await e.MoveNextAsync())
                    {
                        var x = await selector(e.Current).ConfigureAwait(false);
                        if (x > value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<float> MaxAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<float> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float>> selector, CancellationToken cancellationToken)
            {
                float value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = await selector(e.Current, cancellationToken).ConfigureAwait(false);

                    // NaN is ordered less than all other values. We need to do explicit checks
                    // to ensure this, but once we've found a value that is not NaN we need no
                    // longer worry about it, so first loop until such a value is found (or not,
                    // as the case may be).

                    while (float.IsNaN(value))
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                    }

                    while (await e.MoveNextAsync())
                    {
                        var x = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                        if (x > value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

#endif
        /// <summary>
        /// Returns the maximum value in an async-enumerable sequence of <see cref="Nullable{Float}" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="Nullable{Float}" /> values to determine the maximum value of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a single element with the maximum value in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ValueTask<float?> MaxAsync(this IAsyncEnumerable<float?> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<float?> Core(IAsyncEnumerable<float?> source, CancellationToken cancellationToken)
            {
                float? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = e.Current;
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    // NaN is ordered less than all other values. We need to do explicit checks
                    // to ensure this, but once we've found a value that is not NaN we need no
                    // longer worry about it, so first loop until such a value is found (or not,
                    // as the case may be).

                    while (float.IsNaN(valueVal))
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        var cur = e.Current;

                        if (cur.HasValue)
                        {
                            valueVal = (value = cur).GetValueOrDefault();
                        }
                    }

                    while (await e.MoveNextAsync())
                    {
                        var cur = e.Current;
                        var x = cur.GetValueOrDefault();

                        // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                        // unless nulls either never happen or always happen.
                        if (cur.HasValue & x > valueVal)
                        {
                            valueVal = x;
                            value = cur;
                        }
                    }
                }

                return value;
            }
        }

        public static ValueTask<float?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, float?> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<float?> Core(IAsyncEnumerable<TSource> source, Func<TSource, float?> selector, CancellationToken cancellationToken)
            {
                float? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = selector(e.Current);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    // NaN is ordered less than all other values. We need to do explicit checks
                    // to ensure this, but once we've found a value that is not NaN we need no
                    // longer worry about it, so first loop until such a value is found (or not,
                    // as the case may be).

                    while (float.IsNaN(valueVal))
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        var cur = selector(e.Current);

                        if (cur.HasValue)
                        {
                            valueVal = (value = cur).GetValueOrDefault();
                        }
                    }

                    while (await e.MoveNextAsync())
                    {
                        var cur = selector(e.Current);
                        var x = cur.GetValueOrDefault();

                        // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                        // unless nulls either never happen or always happen.
                        if (cur.HasValue & x > valueVal)
                        {
                            valueVal = x;
                            value = cur;
                        }
                    }
                }

                return value;
            }
        }

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the maximum <see cref="Nullable{Float}"/> value.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element of the source.</param>
        /// <param name="cancellationToken">The optional cancellation token to be usef for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static ValueTask<float?> MaxAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<float?> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float?>> selector, CancellationToken cancellationToken)
            {
                float? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = await selector(e.Current).ConfigureAwait(false);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    // NaN is ordered less than all other values. We need to do explicit checks
                    // to ensure this, but once we've found a value that is not NaN we need no
                    // longer worry about it, so first loop until such a value is found (or not,
                    // as the case may be).

                    while (float.IsNaN(valueVal))
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        var cur = await selector(e.Current).ConfigureAwait(false);

                        if (cur.HasValue)
                        {
                            valueVal = (value = cur).GetValueOrDefault();
                        }
                    }

                    while (await e.MoveNextAsync())
                    {
                        var cur = await selector(e.Current).ConfigureAwait(false);
                        var x = cur.GetValueOrDefault();

                        // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                        // unless nulls either never happen or always happen.
                        if (cur.HasValue & x > valueVal)
                        {
                            valueVal = x;
                            value = cur;
                        }
                    }
                }

                return value;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<float?> MaxAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<float?> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float?>> selector, CancellationToken cancellationToken)
            {
                float? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    // NaN is ordered less than all other values. We need to do explicit checks
                    // to ensure this, but once we've found a value that is not NaN we need no
                    // longer worry about it, so first loop until such a value is found (or not,
                    // as the case may be).

                    while (float.IsNaN(valueVal))
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        var cur = await selector(e.Current, cancellationToken).ConfigureAwait(false);

                        if (cur.HasValue)
                        {
                            valueVal = (value = cur).GetValueOrDefault();
                        }
                    }

                    while (await e.MoveNextAsync())
                    {
                        var cur = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                        var x = cur.GetValueOrDefault();

                        // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                        // unless nulls either never happen or always happen.
                        if (cur.HasValue & x > valueVal)
                        {
                            valueVal = x;
                            value = cur;
                        }
                    }
                }

                return value;
            }
        }

#endif
        /// <summary>
        /// Returns the maximum value in an async-enumerable sequence of <see cref="double" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="double" /> values to determine the maximum value of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a single element with the maximum value in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ValueTask<double> MaxAsync(this IAsyncEnumerable<double> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<double> Core(IAsyncEnumerable<double> source, CancellationToken cancellationToken)
            {
                double value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = e.Current;

                    // NaN is ordered less than all other values. We need to do explicit checks
                    // to ensure this, but once we've found a value that is not NaN we need no
                    // longer worry about it, so first loop until such a value is found (or not,
                    // as the case may be).

                    while (double.IsNaN(value))
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = e.Current;
                    }

                    while (await e.MoveNextAsync())
                    {
                        var x = e.Current;
                        if (x > value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

        public static ValueTask<double> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, double> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double> Core(IAsyncEnumerable<TSource> source, Func<TSource, double> selector, CancellationToken cancellationToken)
            {
                double value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = selector(e.Current);

                    // NaN is ordered less than all other values. We need to do explicit checks
                    // to ensure this, but once we've found a value that is not NaN we need no
                    // longer worry about it, so first loop until such a value is found (or not,
                    // as the case may be).

                    while (double.IsNaN(value))
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = selector(e.Current);
                    }

                    while (await e.MoveNextAsync())
                    {
                        var x = selector(e.Current);
                        if (x > value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the maximum <see cref="double"/> value.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element of the source.</param>
        /// <param name="cancellationToken">The optional cancellation token to be usef for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static ValueTask<double> MaxAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double>> selector, CancellationToken cancellationToken)
            {
                double value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = await selector(e.Current).ConfigureAwait(false);

                    // NaN is ordered less than all other values. We need to do explicit checks
                    // to ensure this, but once we've found a value that is not NaN we need no
                    // longer worry about it, so first loop until such a value is found (or not,
                    // as the case may be).

                    while (double.IsNaN(value))
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = await selector(e.Current).ConfigureAwait(false);
                    }

                    while (await e.MoveNextAsync())
                    {
                        var x = await selector(e.Current).ConfigureAwait(false);
                        if (x > value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<double> MaxAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double>> selector, CancellationToken cancellationToken)
            {
                double value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = await selector(e.Current, cancellationToken).ConfigureAwait(false);

                    // NaN is ordered less than all other values. We need to do explicit checks
                    // to ensure this, but once we've found a value that is not NaN we need no
                    // longer worry about it, so first loop until such a value is found (or not,
                    // as the case may be).

                    while (double.IsNaN(value))
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                    }

                    while (await e.MoveNextAsync())
                    {
                        var x = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                        if (x > value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

#endif
        /// <summary>
        /// Returns the maximum value in an async-enumerable sequence of <see cref="Nullable{Double}" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="Nullable{Double}" /> values to determine the maximum value of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a single element with the maximum value in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ValueTask<double?> MaxAsync(this IAsyncEnumerable<double?> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<double?> Core(IAsyncEnumerable<double?> source, CancellationToken cancellationToken)
            {
                double? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = e.Current;
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    // NaN is ordered less than all other values. We need to do explicit checks
                    // to ensure this, but once we've found a value that is not NaN we need no
                    // longer worry about it, so first loop until such a value is found (or not,
                    // as the case may be).

                    while (double.IsNaN(valueVal))
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        var cur = e.Current;

                        if (cur.HasValue)
                        {
                            valueVal = (value = cur).GetValueOrDefault();
                        }
                    }

                    while (await e.MoveNextAsync())
                    {
                        var cur = e.Current;
                        var x = cur.GetValueOrDefault();

                        // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                        // unless nulls either never happen or always happen.
                        if (cur.HasValue & x > valueVal)
                        {
                            valueVal = x;
                            value = cur;
                        }
                    }
                }

                return value;
            }
        }

        public static ValueTask<double?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, double?> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double?> Core(IAsyncEnumerable<TSource> source, Func<TSource, double?> selector, CancellationToken cancellationToken)
            {
                double? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = selector(e.Current);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    // NaN is ordered less than all other values. We need to do explicit checks
                    // to ensure this, but once we've found a value that is not NaN we need no
                    // longer worry about it, so first loop until such a value is found (or not,
                    // as the case may be).

                    while (double.IsNaN(valueVal))
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        var cur = selector(e.Current);

                        if (cur.HasValue)
                        {
                            valueVal = (value = cur).GetValueOrDefault();
                        }
                    }

                    while (await e.MoveNextAsync())
                    {
                        var cur = selector(e.Current);
                        var x = cur.GetValueOrDefault();

                        // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                        // unless nulls either never happen or always happen.
                        if (cur.HasValue & x > valueVal)
                        {
                            valueVal = x;
                            value = cur;
                        }
                    }
                }

                return value;
            }
        }

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the maximum <see cref="Nullable{Double}"/> value.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element of the source.</param>
        /// <param name="cancellationToken">The optional cancellation token to be usef for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static ValueTask<double?> MaxAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double?> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double?>> selector, CancellationToken cancellationToken)
            {
                double? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = await selector(e.Current).ConfigureAwait(false);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    // NaN is ordered less than all other values. We need to do explicit checks
                    // to ensure this, but once we've found a value that is not NaN we need no
                    // longer worry about it, so first loop until such a value is found (or not,
                    // as the case may be).

                    while (double.IsNaN(valueVal))
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        var cur = await selector(e.Current).ConfigureAwait(false);

                        if (cur.HasValue)
                        {
                            valueVal = (value = cur).GetValueOrDefault();
                        }
                    }

                    while (await e.MoveNextAsync())
                    {
                        var cur = await selector(e.Current).ConfigureAwait(false);
                        var x = cur.GetValueOrDefault();

                        // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                        // unless nulls either never happen or always happen.
                        if (cur.HasValue & x > valueVal)
                        {
                            valueVal = x;
                            value = cur;
                        }
                    }
                }

                return value;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<double?> MaxAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double?> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double?>> selector, CancellationToken cancellationToken)
            {
                double? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    // NaN is ordered less than all other values. We need to do explicit checks
                    // to ensure this, but once we've found a value that is not NaN we need no
                    // longer worry about it, so first loop until such a value is found (or not,
                    // as the case may be).

                    while (double.IsNaN(valueVal))
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        var cur = await selector(e.Current, cancellationToken).ConfigureAwait(false);

                        if (cur.HasValue)
                        {
                            valueVal = (value = cur).GetValueOrDefault();
                        }
                    }

                    while (await e.MoveNextAsync())
                    {
                        var cur = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                        var x = cur.GetValueOrDefault();

                        // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                        // unless nulls either never happen or always happen.
                        if (cur.HasValue & x > valueVal)
                        {
                            valueVal = x;
                            value = cur;
                        }
                    }
                }

                return value;
            }
        }

#endif
        /// <summary>
        /// Returns the maximum value in an async-enumerable sequence of <see cref="decimal" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="decimal" /> values to determine the maximum value of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a single element with the maximum value in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ValueTask<decimal> MaxAsync(this IAsyncEnumerable<decimal> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<decimal> Core(IAsyncEnumerable<decimal> source, CancellationToken cancellationToken)
            {
                decimal value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = e.Current;

                    while (await e.MoveNextAsync())
                    {
                        var x = e.Current;
                        if (x > value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

        public static ValueTask<decimal> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, decimal> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<decimal> Core(IAsyncEnumerable<TSource> source, Func<TSource, decimal> selector, CancellationToken cancellationToken)
            {
                decimal value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = selector(e.Current);

                    while (await e.MoveNextAsync())
                    {
                        var x = selector(e.Current);
                        if (x > value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the maximum <see cref="decimal"/> value.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element of the source.</param>
        /// <param name="cancellationToken">The optional cancellation token to be usef for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static ValueTask<decimal> MaxAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<decimal> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal>> selector, CancellationToken cancellationToken)
            {
                decimal value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = await selector(e.Current).ConfigureAwait(false);

                    while (await e.MoveNextAsync())
                    {
                        var x = await selector(e.Current).ConfigureAwait(false);
                        if (x > value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<decimal> MaxAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<decimal> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal>> selector, CancellationToken cancellationToken)
            {
                decimal value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = await selector(e.Current, cancellationToken).ConfigureAwait(false);

                    while (await e.MoveNextAsync())
                    {
                        var x = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                        if (x > value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

#endif
        /// <summary>
        /// Returns the maximum value in an async-enumerable sequence of <see cref="Nullable{Decimal}" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="Nullable{Decimal}" /> values to determine the maximum value of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a single element with the maximum value in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ValueTask<decimal?> MaxAsync(this IAsyncEnumerable<decimal?> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<decimal?> Core(IAsyncEnumerable<decimal?> source, CancellationToken cancellationToken)
            {
                decimal? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = e.Current;
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    while (await e.MoveNextAsync())
                    {
                        var cur = e.Current;
                        var x = cur.GetValueOrDefault();

                        if (cur.HasValue && x > valueVal)
                        {
                            valueVal = x;
                            value = cur;
                        }
                    }
                }

                return value;
            }
        }

        public static ValueTask<decimal?> MaxAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, decimal?> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<decimal?> Core(IAsyncEnumerable<TSource> source, Func<TSource, decimal?> selector, CancellationToken cancellationToken)
            {
                decimal? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = selector(e.Current);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    while (await e.MoveNextAsync())
                    {
                        var cur = selector(e.Current);
                        var x = cur.GetValueOrDefault();

                        if (cur.HasValue && x > valueVal)
                        {
                            valueVal = x;
                            value = cur;
                        }
                    }
                }

                return value;
            }
        }

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the maximum <see cref="Nullable{Decimal}"/> value.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element of the source.</param>
        /// <param name="cancellationToken">The optional cancellation token to be usef for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing the maximum value in the sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static ValueTask<decimal?> MaxAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<decimal?> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal?>> selector, CancellationToken cancellationToken)
            {
                decimal? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = await selector(e.Current).ConfigureAwait(false);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    while (await e.MoveNextAsync())
                    {
                        var cur = await selector(e.Current).ConfigureAwait(false);
                        var x = cur.GetValueOrDefault();

                        if (cur.HasValue && x > valueVal)
                        {
                            valueVal = x;
                            value = cur;
                        }
                    }
                }

                return value;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<decimal?> MaxAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<decimal?> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal?>> selector, CancellationToken cancellationToken)
            {
                decimal? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    while (await e.MoveNextAsync())
                    {
                        var cur = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                        var x = cur.GetValueOrDefault();

                        if (cur.HasValue && x > valueVal)
                        {
                            valueVal = x;
                            value = cur;
                        }
                    }
                }

                return value;
            }
        }

#endif
        /// <summary>
        /// Returns the minimum value in an async-enumerable sequence of <see cref="int" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="int" /> values to determine the minimum value of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a single element with the minimum value in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ValueTask<int> MinAsync(this IAsyncEnumerable<int> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<int> Core(IAsyncEnumerable<int> source, CancellationToken cancellationToken)
            {
                int value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = e.Current;

                    while (await e.MoveNextAsync())
                    {
                        var x = e.Current;
                        if (x < value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

        public static ValueTask<int> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<int> Core(IAsyncEnumerable<TSource> source, Func<TSource, int> selector, CancellationToken cancellationToken)
            {
                int value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = selector(e.Current);

                    while (await e.MoveNextAsync())
                    {
                        var x = selector(e.Current);
                        if (x < value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the minimum <see cref="int"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to determine the minimum element of.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask sequence containing a single element with the value that corresponds to the minimum element in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<int> MinAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<int> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int>> selector, CancellationToken cancellationToken)
            {
                int value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = await selector(e.Current).ConfigureAwait(false);

                    while (await e.MoveNextAsync())
                    {
                        var x = await selector(e.Current).ConfigureAwait(false);
                        if (x < value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<int> MinAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<int> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int>> selector, CancellationToken cancellationToken)
            {
                int value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = await selector(e.Current, cancellationToken).ConfigureAwait(false);

                    while (await e.MoveNextAsync())
                    {
                        var x = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                        if (x < value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

#endif
        /// <summary>
        /// Returns the minimum value in an async-enumerable sequence of <see cref="Nullable{Int}" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="Nullable{Int}" /> values to determine the minimum value of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a single element with the minimum value in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ValueTask<int?> MinAsync(this IAsyncEnumerable<int?> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<int?> Core(IAsyncEnumerable<int?> source, CancellationToken cancellationToken)
            {
                int? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = e.Current;
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    while (await e.MoveNextAsync())
                    {
                        var cur = e.Current;
                        var x = cur.GetValueOrDefault();

                        // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                        // unless nulls either never happen or always happen.
                        if (cur.HasValue & x < valueVal)
                        {
                            valueVal = x;
                            value = cur;
                        }
                    }
                }

                return value;
            }
        }

        public static ValueTask<int?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, int?> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<int?> Core(IAsyncEnumerable<TSource> source, Func<TSource, int?> selector, CancellationToken cancellationToken)
            {
                int? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = selector(e.Current);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    while (await e.MoveNextAsync())
                    {
                        var cur = selector(e.Current);
                        var x = cur.GetValueOrDefault();

                        // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                        // unless nulls either never happen or always happen.
                        if (cur.HasValue & x < valueVal)
                        {
                            valueVal = x;
                            value = cur;
                        }
                    }
                }

                return value;
            }
        }

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the minimum <see cref="Nullable{Int}"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to determine the minimum element of.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask sequence containing a single element with the value that corresponds to the minimum element in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<int?> MinAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<int?> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<int?>> selector, CancellationToken cancellationToken)
            {
                int? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = await selector(e.Current).ConfigureAwait(false);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    while (await e.MoveNextAsync())
                    {
                        var cur = await selector(e.Current).ConfigureAwait(false);
                        var x = cur.GetValueOrDefault();

                        // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                        // unless nulls either never happen or always happen.
                        if (cur.HasValue & x < valueVal)
                        {
                            valueVal = x;
                            value = cur;
                        }
                    }
                }

                return value;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<int?> MinAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<int?> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<int?>> selector, CancellationToken cancellationToken)
            {
                int? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    while (await e.MoveNextAsync())
                    {
                        var cur = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                        var x = cur.GetValueOrDefault();

                        // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                        // unless nulls either never happen or always happen.
                        if (cur.HasValue & x < valueVal)
                        {
                            valueVal = x;
                            value = cur;
                        }
                    }
                }

                return value;
            }
        }

#endif
        /// <summary>
        /// Returns the minimum value in an async-enumerable sequence of <see cref="long" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="long" /> values to determine the minimum value of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a single element with the minimum value in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ValueTask<long> MinAsync(this IAsyncEnumerable<long> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<long> Core(IAsyncEnumerable<long> source, CancellationToken cancellationToken)
            {
                long value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = e.Current;

                    while (await e.MoveNextAsync())
                    {
                        var x = e.Current;
                        if (x < value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

        public static ValueTask<long> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, long> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<long> Core(IAsyncEnumerable<TSource> source, Func<TSource, long> selector, CancellationToken cancellationToken)
            {
                long value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = selector(e.Current);

                    while (await e.MoveNextAsync())
                    {
                        var x = selector(e.Current);
                        if (x < value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the minimum <see cref="long"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to determine the minimum element of.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask sequence containing a single element with the value that corresponds to the minimum element in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<long> MinAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<long> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long>> selector, CancellationToken cancellationToken)
            {
                long value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = await selector(e.Current).ConfigureAwait(false);

                    while (await e.MoveNextAsync())
                    {
                        var x = await selector(e.Current).ConfigureAwait(false);
                        if (x < value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<long> MinAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<long> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long>> selector, CancellationToken cancellationToken)
            {
                long value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = await selector(e.Current, cancellationToken).ConfigureAwait(false);

                    while (await e.MoveNextAsync())
                    {
                        var x = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                        if (x < value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

#endif
        /// <summary>
        /// Returns the minimum value in an async-enumerable sequence of <see cref="Nullable{Long}" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="Nullable{Long}" /> values to determine the minimum value of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a single element with the minimum value in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ValueTask<long?> MinAsync(this IAsyncEnumerable<long?> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<long?> Core(IAsyncEnumerable<long?> source, CancellationToken cancellationToken)
            {
                long? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = e.Current;
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    while (await e.MoveNextAsync())
                    {
                        var cur = e.Current;
                        var x = cur.GetValueOrDefault();

                        // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                        // unless nulls either never happen or always happen.
                        if (cur.HasValue & x < valueVal)
                        {
                            valueVal = x;
                            value = cur;
                        }
                    }
                }

                return value;
            }
        }

        public static ValueTask<long?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, long?> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<long?> Core(IAsyncEnumerable<TSource> source, Func<TSource, long?> selector, CancellationToken cancellationToken)
            {
                long? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = selector(e.Current);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    while (await e.MoveNextAsync())
                    {
                        var cur = selector(e.Current);
                        var x = cur.GetValueOrDefault();

                        // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                        // unless nulls either never happen or always happen.
                        if (cur.HasValue & x < valueVal)
                        {
                            valueVal = x;
                            value = cur;
                        }
                    }
                }

                return value;
            }
        }

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the minimum <see cref="Nullable{Long}"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to determine the minimum element of.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask sequence containing a single element with the value that corresponds to the minimum element in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<long?> MinAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<long?> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<long?>> selector, CancellationToken cancellationToken)
            {
                long? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = await selector(e.Current).ConfigureAwait(false);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    while (await e.MoveNextAsync())
                    {
                        var cur = await selector(e.Current).ConfigureAwait(false);
                        var x = cur.GetValueOrDefault();

                        // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                        // unless nulls either never happen or always happen.
                        if (cur.HasValue & x < valueVal)
                        {
                            valueVal = x;
                            value = cur;
                        }
                    }
                }

                return value;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<long?> MinAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<long?> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<long?>> selector, CancellationToken cancellationToken)
            {
                long? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    while (await e.MoveNextAsync())
                    {
                        var cur = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                        var x = cur.GetValueOrDefault();

                        // Do not replace & with &&. The branch prediction cost outweighs the extra operation
                        // unless nulls either never happen or always happen.
                        if (cur.HasValue & x < valueVal)
                        {
                            valueVal = x;
                            value = cur;
                        }
                    }
                }

                return value;
            }
        }

#endif
        /// <summary>
        /// Returns the minimum value in an async-enumerable sequence of <see cref="float" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="float" /> values to determine the minimum value of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a single element with the minimum value in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ValueTask<float> MinAsync(this IAsyncEnumerable<float> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<float> Core(IAsyncEnumerable<float> source, CancellationToken cancellationToken)
            {
                float value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = e.Current;

                    while (await e.MoveNextAsync())
                    {
                        var x = e.Current;
                        if (x < value)
                        {
                            value = x;
                        }
                        else
                        {
                            // Normally NaN < anything is false, as is anything < NaN
                            // However, this leads to some irksome outcomes in Min and Max.
                            // If we use those semantics then Min(NaN, 5.0) is NaN, but
                            // Min(5.0, NaN) is 5.0!  To fix this, we impose a total
                            // ordering where NaN is smaller than every value, including
                            // negative infinity.
                            // Not testing for NaN therefore isn't an option, but since we
                            // can't find a smaller value, we can short-circuit.

                            if (float.IsNaN(x))
                            {
                                return x;
                            }
                        }
                    }
                }

                return value;
            }
        }

        public static ValueTask<float> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, float> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<float> Core(IAsyncEnumerable<TSource> source, Func<TSource, float> selector, CancellationToken cancellationToken)
            {
                float value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = selector(e.Current);

                    while (await e.MoveNextAsync())
                    {
                        var x = selector(e.Current);
                        if (x < value)
                        {
                            value = x;
                        }
                        else
                        {
                            // Normally NaN < anything is false, as is anything < NaN
                            // However, this leads to some irksome outcomes in Min and Max.
                            // If we use those semantics then Min(NaN, 5.0) is NaN, but
                            // Min(5.0, NaN) is 5.0!  To fix this, we impose a total
                            // ordering where NaN is smaller than every value, including
                            // negative infinity.
                            // Not testing for NaN therefore isn't an option, but since we
                            // can't find a smaller value, we can short-circuit.

                            if (float.IsNaN(x))
                            {
                                return x;
                            }
                        }
                    }
                }

                return value;
            }
        }

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the minimum <see cref="float"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to determine the minimum element of.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask sequence containing a single element with the value that corresponds to the minimum element in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<float> MinAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<float> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float>> selector, CancellationToken cancellationToken)
            {
                float value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = await selector(e.Current).ConfigureAwait(false);

                    while (await e.MoveNextAsync())
                    {
                        var x = await selector(e.Current).ConfigureAwait(false);
                        if (x < value)
                        {
                            value = x;
                        }
                        else
                        {
                            // Normally NaN < anything is false, as is anything < NaN
                            // However, this leads to some irksome outcomes in Min and Max.
                            // If we use those semantics then Min(NaN, 5.0) is NaN, but
                            // Min(5.0, NaN) is 5.0!  To fix this, we impose a total
                            // ordering where NaN is smaller than every value, including
                            // negative infinity.
                            // Not testing for NaN therefore isn't an option, but since we
                            // can't find a smaller value, we can short-circuit.

                            if (float.IsNaN(x))
                            {
                                return x;
                            }
                        }
                    }
                }

                return value;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<float> MinAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<float> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float>> selector, CancellationToken cancellationToken)
            {
                float value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = await selector(e.Current, cancellationToken).ConfigureAwait(false);

                    while (await e.MoveNextAsync())
                    {
                        var x = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                        if (x < value)
                        {
                            value = x;
                        }
                        else
                        {
                            // Normally NaN < anything is false, as is anything < NaN
                            // However, this leads to some irksome outcomes in Min and Max.
                            // If we use those semantics then Min(NaN, 5.0) is NaN, but
                            // Min(5.0, NaN) is 5.0!  To fix this, we impose a total
                            // ordering where NaN is smaller than every value, including
                            // negative infinity.
                            // Not testing for NaN therefore isn't an option, but since we
                            // can't find a smaller value, we can short-circuit.

                            if (float.IsNaN(x))
                            {
                                return x;
                            }
                        }
                    }
                }

                return value;
            }
        }

#endif
        /// <summary>
        /// Returns the minimum value in an async-enumerable sequence of <see cref="Nullable{Float}" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="Nullable{Float}" /> values to determine the minimum value of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a single element with the minimum value in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ValueTask<float?> MinAsync(this IAsyncEnumerable<float?> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<float?> Core(IAsyncEnumerable<float?> source, CancellationToken cancellationToken)
            {
                float? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = e.Current;
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    while (await e.MoveNextAsync())
                    {
                        var cur = e.Current;
                        if (cur.HasValue)
                        {
                            var x = cur.GetValueOrDefault();
                            if (x < valueVal)
                            {
                                valueVal = x;
                                value = cur;
                            }
                            else
                            {
                                // Normally NaN < anything is false, as is anything < NaN
                                // However, this leads to some irksome outcomes in Min and Max.
                                // If we use those semantics then Min(NaN, 5.0) is NaN, but
                                // Min(5.0, NaN) is 5.0!  To fix this, we impose a total
                                // ordering where NaN is smaller than every value, including
                                // negative infinity.
                                // Not testing for NaN therefore isn't an option, but since we
                                // can't find a smaller value, we can short-circuit.

                                if (float.IsNaN(x))
                                {
                                    return cur;
                                }
                            }
                        }
                    }
                }

                return value;
            }
        }

        public static ValueTask<float?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, float?> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<float?> Core(IAsyncEnumerable<TSource> source, Func<TSource, float?> selector, CancellationToken cancellationToken)
            {
                float? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = selector(e.Current);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    while (await e.MoveNextAsync())
                    {
                        var cur = selector(e.Current);
                        if (cur.HasValue)
                        {
                            var x = cur.GetValueOrDefault();
                            if (x < valueVal)
                            {
                                valueVal = x;
                                value = cur;
                            }
                            else
                            {
                                // Normally NaN < anything is false, as is anything < NaN
                                // However, this leads to some irksome outcomes in Min and Max.
                                // If we use those semantics then Min(NaN, 5.0) is NaN, but
                                // Min(5.0, NaN) is 5.0!  To fix this, we impose a total
                                // ordering where NaN is smaller than every value, including
                                // negative infinity.
                                // Not testing for NaN therefore isn't an option, but since we
                                // can't find a smaller value, we can short-circuit.

                                if (float.IsNaN(x))
                                {
                                    return cur;
                                }
                            }
                        }
                    }
                }

                return value;
            }
        }

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the minimum <see cref="Nullable{Float}"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to determine the minimum element of.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask sequence containing a single element with the value that corresponds to the minimum element in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<float?> MinAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<float?> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<float?>> selector, CancellationToken cancellationToken)
            {
                float? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = await selector(e.Current).ConfigureAwait(false);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    while (await e.MoveNextAsync())
                    {
                        var cur = await selector(e.Current).ConfigureAwait(false);
                        if (cur.HasValue)
                        {
                            var x = cur.GetValueOrDefault();
                            if (x < valueVal)
                            {
                                valueVal = x;
                                value = cur;
                            }
                            else
                            {
                                // Normally NaN < anything is false, as is anything < NaN
                                // However, this leads to some irksome outcomes in Min and Max.
                                // If we use those semantics then Min(NaN, 5.0) is NaN, but
                                // Min(5.0, NaN) is 5.0!  To fix this, we impose a total
                                // ordering where NaN is smaller than every value, including
                                // negative infinity.
                                // Not testing for NaN therefore isn't an option, but since we
                                // can't find a smaller value, we can short-circuit.

                                if (float.IsNaN(x))
                                {
                                    return cur;
                                }
                            }
                        }
                    }
                }

                return value;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<float?> MinAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<float?> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<float?>> selector, CancellationToken cancellationToken)
            {
                float? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    while (await e.MoveNextAsync())
                    {
                        var cur = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                        if (cur.HasValue)
                        {
                            var x = cur.GetValueOrDefault();
                            if (x < valueVal)
                            {
                                valueVal = x;
                                value = cur;
                            }
                            else
                            {
                                // Normally NaN < anything is false, as is anything < NaN
                                // However, this leads to some irksome outcomes in Min and Max.
                                // If we use those semantics then Min(NaN, 5.0) is NaN, but
                                // Min(5.0, NaN) is 5.0!  To fix this, we impose a total
                                // ordering where NaN is smaller than every value, including
                                // negative infinity.
                                // Not testing for NaN therefore isn't an option, but since we
                                // can't find a smaller value, we can short-circuit.

                                if (float.IsNaN(x))
                                {
                                    return cur;
                                }
                            }
                        }
                    }
                }

                return value;
            }
        }

#endif
        /// <summary>
        /// Returns the minimum value in an async-enumerable sequence of <see cref="double" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="double" /> values to determine the minimum value of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a single element with the minimum value in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ValueTask<double> MinAsync(this IAsyncEnumerable<double> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<double> Core(IAsyncEnumerable<double> source, CancellationToken cancellationToken)
            {
                double value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = e.Current;

                    while (await e.MoveNextAsync())
                    {
                        var x = e.Current;
                        if (x < value)
                        {
                            value = x;
                        }
                        else
                        {
                            // Normally NaN < anything is false, as is anything < NaN
                            // However, this leads to some irksome outcomes in Min and Max.
                            // If we use those semantics then Min(NaN, 5.0) is NaN, but
                            // Min(5.0, NaN) is 5.0!  To fix this, we impose a total
                            // ordering where NaN is smaller than every value, including
                            // negative infinity.
                            // Not testing for NaN therefore isn't an option, but since we
                            // can't find a smaller value, we can short-circuit.

                            if (double.IsNaN(x))
                            {
                                return x;
                            }
                        }
                    }
                }

                return value;
            }
        }

        public static ValueTask<double> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, double> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double> Core(IAsyncEnumerable<TSource> source, Func<TSource, double> selector, CancellationToken cancellationToken)
            {
                double value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = selector(e.Current);

                    while (await e.MoveNextAsync())
                    {
                        var x = selector(e.Current);
                        if (x < value)
                        {
                            value = x;
                        }
                        else
                        {
                            // Normally NaN < anything is false, as is anything < NaN
                            // However, this leads to some irksome outcomes in Min and Max.
                            // If we use those semantics then Min(NaN, 5.0) is NaN, but
                            // Min(5.0, NaN) is 5.0!  To fix this, we impose a total
                            // ordering where NaN is smaller than every value, including
                            // negative infinity.
                            // Not testing for NaN therefore isn't an option, but since we
                            // can't find a smaller value, we can short-circuit.

                            if (double.IsNaN(x))
                            {
                                return x;
                            }
                        }
                    }
                }

                return value;
            }
        }

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the minimum <see cref="double"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to determine the minimum element of.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask sequence containing a single element with the value that corresponds to the minimum element in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<double> MinAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double>> selector, CancellationToken cancellationToken)
            {
                double value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = await selector(e.Current).ConfigureAwait(false);

                    while (await e.MoveNextAsync())
                    {
                        var x = await selector(e.Current).ConfigureAwait(false);
                        if (x < value)
                        {
                            value = x;
                        }
                        else
                        {
                            // Normally NaN < anything is false, as is anything < NaN
                            // However, this leads to some irksome outcomes in Min and Max.
                            // If we use those semantics then Min(NaN, 5.0) is NaN, but
                            // Min(5.0, NaN) is 5.0!  To fix this, we impose a total
                            // ordering where NaN is smaller than every value, including
                            // negative infinity.
                            // Not testing for NaN therefore isn't an option, but since we
                            // can't find a smaller value, we can short-circuit.

                            if (double.IsNaN(x))
                            {
                                return x;
                            }
                        }
                    }
                }

                return value;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<double> MinAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double>> selector, CancellationToken cancellationToken)
            {
                double value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = await selector(e.Current, cancellationToken).ConfigureAwait(false);

                    while (await e.MoveNextAsync())
                    {
                        var x = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                        if (x < value)
                        {
                            value = x;
                        }
                        else
                        {
                            // Normally NaN < anything is false, as is anything < NaN
                            // However, this leads to some irksome outcomes in Min and Max.
                            // If we use those semantics then Min(NaN, 5.0) is NaN, but
                            // Min(5.0, NaN) is 5.0!  To fix this, we impose a total
                            // ordering where NaN is smaller than every value, including
                            // negative infinity.
                            // Not testing for NaN therefore isn't an option, but since we
                            // can't find a smaller value, we can short-circuit.

                            if (double.IsNaN(x))
                            {
                                return x;
                            }
                        }
                    }
                }

                return value;
            }
        }

#endif
        /// <summary>
        /// Returns the minimum value in an async-enumerable sequence of <see cref="Nullable{Double}" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="Nullable{Double}" /> values to determine the minimum value of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a single element with the minimum value in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ValueTask<double?> MinAsync(this IAsyncEnumerable<double?> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<double?> Core(IAsyncEnumerable<double?> source, CancellationToken cancellationToken)
            {
                double? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = e.Current;
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    while (await e.MoveNextAsync())
                    {
                        var cur = e.Current;
                        if (cur.HasValue)
                        {
                            var x = cur.GetValueOrDefault();
                            if (x < valueVal)
                            {
                                valueVal = x;
                                value = cur;
                            }
                            else
                            {
                                // Normally NaN < anything is false, as is anything < NaN
                                // However, this leads to some irksome outcomes in Min and Max.
                                // If we use those semantics then Min(NaN, 5.0) is NaN, but
                                // Min(5.0, NaN) is 5.0!  To fix this, we impose a total
                                // ordering where NaN is smaller than every value, including
                                // negative infinity.
                                // Not testing for NaN therefore isn't an option, but since we
                                // can't find a smaller value, we can short-circuit.

                                if (double.IsNaN(x))
                                {
                                    return cur;
                                }
                            }
                        }
                    }
                }

                return value;
            }
        }

        public static ValueTask<double?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, double?> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double?> Core(IAsyncEnumerable<TSource> source, Func<TSource, double?> selector, CancellationToken cancellationToken)
            {
                double? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = selector(e.Current);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    while (await e.MoveNextAsync())
                    {
                        var cur = selector(e.Current);
                        if (cur.HasValue)
                        {
                            var x = cur.GetValueOrDefault();
                            if (x < valueVal)
                            {
                                valueVal = x;
                                value = cur;
                            }
                            else
                            {
                                // Normally NaN < anything is false, as is anything < NaN
                                // However, this leads to some irksome outcomes in Min and Max.
                                // If we use those semantics then Min(NaN, 5.0) is NaN, but
                                // Min(5.0, NaN) is 5.0!  To fix this, we impose a total
                                // ordering where NaN is smaller than every value, including
                                // negative infinity.
                                // Not testing for NaN therefore isn't an option, but since we
                                // can't find a smaller value, we can short-circuit.

                                if (double.IsNaN(x))
                                {
                                    return cur;
                                }
                            }
                        }
                    }
                }

                return value;
            }
        }

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the minimum <see cref="Nullable{Double}"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to determine the minimum element of.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask sequence containing a single element with the value that corresponds to the minimum element in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<double?> MinAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double?> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<double?>> selector, CancellationToken cancellationToken)
            {
                double? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = await selector(e.Current).ConfigureAwait(false);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    while (await e.MoveNextAsync())
                    {
                        var cur = await selector(e.Current).ConfigureAwait(false);
                        if (cur.HasValue)
                        {
                            var x = cur.GetValueOrDefault();
                            if (x < valueVal)
                            {
                                valueVal = x;
                                value = cur;
                            }
                            else
                            {
                                // Normally NaN < anything is false, as is anything < NaN
                                // However, this leads to some irksome outcomes in Min and Max.
                                // If we use those semantics then Min(NaN, 5.0) is NaN, but
                                // Min(5.0, NaN) is 5.0!  To fix this, we impose a total
                                // ordering where NaN is smaller than every value, including
                                // negative infinity.
                                // Not testing for NaN therefore isn't an option, but since we
                                // can't find a smaller value, we can short-circuit.

                                if (double.IsNaN(x))
                                {
                                    return cur;
                                }
                            }
                        }
                    }
                }

                return value;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<double?> MinAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<double?> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<double?>> selector, CancellationToken cancellationToken)
            {
                double? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    while (await e.MoveNextAsync())
                    {
                        var cur = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                        if (cur.HasValue)
                        {
                            var x = cur.GetValueOrDefault();
                            if (x < valueVal)
                            {
                                valueVal = x;
                                value = cur;
                            }
                            else
                            {
                                // Normally NaN < anything is false, as is anything < NaN
                                // However, this leads to some irksome outcomes in Min and Max.
                                // If we use those semantics then Min(NaN, 5.0) is NaN, but
                                // Min(5.0, NaN) is 5.0!  To fix this, we impose a total
                                // ordering where NaN is smaller than every value, including
                                // negative infinity.
                                // Not testing for NaN therefore isn't an option, but since we
                                // can't find a smaller value, we can short-circuit.

                                if (double.IsNaN(x))
                                {
                                    return cur;
                                }
                            }
                        }
                    }
                }

                return value;
            }
        }

#endif
        /// <summary>
        /// Returns the minimum value in an async-enumerable sequence of <see cref="decimal" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="decimal" /> values to determine the minimum value of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a single element with the minimum value in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ValueTask<decimal> MinAsync(this IAsyncEnumerable<decimal> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<decimal> Core(IAsyncEnumerable<decimal> source, CancellationToken cancellationToken)
            {
                decimal value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = e.Current;

                    while (await e.MoveNextAsync())
                    {
                        var x = e.Current;
                        if (x < value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

        public static ValueTask<decimal> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, decimal> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<decimal> Core(IAsyncEnumerable<TSource> source, Func<TSource, decimal> selector, CancellationToken cancellationToken)
            {
                decimal value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = selector(e.Current);

                    while (await e.MoveNextAsync())
                    {
                        var x = selector(e.Current);
                        if (x < value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the minimum <see cref="decimal"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to determine the minimum element of.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask sequence containing a single element with the value that corresponds to the minimum element in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<decimal> MinAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<decimal> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal>> selector, CancellationToken cancellationToken)
            {
                decimal value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = await selector(e.Current).ConfigureAwait(false);

                    while (await e.MoveNextAsync())
                    {
                        var x = await selector(e.Current).ConfigureAwait(false);
                        if (x < value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

#if !NO_DEEP_CANCELLATION
[GenerateAsyncOverload]
        private static ValueTask<decimal> MinAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<decimal> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal>> selector, CancellationToken cancellationToken)
            {
                decimal value;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    if (!await e.MoveNextAsync())
                    {
                        throw Error.NoElements();
                    }

                    value = await selector(e.Current, cancellationToken).ConfigureAwait(false);

                    while (await e.MoveNextAsync())
                    {
                        var x = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                        if (x < value)
                        {
                            value = x;
                        }
                    }
                }

                return value;
            }
        }

#endif
        /// <summary>
        /// Returns the minimum value in an async-enumerable sequence of <see cref="Nullable{Decimal}" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="Nullable{Decimal}" /> values to determine the minimum value of.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask containing a single element with the minimum value in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ValueTask<decimal?> MinAsync(this IAsyncEnumerable<decimal?> source, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return Core(source, cancellationToken);

            static async ValueTask<decimal?> Core(IAsyncEnumerable<decimal?> source, CancellationToken cancellationToken)
            {
                decimal? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = e.Current;
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    while (await e.MoveNextAsync())
                    {
                        var cur = e.Current;
                        var x = cur.GetValueOrDefault();

                        if (cur.HasValue && x < valueVal)
                        {
                            valueVal = x;
                            value = cur;
                        }
                    }
                }

                return value;
            }
        }

        public static ValueTask<decimal?> MinAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, decimal?> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<decimal?> Core(IAsyncEnumerable<TSource> source, Func<TSource, decimal?> selector, CancellationToken cancellationToken)
            {
                decimal? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = selector(e.Current);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    while (await e.MoveNextAsync())
                    {
                        var cur = selector(e.Current);
                        var x = cur.GetValueOrDefault();

                        if (cur.HasValue && x < valueVal)
                        {
                            valueVal = x;
                            value = cur;
                        }
                    }
                }

                return value;
            }
        }

        /// <summary>
        /// Invokes and awaits a transform function on each element of a sequence and returns the minimum <see cref="Nullable{Decimal}"/> value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence to determine the minimum element of.</param>
        /// <param name="selector">An asynchronous transform function to invoke and await on each element.</param>
        /// <param name="cancellationToken">The optional cancellation token to be used for cancelling the sequence at any time.</param>
        /// <returns>A ValueTask sequence containing a single element with the value that corresponds to the minimum element in the source sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        /// <remarks>The return type of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.</remarks>
        [GenerateAsyncOverload]
        private static ValueTask<decimal?> MinAwaitAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<decimal?> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<decimal?>> selector, CancellationToken cancellationToken)
            {
                decimal? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = await selector(e.Current).ConfigureAwait(false);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    while (await e.MoveNextAsync())
                    {
                        var cur = await selector(e.Current).ConfigureAwait(false);
                        var x = cur.GetValueOrDefault();

                        if (cur.HasValue && x < valueVal)
                        {
                            valueVal = x;
                            value = cur;
                        }
                    }
                }

                return value;
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static ValueTask<decimal?> MinAwaitWithCancellationAsyncCore<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal?>> selector, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector, cancellationToken);

            static async ValueTask<decimal?> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<decimal?>> selector, CancellationToken cancellationToken)
            {
                decimal? value = null;

                await using (var e = source.GetConfiguredAsyncEnumerator(cancellationToken, false))
                {
                    // Start off knowing that we've a non-null value (or exit here, knowing we don't)
                    // so we don't have to keep testing for nullity.
                    do
                    {
                        if (!await e.MoveNextAsync())
                        {
                            return value;
                        }

                        value = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                    }
                    while (!value.HasValue);

                    // Keep hold of the wrapped value, and do comparisons on that, rather than
                    // using the lifted operation each time.
                    var valueVal = value.GetValueOrDefault();

                    while (await e.MoveNextAsync())
                    {
                        var cur = await selector(e.Current, cancellationToken).ConfigureAwait(false);
                        var x = cur.GetValueOrDefault();

                        if (cur.HasValue && x < valueVal)
                        {
                            valueVal = x;
                            value = cur;
                        }
                    }
                }

                return value;
            }
        }

#endif
    }
}
