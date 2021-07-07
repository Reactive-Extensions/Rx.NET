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
        /// Projects each element of an async-enumerable sequence to an async-enumerable sequence and merges the resulting async-enumerable sequences into one async-enumerable sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TResult">The type of the elements in the projected inner sequences and the elements in the merged result sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of elements to project.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>An async-enumerable sequence whose elements are the result of invoking the one-to-many transform function on each element of the input sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        public static IAsyncEnumerable<TResult> SelectMany<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, IAsyncEnumerable<TResult>> selector)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return new SelectManyAsyncIterator<TSource, TResult>(source, selector);
        }

        // REVIEW: Should we keep these overloads that return ValueTask<IAsyncEnumerable<TResult>>? One could argue the selector is async twice.

        /// <summary>
        /// Projects each element of an async-enumerable sequence into an async-enumerable sequence and merges the resulting async-enumerable sequences into one async-enumerable sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <typeparam name="TResult">The type of elements in the projected inner sequences and the merged result sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of elements to project.</param>
        /// <param name="selector">An asynchronous selector function to apply to each element of the source sequence.</param>
        /// <returns>An async-enumerable sequence whose elements are the result of invoking the one-to-many transform function on each element of the source sequence and awaiting the result.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<TResult> SelectManyAwaitCore<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<IAsyncEnumerable<TResult>>> selector)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return new SelectManyAsyncIteratorWithTask<TSource, TResult>(source, selector);
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<TResult> SelectManyAwaitWithCancellationCore<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<IAsyncEnumerable<TResult>>> selector)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return new SelectManyAsyncIteratorWithTaskAndCancellation<TSource, TResult>(source, selector);
        }
