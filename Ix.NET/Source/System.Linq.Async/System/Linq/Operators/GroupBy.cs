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
        /// Groups the elements of an async-enumerable sequence according to a specified key selector function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the grouping key computed for each element in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <returns>A sequence of async-enumerable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is null.</exception>
        public static IAsyncEnumerable<IAsyncGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector) =>
            new GroupedAsyncEnumerable<TSource, TKey>(source, keySelector, comparer: null);

        /// <summary>
        /// Groups the elements of an async-enumerable sequence according to a specified key selector function and comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the grouping key computed for each element in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <param name="comparer">An equality comparer to compare keys with.</param>
        /// <returns>A sequence of async-enumerable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> or <paramref name="comparer"/> is null.</exception>
        public static IAsyncEnumerable<IAsyncGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer) =>
            new GroupedAsyncEnumerable<TSource, TKey>(source, keySelector, comparer);

        /// <summary>
        /// Groups the elements of an async-enumerable sequence according to a specified key selector function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the grouping key computed for each element in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence whose elements to group.</param>
        /// <param name="keySelector">An asynchronous function to extract the key for each element.</param>
        /// <returns>A sequence of async-enumerable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<IAsyncGrouping<TKey, TSource>> GroupByAwaitCore<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector) =>
            new GroupedAsyncEnumerableWithTask<TSource, TKey>(source, keySelector, comparer: null);

        /// <summary>
        /// Groups the elements of an async-enumerable sequence according to a specified key selector function and comparer.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the grouping key computed for each element in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence whose elements to group.</param>
        /// <param name="keySelector">An asynchronous function to extract the key for each element.</param>
        /// <param name="comparer">An equality comparer to compare keys with.</param>
        /// <returns>A sequence of async-enumerable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> or <paramref name="comparer"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<IAsyncGrouping<TKey, TSource>> GroupByAwaitCore<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IEqualityComparer<TKey>? comparer) =>
            new GroupedAsyncEnumerableWithTask<TSource, TKey>(source, keySelector, comparer);

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<IAsyncGrouping<TKey, TSource>> GroupByAwaitWithCancellationCore<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector) =>
            new GroupedAsyncEnumerableWithTaskAndCancellation<TSource, TKey>(source, keySelector, comparer: null);

        [GenerateAsyncOverload]
        private static IAsyncEnumerable<IAsyncGrouping<TKey, TSource>> GroupByAwaitWithCancellationCore<TSource, TKey>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, IEqualityComparer<TKey>? comparer) =>
            new GroupedAsyncEnumerableWithTaskAndCancellation<TSource, TKey>(source, keySelector, comparer);
#endif

        /// <summary>
        /// Groups the elements of an async-enumerable sequence and selects the resulting elements by using a specified function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the grouping key computed for each element in the source sequence.</typeparam>
        /// <typeparam name="TElement">The type of the elements within the groups computed for each element in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <param name="elementSelector">A function to map each source element to an element in an async-enumerable group.</param>
        /// <returns>A sequence of async-enumerable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> or <paramref name="elementSelector"/> is null.</exception>
        public static IAsyncEnumerable<IAsyncGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector) =>
            new GroupedAsyncEnumerable<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer: null);

        /// <summary>
        /// Groups the elements of an async-enumerable sequence according to a specified key selector function and comparer and selects the resulting elements by using a specified function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the grouping key computed for each element in the source sequence.</typeparam>
        /// <typeparam name="TElement">The type of the elements within the groups computed for each element in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <param name="elementSelector">A function to map each source element to an element in an async-enumerable group.</param>
        /// <param name="comparer">An equality comparer to compare keys with.</param>
        /// <returns>A sequence of async-enumerable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> or <paramref name="elementSelector"/> or <paramref name="comparer"/> is null.</exception>
        public static IAsyncEnumerable<IAsyncGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey>? comparer) =>
            new GroupedAsyncEnumerable<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer);

        /// <summary>
        /// Groups the elements of an async-enumerable sequence and selects the resulting elements by using a specified function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the grouping key computed for each element in the source sequence.</typeparam>
        /// <typeparam name="TElement">The type of the elements within the groups computed for each element in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence whose elements to group.</param>
        /// <param name="keySelector">An asynchronous function to extract the key for each element.</param>
        /// <param name="elementSelector">An asynchronous function to map each source element to an element in an async-enumerable group.</param>
        /// <returns>A sequence of async-enumerable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> or <paramref name="elementSelector"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<IAsyncGrouping<TKey, TElement>> GroupByAwaitCore<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TElement>> elementSelector) =>
            new GroupedAsyncEnumerableWithTask<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer: null);

        /// <summary>
        /// Groups the elements of an async-enumerable sequence and selects the resulting elements by using a specified function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the grouping key computed for each element in the source sequence.</typeparam>
        /// <typeparam name="TElement">The type of the elements within the groups computed for each element in the source sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence whose elements to group.</param>
        /// <param name="keySelector">An asynchronous function to extract the key for each element.</param>
        /// <param name="elementSelector">An asynchronous function to map each source element to an element in an async-enumerable group.</param>
        /// <param name="comparer">An equality comparer to use to compare keys.</param>
        /// <returns>A sequence of async-enumerable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> or <paramref name="elementSelector"/> or <paramref name="comparer"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<IAsyncGrouping<TKey, TElement>> GroupByAwaitCore<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TElement>> elementSelector, IEqualityComparer<TKey>? comparer) =>
            new GroupedAsyncEnumerableWithTask<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer);

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<IAsyncGrouping<TKey, TElement>> GroupByAwaitWithCancellationCore<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TElement>> elementSelector) =>
            new GroupedAsyncEnumerableWithTaskAndCancellation<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer: null);

        [GenerateAsyncOverload]
        private static IAsyncEnumerable<IAsyncGrouping<TKey, TElement>> GroupByAwaitWithCancellationCore<TSource, TKey, TElement>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TElement>> elementSelector, IEqualityComparer<TKey>? comparer) =>
            new GroupedAsyncEnumerableWithTaskAndCancellation<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer);
