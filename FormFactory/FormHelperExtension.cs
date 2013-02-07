using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace FormFactory
{
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
        private static FormVm FormFor(HtmlHelper html, MethodInfo mi)
        {
            return new FormVm(html, mi);
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
            var viewname = html.ViewContext.Controller.ControllerContext.BestViewName(vm.Type, "FormFactory/Property.");
            viewname = viewname ?? html.ViewContext.Controller.ControllerContext.BestViewName(TypeHelper.GetEnumerableType(vm.Type), "FormFactory/Property.IEnumerable.");
            viewname = viewname ?? "FormFactory/Property.System.Object"; //must be some unknown object exposed as an interface
            return html.Partial(viewname, vm);
        }
        public static string BestViewName(this HtmlHelper helper, Type type, string prefix = null)
        {
            return BestViewName(helper.ViewContext.Controller.ControllerContext, type, prefix);
        }
        public static MvcHtmlString BestPartial(this HtmlHelper helper, object model, Type type = null, string prefix = null)
        {
            if (type == null) type = model.GetType();
            return helper.Partial(BestViewName(helper.ViewContext.Controller.ControllerContext, type, prefix), model);
        }
        public static void RenderBestPartial(this HtmlHelper helper, object model, Type type = null, string prefix = null)
        {
            if (type == null) type = model.GetType();
            helper.RenderPartial(BestViewName(helper.ViewContext.Controller.ControllerContext, type, prefix), model);
        }
        
        public static IList<Func<Type, string>> SearchPathRules = new List<Func<Type, string>>()
        {
            t => t.FullName,
            t => t.FullName.StartsWith(t.Assembly.GetName().Name + ".") ? t.FullName.Substring(t.Assembly.GetName().Name.Length + 1) : t.FullName,
            t => t.Name
        };
        public static string BestViewName(this ControllerContext cc, Type type, string prefix = null)
        {
            return SearchPathRules
                .Select(r => BestViewName(cc, type, prefix, r))
                    .FirstOrDefault(v => v != null);
        }

        public static string BestViewName(ControllerContext cc, Type type, string prefix, Func<Type, string> getNameIn)
        {
            if (type == null) return null;
            var getName = getNameIn ?? (t => t.FullName);
            var check = Nullable.GetUnderlyingType(type) ?? type;

            Func<Type, string> getPartialViewName =
                t => prefix + getName(t);
            string partialViewName = getPartialViewName(check);

            var engineResult = ViewEngines.Engines.FindPartialView(cc, partialViewName);
            while (engineResult.View == null && check.BaseType != null)
            {
                check = check.BaseType;
                partialViewName = getPartialViewName(check);
                engineResult = ViewEngines.Engines.FindPartialView(cc, partialViewName);
                ;
            }

            if (engineResult.View == null)
            {
                return null;
            }
            return partialViewName;
        }

        
    }
}