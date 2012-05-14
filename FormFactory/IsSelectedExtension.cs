using System.Runtime.CompilerServices;

namespace FormFactory
{
    public static class IsSelectedExtension
    {
        private static ConditionalWeakTable<object, object> lookup = new ConditionalWeakTable<object, object>();
        public static bool IsSelected(this object item)
        {
            if(item == null) return false;
            return lookup.GetOrCreateValue(item) as bool? ?? false;
        }
        public static T SetSelected<T>(this T item, bool value)
        {
            lookup.Remove(item);
            lookup.Add(item, value);
            return item;
        }
        public static T Selected<T>(this T item)
        {
            return item.SetSelected(true);
        }
    }
}