#endif

        public static IAsyncEnumerable<TResult> GroupBy<TSource, TKey, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IAsyncEnumerable<TSource>, TResult> resultSelector) =>
            new GroupedResultAsyncEnumerable<TSource, TKey, TResult>(source, keySelector, resultSelector, comparer: null);

        public static IAsyncEnumerable<TResult> GroupBy<TSource, TKey, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IAsyncEnumerable<TSource>, TResult> resultSelector, IEqualityComparer<TKey>? comparer) =>
            new GroupedResultAsyncEnumerable<TSource, TKey, TResult>(source, keySelector, resultSelector, comparer);

        /// <summary>
        /// Groups the elements of an async-enumerable sequence according to a specified key selector function, and then applies a result selector function to each group.
        /// </summary>
        /// <typeparam name="TSource">Type of element in the source sequence.</typeparam>
        /// <typeparam name="TKey">Type of the grouping key computed for each element in the source sequence.</typeparam>
        /// <typeparam name="TResult">The result type returned by the result selector function.</typeparam>
        /// <param name="source">An async-enumerable sequence whose elements to group.</param>
        /// <param name="keySelector">An asynchronous function to extract the key for each element.</param>
        /// <param name="resultSelector">An asynchronous function to transform each group into the result type.</param>
        /// <returns>An async-enumerable sequence of results obtained by invoking and awaiting the result-selector function on each group.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> or <paramref name="resultSelector"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<TResult> GroupByAwaitCore<TSource, TKey, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TKey, IAsyncEnumerable<TSource>, ValueTask<TResult>> resultSelector) =>
            new GroupedResultAsyncEnumerableWithTask<TSource, TKey, TResult>(source, keySelector, resultSelector, comparer: null);

        /// <summary>
        /// Groups the elements of an async-enumerable sequence according to a specified key selector function, and then applies a result selector function to each group.
        /// </summary>
        /// <typeparam name="TSource">Type of element in the source sequence.</typeparam>
        /// <typeparam name="TKey">Type of the grouping key computed for each element in the source sequence.</typeparam>
        /// <typeparam name="TResult">The result type returned by the result selector function.</typeparam>
        /// <param name="source">An async-enumerable sequence whose elements to group.</param>
        /// <param name="keySelector">An asynchronous function to extract the key for each element.</param>
        /// <param name="resultSelector">An asynchronous function to transform each group into the result type.</param>
        /// <param name="comparer">An equality comparer to use to compare keys.</param>
        /// <returns>An async-enumerable sequence of results obtained by invoking and awaiting the result-selector function on each group.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> or <paramref name="resultSelector"/> or <paramref name="comparer"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<TResult> GroupByAwaitCore<TSource, TKey, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TKey, IAsyncEnumerable<TSource>, ValueTask<TResult>> resultSelector, IEqualityComparer<TKey>? comparer) =>
            new GroupedResultAsyncEnumerableWithTask<TSource, TKey, TResult>(source, keySelector, resultSelector, comparer);

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<TResult> GroupByAwaitWithCancellationCore<TSource, TKey, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TKey, IAsyncEnumerable<TSource>, CancellationToken, ValueTask<TResult>> resultSelector) =>
            new GroupedResultAsyncEnumerableWithTaskAndCancellation<TSource, TKey, TResult>(source, keySelector, resultSelector, comparer: null);

        [GenerateAsyncOverload]
        private static IAsyncEnumerable<TResult> GroupByAwaitWithCancellationCore<TSource, TKey, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TKey, IAsyncEnumerable<TSource>, CancellationToken, ValueTask<TResult>> resultSelector, IEqualityComparer<TKey>? comparer) =>
            new GroupedResultAsyncEnumerableWithTaskAndCancellation<TSource, TKey, TResult>(source, keySelector, resultSelector, comparer);
#endif

        public static IAsyncEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IAsyncEnumerable<TElement>, TResult> resultSelector) =>
            new GroupedResultAsyncEnumerable<TSource, TKey, TElement, TResult>(source, keySelector, elementSelector, resultSelector, comparer: null);

        public static IAsyncEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IAsyncEnumerable<TElement>, TResult> resultSelector, IEqualityComparer<TKey>? comparer) =>
            new GroupedResultAsyncEnumerable<TSource, TKey, TElement, TResult>(source, keySelector, elementSelector, resultSelector, comparer);

        /// <summary>
        /// Groups the elements of an async-enumerable sequence according to a specified key-selector function, applies an element selector to each element of each group, then applies a result selector to each transformed group.
        /// </summary>
        /// <typeparam name="TSource">The type of element in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the grouping key computed for each element in the source sequence.</typeparam>
        /// <typeparam name="TElement">The type of element computed by the element selector.</typeparam>
        /// <typeparam name="TResult">The type of the final result, computed by applying the result selector to each transformed group of elements.</typeparam>
        /// <param name="source">An async-enumerable sequence whose elements to group.</param>
        /// <param name="keySelector">An asynchronous function to extract the key for each element.</param>
        /// <param name="elementSelector">An asynchronous function to apply to each element of each group. </param>
        /// <param name="resultSelector">An asynchronous function to transform each group into the result type.</param>
        /// <returns>An async-enumerable sequence of results obtained by invoking the result selector function on each group and awaiting the result.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> or <paramref name="elementSelector"/> or <paramref name="resultSelector"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<TResult> GroupByAwaitCore<TSource, TKey, TElement, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TElement>> elementSelector, Func<TKey, IAsyncEnumerable<TElement>, ValueTask<TResult>> resultSelector) =>
            new GroupedResultAsyncEnumerableWithTask<TSource, TKey, TElement, TResult>(source, keySelector, elementSelector, resultSelector, comparer: null);

        /// <summary>
        /// Groups the elements of an async-enumerable sequence according to a specified key-selector function, applies an element selector to each element of each group, then applies a result selector to each transformed group.
        /// </summary>
        /// <typeparam name="TSource">The type of element in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the grouping key computed for each element in the source sequence.</typeparam>
        /// <typeparam name="TElement">The type of element computed by the element selector.</typeparam>
        /// <typeparam name="TResult">The type of the final result, computed by applying the result selector to each transformed group of elements.</typeparam>
        /// <param name="source">An async-enumerable sequence whose elements to group.</param>
        /// <param name="keySelector">An asynchronous function to extract the key for each element.</param>
        /// <param name="elementSelector">An asynchronous function to apply to each element of each group. </param>
        /// <param name="resultSelector">An asynchronous function to transform each group into the result type.</param>
        /// <param name="comparer">An equality comparer to use to compare keys.</param>
        /// <returns>An async-enumerable sequence of results obtained by invoking the result selector function on each group and awaiting the result.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="keySelector"/> or <paramref name="elementSelector"/> or <paramref name="resultSelector"/> or <paramref name="comparer"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<TResult> GroupByAwaitCore<TSource, TKey, TElement, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TElement>> elementSelector, Func<TKey, IAsyncEnumerable<TElement>, ValueTask<TResult>> resultSelector, IEqualityComparer<TKey>? comparer) =>
            new GroupedResultAsyncEnumerableWithTask<TSource, TKey, TElement, TResult>(source, keySelector, elementSelector, resultSelector, comparer);

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<TResult> GroupByAwaitWithCancellationCore<TSource, TKey, TElement, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TElement>> elementSelector, Func<TKey, IAsyncEnumerable<TElement>, CancellationToken, ValueTask<TResult>> resultSelector) =>
            new GroupedResultAsyncEnumerableWithTaskAndCancellation<TSource, TKey, TElement, TResult>(source, keySelector, elementSelector, resultSelector, comparer: null);

        [GenerateAsyncOverload]
        private static IAsyncEnumerable<TResult> GroupByAwaitWithCancellationCore<TSource, TKey, TElement, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TElement>> elementSelector, Func<TKey, IAsyncEnumerable<TElement>, CancellationToken, ValueTask<TResult>> resultSelector, IEqualityComparer<TKey>? comparer) =>
            new GroupedResultAsyncEnumerableWithTaskAndCancellation<TSource, TKey, TElement, TResult>(source, keySelector, elementSelector, resultSelector, comparer);