#endif

        /// <summary>
        /// Projects each element of an async-enumerable sequence to an async-enumerable sequence by incorporating the element's index and merges the resulting async-enumerable sequences into one async-enumerable sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TResult">The type of the elements in the projected inner sequences and the elements in the merged result sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of elements to project.</param>
        /// <param name="selector">A transform function to apply to each element; the second parameter of the function represents the index of the source element.</param>
        /// <returns>An async-enumerable sequence whose elements are the result of invoking the one-to-many transform function on each element of the input sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        public static IAsyncEnumerable<TResult> SelectMany<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, IAsyncEnumerable<TResult>> selector)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector);

            static async IAsyncEnumerable<TResult> Core(IAsyncEnumerable<TSource> source, Func<TSource, int, IAsyncEnumerable<TResult>> selector, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
            {
                var index = -1;

                await foreach (var element in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    checked
                    {
                        index++;
                    }

                    var inner = selector(element, index);

                    await foreach (var subElement in inner.WithCancellation(cancellationToken).ConfigureAwait(false))
                    {
                        yield return subElement;
                    }
                }
            }
        }

        /// <summary>
        /// Projects each element of an async-enumerable sequence into an async-enumerable sequence by incorporating the element's index and merges the resulting async-enumerable sequences into an async-enumerable sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <typeparam name="TResult">The type of elements in the projected inner sequences and the merged result sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of elements to project.</param>
        /// <param name="selector">An asynchronous selector function to apply to each element; the second parameter represents the index of the element.</param>
        /// <returns>An async-enumerable sequence who's elements are the result of invoking the one-to-many transform function on each element of the source sequence and awaiting the result.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null.</exception>
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<TResult> SelectManyAwaitCore<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, ValueTask<IAsyncEnumerable<TResult>>> selector)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector);

            static async IAsyncEnumerable<TResult> Core(IAsyncEnumerable<TSource> source, Func<TSource, int, ValueTask<IAsyncEnumerable<TResult>>> selector, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
            {
                var index = -1;

                await foreach (var element in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    checked
                    {
                        index++;
                    }

                    var inner = await selector(element, index).ConfigureAwait(false);

                    await foreach (var subElement in inner.WithCancellation(cancellationToken).ConfigureAwait(false))
                    {
                        yield return subElement;
                    }
                }
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<TResult> SelectManyAwaitWithCancellationCore<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, ValueTask<IAsyncEnumerable<TResult>>> selector)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (selector == null)
                throw Error.ArgumentNull(nameof(selector));

            return Core(source, selector);

            static async IAsyncEnumerable<TResult> Core(IAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, ValueTask<IAsyncEnumerable<TResult>>> selector, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
            {
                var index = -1;

                await foreach (var element in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    checked
                    {
                        index++;
                    }

                    var inner = await selector(element, index, cancellationToken).ConfigureAwait(false);

                    await foreach (var subElement in inner.WithCancellation(cancellationToken).ConfigureAwait(false))
                    {
                        yield return subElement;
                    }
                }
            }
        }
#endif

        /// <summary>
        /// Projects each element of an async-enumerable sequence to an async-enumerable sequence, invokes the result selector for the source element and each of the corresponding inner sequence's elements, and merges the results into one async-enumerable sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TCollection">The type of the elements in the projected intermediate sequences.</typeparam>
        /// <typeparam name="TResult">The type of the elements in the result sequence, obtained by using the selector to combine source sequence elements with their corresponding intermediate sequence elements.</typeparam>
        /// <param name="source">An async-enumerable sequence of elements to project.</param>
        /// <param name="collectionSelector">A transform function to apply to each element.</param>
        /// <param name="resultSelector">A transform function to apply to each element of the intermediate sequence.</param>
        /// <returns>An async-enumerable sequence whose elements are the result of invoking the one-to-many transform function collectionSelector on each element of the input sequence and then mapping each of those sequence elements and their corresponding source element to a result element.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="collectionSelector"/> or <paramref name="resultSelector"/> is null.</exception>
        public static IAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, IAsyncEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (collectionSelector == null)
                throw Error.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null)
                throw Error.ArgumentNull(nameof(resultSelector));

            return Core(source, collectionSelector, resultSelector);

            static async IAsyncEnumerable<TResult> Core(IAsyncEnumerable<TSource> source, Func<TSource, IAsyncEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
            {
                await foreach (var element in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var inner = collectionSelector(element);

                    await foreach (var subElement in inner.WithCancellation(cancellationToken).ConfigureAwait(false))
                    {
                        yield return resultSelector(element, subElement);
                    }
                }
            }
        }

        /// <summary>
        /// Projects each element of an async-enumerable sequence to an async-enumerable sequence by awaiting the result of a transform function, invokes the result selector for each of the source elements and each of the corrasponding inner-sequence's elements and awaits the result, and merges the results into one async-enumerable sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <typeparam name="TCollection">The type of elements in the projected intermediate sequences.</typeparam>
        /// <typeparam name="TResult">The type of elements in the result sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of elements to project.</param>
        /// <param name="collectionSelector">An asynchronous transform function to apply to each source element.</param>
        /// <param name="resultSelector">An asynchronous transform function to apply to each element of the intermediate sequence.</param>
        /// <returns>An async-enumerable sequence whose elements are the result of invoking the one-to-many transform function <paramref name="collectionSelector"/> on each element of the input sequence, awaiting the result, applying <paramref name="resultSelector"/> to each element of the intermediate sequences along with their corrasponding source element and awaiting the result.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/>, <paramref name="collectionSelector"/>, or <paramref name="resultSelector"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<TResult> SelectManyAwaitCore<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<IAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, ValueTask<TResult>> resultSelector)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (collectionSelector == null)
                throw Error.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null)
                throw Error.ArgumentNull(nameof(resultSelector));

            return Core(source, collectionSelector, resultSelector);

            static async IAsyncEnumerable<TResult> Core(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<IAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, ValueTask<TResult>> resultSelector, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
            {
                await foreach (var element in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var inner = await collectionSelector(element).ConfigureAwait(false);

                    await foreach (var subElement in inner.WithCancellation(cancellationToken).ConfigureAwait(false))
                    {
                        yield return await resultSelector(element, subElement).ConfigureAwait(false);
                    }
                }
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<TResult> SelectManyAwaitWithCancellationCore<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<IAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, CancellationToken, ValueTask<TResult>> resultSelector)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (collectionSelector == null)
                throw Error.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null)
                throw Error.ArgumentNull(nameof(resultSelector));

            return Core(source, collectionSelector, resultSelector);

            static async IAsyncEnumerable<TResult> Core(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<IAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, CancellationToken, ValueTask<TResult>> resultSelector, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
            {
                await foreach (var element in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var inner = await collectionSelector(element, cancellationToken).ConfigureAwait(false);

                    await foreach (var subElement in inner.WithCancellation(cancellationToken).ConfigureAwait(false))
                    {
                        yield return await resultSelector(element, subElement, cancellationToken).ConfigureAwait(false);
                    }
                }
            }
        }
