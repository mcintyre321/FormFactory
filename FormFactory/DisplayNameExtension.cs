using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace FormFactory
{
    public interface IHasDisplayName
    {
        string DisplayName { get; }
    }
    public interface IHasName
    {
        string Name { get; }
    }

    public static class DisplayNameExtension
    {
        static readonly ConditionalWeakTable<object, string> lookup = new ConditionalWeakTable<object, string>();

        public static List<Func<Type, object, bool, string>> Rules = new List<Func<Type, object, bool, string>>()
        {
            (type, o, tc) => lookup.GetValue(o, t => null),
            (type, o, tc) => o is IHasDisplayName ? ((IHasDisplayName) o).DisplayName : null,
            (type, o, tc) => o is IHasName ? ((IHasName) o).Name.Sentencise(tc): null,
            (type, o, tc) => o is Enum ? GetNameFromDisplayAttribute(o): null,
            (type, o, tc) => o is Enum ? (o.ToString()).Sentencise(tc): null,
            (type, o, tc) => o is Type ? (((Type)o).Name).Sentencise(tc): null,
            (type, o, tc) => o.GetType().Name.Sentencise(tc)
        };

        private static string GetNameFromDisplayAttribute(object enumValue)
        {
            if (enumValue == null) return "";
            return enumValue.GetType().GetChoicesForEnumType().Single(t => t.Item2 == enumValue).Item1;
        }

        public static string DisplayName<T>(this T o, bool titleCase = false)
        {
            return Rules.Select(r => r(typeof(T), o, titleCase)).FirstOrDefault(displayName => displayName != null);
        }

        public static T DisplayName<T>(this T t, string displayName)
        {
            lookup.Remove(t);
            lookup.GetValue(t, table => displayName);
            return t;
        }
    }
}