#endif

        private sealed class GroupedResultAsyncEnumerable<TSource, TKey, TResult> : AsyncIterator<TResult>, IAsyncIListProvider<TResult>
        {
            private readonly IAsyncEnumerable<TSource> _source;
            private readonly Func<TSource, TKey> _keySelector;
            private readonly Func<TKey, IAsyncEnumerable<TSource>, TResult> _resultSelector;
            private readonly IEqualityComparer<TKey>? _comparer;

            private Internal.Lookup<TKey, TSource>? _lookup;
            private IEnumerator<TResult>? _enumerator;

            public GroupedResultAsyncEnumerable(IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IAsyncEnumerable<TSource>, TResult> resultSelector, IEqualityComparer<TKey>? comparer)
            {
                _source = source ?? throw Error.ArgumentNull(nameof(source));
                _keySelector = keySelector ?? throw Error.ArgumentNull(nameof(keySelector));
                _resultSelector = resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector));
                _comparer = comparer;
            }

            public override AsyncIteratorBase<TResult> Clone()
            {
                return new GroupedResultAsyncEnumerable<TSource, TKey, TResult>(_source, _keySelector, _resultSelector, _comparer);
            }

            public override async ValueTask DisposeAsync()
            {
                if (_enumerator != null)
                {
                    _enumerator.Dispose();
                    _enumerator = null;
                    _lookup = null;
                }

                await base.DisposeAsync().ConfigureAwait(false);
            }

            protected override async ValueTask<bool> MoveNextCore()
            {
                switch (_state)
                {
                    case AsyncIteratorState.Allocated:
                        _lookup = await Internal.Lookup<TKey, TSource>.CreateAsync(_source, _keySelector, _comparer, _cancellationToken).ConfigureAwait(false);
                        _enumerator = _lookup.ApplyResultSelector(_resultSelector).GetEnumerator();
                        _state = AsyncIteratorState.Iterating;
                        goto case AsyncIteratorState.Iterating;

                    case AsyncIteratorState.Iterating:
                        if (_enumerator!.MoveNext())
                        {
                            _current = _enumerator.Current;
                            return true;
                        }

                        await DisposeAsync().ConfigureAwait(false);
                        break;
                }

                return false;
            }

            public async ValueTask<TResult[]> ToArrayAsync(CancellationToken cancellationToken)
            {
                var l = await Internal.Lookup<TKey, TSource>.CreateAsync(_source, _keySelector, _comparer, cancellationToken).ConfigureAwait(false);
                return l.ToArray(_resultSelector);
            }

            public async ValueTask<List<TResult>> ToListAsync(CancellationToken cancellationToken)
            {
                var l = await Internal.Lookup<TKey, TSource>.CreateAsync(_source, _keySelector, _comparer, cancellationToken).ConfigureAwait(false);
                return l.ToList(_resultSelector);
            }

            public ValueTask<int> GetCountAsync(bool onlyIfCheap, CancellationToken cancellationToken)
            {
                if (onlyIfCheap)
                {
                    return new ValueTask<int>(-1);
                }

                return Core();

                async ValueTask<int> Core()
                {
                    var l = await Internal.Lookup<TKey, TSource>.CreateAsync(_source, _keySelector, _comparer, cancellationToken).ConfigureAwait(false);

                    return l.Count;
                }
            }
        }

        private sealed class GroupedResultAsyncEnumerableWithTask<TSource, TKey, TResult> : AsyncIterator<TResult>, IAsyncIListProvider<TResult>
        {
            private readonly IAsyncEnumerable<TSource> _source;
            private readonly Func<TSource, ValueTask<TKey>> _keySelector;
            private readonly Func<TKey, IAsyncEnumerable<TSource>, ValueTask<TResult>> _resultSelector;
            private readonly IEqualityComparer<TKey>? _comparer;

            private Internal.LookupWithTask<TKey, TSource>? _lookup;
            private IAsyncEnumerator<TResult>? _enumerator;

            public GroupedResultAsyncEnumerableWithTask(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TKey, IAsyncEnumerable<TSource>, ValueTask<TResult>> resultSelector, IEqualityComparer<TKey>? comparer)
            {
                _source = source ?? throw Error.ArgumentNull(nameof(source));
                _keySelector = keySelector ?? throw Error.ArgumentNull(nameof(keySelector));
                _resultSelector = resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector));
                _comparer = comparer;
            }

            public override AsyncIteratorBase<TResult> Clone()
            {
                return new GroupedResultAsyncEnumerableWithTask<TSource, TKey, TResult>(_source, _keySelector, _resultSelector, _comparer);
            }

            public override async ValueTask DisposeAsync()
            {
                if (_enumerator != null)
                {
                    await _enumerator.DisposeAsync().ConfigureAwait(false);
                    _enumerator = null;
                    _lookup = null;
                }

                await base.DisposeAsync().ConfigureAwait(false);
            }

            protected override async ValueTask<bool> MoveNextCore()
            {
                switch (_state)
                {
                    case AsyncIteratorState.Allocated:
                        _lookup = await Internal.LookupWithTask<TKey, TSource>.CreateAsync(_source, _keySelector, _comparer, _cancellationToken).ConfigureAwait(false);
                        _enumerator = _lookup.SelectAwaitCore(async g => await _resultSelector(g.Key, g).ConfigureAwait(false)).GetAsyncEnumerator(_cancellationToken); // REVIEW: Introduce another ApplyResultSelector?
                        _state = AsyncIteratorState.Iterating;
                        goto case AsyncIteratorState.Iterating;

                    case AsyncIteratorState.Iterating:
                        if (await _enumerator!.MoveNextAsync().ConfigureAwait(false))
                        {
                            _current = _enumerator.Current;
                            return true;
                        }

                        await DisposeAsync().ConfigureAwait(false);
                        break;
                }

                return false;
            }

            public async ValueTask<TResult[]> ToArrayAsync(CancellationToken cancellationToken)
            {
                var l = await Internal.LookupWithTask<TKey, TSource>.CreateAsync(_source, _keySelector, _comparer, cancellationToken).ConfigureAwait(false);
                return await l.ToArray(_resultSelector).ConfigureAwait(false);
            }

            public async ValueTask<List<TResult>> ToListAsync(CancellationToken cancellationToken)
            {
                var l = await Internal.LookupWithTask<TKey, TSource>.CreateAsync(_source, _keySelector, _comparer, cancellationToken).ConfigureAwait(false);
                return await l.ToList(_resultSelector).ConfigureAwait(false);
            }

            public ValueTask<int> GetCountAsync(bool onlyIfCheap, CancellationToken cancellationToken)
            {
                if (onlyIfCheap)
                {
                    return new ValueTask<int>(-1);
                }

                return Core();

                async ValueTask<int> Core()
                {
                    var l = await Internal.LookupWithTask<TKey, TSource>.CreateAsync(_source, _keySelector, _comparer, cancellationToken).ConfigureAwait(false);

                    return l.Count;
                }
            }
        }