#endif

        /// <summary>
        /// Projects each element of an async-enumerable sequence to an async-enumerable sequence by incorporating the element's index, invokes the result selector for the source element and each of the corresponding inner sequence's elements, and merges the results into one async-enumerable sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TCollection">The type of the elements in the projected intermediate sequences.</typeparam>
        /// <typeparam name="TResult">The type of the elements in the result sequence, obtained by using the selector to combine source sequence elements with their corresponding intermediate sequence elements.</typeparam>
        /// <param name="source">An async-enumerable sequence of elements to project.</param>
        /// <param name="collectionSelector">A transform function to apply to each element; the second parameter of the function represents the index of the source element.</param>
        /// <param name="resultSelector">A transform function to apply to each element of the intermediate sequence; the second parameter of the function represents the index of the source element and the fourth parameter represents the index of the intermediate element.</param>
        /// <returns>An async-enumerable sequence whose elements are the result of invoking the one-to-many transform function collectionSelector on each element of the input sequence and then mapping each of those sequence elements and their corresponding source element to a result element.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="collectionSelector"/> or <paramref name="resultSelector"/> is null.</exception>
        public static IAsyncEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, IAsyncEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (collectionSelector == null)
                throw Error.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null)
                throw Error.ArgumentNull(nameof(resultSelector));

            return Core(source, collectionSelector, resultSelector);

            static async IAsyncEnumerable<TResult> Core(IAsyncEnumerable<TSource> source, Func<TSource, int, IAsyncEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
            {
                var index = -1;

                await foreach (var element in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    checked
                    {
                        index++;
                    }

                    var inner = collectionSelector(element, index);

                    await foreach (var subElement in inner.WithCancellation(cancellationToken).ConfigureAwait(false))
                    {
                        yield return resultSelector(element, subElement);
                    }
                }
            }
        }

        /// <summary>
        /// Projects each element of an async-enumerable sequence to an async-enumerable sequence by awaiting the result of a transform function that incorporates each element's index,
        /// invokes the result selector for the source element and each of the corrasponding inner-sequence's elements and awaits the result, and merges the results into one async-enumerable sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
        /// <typeparam name="TCollection">The type of elements in the projected intermediate sequences.</typeparam>
        /// <typeparam name="TResult">The type of elements in the result sequence.</typeparam>
        /// <param name="source">An async-enumerable sequence of elements to project.</param>
        /// <param name="collectionSelector">An asynchronous transform function to apply to each source element; the second parameter represents the index of the element.</param>
        /// <param name="resultSelector">An asynchronous transform function to apply to each element of the intermediate sequence.</param>
        /// <returns>An async-enumerable sequence whose elements are the result of invoking the one-to-many transform function <paramref name="collectionSelector"/> on each element of the input sequence, awaiting the result, applying <paramref name="resultSelector"/> to each element of the intermediate sequences olong with their corrasponding source element and awaiting the result.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/>, <paramref name="collectionSelector"/>, or <paramref name="resultSelector"/> is <see langword="null"/>.</exception>
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<TResult> SelectManyAwaitCore<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, ValueTask<IAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, ValueTask<TResult>> resultSelector)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (collectionSelector == null)
                throw Error.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null)
                throw Error.ArgumentNull(nameof(resultSelector));

            return Core(source, collectionSelector, resultSelector);

            static async IAsyncEnumerable<TResult> Core(IAsyncEnumerable<TSource> source, Func<TSource, int, ValueTask<IAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, ValueTask<TResult>> resultSelector, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
            {
                var index = -1;

                await foreach (var element in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    checked
                    {
                        index++;
                    }

                    var inner = await collectionSelector(element, index).ConfigureAwait(false);

                    await foreach (var subElement in inner.WithCancellation(cancellationToken).ConfigureAwait(false))
                    {
                        yield return await resultSelector(element, subElement).ConfigureAwait(false);
                    }
                }
            }
        }

