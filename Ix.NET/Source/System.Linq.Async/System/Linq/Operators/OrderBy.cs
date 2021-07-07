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
        /// Sorts the elements of a sequence in ascending order according to a key.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <returns>An ordered async-enumerable sequence whose elements are sorted according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        public static IOrderedAsyncEnumerable<TSource> OrderBy<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector) =>
            new OrderedAsyncEnumerable<TSource, TKey>(source, keySelector, comparer: null, descending: false, parent: null);

        /// <summary>
        /// Sorts the elements of a sequence in ascending order according to a key obtained by invoking a transform function on each element and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to order.</param>
        /// <param name="keySelector">An asynchronous function to extract a key from an element.</param>
        /// <returns>An ordered async-enumerable sequence whose elements are sorted according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        [GenerateAsyncOverload]
        private static IOrderedAsyncEnumerable<TSource> OrderByAwaitCore<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector) =>
            new OrderedAsyncEnumerableWithTask<TSource, TKey>(source, keySelector, comparer: null, descending: false, parent: null);

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static IOrderedAsyncEnumerable<TSource> OrderByAwaitWithCancellationCore<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector) =>
            new OrderedAsyncEnumerableWithTaskAndCancellation<TSource, TKey>(source, keySelector, comparer: null, descending: false, parent: null);
#endif

        /// <summary>
        /// Sorts the elements of a sequence in ascending order by using a specified comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <param name="comparer">A comparer to compare keys.</param>
        /// <returns>An ordered async-enumerable sequence whose elements are sorted according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        public static IOrderedAsyncEnumerable<TSource> OrderBy<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer) =>
            new OrderedAsyncEnumerable<TSource, TKey>(source, keySelector, comparer, descending: false, parent: null);

        /// <summary>
        /// Sorts the elements of a sequence in ascending order by using a specified comparer. The keys are obtained by invoking the transform function on each element and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to order.</param>
        /// <param name="keySelector">An asynchronous function to extract a key from an element.</param>
        /// <param name="comparer">A comparer to compare keys.</param>
        /// <returns>An ordered async-enumerable sequence whose elements are sorted according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        [GenerateAsyncOverload]
        private static IOrderedAsyncEnumerable<TSource> OrderByAwaitCore<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IComparer<TKey> comparer) =>
            new OrderedAsyncEnumerableWithTask<TSource, TKey>(source, keySelector, comparer, descending: false, parent: null);

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static IOrderedAsyncEnumerable<TSource> OrderByAwaitWithCancellationCore<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, IComparer<TKey> comparer) =>
            new OrderedAsyncEnumerableWithTaskAndCancellation<TSource, TKey>(source, keySelector, comparer, descending: false, parent: null);
#endif

        /// <summary>
        /// Sorts the elements of a sequence in descending order according to a key.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <returns>An ordered async-enumerable sequence whose elements are sorted in descending order according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        public static IOrderedAsyncEnumerable<TSource> OrderByDescending<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector) =>
            new OrderedAsyncEnumerable<TSource, TKey>(source, keySelector, comparer: null, descending: true, parent: null);

        /// <summary>
        /// Sorts the elements of a sequence in descending order according to a key obtained by invoking a transform function on each element and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to order.</param>
        /// <param name="keySelector">An asynchronous function to extract a key from an element.</param>
        /// <returns>An ordered async-enumerable sequence whose elements are sorted in descending order according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        [GenerateAsyncOverload]
        private static IOrderedAsyncEnumerable<TSource> OrderByDescendingAwaitCore<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector) =>
            new OrderedAsyncEnumerableWithTask<TSource, TKey>(source, keySelector, comparer: null, descending: true, parent: null);

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static IOrderedAsyncEnumerable<TSource> OrderByDescendingAwaitWithCancellationCore<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector) =>
            new OrderedAsyncEnumerableWithTaskAndCancellation<TSource, TKey>(source, keySelector, comparer: null, descending: true, parent: null);
#endif

        /// <summary>
        /// Sorts the elements of a sequence in descending order by using a specified comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <param name="comparer">A comparer to compare keys.</param>
        /// <returns>An ordered async-enumerable sequence whose elements are sorted in descending order according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        public static IOrderedAsyncEnumerable<TSource> OrderByDescending<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer) =>
            new OrderedAsyncEnumerable<TSource, TKey>(source, keySelector, comparer, descending: true, parent: null);

        /// <summary>
        /// Sorts the elements of a sequence in descending order by using a specified comparer. The keys are obtained by invoking the transform function on each element and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An async-enumerable sequence of values to order.</param>
        /// <param name="keySelector">An asynchronous function to extract a key from an element.</param>
        /// <param name="comparer">A comparer to compare keys.</param>
        /// <returns>An ordered async-enumerable sequence whose elements are sorted in descending order according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        [GenerateAsyncOverload]
        private static IOrderedAsyncEnumerable<TSource> OrderByDescendingAwaitCore<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IComparer<TKey> comparer) =>
            new OrderedAsyncEnumerableWithTask<TSource, TKey>(source, keySelector, comparer, descending: true, parent: null);

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static IOrderedAsyncEnumerable<TSource> OrderByDescendingAwaitWithCancellationCore<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, IComparer<TKey> comparer) =>
            new OrderedAsyncEnumerableWithTaskAndCancellation<TSource, TKey>(source, keySelector, comparer, descending: true, parent: null);