#if !NO_DEEP_CANCELLATION
        private sealed class GroupedResultAsyncEnumerableWithTaskAndCancellation<TSource, TKey, TResult> : AsyncIterator<TResult>, IAsyncIListProvider<TResult>
        {
            private readonly IAsyncEnumerable<TSource> _source;
            private readonly Func<TSource, CancellationToken, ValueTask<TKey>> _keySelector;
            private readonly Func<TKey, IAsyncEnumerable<TSource>, CancellationToken, ValueTask<TResult>> _resultSelector;
            private readonly IEqualityComparer<TKey>? _comparer;

            private Internal.LookupWithTask<TKey, TSource>? _lookup;
            private IAsyncEnumerator<TResult>? _enumerator;

            public GroupedResultAsyncEnumerableWithTaskAndCancellation(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TKey, IAsyncEnumerable<TSource>, CancellationToken, ValueTask<TResult>> resultSelector, IEqualityComparer<TKey>? comparer)
            {
                _source = source ?? throw Error.ArgumentNull(nameof(source));
                _keySelector = keySelector ?? throw Error.ArgumentNull(nameof(keySelector));
                _resultSelector = resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector));
                _comparer = comparer;
            }

            public override AsyncIteratorBase<TResult> Clone()
            {
                return new GroupedResultAsyncEnumerableWithTaskAndCancellation<TSource, TKey, TResult>(_source, _keySelector, _resultSelector, _comparer);
            }

            public override async ValueTask DisposeAsync()
            {
                if (_enumerator != null)
                {
                    await _enumerator.DisposeAsync().ConfigureAwait(false);
                    _enumerator = null;
                    _lookup = null;
                }

                await base.DisposeAsync().ConfigureAwait(false);
            }

            protected override async ValueTask<bool> MoveNextCore()
            {
                switch (_state)
                {
                    case AsyncIteratorState.Allocated:
                        _lookup = await Internal.LookupWithTask<TKey, TSource>.CreateAsync(_source, _keySelector, _comparer, _cancellationToken).ConfigureAwait(false);
                        _enumerator = _lookup.SelectAwaitCore(async g => await _resultSelector(g.Key, g, _cancellationToken).ConfigureAwait(false)).GetAsyncEnumerator(_cancellationToken); // REVIEW: Introduce another ApplyResultSelector?
                        _state = AsyncIteratorState.Iterating;
                        goto case AsyncIteratorState.Iterating;

                    case AsyncIteratorState.Iterating:
                        if (await _enumerator!.MoveNextAsync().ConfigureAwait(false))
                        {
                            _current = _enumerator.Current;
                            return true;
                        }

                        await DisposeAsync().ConfigureAwait(false);
                        break;
                }

                return false;
            }

            public async ValueTask<TResult[]> ToArrayAsync(CancellationToken cancellationToken)
            {
                var l = await Internal.LookupWithTask<TKey, TSource>.CreateAsync(_source, _keySelector, _comparer, cancellationToken).ConfigureAwait(false);
                return await l.ToArray(_resultSelector, cancellationToken).ConfigureAwait(false);
            }

            public async ValueTask<List<TResult>> ToListAsync(CancellationToken cancellationToken)
            {
                var l = await Internal.LookupWithTask<TKey, TSource>.CreateAsync(_source, _keySelector, _comparer, cancellationToken).ConfigureAwait(false);
                return await l.ToList(_resultSelector, cancellationToken).ConfigureAwait(false);
            }

            public ValueTask<int> GetCountAsync(bool onlyIfCheap, CancellationToken cancellationToken)
            {
                if (onlyIfCheap)
                {
                    return new ValueTask<int>(-1);
                }

                return Core();

                async ValueTask<int> Core()
                {
                    var l = await Internal.LookupWithTask<TKey, TSource>.CreateAsync(_source, _keySelector, _comparer, cancellationToken).ConfigureAwait(false);

                    return l.Count;
                }
            }
        }
#endif

        private sealed class GroupedResultAsyncEnumerable<TSource, TKey, TElement, TResult> : AsyncIterator<TResult>, IAsyncIListProvider<TResult>
        {
            private readonly IAsyncEnumerable<TSource> _source;
            private readonly Func<TSource, TKey> _keySelector;
            private readonly Func<TSource, TElement> _elementSelector;
            private readonly Func<TKey, IAsyncEnumerable<TElement>, TResult> _resultSelector;
            private readonly IEqualityComparer<TKey>? _comparer;

            private Internal.Lookup<TKey, TElement>? _lookup;
            private IEnumerator<TResult>? _enumerator;

            public GroupedResultAsyncEnumerable(IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IAsyncEnumerable<TElement>, TResult> resultSelector, IEqualityComparer<TKey>? comparer)
            {
                _source = source ?? throw Error.ArgumentNull(nameof(source));
                _keySelector = keySelector ?? throw Error.ArgumentNull(nameof(keySelector));
                _elementSelector = elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector));
                _resultSelector = resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector));
                _comparer = comparer;
            }

            public override AsyncIteratorBase<TResult> Clone()
            {
                return new GroupedResultAsyncEnumerable<TSource, TKey, TElement, TResult>(_source, _keySelector, _elementSelector, _resultSelector, _comparer);
            }

            public override async ValueTask DisposeAsync()
            {
                if (_enumerator != null)
                {
                    _enumerator.Dispose();
                    _enumerator = null;
                    _lookup = null;
                }

                await base.DisposeAsync().ConfigureAwait(false);
            }

            protected override async ValueTask<bool> MoveNextCore()
            {
                switch (_state)
                {
                    case AsyncIteratorState.Allocated:
                        _lookup = await Internal.Lookup<TKey, TElement>.CreateAsync(_source, _keySelector, _elementSelector, _comparer, _cancellationToken).ConfigureAwait(false);
                        _enumerator = _lookup.ApplyResultSelector(_resultSelector).GetEnumerator();
                        _state = AsyncIteratorState.Iterating;
                        goto case AsyncIteratorState.Iterating;

                    case AsyncIteratorState.Iterating:
                        if (_enumerator!.MoveNext())
                        {
                            _current = _enumerator.Current;
                            return true;
                        }

                        await DisposeAsync().ConfigureAwait(false);
                        break;
                }

                return false;
            }

            public async ValueTask<TResult[]> ToArrayAsync(CancellationToken cancellationToken)
            {
                var l = await Internal.Lookup<TKey, TElement>.CreateAsync(_source, _keySelector, _elementSelector, _comparer, cancellationToken).ConfigureAwait(false);
                return l.ToArray(_resultSelector);
            }

            public async ValueTask<List<TResult>> ToListAsync(CancellationToken cancellationToken)
            {
                var l = await Internal.Lookup<TKey, TElement>.CreateAsync(_source, _keySelector, _elementSelector, _comparer, cancellationToken).ConfigureAwait(false);
                return l.ToList(_resultSelector);
            }

            public ValueTask<int> GetCountAsync(bool onlyIfCheap, CancellationToken cancellationToken)
            {
                if (onlyIfCheap)
                {
                    return new ValueTask<int>(-1);
                }

                return Core();

                async ValueTask<int> Core()
                {
                    var l = await Internal.Lookup<TKey, TElement>.CreateAsync(_source, _keySelector, _elementSelector, _comparer, cancellationToken).ConfigureAwait(false);

                    return l.Count;
                }
            }
        }

        private sealed class GroupedResultAsyncEnumerableWithTask<TSource, TKey, TElement, TResult> : AsyncIterator<TResult>, IAsyncIListProvider<TResult>
        {
            private readonly IAsyncEnumerable<TSource> _source;
            private readonly Func<TSource, ValueTask<TKey>> _keySelector;
            private readonly Func<TSource, ValueTask<TElement>> _elementSelector;
            private readonly Func<TKey, IAsyncEnumerable<TElement>, ValueTask<TResult>> _resultSelector;
            private readonly IEqualityComparer<TKey>? _comparer;

            private Internal.LookupWithTask<TKey, TElement>? _lookup;
            private IAsyncEnumerator<TResult>? _enumerator;

            public GroupedResultAsyncEnumerableWithTask(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TElement>> elementSelector, Func<TKey, IAsyncEnumerable<TElement>, ValueTask<TResult>> resultSelector, IEqualityComparer<TKey>? comparer)
            {
                _source = source ?? throw Error.ArgumentNull(nameof(source));
                _keySelector = keySelector ?? throw Error.ArgumentNull(nameof(keySelector));
                _elementSelector = elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector));
                _resultSelector = resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector));
                _comparer = comparer;
            }

            public override AsyncIteratorBase<TResult> Clone()
            {
                return new GroupedResultAsyncEnumerableWithTask<TSource, TKey, TElement, TResult>(_source, _keySelector, _elementSelector, _resultSelector, _comparer);
            }

            public override async ValueTask DisposeAsync()
            {
                if (_enumerator != null)
                {
                    await _enumerator.DisposeAsync().ConfigureAwait(false);
                    _enumerator = null;
                    _lookup = null;
                }

                await base.DisposeAsync().ConfigureAwait(false);
            }

            protected override async ValueTask<bool> MoveNextCore()
            {
                switch (_state)
                {
                    case AsyncIteratorState.Allocated:
                        _lookup = await Internal.LookupWithTask<TKey, TElement>.CreateAsync(_source, _keySelector, _elementSelector, _comparer, _cancellationToken).ConfigureAwait(false);
                        _enumerator = _lookup.SelectAwaitCore(async g => await _resultSelector(g.Key, g).ConfigureAwait(false)).GetAsyncEnumerator(_cancellationToken); // REVIEW: Introduce another ApplyResultSelector?
                        _state = AsyncIteratorState.Iterating;
                        goto case AsyncIteratorState.Iterating;

                    case AsyncIteratorState.Iterating:
                        if (await _enumerator!.MoveNextAsync().ConfigureAwait(false))
                        {
                            _current = _enumerator.Current;
                            return true;
                        }

                        await DisposeAsync().ConfigureAwait(false);
                        break;
                }

                return false;
            }

            public async ValueTask<TResult[]> ToArrayAsync(CancellationToken cancellationToken)
            {
                var l = await Internal.LookupWithTask<TKey, TElement>.CreateAsync(_source, _keySelector, _elementSelector, _comparer, cancellationToken).ConfigureAwait(false);
                return await l.ToArray(_resultSelector).ConfigureAwait(false);
            }

            public async ValueTask<List<TResult>> ToListAsync(CancellationToken cancellationToken)
            {
                var l = await Internal.LookupWithTask<TKey, TElement>.CreateAsync(_source, _keySelector, _elementSelector, _comparer, cancellationToken).ConfigureAwait(false);
                return await l.ToList(_resultSelector).ConfigureAwait(false);
            }

            public ValueTask<int> GetCountAsync(bool onlyIfCheap, CancellationToken cancellationToken)
            {
                if (onlyIfCheap)
                {
                    return new ValueTask<int>(-1);
                }

                return Core();

                async ValueTask<int> Core()
                {
                    var l = await Internal.LookupWithTask<TKey, TElement>.CreateAsync(_source, _keySelector, _elementSelector, _comparer, cancellationToken).ConfigureAwait(false);

                    return l.Count;
                }
            }
        }