#if !NO_DEEP_CANCELLATION
        [GenerateAsyncOverload]
        private static IAsyncEnumerable<TResult> SelectManyAwaitWithCancellationCore<TSource, TCollection, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, ValueTask<IAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, CancellationToken, ValueTask<TResult>> resultSelector)
        {
            if (source == null)
                throw Error.ArgumentNull(nameof(source));
            if (collectionSelector == null)
                throw Error.ArgumentNull(nameof(collectionSelector));
            if (resultSelector == null)
                throw Error.ArgumentNull(nameof(resultSelector));

            return Core(source, collectionSelector, resultSelector);

            static async IAsyncEnumerable<TResult> Core(IAsyncEnumerable<TSource> source, Func<TSource, int, CancellationToken, ValueTask<IAsyncEnumerable<TCollection>>> collectionSelector, Func<TSource, TCollection, CancellationToken, ValueTask<TResult>> resultSelector, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
            {
                var index = -1;

                await foreach (var element in source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    checked
                    {
                        index++;
                    }

                    var inner = await collectionSelector(element, index, cancellationToken).ConfigureAwait(false);

                    await foreach (var subElement in inner.WithCancellation(cancellationToken).ConfigureAwait(false))
                    {
                        yield return await resultSelector(element, subElement, cancellationToken).ConfigureAwait(false);
                    }
                }
            }
        }
#endif

        private sealed class SelectManyAsyncIterator<TSource, TResult> : AsyncIterator<TResult>, IAsyncIListProvider<TResult>
        {
            private const int State_Source = 1;
            private const int State_Result = 2;

            private readonly Func<TSource, IAsyncEnumerable<TResult>> _selector;
            private readonly IAsyncEnumerable<TSource> _source;

            private int _mode;
            private IAsyncEnumerator<TResult>? _resultEnumerator;
            private IAsyncEnumerator<TSource>? _sourceEnumerator;

            public SelectManyAsyncIterator(IAsyncEnumerable<TSource> source, Func<TSource, IAsyncEnumerable<TResult>> selector)
            {
                _source = source;
                _selector = selector;
            }

            public override AsyncIteratorBase<TResult> Clone()
            {
                return new SelectManyAsyncIterator<TSource, TResult>(_source, _selector);
            }

            public override async ValueTask DisposeAsync()
            {
                if (_resultEnumerator != null)
                {
                    await _resultEnumerator.DisposeAsync().ConfigureAwait(false);
                    _resultEnumerator = null;
                }

                if (_sourceEnumerator != null)
                {
                    await _sourceEnumerator.DisposeAsync().ConfigureAwait(false);
                    _sourceEnumerator = null;
                }

                await base.DisposeAsync().ConfigureAwait(false);
            }

            public ValueTask<int> GetCountAsync(bool onlyIfCheap, CancellationToken cancellationToken)
            {
                if (onlyIfCheap)
                {
                    return new ValueTask<int>(-1);
                }

                return Core(cancellationToken);

                async ValueTask<int> Core(CancellationToken cancellationToken)
                {
                    var count = 0;

                    await foreach (var element in _source.WithCancellation(cancellationToken).ConfigureAwait(false))
                    {
                        checked
                        {
                            count += await _selector(element).CountAsync().ConfigureAwait(false);
                        }
                    }

                    return count;
                }
            }

            public async ValueTask<TResult[]> ToArrayAsync(CancellationToken cancellationToken)
            {
                // REVIEW: Substitute for SparseArrayBuilder<T> logic once we have access to that.

                var list = await ToListAsync(cancellationToken).ConfigureAwait(false);

                return list.ToArray();
            }

            public async ValueTask<List<TResult>> ToListAsync(CancellationToken cancellationToken)
            {
                var list = new List<TResult>();

                await foreach (var element in _source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var items = _selector(element);

                    await list.AddRangeAsync(items, cancellationToken).ConfigureAwait(false);
                }

                return list;
            }

            protected override async ValueTask<bool> MoveNextCore()
            {
                switch (_state)
                {
                    case AsyncIteratorState.Allocated:
                        _sourceEnumerator = _source.GetAsyncEnumerator(_cancellationToken);
                        _mode = State_Source;
                        _state = AsyncIteratorState.Iterating;
                        goto case AsyncIteratorState.Iterating;

                    case AsyncIteratorState.Iterating:
                        switch (_mode)
                        {
                            case State_Source:
                                if (await _sourceEnumerator!.MoveNextAsync().ConfigureAwait(false))
                                {
                                    if (_resultEnumerator != null)
                                    {
                                        await _resultEnumerator.DisposeAsync().ConfigureAwait(false);
                                    }

                                    var inner = _selector(_sourceEnumerator.Current);
                                    _resultEnumerator = inner.GetAsyncEnumerator(_cancellationToken);

                                    _mode = State_Result;
                                    goto case State_Result;
                                }
                                break;

                            case State_Result:
                                if (await _resultEnumerator!.MoveNextAsync().ConfigureAwait(false))
                                {
                                    _current = _resultEnumerator.Current;
                                    return true;
                                }

                                _mode = State_Source;
                                goto case State_Source; // loop
                        }

                        break;
                }

                await DisposeAsync().ConfigureAwait(false);
                return false;
            }
        }

        private sealed class SelectManyAsyncIteratorWithTask<TSource, TResult> : AsyncIterator<TResult>, IAsyncIListProvider<TResult>
        {
            private const int State_Source = 1;
            private const int State_Result = 2;

            private readonly Func<TSource, ValueTask<IAsyncEnumerable<TResult>>> _selector;
            private readonly IAsyncEnumerable<TSource> _source;

            private int _mode;
            private IAsyncEnumerator<TResult>? _resultEnumerator;
            private IAsyncEnumerator<TSource>? _sourceEnumerator;

            public SelectManyAsyncIteratorWithTask(IAsyncEnumerable<TSource> source, Func<TSource, ValueTask<IAsyncEnumerable<TResult>>> selector)
            {
                _source = source;
                _selector = selector;
            }

            public override AsyncIteratorBase<TResult> Clone()
            {
                return new SelectManyAsyncIteratorWithTask<TSource, TResult>(_source, _selector);
            }

            public override async ValueTask DisposeAsync()
            {
                if (_resultEnumerator != null)
                {
                    await _resultEnumerator.DisposeAsync().ConfigureAwait(false);
                    _resultEnumerator = null;
                }

                if (_sourceEnumerator != null)
                {
                    await _sourceEnumerator.DisposeAsync().ConfigureAwait(false);
                    _sourceEnumerator = null;
                }

                await base.DisposeAsync().ConfigureAwait(false);
            }

            public ValueTask<int> GetCountAsync(bool onlyIfCheap, CancellationToken cancellationToken)
            {
                if (onlyIfCheap)
                {
                    return new ValueTask<int>(-1);
                }

                return Core(cancellationToken);

                async ValueTask<int> Core(CancellationToken cancellationToken)
                {
                    var count = 0;

                    await foreach (var element in _source.WithCancellation(cancellationToken).ConfigureAwait(false))
                    {
                        var items = await _selector(element).ConfigureAwait(false);

                        checked
                        {
                            count += await items.CountAsync().ConfigureAwait(false);
                        }
                    }

                    return count;
                }
            }

            public async ValueTask<TResult[]> ToArrayAsync(CancellationToken cancellationToken)
            {
                // REVIEW: Substitute for SparseArrayBuilder<T> logic once we have access to that.

                var list = await ToListAsync(cancellationToken).ConfigureAwait(false);

                return list.ToArray();
            }

            public async ValueTask<List<TResult>> ToListAsync(CancellationToken cancellationToken)
            {
                var list = new List<TResult>();

                await foreach (var element in _source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var items = await _selector(element).ConfigureAwait(false);

                    await list.AddRangeAsync(items, cancellationToken).ConfigureAwait(false);
                }

                return list;
            }

            protected override async ValueTask<bool> MoveNextCore()
            {
                switch (_state)
                {
                    case AsyncIteratorState.Allocated:
                        _sourceEnumerator = _source.GetAsyncEnumerator(_cancellationToken);
                        _mode = State_Source;
                        _state = AsyncIteratorState.Iterating;
                        goto case AsyncIteratorState.Iterating;

                    case AsyncIteratorState.Iterating:
                        switch (_mode)
                        {
                            case State_Source:
                                if (await _sourceEnumerator!.MoveNextAsync().ConfigureAwait(false))
                                {
                                    if (_resultEnumerator != null)
                                    {
                                        await _resultEnumerator.DisposeAsync().ConfigureAwait(false);
                                    }

                                    var inner = await _selector(_sourceEnumerator.Current).ConfigureAwait(false);
                                    _resultEnumerator = inner.GetAsyncEnumerator(_cancellationToken);

                                    _mode = State_Result;
                                    goto case State_Result;
                                }
                                break;

                            case State_Result:
                                if (await _resultEnumerator!.MoveNextAsync().ConfigureAwait(false))
                                {
                                    _current = _resultEnumerator.Current;
                                    return true;
                                }

                                _mode = State_Source;
                                goto case State_Source; // loop
                        }

                        break;
                }

                await DisposeAsync().ConfigureAwait(false);
                return false;
            }
        }

#if !NO_DEEP_CANCELLATION
        private sealed class SelectManyAsyncIteratorWithTaskAndCancellation<TSource, TResult> : AsyncIterator<TResult>, IAsyncIListProvider<TResult>
        {
            private const int State_Source = 1;
            private const int State_Result = 2;

            private readonly Func<TSource, CancellationToken, ValueTask<IAsyncEnumerable<TResult>>> _selector;
            private readonly IAsyncEnumerable<TSource> _source;

            private int _mode;
            private IAsyncEnumerator<TResult>? _resultEnumerator;
            private IAsyncEnumerator<TSource>? _sourceEnumerator;

            public SelectManyAsyncIteratorWithTaskAndCancellation(IAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, ValueTask<IAsyncEnumerable<TResult>>> selector)
            {
                _source = source;
                _selector = selector;
            }

            public override AsyncIteratorBase<TResult> Clone()
            {
                return new SelectManyAsyncIteratorWithTaskAndCancellation<TSource, TResult>(_source, _selector);
            }

            public override async ValueTask DisposeAsync()
            {
                if (_resultEnumerator != null)
                {
                    await _resultEnumerator.DisposeAsync().ConfigureAwait(false);
                    _resultEnumerator = null;
                }

                if (_sourceEnumerator != null)
                {
                    await _sourceEnumerator.DisposeAsync().ConfigureAwait(false);
                    _sourceEnumerator = null;
                }

                await base.DisposeAsync().ConfigureAwait(false);
            }

            public ValueTask<int> GetCountAsync(bool onlyIfCheap, CancellationToken cancellationToken)
            {
                if (onlyIfCheap)
                {
                    return new ValueTask<int>(-1);
                }

                return Core(cancellationToken);

                async ValueTask<int> Core(CancellationToken cancellationToken)
                {
                    var count = 0;

                    await foreach (var element in _source.WithCancellation(cancellationToken).ConfigureAwait(false))
                    {
                        var items = await _selector(element, cancellationToken).ConfigureAwait(false);

                        checked
                        {
                            count += await items.CountAsync().ConfigureAwait(false);
                        }
                    }

                    return count;
                }
            }

            public async ValueTask<TResult[]> ToArrayAsync(CancellationToken cancellationToken)
            {
                // REVIEW: Substitute for SparseArrayBuilder<T> logic once we have access to that.

                var list = await ToListAsync(cancellationToken).ConfigureAwait(false);

                return list.ToArray();
            }

            public async ValueTask<List<TResult>> ToListAsync(CancellationToken cancellationToken)
            {
                var list = new List<TResult>();

                await foreach (var element in _source.WithCancellation(cancellationToken).ConfigureAwait(false))
                {
                    var items = await _selector(element, cancellationToken).ConfigureAwait(false);

                    await list.AddRangeAsync(items, cancellationToken).ConfigureAwait(false);
                }

                return list;
            }

            protected override async ValueTask<bool> MoveNextCore()
            {
                switch (_state)
                {
                    case AsyncIteratorState.Allocated:
                        _sourceEnumerator = _source.GetAsyncEnumerator(_cancellationToken);
                        _mode = State_Source;
                        _state = AsyncIteratorState.Iterating;
                        goto case AsyncIteratorState.Iterating;

                    case AsyncIteratorState.Iterating:
                        switch (_mode)
                        {
                            case State_Source:
                                if (await _sourceEnumerator!.MoveNextAsync().ConfigureAwait(false))
                                {
                                    if (_resultEnumerator != null)
                                    {
                                        await _resultEnumerator.DisposeAsync().ConfigureAwait(false);
                                    }

                                    var inner = await _selector(_sourceEnumerator.Current, _cancellationToken).ConfigureAwait(false);
                                    _resultEnumerator = inner.GetAsyncEnumerator(_cancellationToken);

                                    _mode = State_Result;
                                    goto case State_Result;
                                }
                                break;

                            case State_Result:
                                if (await _resultEnumerator!.MoveNextAsync().ConfigureAwait(false))
                                {
                                    _current = _resultEnumerator.Current;
                                    return true;
                                }

                                _mode = State_Source;
                                goto case State_Source; // loop
                        }

                        break;
                }

                await DisposeAsync().ConfigureAwait(false);
                return false;
            }
        }
#endif
    }
}
