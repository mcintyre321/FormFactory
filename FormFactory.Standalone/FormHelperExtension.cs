using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FormFactory.Standalone;
using FormFactory.ViewHelpers;

namespace FormFactory
{
    public static class FormHelperExtension
    {
        public static PropertyVm CreatePropertyVm(this MyFfHtmlHelper helper, Type objectType, string name)
        {
            return helper.CreatePropertyVm(objectType, name);
        }

        public static ObjectChoices[] Choices(this MyFfHtmlHelper html, PropertyVm model)
        {
            var choices = (from obj in model.Choices.Cast<object>().ToArray()
                let choiceType = obj == null ? model.Type : obj.GetType()
                let properties = FF.PropertiesFor(obj, choiceType)
                    .Each(p => p.Name = model.Name + "." + p.Name)
                    .Each(p => p.Readonly |= model.Readonly)
                    .Each(p => p.Id = Guid.NewGuid().ToString())
                select new ObjectChoices
                {
                    obj = obj, choiceType = choiceType, properties = properties,
                    name = (obj != null ? obj.DisplayName() : choiceType.DisplayName())
                }).ToArray();
            return choices;
        }

        public static MethodInfo MethodInfo(this Expression method)
        {
            var lambda = method as LambdaExpression;
            if (lambda == null) throw new ArgumentNullException("method");
            MethodCallExpression methodExpr = null;
            if (lambda.Body.NodeType == ExpressionType.Call)
                methodExpr = lambda.Body as MethodCallExpression;

            if (methodExpr == null) throw new ArgumentNullException("method");
            return methodExpr.Method;
        }

        private static bool IsNullable<T>(T t)
        {
            return false;
        }

        private static bool IsNullable<T>(T? t) where T : struct
        {
            return true;
        }
    }
}