#if !NO_DEEP_CANCELLATION
        private sealed class GroupedResultAsyncEnumerableWithTaskAndCancellation<TSource, TKey, TElement, TResult> : AsyncIterator<TResult>, IAsyncIListProvider<TResult>
        {
            private readonly IAsyncEnumerable<TSource> _source;
            private readonly Func<TSource, CancellationToken, ValueTask<TKey>> _keySelector;
            private readonly Func<TSource, CancellationToken, ValueTask<TElement>> _elementSelector;
            private readonly Func<TKey, IAsyncEnumerable<TElement>, CancellationToken, ValueTask<TResult>> _resultSelector;
            private readonly IEqualityComparer<TKey>? _comparer;

            private Internal.LookupWithTask<TKey, TElement>? _lookup;
            private IAsyncEnumerator<TResult>? _enumerator;

            public GroupedResultAsyncEnumerableWithTaskAndCancellation(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TElement>> elementSelector, Func<TKey, IAsyncEnumerable<TElement>, CancellationToken, ValueTask<TResult>> resultSelector, IEqualityComparer<TKey>? comparer)
            {
                _source = source ?? throw Error.ArgumentNull(nameof(source));
                _keySelector = keySelector ?? throw Error.ArgumentNull(nameof(keySelector));
                _elementSelector = elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector));
                _resultSelector = resultSelector ?? throw Error.ArgumentNull(nameof(resultSelector));
                _comparer = comparer;
            }

            public override AsyncIteratorBase<TResult> Clone()
            {
                return new GroupedResultAsyncEnumerableWithTaskAndCancellation<TSource, TKey, TElement, TResult>(_source, _keySelector, _elementSelector, _resultSelector, _comparer);
            }

            public override async ValueTask DisposeAsync()
            {
                if (_enumerator != null)
                {
                    await _enumerator.DisposeAsync().ConfigureAwait(false);
                    _enumerator = null;
                    _lookup = null;
                }

                await base.DisposeAsync().ConfigureAwait(false);
            }

            protected override async ValueTask<bool> MoveNextCore()
            {
                switch (_state)
                {
                    case AsyncIteratorState.Allocated:
                        _lookup = await Internal.LookupWithTask<TKey, TElement>.CreateAsync(_source, _keySelector, _elementSelector, _comparer, _cancellationToken).ConfigureAwait(false);
                        _enumerator = _lookup.SelectAwaitCore(async g => await _resultSelector(g.Key, g, _cancellationToken).ConfigureAwait(false)).GetAsyncEnumerator(_cancellationToken); // REVIEW: Introduce another ApplyResultSelector?
                        _state = AsyncIteratorState.Iterating;
                        goto case AsyncIteratorState.Iterating;

                    case AsyncIteratorState.Iterating:
                        if (await _enumerator!.MoveNextAsync().ConfigureAwait(false))
                        {
                            _current = _enumerator.Current;
                            return true;
                        }

                        await DisposeAsync().ConfigureAwait(false);
                        break;
                }

                return false;
            }

            public async ValueTask<TResult[]> ToArrayAsync(CancellationToken cancellationToken)
            {
                var l = await Internal.LookupWithTask<TKey, TElement>.CreateAsync(_source, _keySelector, _elementSelector, _comparer, cancellationToken).ConfigureAwait(false);
                return await l.ToArray(_resultSelector, cancellationToken).ConfigureAwait(false);
            }

            public async ValueTask<List<TResult>> ToListAsync(CancellationToken cancellationToken)
            {
                var l = await Internal.LookupWithTask<TKey, TElement>.CreateAsync(_source, _keySelector, _elementSelector, _comparer, cancellationToken).ConfigureAwait(false);
                return await l.ToList(_resultSelector, cancellationToken).ConfigureAwait(false);
            }

            public ValueTask<int> GetCountAsync(bool onlyIfCheap, CancellationToken cancellationToken)
            {
                if (onlyIfCheap)
                {
                    return new ValueTask<int>(-1);
                }

                return Core();

                async ValueTask<int> Core()
                {
                    var l = await Internal.LookupWithTask<TKey, TElement>.CreateAsync(_source, _keySelector, _elementSelector, _comparer, cancellationToken).ConfigureAwait(false);

                    return l.Count;
                }
            }
        }
