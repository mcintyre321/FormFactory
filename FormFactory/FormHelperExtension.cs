using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace FormFactory
{
    public static class ThenHelper
    {
        public static T Then<T>(this T t, Action<T> action)
        {
            action(t);
            return t;
        }

    }
    public static class FormHelperExtension
    {
        #region generic overrides
        public static FormVm FormForAction<TController, TActionResult>(this HtmlHelper html, Expression<Func<TController, TActionResult>> action)
            where TController : IController
            where TActionResult : ActionResult
        {
            return FormFor(html, action.MethodInfo());
        }
        public static FormVm FormForAction<TController, TArg1, TActionResult>(this HtmlHelper html, Expression<Func<TController, TArg1, TActionResult>> action)
            where TController : IController
            where TActionResult : ActionResult
        {
            return FormFor(html, action.MethodInfo());
        }
        public static FormVm FormForAction<TController, TArg1, TArg2, TActionResult>(this HtmlHelper html, Expression<Func<TController, TArg1, TArg2, TActionResult>> action)
            where TController : IController
            where TActionResult : ActionResult
        {
            return FormFor(html, action.MethodInfo());
        }
        public static FormVm FormForAction<TController, TArg1, TArg2, TArg3, TActionResult>(this HtmlHelper html, Expression<Func<TController, TArg1, TArg2, TArg3, TActionResult>> action)
            where TController : IController
            where TActionResult : ActionResult
        {
            return FormFor(html, action.MethodInfo());
        }
        public static FormVm FormForAction<TController, TArg1, TArg2, TArg3, TArg4, TActionResult>(this HtmlHelper html, Expression<Func<TController, TArg1, TArg2, TArg3, TArg4, TActionResult>> action)
            where TController : IController
            where TActionResult : ActionResult
        {
            return FormFor(html, action.MethodInfo());
        }
        public static FormVm FormForAction<TController, TArg1, TArg2, TArg3, TArg4, TArg5, TActionResult>(this HtmlHelper html, Expression<Func<TController, TArg1, TArg2, TArg3, TArg4, TArg5, TActionResult>> action)
            where TController : IController
            where TActionResult : ActionResult
        {
            return FormFor(html, action.MethodInfo());
        }
        #endregion
        private static FormVm FormFor(HtmlHelper html, MethodInfo mi, string displayName = null)
        {
            return new FormVm(html, mi, displayName);
        }

        public static UrlHelper Url(this HtmlHelper helper)
        {
            return new UrlHelper(helper.ViewContext.RequestContext);
        }


        public static string Action<TController>(this UrlHelper helper, Expression<Action<TController>> action)
            where TController : Controller
        {
            var routeValuesFromExpression = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression<TController>(action);

            return helper.Action(routeValuesFromExpression["action"].ToString(), routeValuesFromExpression);
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

        static bool IsNullable<T>(T t) { return false; }
        static bool IsNullable<T>(T? t) where T : struct { return true; }
        public static MvcHtmlString BestProperty(this HtmlHelper html, PropertyVm vm)
        {
            var viewNameExtension = "Property";
            var check = Nullable.GetUnderlyingType(vm.Type) ?? vm.Type;;
            string partialViewName = viewNameExtension + "." + vm.Type;

            var engineResult = ViewEngines.Engines.FindPartialView(html.ViewContext.Controller.ControllerContext, "FormFactory/" + partialViewName);
            while (engineResult.View == null && check.BaseType != null)
            {
                check = check.BaseType;
                partialViewName = viewNameExtension + "." + check.FullName;
                engineResult = ViewEngines.Engines.FindPartialView(html.ViewContext.Controller.ControllerContext, "FormFactory/" + partialViewName); ;
            }
            if (engineResult.View == null)
            {
                Type enumerableType = GetEnumerableType(vm.Type);
                if (enumerableType != null)
                {
                    viewNameExtension = "Property.IEnumerable";
                    partialViewName = viewNameExtension + "." + enumerableType;
                    engineResult = ViewEngines.Engines.FindPartialView(html.ViewContext.Controller.ControllerContext, "FormFactory/" + partialViewName);

                    while (engineResult.View == null && enumerableType.BaseType != null)
                    {
                        check = check.BaseType;
                        partialViewName = viewNameExtension + "." + enumerableType.FullName;
                        engineResult = ViewEngines.Engines.FindPartialView(html.ViewContext.Controller.ControllerContext, "FormFactory/" + partialViewName);
                    }
                }
            }
            if (engineResult.View == null)
            {
                partialViewName = viewNameExtension + ".System.Object";
            }
            return html.Partial("FormFactory/" + partialViewName, vm);
        }

        static Type GetEnumerableType(Type type)
        {
            foreach (Type intType in type.GetInterfaces())
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

    public static class ModelHelper
    {
        public static FormVm RenderPropertiesFor<T>(this FormVm form, T model, Func<PropertyVm, bool> filter = null, bool displayOnly = false)
        {
            form.HtmlHelper.RenderPropertiesFor(model, filter, displayOnly);
            return form;
        }
        public static void RenderPropertiesFor<T>(this HtmlHelper helper, T model, Func<PropertyVm, bool> filter = null, bool displayOnly = false)
        {
            filter = filter ?? (p => true);
            var properties = model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                if (properties.Any(p => p.Name + "Choices" == property.Name))
                {
                    continue; //skip this is it is choice
                }

                var methodInfo = (displayOnly ? null : property.GetSetMethod()) ?? property.GetGetMethod();
                var inputVm = new PropertyVm(model, property, helper);
                PropertyInfo choices = properties.SingleOrDefault(p => p.Name == property.Name + "Choices");
                if (choices != null)
                {
                    inputVm.Choices = (IEnumerable)choices.GetGetMethod().Invoke(model, null);
                }
                if (methodInfo != null && filter(inputVm))
                {
                    if (displayOnly) inputVm.IsWritable = false;

                    helper.RenderPartial("FormFactory/Form.Property", inputVm);
                    continue;
                }
            }
        }
    }
}