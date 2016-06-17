﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information. 

#if HAS_WINRT
using System.Threading;
using Windows.Foundation;

namespace System.Reactive.Linq
{
    /// <summary>
    /// Provides a set of static methods for importing typed events from Windows Runtime APIs.
    /// </summary>
    public static partial class WindowsObservable
    {
        /// <summary>
        /// Converts a typed event, conforming to the standard event pattern, to an observable sequence.
        /// </summary>
        /// <typeparam name="TSender">The type of the sender that raises the event.</typeparam>
        /// <typeparam name="TResult">The type of the event data generated by the event.</typeparam>
        /// <param name="addHandler">Action that attaches the given event handler to the underlying .NET event.</param>
        /// <param name="removeHandler">Action that detaches the given event handler from the underlying .NET event.</param>
        /// <returns>The observable sequence that contains data representations of invocations of the underlying typed event.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="addHandler"/> or <paramref name="removeHandler"/> is null.</exception>
        /// <seealso cref="WindowsObservable.ToEventPattern"/>
        public static IObservable<EventPattern<TSender, TResult>> FromEventPattern<TSender, TResult>(Action<TypedEventHandler<TSender, TResult>> addHandler, Action<TypedEventHandler<TSender, TResult>> removeHandler)
        {
            if (addHandler == null)
                throw new ArgumentNullException("addHandler");
            if (removeHandler == null)
                throw new ArgumentNullException("removeHandler");

            return Observable.Create<EventPattern<TSender, TResult>>(observer =>
            {
                var h = new TypedEventHandler<TSender, TResult>((sender, args) =>
                {
                    observer.OnNext(new EventPattern<TSender, TResult>(sender, args));
                });

                addHandler(h);

                return () =>
                {
                    removeHandler(h);
                };
            });
        }

        /// <summary>
        /// Converts a typed event, conforming to the standard event pattern, to an observable sequence.
        /// </summary>
        /// <typeparam name="TDelegate">The delegate type of the event to be converted.</typeparam>
        /// <typeparam name="TSender">The type of the sender that raises the event.</typeparam>
        /// <typeparam name="TResult">The type of the event data generated by the event.</typeparam>
        /// <param name="conversion">A function used to convert the given event handler to a delegate compatible with the underlying typed event. The resulting delegate is used in calls to the addHandler and removeHandler action parameters.</param>
        /// <param name="addHandler">Action that attaches the given event handler to the underlying .NET event.</param>
        /// <param name="removeHandler">Action that detaches the given event handler from the underlying .NET event.</param>
        /// <returns>The observable sequence that contains data representations of invocations of the underlying typed event.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="conversion"/> or <paramref name="addHandler"/> or <paramref name="removeHandler"/> is null.</exception>
        /// <seealso cref="WindowsObservable.ToEventPattern"/>
        public static IObservable<EventPattern<TSender, TResult>> FromEventPattern<TDelegate, TSender, TResult>(Func<TypedEventHandler<TSender, TResult>, TDelegate> conversion, Action<TDelegate> addHandler, Action<TDelegate> removeHandler)
        {
            if (conversion == null)
                throw new ArgumentNullException("conversion");
            if (addHandler == null)
                throw new ArgumentNullException("addHandler");
            if (removeHandler == null)
                throw new ArgumentNullException("removeHandler");

            return Observable.Create<EventPattern<TSender, TResult>>(observer =>
            {
                var h = conversion(new TypedEventHandler<TSender, TResult>((sender, args) =>
                {
                    observer.OnNext(new EventPattern<TSender, TResult>(sender, args));
                }));

                addHandler(h);

                return () =>
                {
                    removeHandler(h);
                };
            });
        }

        /// <summary>
        /// Exposes an observable sequence as an object with a typed event.
        /// </summary>
        /// <typeparam name="TSender">The type of the sender that raises the event.</typeparam>
        /// <typeparam name="TEventArgs">The type of the event data generated by the event.</typeparam>
        /// <param name="source">Observable source sequence.</param>
        /// <returns>The event source object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static IEventPatternSource<TSender, TEventArgs> ToEventPattern<TSender, TEventArgs>(this IObservable<EventPattern<TSender, TEventArgs>> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            return new EventPatternSource<TSender, TEventArgs>(source, (h, evt) => h(evt.Sender, evt.EventArgs));
        }
    }
}
#endif