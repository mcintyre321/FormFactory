using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FormFactory
{
    class TypeHelper
    {
        internal static Type GetEnumerableType(Type type)
        {
            var interfaceTypes = type.GetInterfaces().ToList();
            interfaceTypes.Insert(0, type);
            foreach (Type intType in interfaceTypes)
            {
                if (intType.IsGenericType
                    && intType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return intType.GetGenericArguments()[0];
                }
            }
            return null;
        }
    }
}