#endif

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in ascending order according to a key.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An ordered async-enumerable sequence that contains elements to sort.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <returns>An ordered async-enumerable whose elements are sorted according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        public static IOrderedAsyncEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return source.CreateOrderedEnumerable(keySelector, comparer: null, descending: false);
        }

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in ascending order according to a key obtained by invoking a transform function on each element and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An ordered async-enumerable sequence that contains elements to sort.</param>
        /// <param name="keySelector">An asynchronous function to extract a key from each element.</param>
        /// <returns>An ordered async-enumerable whose elements are sorted according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        [GenerateAsyncOverload]
        private static IOrderedAsyncEnumerable<TSource> ThenByAwaitCore<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return source.CreateOrderedEnumerable(keySelector, comparer: default(IComparer<TKey>), descending: false);
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static IOrderedAsyncEnumerable<TSource> ThenByAwaitWithCancellationCore<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return source.CreateOrderedEnumerable(keySelector, comparer: null, descending: false);
        }
#endif

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in ascending order by using a specified comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An ordered async-enumerable sequence that contains elements to sort.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="comparer">A comparer to compare keys.</param>
        /// <returns>An ordered async-enumerable whose elements are sorted according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        public static IOrderedAsyncEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return source.CreateOrderedEnumerable(keySelector, comparer, descending: false);
        }

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in ascending order by using a specified comparer. The keys are obtained by invoking a transform function on each element and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An ordered async-enumerable sequence that contains elements to sort.</param>
        /// <param name="keySelector">An asynchronous function to extract a key from each element.</param>
        /// <param name="comparer">A comparer to compare keys.</param>
        /// <returns>An ordered async-enumerable whose elements are sorted according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        [GenerateAsyncOverload]
        private static IOrderedAsyncEnumerable<TSource> ThenByAwaitCore<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return source.CreateOrderedEnumerable(keySelector, comparer, descending: false);
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static IOrderedAsyncEnumerable<TSource> ThenByAwaitWithCancellationCore<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return source.CreateOrderedEnumerable(keySelector, comparer, descending: false);
        }
#endif

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in descending order, according to a key.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An ordered async-enumerable sequence that contains elements to sort.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <returns>An ordered async-enumerable sequence whose elements are sorted in descending order according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        public static IOrderedAsyncEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return source.CreateOrderedEnumerable(keySelector, comparer: null, descending: true);
        }

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in descending order, according to a key obtained by invoking a transform function on each element and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An ordered async-enumerable sequence that contains elements to sort.</param>
        /// <param name="keySelector">An asynchronous function to extract a key from each element.</param>
        /// <returns>An ordered async-enumerable sequence whose elements are sorted in descending order according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        [GenerateAsyncOverload]
        private static IOrderedAsyncEnumerable<TSource> ThenByDescendingAwaitCore<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return source.CreateOrderedEnumerable(keySelector, comparer: default(IComparer<TKey>), descending: true);
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static IOrderedAsyncEnumerable<TSource> ThenByDescendingAwaitWithCancellationCore<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return source.CreateOrderedEnumerable(keySelector, comparer: null, descending: true);
        }
#endif

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in descending order by using a specified comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An ordered async-enumerable sequence that contains elements to sort.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="comparer">A comparer to compare keys.</param>
        /// <returns>An ordered async-enumerable sequence whose elements are sorted in descending order according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        public static IOrderedAsyncEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return source.CreateOrderedEnumerable(keySelector, comparer, descending: true);
        }

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in descending order by using a specified comparer. The keys are obtained by invoking a transform function on each element and awaiting the result.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An ordered async-enumerable sequence that contains elements to sort.</param>
        /// <param name="keySelector">An asynchronous function to extract a key from each element.</param>
        /// <param name="comparer">A comparer to compare keys.</param>
        /// <returns>An ordered async-enumerable sequence whose elements are sorted in descending order according to a key.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        [GenerateAsyncOverload]
        private static IOrderedAsyncEnumerable<TSource> ThenByDescendingAwaitCore<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return source.CreateOrderedEnumerable(keySelector, comparer, descending: true);
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static IOrderedAsyncEnumerable<TSource> ThenByDescendingAwaitWithCancellationCore<TSource, TKey>(this IOrderedAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, IComparer<TKey> comparer)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));

            return source.CreateOrderedEnumerable(keySelector, comparer, descending: true);
        }
#endif
    }
}