#endif

        private sealed class GroupedAsyncEnumerable<TSource, TKey, TElement> : AsyncIterator<IAsyncGrouping<TKey, TElement>>, IAsyncIListProvider<IAsyncGrouping<TKey, TElement>>
        {
            private readonly IAsyncEnumerable<TSource> _source;
            private readonly Func<TSource, TKey> _keySelector;
            private readonly Func<TSource, TElement> _elementSelector;
            private readonly IEqualityComparer<TKey>? _comparer;

            private Internal.Lookup<TKey, TElement>? _lookup;
            private IEnumerator<IGrouping<TKey, TElement>>? _enumerator;

            public GroupedAsyncEnumerable(IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey>? comparer)
            {
                _source = source ?? throw Error.ArgumentNull(nameof(source));
                _keySelector = keySelector ?? throw Error.ArgumentNull(nameof(keySelector));
                _elementSelector = elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector));
                _comparer = comparer;
            }

            public override AsyncIteratorBase<IAsyncGrouping<TKey, TElement>> Clone()
            {
                return new GroupedAsyncEnumerable<TSource, TKey, TElement>(_source, _keySelector, _elementSelector, _comparer);
            }

            public override async ValueTask DisposeAsync()
            {
                if (_enumerator != null)
                {
                    _enumerator.Dispose();
                    _enumerator = null;
                    _lookup = null;
                }

                await base.DisposeAsync().ConfigureAwait(false);
            }

            protected override async ValueTask<bool> MoveNextCore()
            {
                switch (_state)
                {
                    case AsyncIteratorState.Allocated:
                        _lookup = await Internal.Lookup<TKey, TElement>.CreateAsync(_source, _keySelector, _elementSelector, _comparer, _cancellationToken).ConfigureAwait(false);
                        _enumerator = _lookup.GetEnumerator();
                        _state = AsyncIteratorState.Iterating;
                        goto case AsyncIteratorState.Iterating;

                    case AsyncIteratorState.Iterating:
                        if (_enumerator!.MoveNext())
                        {
                            _current = (IAsyncGrouping<TKey, TElement>)_enumerator.Current;
                            return true;
                        }

                        await DisposeAsync().ConfigureAwait(false);
                        break;
                }

                return false;
            }
            
            public async ValueTask<IAsyncGrouping<TKey, TElement>[]> ToArrayAsync(CancellationToken cancellationToken)
            {
                IAsyncIListProvider<IAsyncGrouping<TKey, TElement>> l = await Internal.Lookup<TKey, TElement>.CreateAsync(_source, _keySelector, _elementSelector, _comparer, cancellationToken).ConfigureAwait(false);
                return await l.ToArrayAsync(cancellationToken).ConfigureAwait(false);
            }

            public async ValueTask<List<IAsyncGrouping<TKey, TElement>>> ToListAsync(CancellationToken cancellationToken)
            {
                IAsyncIListProvider<IAsyncGrouping<TKey, TElement>> l = await Internal.Lookup<TKey, TElement>.CreateAsync(_source, _keySelector, _elementSelector, _comparer, cancellationToken).ConfigureAwait(false);
                return await l.ToListAsync(cancellationToken).ConfigureAwait(false);
            }

            public ValueTask<int> GetCountAsync(bool onlyIfCheap, CancellationToken cancellationToken)
            {
                if (onlyIfCheap)
                {
                    return new ValueTask<int>(-1);
                }

                return Core();

                async ValueTask<int> Core()
                {
                    var l = await Internal.Lookup<TKey, TElement>.CreateAsync(_source, _keySelector, _elementSelector, _comparer, cancellationToken).ConfigureAwait(false);

                    return l.Count;
                }
            }
        }

        private sealed class GroupedAsyncEnumerableWithTask<TSource, TKey, TElement> : AsyncIterator<IAsyncGrouping<TKey, TElement>>, IAsyncIListProvider<IAsyncGrouping<TKey, TElement>>
        {
            private readonly IAsyncEnumerable<TSource> _source;
            private readonly Func<TSource, ValueTask<TKey>> _keySelector;
            private readonly Func<TSource, ValueTask<TElement>> _elementSelector;
            private readonly IEqualityComparer<TKey>? _comparer;

            private Internal.LookupWithTask<TKey, TElement>? _lookup;
            private IEnumerator<IGrouping<TKey, TElement>>? _enumerator;

            public GroupedAsyncEnumerableWithTask(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, Func<TSource, ValueTask<TElement>> elementSelector, IEqualityComparer<TKey>? comparer)
            {
                _source = source ?? throw Error.ArgumentNull(nameof(source));
                _keySelector = keySelector ?? throw Error.ArgumentNull(nameof(keySelector));
                _elementSelector = elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector));
                _comparer = comparer;
            }

            public override AsyncIteratorBase<IAsyncGrouping<TKey, TElement>> Clone()
            {
                return new GroupedAsyncEnumerableWithTask<TSource, TKey, TElement>(_source, _keySelector, _elementSelector, _comparer);
            }

            public override async ValueTask DisposeAsync()
            {
                if (_enumerator != null)
                {
                    _enumerator.Dispose();
                    _enumerator = null;
                    _lookup = null;
                }

                await base.DisposeAsync().ConfigureAwait(false);
            }

            protected override async ValueTask<bool> MoveNextCore()
            {
                switch (_state)
                {
                    case AsyncIteratorState.Allocated:
                        _lookup = await Internal.LookupWithTask<TKey, TElement>.CreateAsync(_source, _keySelector, _elementSelector, _comparer, _cancellationToken).ConfigureAwait(false);
                        _enumerator = _lookup.GetEnumerator();
                        _state = AsyncIteratorState.Iterating;
                        goto case AsyncIteratorState.Iterating;

                    case AsyncIteratorState.Iterating:
                        if (_enumerator!.MoveNext())
                        {
                            _current = (IAsyncGrouping<TKey, TElement>)_enumerator.Current;
                            return true;
                        }

                        await DisposeAsync().ConfigureAwait(false);
                        break;
                }

                return false;
            }

            public async ValueTask<IAsyncGrouping<TKey, TElement>[]> ToArrayAsync(CancellationToken cancellationToken)
            {
                IAsyncIListProvider<IAsyncGrouping<TKey, TElement>> l = await Internal.LookupWithTask<TKey, TElement>.CreateAsync(_source, _keySelector, _elementSelector, _comparer, cancellationToken).ConfigureAwait(false);
                return await l.ToArrayAsync(cancellationToken).ConfigureAwait(false);
            }

            public async ValueTask<List<IAsyncGrouping<TKey, TElement>>> ToListAsync(CancellationToken cancellationToken)
            {
                IAsyncIListProvider<IAsyncGrouping<TKey, TElement>> l = await Internal.LookupWithTask<TKey, TElement>.CreateAsync(_source, _keySelector, _elementSelector, _comparer, cancellationToken).ConfigureAwait(false);
                return await l.ToListAsync(cancellationToken).ConfigureAwait(false);
            }

            public ValueTask<int> GetCountAsync(bool onlyIfCheap, CancellationToken cancellationToken)
            {
                if (onlyIfCheap)
                {
                    return new ValueTask<int>(-1);
                }

                return Core();

                async ValueTask<int> Core()
                {
                    var l = await Internal.LookupWithTask<TKey, TElement>.CreateAsync(_source, _keySelector, _elementSelector, _comparer, cancellationToken).ConfigureAwait(false);

                    return l.Count;
                }
            }
        }

