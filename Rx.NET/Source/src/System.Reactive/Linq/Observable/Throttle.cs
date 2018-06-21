﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information. 

using System.Reactive.Concurrency;
using System.Reactive.Disposables;

namespace System.Reactive.Linq.ObservableImpl
{
    internal sealed class Throttle<TSource> : Producer<TSource, Throttle<TSource>._>
    {
        private readonly IObservable<TSource> _source;
        private readonly TimeSpan _dueTime;
        private readonly IScheduler _scheduler;

        public Throttle(IObservable<TSource> source, TimeSpan dueTime, IScheduler scheduler)
        {
            _source = source;
            _dueTime = dueTime;
            _scheduler = scheduler;
        }

        protected override _ CreateSink(IObserver<TSource> observer) => new _(this, observer);

        protected override void Run(_ sink) => sink.Run(_source);

        internal sealed class _ : IdentitySink<TSource>
        {
            private readonly TimeSpan _dueTime;
            private readonly IScheduler _scheduler;

            public _(Throttle<TSource> parent, IObserver<TSource> observer)
                : base(observer)
            {
                _dueTime = parent._dueTime;
                _scheduler = parent._scheduler;
            }

            private readonly object _gate = new object();
            private TSource _value;
            private bool _hasValue;
            private IDisposable _serialCancelable;
            private ulong _id;

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    Disposable.TryDispose(ref _serialCancelable);
                }
                base.Dispose(disposing);
            }

            public override void OnNext(TSource value)
            {
                var currentid = default(ulong);
                lock (_gate)
                {
                    _hasValue = true;
                    _value = value;
                    _id = unchecked(_id + 1);
                    currentid = _id;
                }
                var d = new SingleAssignmentDisposable();
                Disposable.TrySetSerial(ref _serialCancelable, d);
                d.Disposable = _scheduler.Schedule(currentid, _dueTime, Propagate);
            }

            private IDisposable Propagate(IScheduler self, ulong currentid)
            {
                lock (_gate)
                {
                    if (_hasValue && _id == currentid)
                        ForwardOnNext(_value);
                    _hasValue = false;
                }

                return Disposable.Empty;
            }

            public override void OnError(Exception error)
            {
                Disposable.TryDispose(ref _serialCancelable);

                lock (_gate)
                {
                    ForwardOnError(error);

                    _hasValue = false;
                    _id = unchecked(_id + 1);
                }
            }
            
            public override void OnCompleted()
            {
                Disposable.TryDispose(ref _serialCancelable);

                lock (_gate)
                {
                    if (_hasValue)
                        ForwardOnNext(_value);

                    ForwardOnCompleted();

                    _hasValue = false;
                    _id = unchecked(_id + 1);
                }
            }
        }
    }

    internal sealed class Throttle<TSource, TThrottle> : Producer<TSource, Throttle<TSource, TThrottle>._>
    {
        private readonly IObservable<TSource> _source;
        private readonly Func<TSource, IObservable<TThrottle>> _throttleSelector;

        public Throttle(IObservable<TSource> source, Func<TSource, IObservable<TThrottle>> throttleSelector)
        {
            _source = source;
            _throttleSelector = throttleSelector;
        }

        protected override _ CreateSink(IObserver<TSource> observer) => new _(this, observer);

        protected override void Run(_ sink) => sink.Run(this);

        internal sealed class _ : IdentitySink<TSource>
        {
            private readonly Func<TSource, IObservable<TThrottle>> _throttleSelector;

            public _(Throttle<TSource, TThrottle> parent, IObserver<TSource> observer)
                : base(observer)
            {
                _throttleSelector = parent._throttleSelector;
            }

            private object _gate;
            private TSource _value;
            private bool _hasValue;
            private IDisposable _serialCancelable;
            private ulong _id;

            public void Run(Throttle<TSource, TThrottle> parent)
            {
                _gate = new object();
                _value = default(TSource);
                _hasValue = false;
                _id = 0UL;

                base.Run(parent._source);
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    Disposable.TryDispose(ref _serialCancelable);
                }
                base.Dispose(disposing);
            }

            public override void OnNext(TSource value)
            {
                var throttle = default(IObservable<TThrottle>);
                try
                {
                    throttle = _throttleSelector(value);
                }
                catch (Exception error)
                {
                    lock (_gate)
                    {
                        ForwardOnError(error);
                    }

                    return;
                }

                ulong currentid;
                lock (_gate)
                {
                    _hasValue = true;
                    _value = value;
                    _id = unchecked(_id + 1);
                    currentid = _id;
                }

                var d = new SingleAssignmentDisposable();
                Disposable.TrySetSerial(ref _serialCancelable, d);
                d.Disposable = throttle.SubscribeSafe(new ThrottleObserver(this, value, currentid, d));
            }

            public override void OnError(Exception error)
            {
                Disposable.TryDispose(ref _serialCancelable);

                lock (_gate)
                {
                    ForwardOnError(error);

                    _hasValue = false;
                    _id = unchecked(_id + 1);
                }
            }

            public override void OnCompleted()
            {
                Disposable.TryDispose(ref _serialCancelable);

                lock (_gate)
                {
                    if (_hasValue)
                        ForwardOnNext(_value);

                    ForwardOnCompleted();

                    _hasValue = false;
                    _id = unchecked(_id + 1);
                }
            }

            private sealed class ThrottleObserver : IObserver<TThrottle>
            {
                private readonly _ _parent;
                private readonly TSource _value;
                private readonly ulong _currentid;
                private readonly IDisposable _self;

                public ThrottleObserver(_ parent, TSource value, ulong currentid, IDisposable self)
                {
                    _parent = parent;
                    _value = value;
                    _currentid = currentid;
                    _self = self;
                }

                public void OnNext(TThrottle value)
                {
                    lock (_parent._gate)
                    {
                        if (_parent._hasValue && _parent._id == _currentid)
                            _parent.ForwardOnNext(_value);

                        _parent._hasValue = false;
                        _self.Dispose();
                    }
                }

                public void OnError(Exception error)
                {
                    lock (_parent._gate)
                    {
                        _parent.ForwardOnError(error);
                    }
                }

                public void OnCompleted()
                {
                    lock (_parent._gate)
                    {
                        if (_parent._hasValue && _parent._id == _currentid)
                            _parent.ForwardOnNext(_value);

                        _parent._hasValue = false;
                        _self.Dispose();
                    }
                }
            }
        }
    }
}
