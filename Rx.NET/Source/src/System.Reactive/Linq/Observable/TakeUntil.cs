﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information. 

using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Threading;

namespace System.Reactive.Linq.ObservableImpl
{
    internal sealed class TakeUntil<TSource, TOther> : Producer<TSource, TakeUntil<TSource, TOther>._>
    {
        private readonly IObservable<TSource> _source;
        private readonly IObservable<TOther> _other;

        public TakeUntil(IObservable<TSource> source, IObservable<TOther> other)
        {
            _source = source;
            _other = other;
        }

        protected override _ CreateSink(IObserver<TSource> observer, IDisposable cancel) => new _(observer, cancel);

        protected override IDisposable Run(_ sink) => sink.Run(this);

        internal sealed class _ : IdentitySink<TSource>
        {
            private readonly OtherObserver _other;

            private IDisposable _mainDisposable;

            private int _halfSerializer;

            private Exception _error;

            public _(IObserver<TSource> observer, IDisposable cancel)
                : base(observer, cancel)
            {
                _other = new OtherObserver(this);
            }

            public IDisposable Run(TakeUntil<TSource, TOther> parent)
            {
                _other.OnSubscribe(parent._other.Subscribe(_other));

                Disposable.TrySetSingle(ref _mainDisposable, parent._source.Subscribe(this));

                return this;
            }

            protected override void Dispose(bool disposing)
            {
                base.Dispose(disposing);
                if (!Disposable.GetIsDisposed(ref _mainDisposable))
                {
                    Disposable.TryDispose(ref _mainDisposable);
                }
                _other.Dispose();
            }

            public override void OnNext(TSource value)
            {
                if (Interlocked.CompareExchange(ref _halfSerializer, 1, 0) == 0)
                {
                    ForwardOnNext(value);
                    if (Interlocked.Decrement(ref _halfSerializer) != 0)
                    {
                        var ex = _error;
                        if (ex != TakeUntilTerminalException.Instance)
                        {
                            _error = TakeUntilTerminalException.Instance;
                            ForwardOnError(ex);
                        }
                        else
                        {
                            ForwardOnCompleted();
                        }
                    }
                }
            }

            public override void OnError(Exception ex)
            {
                if (Interlocked.CompareExchange(ref _error, ex, null) == null)
                {
                    if (Interlocked.Increment(ref _halfSerializer) == 1)
                    {
                        _error = TakeUntilTerminalException.Instance;
                        ForwardOnError(ex);
                    }
                }
            }

            public override void OnCompleted()
            {
                if (Interlocked.CompareExchange(ref _error, TakeUntilTerminalException.Instance, null) == null)
                {
                    if (Interlocked.Increment(ref _halfSerializer) == 1)
                    {
                        ForwardOnCompleted();
                    }
                }
            }

            sealed class OtherObserver : IObserver<TOther>, IDisposable
            {
                readonly _ _parent;

                IDisposable _upstream;

                public OtherObserver(_ parent)
                {
                    _parent = parent;
                }

                public void Dispose()
                {
                    if (!Disposable.GetIsDisposed(ref _upstream))
                    {
                        Disposable.TryDispose(ref _upstream);
                    }
                }

                public void OnSubscribe(IDisposable d)
                {
                    Disposable.TrySetSingle(ref _upstream, d);
                }

                public void OnCompleted()
                {
                    // Completion doesn't mean termination in Rx.NET for this operator
                    Dispose();
                }

                public void OnError(Exception error)
                {
                    _parent.OnError(error);
                }

                public void OnNext(TOther value)
                {
                    _parent.OnCompleted();
                }
            }

        }
    }

    internal static class TakeUntilTerminalException
    {
        internal static readonly Exception Instance = new Exception("No further exceptions");
    }

    internal sealed class TakeUntil<TSource> : Producer<TSource, TakeUntil<TSource>._>
    {
        private readonly IObservable<TSource> _source;
        private readonly DateTimeOffset _endTime;
        internal readonly IScheduler _scheduler;

        public TakeUntil(IObservable<TSource> source, DateTimeOffset endTime, IScheduler scheduler)
        {
            _source = source;
            _endTime = endTime;
            _scheduler = scheduler;
        }

        public IObservable<TSource> Combine(DateTimeOffset endTime)
        {
            //
            // Minimum semantics:
            //
            //   t                     0--1--2--3--4--5--6--7->   t                     0--1--2--3--4--5--6--7->
            //
            //   xs                    --o--o--o--o--o--o--|      xs                    --o--o--o--o--o--o--|
            //   xs.TU(5AM)            --o--o--o--o--o|           xs.TU(3AM)            --o--o--o|
            //   xs.TU(5AM).TU(3AM)    --o--o--o|                 xs.TU(3AM).TU(5AM)    --o--o--o|
            //
            if (_endTime <= endTime)
                return this;
            else
                return new TakeUntil<TSource>(_source, endTime, _scheduler);
        }

        protected override _ CreateSink(IObserver<TSource> observer, IDisposable cancel) => new _(observer, cancel);

        protected override IDisposable Run(_ sink) => sink.Run(this);

        internal sealed class _ : IdentitySink<TSource>
        {
            private readonly object _gate = new object();

            public _(IObserver<TSource> observer, IDisposable cancel)
                : base(observer, cancel)
            {
            }

            public IDisposable Run(TakeUntil<TSource> parent)
            {
                var t = parent._scheduler.Schedule(parent._endTime, Tick);
                var d = parent._source.SubscribeSafe(this);
                return StableCompositeDisposable.Create(t, d);
            }

            private void Tick()
            {
                lock (_gate)
                {
                    ForwardOnCompleted();
                }
            }

            public override void OnNext(TSource value)
            {
                lock (_gate)
                {
                    ForwardOnNext(value);
                }
            }

            public override void OnError(Exception error)
            {
                lock (_gate)
                {
                    ForwardOnError(error);
                }
            }

            public override void OnCompleted()
            {
                lock (_gate)
                {
                    ForwardOnCompleted();
                }
            }
        }
    }
}
