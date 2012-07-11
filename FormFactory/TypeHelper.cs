using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FormFactory
{
    static class TypeHelper
    {
        internal static Type GetEnumerableType(this Type type)
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

        internal static IEnumerable<Tuple<string, string>> GetChoicesForEnumType(this Type enumType)
        {
            Func<FieldInfo, string> getName = fieldInfo => Enum.GetName(enumType, (int) fieldInfo.GetValue(null));
            Func<FieldInfo, string> getDisplayName = fieldInfo =>
                                                         {
                                                             var attr =
                                                                 fieldInfo.GetCustomAttributes(
                                                                     typeof (DisplayAttribute), true).Cast
                                                                     <DisplayAttribute>().FirstOrDefault();
                                                             return attr != null ? attr.Name : null;
                                                         };
            Func<FieldInfo, string> getLabel = fieldInfo => getDisplayName(fieldInfo) ?? getName(fieldInfo).Sentencise();

            return enumType.GetFields(BindingFlags.Static | BindingFlags.GetField |
                                              BindingFlags.Public).Select(x => new Tuple<string, string>(getName(x), getLabel(x)));
        }

        internal static Type GetUnderlyingFlattenedType(this Type type)
        {
            var checkType = type.GetEnumerableType() ?? type;
            return Nullable.GetUnderlyingType(checkType) ?? checkType;
        }
    }
}
