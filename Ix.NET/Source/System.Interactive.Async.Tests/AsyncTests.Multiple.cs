﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information. 

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xunit;
using System.Threading;
using System.Threading.Tasks;

namespace Tests
{
    public partial class AsyncTests
    {
        [Fact]
        public void Concat_Null()
        {
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Concat<int>(null, AsyncEnumerable.Return(42)));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Concat<int>(AsyncEnumerable.Return(42), null));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Concat<int>(default(IAsyncEnumerable<int>[])));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Concat<int>(default(IEnumerable<IAsyncEnumerable<int>>)));
        }

        [Fact]
        public void Concat1()
        {
            var ys = new[] { 1, 2, 3 }.ToAsyncEnumerable().Concat(new[] { 4, 5, 6 }.ToAsyncEnumerable());

            var e = ys.GetEnumerator();
            HasNext(e, 1);
            HasNext(e, 2);
            HasNext(e, 3);
            HasNext(e, 4);
            HasNext(e, 5);
            HasNext(e, 6);
            NoNext(e);
        }

        [Fact]
        public void Concat2()
        {
            var ex = new Exception("Bang");
            var ys = new[] { 1, 2, 3 }.ToAsyncEnumerable().Concat(AsyncEnumerable.Throw<int>(ex));

            var e = ys.GetEnumerator();
            HasNext(e, 1);
            HasNext(e, 2);
            HasNext(e, 3);
            AssertThrows<Exception>(() => e.MoveNext().Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void Concat3()
        {
            var ex = new Exception("Bang");
            var ys = AsyncEnumerable.Throw<int>(ex).Concat(new[] { 4, 5, 6 }.ToAsyncEnumerable());

            var e = ys.GetEnumerator();
            AssertThrows<Exception>(() => e.MoveNext().Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void Concat4()
        {
            var xs = new[] { 1, 2, 3 }.ToAsyncEnumerable();
            var ys = new[] { 4, 5 }.ToAsyncEnumerable();
            var zs = new[] { 6, 7, 8 }.ToAsyncEnumerable();

            var res = AsyncEnumerable.Concat(xs, ys, zs);

            var e = res.GetEnumerator();
            HasNext(e, 1);
            HasNext(e, 2);
            HasNext(e, 3);
            HasNext(e, 4);
            HasNext(e, 5);
            HasNext(e, 6);
            HasNext(e, 7);
            HasNext(e, 8);
            NoNext(e);
        }

        [Fact]
        public void Concat5()
        {
            var ex = new Exception("Bang");
            var xs = new[] { 1, 2, 3 }.ToAsyncEnumerable();
            var ys = new[] { 4, 5 }.ToAsyncEnumerable();
            var zs = AsyncEnumerable.Throw<int>(ex);

            var res = AsyncEnumerable.Concat(xs, ys, zs);

            var e = res.GetEnumerator();
            HasNext(e, 1);
            HasNext(e, 2);
            HasNext(e, 3);
            HasNext(e, 4);
            HasNext(e, 5);
            AssertThrows<Exception>(() => e.MoveNext().Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void Concat6()
        {
            var res = AsyncEnumerable.Concat(ConcatXss());

            var e = res.GetEnumerator();
            HasNext(e, 1);
            HasNext(e, 2);
            HasNext(e, 3);
            HasNext(e, 4);
            HasNext(e, 5);
            AssertThrows<Exception>(() => e.MoveNext().Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single().Message == "Bang!");
        }

        [Fact]
        public void Concat7()
        {
            var ws = new[] { 1, 2, 3 }.ToAsyncEnumerable();
            var xs = new[] { 4, 5 }.ToAsyncEnumerable();
            var ys = new[] { 6, 7, 8 }.ToAsyncEnumerable();
            var zs = new[] { 9, 10, 11 }.ToAsyncEnumerable();

            var res = ws.Concat(xs).Concat(ys).Concat(zs);

            var e = res.GetEnumerator();
            HasNext(e, 1);
            HasNext(e, 2);
            HasNext(e, 3);
            HasNext(e, 4);
            HasNext(e, 5);
            HasNext(e, 6);
            HasNext(e, 7);
            HasNext(e, 8);
            HasNext(e, 9);
            HasNext(e, 10);
            HasNext(e, 11);
            NoNext(e);
        }

        [Fact]
        public async Task Concat8()
        {
            var ws = new[] { 1, 2, 3 }.ToAsyncEnumerable();
            var xs = new[] { 4, 5 }.ToAsyncEnumerable();
            var ys = new[] { 6, 7, 8 }.ToAsyncEnumerable();
            var zs = new[] { 9, 10, 11 }.ToAsyncEnumerable();

            var res = ws.Concat(xs).Concat(ys).Concat(zs);

            await SequenceIdentity(res);
        }

        [Fact]
        public async Task Concat9()
        {
            var xs = new[] { 1, 2, 3 }.ToAsyncEnumerable();
            var ys = new[] { 4, 5 }.ToAsyncEnumerable();
            var zs = new[] { 6, 7, 8 }.ToAsyncEnumerable();

            var res = AsyncEnumerable.Concat(xs, ys, zs);

            await SequenceIdentity(res);
        }

        [Fact]
        public async Task Concat10()
        {
            var xs = new[] { 1, 2, 3 }.ToAsyncEnumerable();
            var ys = new[] { 4, 5 }.ToAsyncEnumerable();
            var zs = new[] { 6, 7, 8 }.ToAsyncEnumerable();

            var c = xs.Concat(ys).Concat(zs);

            var res = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            Assert.True(res.SequenceEqual(await c.ToArray()));
        }

        [Fact]
        public async Task Concat11()
        {
            var xs = new[] { 1, 2, 3 }.ToAsyncEnumerable();
            var ys = new[] { 4, 5 }.ToAsyncEnumerable();
            var zs = new[] { 6, 7, 8 }.ToAsyncEnumerable();

            var c = xs.Concat(ys).Concat(zs);

            var res = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            Assert.True(res.SequenceEqual(await c.ToList()));
        }

        [Fact]
        public async Task Concat12()
        {
            var xs = new[] { 1, 2, 3 }.ToAsyncEnumerable();
            var ys = new[] { 4, 5 }.ToAsyncEnumerable();
            var zs = new[] { 6, 7, 8 }.ToAsyncEnumerable();

            var c = xs.Concat(ys).Concat(zs);

            Assert.Equal(8, await c.Count());
        }


        static IEnumerable<IAsyncEnumerable<int>> ConcatXss()
        {
            yield return new[] { 1, 2, 3 }.ToAsyncEnumerable();
            yield return new[] { 4, 5 }.ToAsyncEnumerable();
            throw new Exception("Bang!");
        }

        [Fact]
        public void Zip_Null()
        {
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Zip<int, int, int>(null, AsyncEnumerable.Return(42), (x, y) => x + y));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Zip<int, int, int>(AsyncEnumerable.Return(42), null, (x, y) => x + y));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Zip<int, int, int>(AsyncEnumerable.Return(42), AsyncEnumerable.Return(42), null));
        }

        [Fact]
        public void Zip1()
        {
            var xs = new[] { 1, 2, 3 }.ToAsyncEnumerable();
            var ys = new[] { 4, 5, 6 }.ToAsyncEnumerable();
            var res = xs.Zip(ys, (x, y) => x * y);

            var e = res.GetEnumerator();
            HasNext(e, 1 * 4);
            HasNext(e, 2 * 5);
            HasNext(e, 3 * 6);
            NoNext(e);
        }

        [Fact]
        public void Zip2()
        {
            var xs = new[] { 1, 2, 3 }.ToAsyncEnumerable();
            var ys = new[] { 4, 5, 6, 7 }.ToAsyncEnumerable();
            var res = xs.Zip(ys, (x, y) => x * y);

            var e = res.GetEnumerator();
            HasNext(e, 1 * 4);
            HasNext(e, 2 * 5);
            HasNext(e, 3 * 6);
            NoNext(e);
        }

        [Fact]
        public void Zip3()
        {
            var xs = new[] { 1, 2, 3, 4 }.ToAsyncEnumerable();
            var ys = new[] { 4, 5, 6 }.ToAsyncEnumerable();
            var res = xs.Zip(ys, (x, y) => x * y);

            var e = res.GetEnumerator();
            HasNext(e, 1 * 4);
            HasNext(e, 2 * 5);
            HasNext(e, 3 * 6);
            NoNext(e);
        }

        [Fact]
        public void Zip4()
        {
            var ex = new Exception("Bang!");
            var xs = new[] { 1, 2, 3, 4 }.ToAsyncEnumerable();
            var ys = AsyncEnumerable.Throw<int>(ex);
            var res = xs.Zip(ys, (x, y) => x * y);

            var e = res.GetEnumerator();
            AssertThrows<Exception>(() => e.MoveNext().Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void Zip5()
        {
            var ex = new Exception("Bang!");
            var xs = AsyncEnumerable.Throw<int>(ex);
            var ys = new[] { 1, 2, 3, 4 }.ToAsyncEnumerable();
            var res = xs.Zip(ys, (x, y) => x * y);

            var e = res.GetEnumerator();
            AssertThrows<Exception>(() => e.MoveNext().Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void Zip6()
        {
            var ex = new Exception("Bang!");
            var xs = new[] { 1, 2, 3, 4 }.ToAsyncEnumerable();
            var ys = new[] { 1, 2, 3, 4 }.ToAsyncEnumerable();
            var res = xs.Zip(ys, (x, y) => { if (x > 0) throw ex; return x * y; });

            var e = res.GetEnumerator();
            AssertThrows<Exception>(() => e.MoveNext().Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public async Task Zip7()
        {
            var xs = new[] { 1, 2, 3 }.ToAsyncEnumerable();
            var ys = new[] { 4, 5, 6 }.ToAsyncEnumerable();
            var res = xs.Zip(ys, (x, y) => x * y);

            await SequenceIdentity(res);
        }

        [Fact]
        public void Union_Null()
        {
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Union<int>(null, AsyncEnumerable.Return(42)));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Union<int>(AsyncEnumerable.Return(42), null));

            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Union<int>(null, AsyncEnumerable.Return(42), new Eq()));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Union<int>(AsyncEnumerable.Return(42), null, new Eq()));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Union<int>(AsyncEnumerable.Return(42), AsyncEnumerable.Return(42), null));
        }

        [Fact]
        public void Union1()
        {
            var xs = new[] { 1, 2, 3 }.ToAsyncEnumerable();
            var ys = new[] { 3, 5, 1, 4 }.ToAsyncEnumerable();
            var res = xs.Union(ys);

            var e = res.GetEnumerator();
            HasNext(e, 1);
            HasNext(e, 2);
            HasNext(e, 3);
            HasNext(e, 5);
            HasNext(e, 4);
            NoNext(e);
        }

        [Fact]
        public void Union2()
        {
            var xs = new[] { 1, 2, -3 }.ToAsyncEnumerable();
            var ys = new[] { 3, 5, -1, 4 }.ToAsyncEnumerable();
            var res = xs.Union(ys, new Eq());

            var e = res.GetEnumerator();
            HasNext(e, 1);
            HasNext(e, 2);
            HasNext(e, -3);
            HasNext(e, 5);
            HasNext(e, 4);
            NoNext(e);
        }

        [Fact]
        public void Intersect_Null()
        {
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Intersect<int>(null, AsyncEnumerable.Return(42)));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Intersect<int>(AsyncEnumerable.Return(42), null));

            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Intersect<int>(null, AsyncEnumerable.Return(42), new Eq()));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Intersect<int>(AsyncEnumerable.Return(42), null, new Eq()));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Intersect<int>(AsyncEnumerable.Return(42), AsyncEnumerable.Return(42), null));
        }

        [Fact]
        public void Intersect1()
        {
            var xs = new[] { 1, 2, 3 }.ToAsyncEnumerable();
            var ys = new[] { 3, 5, 1, 4 }.ToAsyncEnumerable();
            var res = xs.Intersect(ys);

            var e = res.GetEnumerator();
            HasNext(e, 1);
            HasNext(e, 3);
            NoNext(e);
        }

        [Fact]
        public void Intersect2()
        {
            var xs = new[] { 1, 2, -3 }.ToAsyncEnumerable();
            var ys = new[] { 3, 5, -1, 4 }.ToAsyncEnumerable();
            var res = xs.Intersect(ys, new Eq());

            var e = res.GetEnumerator();
            HasNext(e, 1);
            HasNext(e, -3);
            NoNext(e);
        }

        [Fact]
        public async Task Intersect3()
        {
            var xs = new[] { 1, 2, 3 }.ToAsyncEnumerable();
            var ys = new[] { 3, 5, 1, 4 }.ToAsyncEnumerable();
            var res = xs.Intersect(ys);

            await SequenceIdentity(res);
        }


        [Fact]
        public void Except_Null()
        {
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Except<int>(null, AsyncEnumerable.Return(42)));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Except<int>(AsyncEnumerable.Return(42), null));

            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Except<int>(null, AsyncEnumerable.Return(42), new Eq()));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Except<int>(AsyncEnumerable.Return(42), null, new Eq()));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Except<int>(AsyncEnumerable.Return(42), AsyncEnumerable.Return(42), null));
        }

        [Fact]
        public void Except1()
        {
            var xs = new[] { 1, 2, 3 }.ToAsyncEnumerable();
            var ys = new[] { 3, 5, 1, 4 }.ToAsyncEnumerable();
            var res = xs.Except(ys);

            var e = res.GetEnumerator();
            HasNext(e, 2);
            NoNext(e);
        }

        [Fact]
        public void Except2()
        {
            var xs = new[] { 1, 2, -3 }.ToAsyncEnumerable();
            var ys = new[] { 3, 5, -1, 4 }.ToAsyncEnumerable();
            var res = xs.Except(ys, new Eq());

            var e = res.GetEnumerator();
            HasNext(e, 2);
            NoNext(e);
        }

        [Fact]
        public async Task Except3()
        {
            var xs = new[] { 1, 2, 3 }.ToAsyncEnumerable();
            var ys = new[] { 3, 5, 1, 4 }.ToAsyncEnumerable();
            var res = xs.Except(ys);

            await SequenceIdentity(res);
        }

        [Fact]
        public async Task SequenceEqual_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.SequenceEqual<int>(null, AsyncEnumerable.Return(42)));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.SequenceEqual<int>(AsyncEnumerable.Return(42), null));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.SequenceEqual<int>(null, AsyncEnumerable.Return(42), new Eq()));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.SequenceEqual<int>(AsyncEnumerable.Return(42), null, new Eq()));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.SequenceEqual<int>(AsyncEnumerable.Return(42), AsyncEnumerable.Return(42), null));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.SequenceEqual<int>(null, AsyncEnumerable.Return(42), CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.SequenceEqual<int>(AsyncEnumerable.Return(42), null, CancellationToken.None));

            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.SequenceEqual<int>(null, AsyncEnumerable.Return(42), new Eq(), CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.SequenceEqual<int>(AsyncEnumerable.Return(42), null, new Eq(), CancellationToken.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => AsyncEnumerable.SequenceEqual<int>(AsyncEnumerable.Return(42), AsyncEnumerable.Return(42), null, CancellationToken.None));
        }

        [Fact]
        public void SequenceEqual1()
        {
            var xs = new[] { 1, 2, 3 }.ToAsyncEnumerable();
            var res = xs.SequenceEqual(xs);
            Assert.True(res.Result);
        }

        [Fact]
        public void SequenceEqual2()
        {
            var xs = AsyncEnumerable.Empty<int>();
            var res = xs.SequenceEqual(xs);
            Assert.True(res.Result);
        }

        [Fact]
        public void SequenceEqual3()
        {
            var xs = new[] { 1, 2, 3 }.ToAsyncEnumerable();
            var ys = new[] { 1, 3, 2 }.ToAsyncEnumerable();
            var res = xs.SequenceEqual(ys);
            Assert.False(res.Result);
        }

        [Fact]
        public void SequenceEqual4()
        {
            var xs = new[] { 1, 2, 3, 4 }.ToAsyncEnumerable();
            var ys = new[] { 1, 2, 3 }.ToAsyncEnumerable();
            var res = xs.SequenceEqual(ys);
            Assert.False(res.Result);
        }

        [Fact]
        public void SequenceEqual5()
        {
            var xs = new[] { 1, 2, 3 }.ToAsyncEnumerable();
            var ys = new[] { 1, 2, 3, 4 }.ToAsyncEnumerable();
            var res = xs.SequenceEqual(ys);
            Assert.False(res.Result);
        }

        [Fact]
        public void SequenceEqual6()
        {
            var ex = new Exception("Bang!");
            var xs = new[] { 1, 2, 3, 4 }.ToAsyncEnumerable();
            var ys = AsyncEnumerable.Throw<int>(ex);
            var res = xs.SequenceEqual(ys);

            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void SequenceEqual7()
        {
            var ex = new Exception("Bang!");
            var xs = AsyncEnumerable.Throw<int>(ex);
            var ys = new[] { 1, 2, 3, 4 }.ToAsyncEnumerable();
            var res = xs.SequenceEqual(ys);

            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void SequenceEqual8()
        {
            var xs = new[] { 1, 2, 3 }.ToAsyncEnumerable();
            var res = xs.SequenceEqual(xs, new Eq());
            Assert.True(res.Result);
        }

        [Fact]
        public void SequenceEqual9()
        {
            var xs = AsyncEnumerable.Empty<int>();
            var res = xs.SequenceEqual(xs, new Eq());
            Assert.True(res.Result);
        }

        [Fact]
        public void SequenceEqual10()
        {
            var xs = new[] { 1, 2, 3 }.ToAsyncEnumerable();
            var ys = new[] { 1, 3, 2 }.ToAsyncEnumerable();
            var res = xs.SequenceEqual(ys, new Eq());
            Assert.False(res.Result);
        }

        [Fact]
        public void SequenceEqual11()
        {
            var xs = new[] { 1, 2, 3, 4 }.ToAsyncEnumerable();
            var ys = new[] { 1, 2, 3 }.ToAsyncEnumerable();
            var res = xs.SequenceEqual(ys, new Eq());
            Assert.False(res.Result);
        }

        [Fact]
        public void SequenceEqual12()
        {
            var xs = new[] { 1, 2, 3 }.ToAsyncEnumerable();
            var ys = new[] { 1, 2, 3, 4 }.ToAsyncEnumerable();
            var res = xs.SequenceEqual(ys, new Eq());
            Assert.False(res.Result);
        }

        [Fact]
        public void SequenceEqual13()
        {
            var ex = new Exception("Bang!");
            var xs = new[] { 1, 2, 3, 4 }.ToAsyncEnumerable();
            var ys = AsyncEnumerable.Throw<int>(ex);
            var res = xs.SequenceEqual(ys, new Eq());

            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void SequenceEqual14()
        {
            var ex = new Exception("Bang!");
            var xs = AsyncEnumerable.Throw<int>(ex);
            var ys = new[] { 1, 2, 3, 4 }.ToAsyncEnumerable();
            var res = xs.SequenceEqual(ys, new Eq());

            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void SequenceEqual15()
        {
            var xs = new[] { 1, 2, -3, 4 }.ToAsyncEnumerable();
            var ys = new[] { 1, -2, 3, 4 }.ToAsyncEnumerable();
            var res = xs.SequenceEqual(ys, new Eq());
            Assert.True(res.Result);
        }

        [Fact]
        public void SequenceEqual16()
        {
            var xs = new[] { 1, 2, -3, 4 }.ToAsyncEnumerable();
            var ys = new[] { 1, -2, 3, 4 }.ToAsyncEnumerable();
            var res = xs.SequenceEqual(ys, new EqEx());
            AssertThrows<Exception>(() => res.Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() is NotImplementedException);
        }

        class EqEx : IEqualityComparer<int>
        {
            public bool Equals(int x, int y)
            {
                throw new NotImplementedException();
            }

            public int GetHashCode(int obj)
            {
                throw new NotImplementedException();
            }
        }
        
        [Fact]
        public void GroupJoin_Null()
        {
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.GroupJoin<int, int, int, int>(null, AsyncEnumerable.Return(42), x => x, x => x, (x, y) => x));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.GroupJoin<int, int, int, int>(AsyncEnumerable.Return(42), null, x => x, x => x, (x, y) => x));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.GroupJoin<int, int, int, int>(AsyncEnumerable.Return(42), AsyncEnumerable.Return(42), null, x => x, (x, y) => x));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.GroupJoin<int, int, int, int>(AsyncEnumerable.Return(42), AsyncEnumerable.Return(42), x => x, null, (x, y) => x));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.GroupJoin<int, int, int, int>(AsyncEnumerable.Return(42), AsyncEnumerable.Return(42), x => x, x => x, null));

            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.GroupJoin<int, int, int, int>(null, AsyncEnumerable.Return(42), x => x, x => x, (x, y) => x, EqualityComparer<int>.Default));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.GroupJoin<int, int, int, int>(AsyncEnumerable.Return(42), null, x => x, x => x, (x, y) => x, EqualityComparer<int>.Default));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.GroupJoin<int, int, int, int>(AsyncEnumerable.Return(42), AsyncEnumerable.Return(42), null, x => x, (x, y) => x, EqualityComparer<int>.Default));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.GroupJoin<int, int, int, int>(AsyncEnumerable.Return(42), AsyncEnumerable.Return(42), x => x, null, (x, y) => x, EqualityComparer<int>.Default));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.GroupJoin<int, int, int, int>(AsyncEnumerable.Return(42), AsyncEnumerable.Return(42), x => x, x => x, null, EqualityComparer<int>.Default));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.GroupJoin<int, int, int, int>(AsyncEnumerable.Return(42), AsyncEnumerable.Return(42), x => x, x => x, (x, y) => x, default(IEqualityComparer<int>)));
        }

        [Fact]
        public void GroupJoin1()
        {
            var xs = new[] { 0, 1, 2 }.ToAsyncEnumerable();
            var ys = new[] { 4, 7, 6, 2, 3, 4, 8, 9 }.ToAsyncEnumerable();

            var res = xs.GroupJoin(ys, x => x % 3, y => y % 3, (x, i) => x + " - " + i.Aggregate("", (s, j) => s + j).Result);

            var e = res.GetEnumerator();
            HasNext(e, "0 - 639");
            HasNext(e, "1 - 474");
            HasNext(e, "2 - 28");
            NoNext(e);
        }

        [Fact]
        public void GroupJoin2()
        {
            var xs = new[] { 0, 1, 2 }.ToAsyncEnumerable();
            var ys = new[] { 3, 6, 4 }.ToAsyncEnumerable();

            var res = xs.GroupJoin(ys, x => x % 3, y => y % 3, (x, i) => x + " - " + i.Aggregate("", (s, j) => s + j).Result);

            var e = res.GetEnumerator();
            HasNext(e, "0 - 36");
            HasNext(e, "1 - 4");
            HasNext(e, "2 - ");
            NoNext(e);
        }

        [Fact]
        public void GroupJoin3()
        {
            var ex = new Exception("Bang!");
            var xs = AsyncEnumerable.Throw<int>(ex);
            var ys = new[] { 3, 6, 4 }.ToAsyncEnumerable();

            var res = xs.GroupJoin(ys, x => x % 3, y => y % 3, (x, i) => x + " - " + i.Aggregate("", (s, j) => s + j).Result);

            var e = res.GetEnumerator();
            AssertThrows<Exception>(() => e.MoveNext().Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void GroupJoin4()
        {
            var ex = new Exception("Bang!");
            var xs = new[] { 0, 1, 2 }.ToAsyncEnumerable();
            var ys = AsyncEnumerable.Throw<int>(ex);

            var res = xs.GroupJoin(ys, x => x % 3, y => y % 3, (x, i) => x + " - " + i.Aggregate("", (s, j) => s + j).Result);

            var e = res.GetEnumerator();
            AssertThrows<Exception>(() => e.MoveNext().Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void GroupJoin5()
        {
            var ex = new Exception("Bang!");
            var xs = new[] { 0, 1, 2 }.ToAsyncEnumerable();
            var ys = new[] { 3, 6, 4 }.ToAsyncEnumerable();

            var res = xs.GroupJoin(ys, x => { throw ex; }, y => y % 3, (x, i) => x + " - " + i.Aggregate("", (s, j) => s + j).Result);

            var e = res.GetEnumerator();
            AssertThrows<Exception>(() => e.MoveNext().Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void GroupJoin6()
        {
            var ex = new Exception("Bang!");
            var xs = new[] { 0, 1, 2 }.ToAsyncEnumerable();
            var ys = new[] { 3, 6, 4 }.ToAsyncEnumerable();

            var res = xs.GroupJoin(ys, x => x % 3, y => { throw ex; }, (x, i) => x + " - " + i.Aggregate("", (s, j) => s + j).Result);

            var e = res.GetEnumerator();
            AssertThrows<Exception>(() => e.MoveNext().Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void GroupJoin7()
        {
            var ex = new Exception("Bang!");
            var xs = new[] { 0, 1, 2 }.ToAsyncEnumerable();
            var ys = new[] { 3, 6, 4 }.ToAsyncEnumerable();

            var res = xs.GroupJoin(ys, x => x % 3, y => y % 3, (x, i) => {
                if (x == 1)
                    throw ex;
                return x + " - " + i.Aggregate("", (s, j) => s + j).Result;
            });

            var e = res.GetEnumerator();
            HasNext(e, "0 - 36");
            AssertThrows<Exception>(() => e.MoveNext().Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void Join_Null()
        {
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Join<int, int, int, int>(null, AsyncEnumerable.Return(42), x => x, x => x, (x, y) => x));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Join<int, int, int, int>(AsyncEnumerable.Return(42), null, x => x, x => x, (x, y) => x));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Join<int, int, int, int>(AsyncEnumerable.Return(42), AsyncEnumerable.Return(42), null, x => x, (x, y) => x));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Join<int, int, int, int>(AsyncEnumerable.Return(42), AsyncEnumerable.Return(42), x => x, null, (x, y) => x));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Join<int, int, int, int>(AsyncEnumerable.Return(42), AsyncEnumerable.Return(42), x => x, x => x, null));

            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Join<int, int, int, int>(null, AsyncEnumerable.Return(42), x => x, x => x, (x, y) => x, EqualityComparer<int>.Default));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Join<int, int, int, int>(AsyncEnumerable.Return(42), null, x => x, x => x, (x, y) => x, EqualityComparer<int>.Default));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Join<int, int, int, int>(AsyncEnumerable.Return(42), AsyncEnumerable.Return(42), null, x => x, (x, y) => x, EqualityComparer<int>.Default));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Join<int, int, int, int>(AsyncEnumerable.Return(42), AsyncEnumerable.Return(42), x => x, null, (x, y) => x, EqualityComparer<int>.Default));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Join<int, int, int, int>(AsyncEnumerable.Return(42), AsyncEnumerable.Return(42), x => x, x => x, null, EqualityComparer<int>.Default));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.Join<int, int, int, int>(AsyncEnumerable.Return(42), AsyncEnumerable.Return(42), x => x, x => x, (x, y) => x, default(IEqualityComparer<int>)));
        }

        [Fact]
        public void Join1()
        {
            var xs = new[] { 0, 1, 2 }.ToAsyncEnumerable();
            var ys = new[] { 3, 6, 4 }.ToAsyncEnumerable();

            var res = xs.Join(ys, x => x % 3, y => y % 3, (x, y) => x + y);

            var e = res.GetEnumerator();
            HasNext(e, 0 + 3);
            HasNext(e, 0 + 6);
            HasNext(e, 1 + 4);
            NoNext(e);
        }

        [Fact]
        public void Join2()
        {
            var xs = new[] { 3, 6, 4 }.ToAsyncEnumerable();
            var ys = new[] { 0, 1, 2 }.ToAsyncEnumerable();

            var res = xs.Join(ys, x => x % 3, y => y % 3, (x, y) => x + y);

            var e = res.GetEnumerator();
            HasNext(e, 3 + 0);
            HasNext(e, 6 + 0);
            HasNext(e, 4 + 1);
            NoNext(e);
        }

        [Fact]
        public void Join3()
        {
            var xs = new[] { 0, 1, 2 }.ToAsyncEnumerable();
            var ys = new[] { 3, 6 }.ToAsyncEnumerable();

            var res = xs.Join(ys, x => x % 3, y => y % 3, (x, y) => x + y);

            var e = res.GetEnumerator();
            HasNext(e, 0 + 3);
            HasNext(e, 0 + 6);
            NoNext(e);
        }

        [Fact]
        public void Join4()
        {
            var xs = new[] { 3, 6 }.ToAsyncEnumerable();
            var ys = new[] { 0, 1, 2 }.ToAsyncEnumerable();

            var res = xs.Join(ys, x => x % 3, y => y % 3, (x, y) => x + y);

            var e = res.GetEnumerator();
            HasNext(e, 3 + 0);
            HasNext(e, 6 + 0);
            NoNext(e);
        }

        [Fact]
        public void Join5()
        {
            var ex = new Exception("Bang!");
            var xs = AsyncEnumerable.Throw<int>(ex);
            var ys = new[] { 0, 1, 2 }.ToAsyncEnumerable();

            var res = xs.Join(ys, x => x % 3, y => y % 3, (x, y) => x + y);

            var e = res.GetEnumerator();
            AssertThrows<Exception>(() => e.MoveNext().Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void Join6()
        {
            var ex = new Exception("Bang!");
            var xs = new[] { 0, 1, 2 }.ToAsyncEnumerable();
            var ys = AsyncEnumerable.Throw<int>(ex);

            var res = xs.Join(ys, x => x % 3, y => y % 3, (x, y) => x + y);

            var e = res.GetEnumerator();
            AssertThrows<Exception>(() => e.MoveNext().Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void Join7()
        {
            var ex = new Exception("Bang!");
            var xs = new[] { 0, 1, 2 }.ToAsyncEnumerable();
            var ys = new[] { 0, 1, 2 }.ToAsyncEnumerable();

            var res = xs.Join(ys, x => { throw ex; }, y => y, (x, y) => x + y);

            var e = res.GetEnumerator();
            AssertThrows<Exception>(() => e.MoveNext().Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void Join8()
        {
            var ex = new Exception("Bang!");
            var xs = new[] { 0, 1, 2 }.ToAsyncEnumerable();
            var ys = new[] { 0, 1, 2 }.ToAsyncEnumerable();

            var res = xs.Join(ys, x => x, y => { throw ex; }, (x, y) => x + y);

            var e = res.GetEnumerator();
            AssertThrows<Exception>(() => e.MoveNext().Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public void Join9()
        {
            var ex = new Exception("Bang!");
            var xs = new[] { 0, 1, 2 }.ToAsyncEnumerable();
            var ys = new[] { 0, 1, 2 }.ToAsyncEnumerable();

            var res = xs.Join<int, int, int, int>(ys, x => x, y => y, (x, y) => { throw ex; });

            var e = res.GetEnumerator();
            AssertThrows<Exception>(() => e.MoveNext().Wait(WaitTimeoutMs), ex_ => ((AggregateException)ex_).Flatten().InnerExceptions.Single() == ex);
        }

        [Fact]
        public async Task Join10()
        {
            var xs = new[] { 0, 1, 2 }.ToAsyncEnumerable();
            var ys = new[] { 3, 6, 4 }.ToAsyncEnumerable();

            var res = xs.Join(ys, x => x % 3, y => y % 3, (x, y) => x + y);

            await SequenceIdentity(res);
        }


        [Fact]
        public void Join11()
        {
            var customers = new List<Customer>
            {
                new Customer {CustomerId = "ALFKI"},
                new Customer {CustomerId = "ANANT"},
                new Customer {CustomerId = "FISSA"}
            };
            var orders = new List<Order>
            {
                new Order { OrderId = 1, CustomerId = "ALFKI"},
                new Order { OrderId = 2, CustomerId = "ALFKI"},
                new Order { OrderId = 3, CustomerId = "ALFKI"},
                new Order { OrderId = 4, CustomerId = "FISSA"},
                new Order { OrderId = 5, CustomerId = "FISSA"},
                new Order { OrderId = 6, CustomerId = "FISSA"},
            };

            var asyncResult = customers.ToAsyncEnumerable()
                                       .Join(orders.ToAsyncEnumerable(), c => c.CustomerId, o => o.CustomerId,
                                            (c, o) => new CustomerOrder { CustomerId = c.CustomerId, OrderId = o.OrderId });

            var e = asyncResult.GetEnumerator();
            HasNext(e, new CustomerOrder { CustomerId = "ALFKI", OrderId = 1 });
            HasNext(e, new CustomerOrder { CustomerId = "ALFKI", OrderId = 2 });
            HasNext(e, new CustomerOrder { CustomerId = "ALFKI", OrderId = 3 });
            HasNext(e, new CustomerOrder { CustomerId = "FISSA", OrderId = 4 });
            HasNext(e, new CustomerOrder { CustomerId = "FISSA", OrderId = 5 });
            HasNext(e, new CustomerOrder { CustomerId = "FISSA", OrderId = 6 });
            NoNext(e);
        }

        [Fact]
        public void Join12()
        {
            var customers = new List<Customer>
            {
                new Customer {CustomerId = "ANANT"},
                new Customer {CustomerId = "ALFKI"},
                new Customer {CustomerId = "FISSA"}
            };
            var orders = new List<Order>
            {
                new Order { OrderId = 1, CustomerId = "ALFKI"},
                new Order { OrderId = 2, CustomerId = "ALFKI"},
                new Order { OrderId = 3, CustomerId = "ALFKI"},
                new Order { OrderId = 4, CustomerId = "FISSA"},
                new Order { OrderId = 5, CustomerId = "FISSA"},
                new Order { OrderId = 6, CustomerId = "FISSA"},
            };

            var asyncResult = customers.ToAsyncEnumerable()
                                       .Join(orders.ToAsyncEnumerable(), c => c.CustomerId, o => o.CustomerId,
                                            (c, o) => new CustomerOrder { CustomerId = c.CustomerId, OrderId = o.OrderId });

            var e = asyncResult.GetEnumerator();
            HasNext(e, new CustomerOrder { CustomerId = "ALFKI", OrderId = 1 });
            HasNext(e, new CustomerOrder { CustomerId = "ALFKI", OrderId = 2 });
            HasNext(e, new CustomerOrder { CustomerId = "ALFKI", OrderId = 3 });
            HasNext(e, new CustomerOrder { CustomerId = "FISSA", OrderId = 4 });
            HasNext(e, new CustomerOrder { CustomerId = "FISSA", OrderId = 5 });
            HasNext(e, new CustomerOrder { CustomerId = "FISSA", OrderId = 6 });
            NoNext(e);
        }


        [Fact]
        public void SelectManyMultiple_Null()
        {
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.SelectMany<int, int>(default(IAsyncEnumerable<int>), AsyncEnumerable.Return(42)));
            AssertThrows<ArgumentNullException>(() => AsyncEnumerable.SelectMany<int, int>(AsyncEnumerable.Return(42), default(IAsyncEnumerable<int>)));
        }

        [Fact]
        public void SelectManyMultiple1()
        {
            var xs = new[] { 0, 1, 2 }.ToAsyncEnumerable();
            var ys = new[] { 3, 4 }.ToAsyncEnumerable();

            var res = xs.SelectMany(ys);

            var e = res.GetEnumerator();
            HasNext(e, 3);
            HasNext(e, 4);
            HasNext(e, 3);
            HasNext(e, 4);
            HasNext(e, 3);
            HasNext(e, 4);
            NoNext(e);
        }


        public class Customer
        {
            public string CustomerId { get; set; }
        }

        public class Order
        {
            public int OrderId { get; set; }
            public string CustomerId { get; set; }
        }

        [DebuggerDisplay("CustomerId = {CustomerId}, OrderId = {OrderId}")]
        public class CustomerOrder : IEquatable<CustomerOrder>
        {
            public bool Equals(CustomerOrder other)
            {
                if (other is null) return false;
                if (ReferenceEquals(this, other)) return true;
                return OrderId == other.OrderId && string.Equals(CustomerId, other.CustomerId);
            }

            public override bool Equals(object obj)
            {
                if (obj is null) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;
                return Equals((CustomerOrder)obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (OrderId * 397) ^ (CustomerId != null ? CustomerId.GetHashCode() : 0);
                }
            }

            public static bool operator ==(CustomerOrder left, CustomerOrder right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(CustomerOrder left, CustomerOrder right)
            {
                return !Equals(left, right);
            }

            public int OrderId { get; set; }
            public string CustomerId { get; set; }
        }

    }
}
