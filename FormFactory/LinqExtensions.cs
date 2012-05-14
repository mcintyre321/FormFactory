using System;
using System.Collections.Generic;

namespace FormFactory
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> Each<T>(this IEnumerable<T> ts, Action<T> action)
        {
            foreach (var t in ts)
            {
                yield return t.Then(action);
            }
        }

        public static T Then<T>(this T t, Action<T> action)
        {
            action(t);
            return t;
        }

    }
}