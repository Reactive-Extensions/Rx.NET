﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT License.
// See the LICENSE file in the project root for more information. 

using System.Collections.Generic;

namespace System.Reactive.Linq
{
    public partial class AsyncObservable
    {
        public static IAsyncObservable<bool> Contains<TSource>(this IAsyncObservable<TSource> source, TSource element)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return CreateAsyncObservable<bool>.From(
                source,
                element,
                static (source, element, observer) => source.SubscribeSafeAsync(AsyncObserver.Contains(observer, element)));
        }

        public static IAsyncObservable<bool> Contains<TSource>(this IAsyncObservable<TSource> source, TSource element, IEqualityComparer<TSource> comparer)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            return CreateAsyncObservable<bool>.From(
                source,
                (element, comparer),
                static (source, state, observer) => source.SubscribeSafeAsync(AsyncObserver.Contains(observer, state.element, state.comparer)));
        }
    }

    public partial class AsyncObserver
    {
        public static IAsyncObserver<TSource> Contains<TSource>(IAsyncObserver<bool> observer, TSource element)
        {
            if (observer == null)
                throw new ArgumentNullException(nameof(observer));

            return Contains(observer, element, EqualityComparer<TSource>.Default);
        }

        public static IAsyncObserver<TSource> Contains<TSource>(IAsyncObserver<bool> observer, TSource element, IEqualityComparer<TSource> comparer)
        {
            if (observer == null)
                throw new ArgumentNullException(nameof(observer));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            return Create<TSource>(
                async x =>
                {
                    var equals = false;
                    try
                    {
                        equals = comparer.Equals(x, element);
                    }
                    catch (Exception ex)
                    {
                        await observer.OnErrorAsync(ex).ConfigureAwait(false);
                        return;
                    }

                    if (equals)
                    {
                        await observer.OnNextAsync(true).ConfigureAwait(false);
                        await observer.OnCompletedAsync().ConfigureAwait(false);
                    }
                },
                observer.OnErrorAsync,
                async () =>
                {
                    await observer.OnNextAsync(false).ConfigureAwait(false);
                    await observer.OnCompletedAsync().ConfigureAwait(false);
                }
            );
        }
    }
}