#if !NO_DEEP_CANCELLATION
        private sealed class GroupedAsyncEnumerableWithTaskAndCancellation<TSource, TKey, TElement> : AsyncIterator<IAsyncGrouping<TKey, TElement>>, IAsyncIListProvider<IAsyncGrouping<TKey, TElement>>
        {
            private readonly IAsyncEnumerable<TSource> _source;
            private readonly Func<TSource, CancellationToken, ValueTask<TKey>> _keySelector;
            private readonly Func<TSource, CancellationToken, ValueTask<TElement>> _elementSelector;
            private readonly IEqualityComparer<TKey>? _comparer;

            private Internal.LookupWithTask<TKey, TElement>? _lookup;
            private IEnumerator<IGrouping<TKey, TElement>>? _enumerator;

            public GroupedAsyncEnumerableWithTaskAndCancellation(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, Func<TSource, CancellationToken, ValueTask<TElement>> elementSelector, IEqualityComparer<TKey>? comparer)
            {
                _source = source ?? throw Error.ArgumentNull(nameof(source));
                _keySelector = keySelector ?? throw Error.ArgumentNull(nameof(keySelector));
                _elementSelector = elementSelector ?? throw Error.ArgumentNull(nameof(elementSelector));
                _comparer = comparer;
            }

            public override AsyncIteratorBase<IAsyncGrouping<TKey, TElement>> Clone()
            {
                return new GroupedAsyncEnumerableWithTaskAndCancellation<TSource, TKey, TElement>(_source, _keySelector, _elementSelector, _comparer);
            }

            public override async ValueTask DisposeAsync()
            {
                if (_enumerator != null)
                {
                    _enumerator.Dispose();
                    _enumerator = null;
                    _lookup = null;
                }

                await base.DisposeAsync().ConfigureAwait(false);
            }

            protected override async ValueTask<bool> MoveNextCore()
            {
                switch (_state)
                {
                    case AsyncIteratorState.Allocated:
                        _lookup = await Internal.LookupWithTask<TKey, TElement>.CreateAsync(_source, _keySelector, _elementSelector, _comparer, _cancellationToken).ConfigureAwait(false);
                        _enumerator = _lookup.GetEnumerator();
                        _state = AsyncIteratorState.Iterating;
                        goto case AsyncIteratorState.Iterating;

                    case AsyncIteratorState.Iterating:
                        if (_enumerator!.MoveNext())
                        {
                            _current = (IAsyncGrouping<TKey, TElement>)_enumerator.Current;
                            return true;
                        }

                        await DisposeAsync().ConfigureAwait(false);
                        break;
                }

                return false;
            }

            public async ValueTask<IAsyncGrouping<TKey, TElement>[]> ToArrayAsync(CancellationToken cancellationToken)
            {
                IAsyncIListProvider<IAsyncGrouping<TKey, TElement>> l = await Internal.LookupWithTask<TKey, TElement>.CreateAsync(_source, _keySelector, _elementSelector, _comparer, cancellationToken).ConfigureAwait(false);
                return await l.ToArrayAsync(cancellationToken).ConfigureAwait(false);
            }

            public async ValueTask<List<IAsyncGrouping<TKey, TElement>>> ToListAsync(CancellationToken cancellationToken)
            {
                IAsyncIListProvider<IAsyncGrouping<TKey, TElement>> l = await Internal.LookupWithTask<TKey, TElement>.CreateAsync(_source, _keySelector, _elementSelector, _comparer, cancellationToken).ConfigureAwait(false);
                return await l.ToListAsync(cancellationToken).ConfigureAwait(false);
            }

            public ValueTask<int> GetCountAsync(bool onlyIfCheap, CancellationToken cancellationToken)
            {
                if (onlyIfCheap)
                {
                    return new ValueTask<int>(-1);
                }

                return Core();

                async ValueTask<int> Core()
                {
                    var l = await Internal.LookupWithTask<TKey, TElement>.CreateAsync(_source, _keySelector, _elementSelector, _comparer, cancellationToken).ConfigureAwait(false);

                    return l.Count;
                }
            }
        }
