using System;
using System.Collections.Generic;
using System.Linq;

namespace FormFactory
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> Each<T>(this IEnumerable<T> ts, Action<T> action)
        {
            return ts.Select(t => t.Then(action));
        }

        public static IEnumerable<T> Eval<T>(this IEnumerable<T> ts)
        {
            return ts.ToArray();
        }

        public static T Then<T>(this T t, Action<T> action)
        {
            action(t);
            return t;
        }

    }
}