// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq
{
    internal static class FunctionalHelpers
    {
        public static T Identity<T>(T value) => value;

        public static ValueTask<T> IdentityAsync<T>(T value) => new(value);

        public static ValueTask<T> IdentityAsync<T>(T value, CancellationToken token) => new(value);

        public static TKey Key<TKey, TValue>(KeyValuePair<TKey, TValue> kvp) => kvp.Key;

        public static TValue Value<TKey, TValue>(KeyValuePair<TKey, TValue> kvp) => kvp.Value;
    }
}
