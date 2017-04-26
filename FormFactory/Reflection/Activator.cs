using System;

namespace FormFactory.Reflection
{
    public class Activator
    {
        public static object CreateInstance(Type type) => System.Activator.CreateInstance(type);
    }
}