#endif

        private sealed class GroupedAsyncEnumerable<TSource, TKey> : AsyncIterator<IAsyncGrouping<TKey, TSource>>, IAsyncIListProvider<IAsyncGrouping<TKey, TSource>>
        {
            private readonly IAsyncEnumerable<TSource> _source;
            private readonly Func<TSource, TKey> _keySelector;
            private readonly IEqualityComparer<TKey>? _comparer;

            private Internal.Lookup<TKey, TSource>? _lookup;
            private IEnumerator<IGrouping<TKey, TSource>>? _enumerator;

            public GroupedAsyncEnumerable(IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer)
            {
                _source = source ?? throw Error.ArgumentNull(nameof(source));
                _keySelector = keySelector ?? throw Error.ArgumentNull(nameof(keySelector));
                _comparer = comparer;
            }

            public override AsyncIteratorBase<IAsyncGrouping<TKey, TSource>> Clone()
            {
                return new GroupedAsyncEnumerable<TSource, TKey>(_source, _keySelector, _comparer);
            }

            public override async ValueTask DisposeAsync()
            {
                if (_enumerator != null)
                {
                    _enumerator.Dispose();
                    _enumerator = null;
                    _lookup = null;
                }

                await base.DisposeAsync().ConfigureAwait(false);
            }

            protected override async ValueTask<bool> MoveNextCore()
            {
                switch (_state)
                {
                    case AsyncIteratorState.Allocated:
                        _lookup = await Internal.Lookup<TKey, TSource>.CreateAsync(_source, _keySelector, _comparer, _cancellationToken).ConfigureAwait(false);
                        _enumerator = _lookup.GetEnumerator();
                        _state = AsyncIteratorState.Iterating;
                        goto case AsyncIteratorState.Iterating;

                    case AsyncIteratorState.Iterating:
                        if (_enumerator!.MoveNext())
                        {
                            _current = (IAsyncGrouping<TKey, TSource>)_enumerator.Current;
                            return true;
                        }

                        await DisposeAsync().ConfigureAwait(false);
                        break;
                }

                return false;
            }

            public async ValueTask<IAsyncGrouping<TKey, TSource>[]> ToArrayAsync(CancellationToken cancellationToken)
            {
                IAsyncIListProvider<IAsyncGrouping<TKey, TSource>> l = await Internal.Lookup<TKey, TSource>.CreateAsync(_source, _keySelector, _comparer, cancellationToken).ConfigureAwait(false);
                return await l.ToArrayAsync(cancellationToken).ConfigureAwait(false);
            }

            public async ValueTask<List<IAsyncGrouping<TKey, TSource>>> ToListAsync(CancellationToken cancellationToken)
            {
                IAsyncIListProvider<IAsyncGrouping<TKey, TSource>> l = await Internal.Lookup<TKey, TSource>.CreateAsync(_source, _keySelector, _comparer, cancellationToken).ConfigureAwait(false);
                return await l.ToListAsync(cancellationToken).ConfigureAwait(false);
            }

            public ValueTask<int> GetCountAsync(bool onlyIfCheap, CancellationToken cancellationToken)
            {
                if (onlyIfCheap)
                {
                    return new ValueTask<int>(-1);
                }

                return Core();

                async ValueTask<int> Core()
                {
                    var l = await Internal.Lookup<TKey, TSource>.CreateAsync(_source, _keySelector, _comparer, cancellationToken).ConfigureAwait(false);

                    return l.Count;
                }
            }
        }

        private sealed class GroupedAsyncEnumerableWithTask<TSource, TKey> : AsyncIterator<IAsyncGrouping<TKey, TSource>>, IAsyncIListProvider<IAsyncGrouping<TKey, TSource>>
        {
            private readonly IAsyncEnumerable<TSource> _source;
            private readonly Func<TSource, ValueTask<TKey>> _keySelector;
            private readonly IEqualityComparer<TKey>? _comparer;

            private Internal.LookupWithTask<TKey, TSource>? _lookup;
            private IEnumerator<IGrouping<TKey, TSource>>? _enumerator;

            public GroupedAsyncEnumerableWithTask(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<TKey>> keySelector, IEqualityComparer<TKey>? comparer)
            {
                _source = source ?? throw Error.ArgumentNull(nameof(source));
                _keySelector = keySelector ?? throw Error.ArgumentNull(nameof(keySelector));
                _comparer = comparer;
            }

            public override AsyncIteratorBase<IAsyncGrouping<TKey, TSource>> Clone()
            {
                return new GroupedAsyncEnumerableWithTask<TSource, TKey>(_source, _keySelector, _comparer);
            }

            public override async ValueTask DisposeAsync()
            {
                if (_enumerator != null)
                {
                    _enumerator.Dispose();
                    _enumerator = null;
                    _lookup = null;
                }

                await base.DisposeAsync().ConfigureAwait(false);
            }

            protected override async ValueTask<bool> MoveNextCore()
            {
                switch (_state)
                {
                    case AsyncIteratorState.Allocated:
                        _lookup = await Internal.LookupWithTask<TKey, TSource>.CreateAsync(_source, _keySelector, _comparer, _cancellationToken).ConfigureAwait(false);
                        _enumerator = _lookup.GetEnumerator();
                        _state = AsyncIteratorState.Iterating;
                        goto case AsyncIteratorState.Iterating;

                    case AsyncIteratorState.Iterating:
                        if (_enumerator!.MoveNext())
                        {
                            _current = (IAsyncGrouping<TKey, TSource>)_enumerator.Current;
                            return true;
                        }

                        await DisposeAsync().ConfigureAwait(false);
                        break;
                }

                return false;
            }

            public async ValueTask<IAsyncGrouping<TKey, TSource>[]> ToArrayAsync(CancellationToken cancellationToken)
            {
                IAsyncIListProvider<IAsyncGrouping<TKey, TSource>> l = await Internal.LookupWithTask<TKey, TSource>.CreateAsync(_source, _keySelector, _comparer, cancellationToken).ConfigureAwait(false);
                return await l.ToArrayAsync(cancellationToken).ConfigureAwait(false);
            }

            public async ValueTask<List<IAsyncGrouping<TKey, TSource>>> ToListAsync(CancellationToken cancellationToken)
            {
                IAsyncIListProvider<IAsyncGrouping<TKey, TSource>> l = await Internal.LookupWithTask<TKey, TSource>.CreateAsync(_source, _keySelector, _comparer, cancellationToken).ConfigureAwait(false);
                return await l.ToListAsync(cancellationToken).ConfigureAwait(false);
            }

            public ValueTask<int> GetCountAsync(bool onlyIfCheap, CancellationToken cancellationToken)
            {
                if (onlyIfCheap)
                {
                    return new ValueTask<int>(-1);
                }

                return Core();

                async ValueTask<int> Core()
                {
                    var l = await Internal.LookupWithTask<TKey, TSource>.CreateAsync(_source, _keySelector, _comparer, cancellationToken).ConfigureAwait(false);

                    return l.Count;
                }
            }
        }

#if !NO_DEEP_CANCELLATION
        private sealed class GroupedAsyncEnumerableWithTaskAndCancellation<TSource, TKey> : AsyncIterator<IAsyncGrouping<TKey, TSource>>, IAsyncIListProvider<IAsyncGrouping<TKey, TSource>>
        {
            private readonly IAsyncEnumerable<TSource> _source;
            private readonly Func<TSource, CancellationToken, ValueTask<TKey>> _keySelector;
            private readonly IEqualityComparer<TKey>? _comparer;

            private Internal.LookupWithTask<TKey, TSource>? _lookup;
            private IEnumerator<IGrouping<TKey, TSource>>? _enumerator;

            public GroupedAsyncEnumerableWithTaskAndCancellation(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<TKey>> keySelector, IEqualityComparer<TKey>? comparer)
            {
                _source = source ?? throw Error.ArgumentNull(nameof(source));
                _keySelector = keySelector ?? throw Error.ArgumentNull(nameof(keySelector));
                _comparer = comparer;
            }

            public override AsyncIteratorBase<IAsyncGrouping<TKey, TSource>> Clone()
            {
                return new GroupedAsyncEnumerableWithTaskAndCancellation<TSource, TKey>(_source, _keySelector, _comparer);
            }

            public override async ValueTask DisposeAsync()
            {
                if (_enumerator != null)
                {
                    _enumerator.Dispose();
                    _enumerator = null;
                    _lookup = null;
                }

                await base.DisposeAsync().ConfigureAwait(false);
            }

            protected override async ValueTask<bool> MoveNextCore()
            {
                switch (_state)
                {
                    case AsyncIteratorState.Allocated:
                        _lookup = await Internal.LookupWithTask<TKey, TSource>.CreateAsync(_source, _keySelector, _comparer, _cancellationToken).ConfigureAwait(false);
                        _enumerator = _lookup.GetEnumerator();
                        _state = AsyncIteratorState.Iterating;
                        goto case AsyncIteratorState.Iterating;

                    case AsyncIteratorState.Iterating:
                        if (_enumerator!.MoveNext())
                        {
                            _current = (IAsyncGrouping<TKey, TSource>)_enumerator.Current;
                            return true;
                        }

                        await DisposeAsync().ConfigureAwait(false);
                        break;
                }

                return false;
            }

            public async ValueTask<IAsyncGrouping<TKey, TSource>[]> ToArrayAsync(CancellationToken cancellationToken)
            {
                IAsyncIListProvider<IAsyncGrouping<TKey, TSource>> l = await Internal.LookupWithTask<TKey, TSource>.CreateAsync(_source, _keySelector, _comparer, cancellationToken).ConfigureAwait(false);
                return await l.ToArrayAsync(cancellationToken).ConfigureAwait(false);
            }

            public async ValueTask<List<IAsyncGrouping<TKey, TSource>>> ToListAsync(CancellationToken cancellationToken)
            {
                IAsyncIListProvider<IAsyncGrouping<TKey, TSource>> l = await Internal.LookupWithTask<TKey, TSource>.CreateAsync(_source, _keySelector, _comparer, cancellationToken).ConfigureAwait(false);
                return await l.ToListAsync(cancellationToken).ConfigureAwait(false);
            }

            public ValueTask<int> GetCountAsync(bool onlyIfCheap, CancellationToken cancellationToken)
            {
                if (onlyIfCheap)
                {
                    return new ValueTask<int>(-1);
                }

                return Core();

                async ValueTask<int> Core()
                {
                    var l = await Internal.LookupWithTask<TKey, TSource>.CreateAsync(_source, _keySelector, _comparer, cancellationToken).ConfigureAwait(false);

                    return l.Count;
                }
            }
        }
#endif
    }
}
