using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FormFactory
{
    public static class TypeHelper
    {
        public static Type GetEnumerableType(this Type type)
        {
            var interfaceTypes = type.GetTypeInfo().ImplementedInterfaces.ToList();
            interfaceTypes.Insert(0, type);
            foreach (Type intType in interfaceTypes)
            {
                if (intType.GetTypeInfo().IsGenericType
                    && intType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return intType.GetTypeInfo().GenericTypeArguments[0];
                }
            }
            return null;
        }

        static ConcurrentDictionary<Type, IEnumerable<Tuple<string, object>>> enumChoices = new ConcurrentDictionary<Type, IEnumerable<Tuple<string, object>>>();

        public static IEnumerable<Tuple<string, object>> GetChoicesForEnumType(this Type inType)
        {
            var underlyingType = Nullable.GetUnderlyingType(inType);
            if (underlyingType == null) yield return Tuple.Create("", "" as object);
            var enumType = underlyingType ?? inType;
            var choices = enumChoices.GetOrAdd(enumType, t =>
            {
                Func<FieldInfo, string> getName = fieldInfo => Enum.GetName(enumType, (int) fieldInfo.GetValue(null));
                Func<FieldInfo, string> getDisplayName = fieldInfo =>
                {
                    var attr =
                        fieldInfo.GetCustomAttributes(typeof (DisplayAttribute), true).Cast<DisplayAttribute>().
                            FirstOrDefault();
                    return attr != null ? attr.Name : null;
                };
                Func<FieldInfo, string> getLabel =
                    fieldInfo => getDisplayName(fieldInfo) ?? getName(fieldInfo).Sentencise();
                return enumType.GetTypeInfo().DeclaredFields.Where(f => f.IsStatic) 
                    .Select(x => Tuple.Create(getLabel(x), x.GetValue(null))).ToList().Select(f => f);
            });
            foreach (var field in choices)
            {
                yield return field;
            }
            
        }

        public static Type GetUnderlyingFlattenedType(this Type type)
        {
            var checkType = type.GetEnumerableType() ?? type;
            return Nullable.GetUnderlyingType(checkType) ?? checkType;
        }
    }
}
