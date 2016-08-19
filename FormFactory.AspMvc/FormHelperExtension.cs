using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Security;
using FormFactory.AspMvc.Wrappers;
using FormFactory.ViewHelpers;

namespace FormFactory
{
    public static class FormHelperExtension
    {
        #region generic overrides

        public static FormVm FormForAction<TController, TActionResult>(this System.Web.Mvc.HtmlHelper html,
                                                                       Expression<Func<TController, TActionResult>>
                                                                           action)
            where TController : IController
            where TActionResult : ActionResult
        {
            return FormFor(html, action.MethodInfo());
        }

        public static FormVm FormForAction<TController, TArg1, TActionResult>(this System.Web.Mvc.HtmlHelper html,
                                                                              Expression
                                                                                  <
                                                                                  Func
                                                                                  <TController, TArg1, TActionResult>>
                                                                                  action)
            where TController : IController
            where TActionResult : ActionResult
        {
            return FormFor(html, action.MethodInfo());
        }

        public static FormVm FormForAction<TController, TArg1, TArg2, TActionResult>(
            this System.Web.Mvc.HtmlHelper html, Expression<Func<TController, TArg1, TArg2, TActionResult>> action)
            where TController : IController
            where TActionResult : ActionResult
        {
            return FormFor(html, action.MethodInfo());
        }

        public static FormVm FormForAction<TController, TArg1, TArg2, TArg3, TActionResult>(
            this System.Web.Mvc.HtmlHelper html,
            Expression<Func<TController, TArg1, TArg2, TArg3, TActionResult>> action)
            where TController : IController
            where TActionResult : ActionResult
        {
            return FormFor(html, action.MethodInfo());
        }

        public static FormVm FormForAction<TController, TArg1, TArg2, TArg3, TArg4, TActionResult>(
            this System.Web.Mvc.HtmlHelper html,
            Expression<Func<TController, TArg1, TArg2, TArg3, TArg4, TActionResult>> action)
            where TController : IController
            where TActionResult : ActionResult
        {
            return FormFor(html, action.MethodInfo());
        }

        public static FormVm FormForAction<TController, TArg1, TArg2, TArg3, TArg4, TArg5, TActionResult>(
            this System.Web.Mvc.HtmlHelper html,
            Expression<Func<TController, TArg1, TArg2, TArg3, TArg4, TArg5, TActionResult>> action)
            where TController : IController
            where TActionResult : ActionResult
        {
            return FormFor(html, action.MethodInfo());
        }

        #endregion

        private static FormVm FormFor(System.Web.Mvc.HtmlHelper html, MethodInfo mi)
        {
            var formFactoryHtmlHelper = new FormFactoryHtmlHelper(html);
            var actionName = mi.Name;
            var controllerTypeName = mi.ReflectedType.Name;
            var controllerName = controllerTypeName.Substring(0, controllerTypeName.LastIndexOf("Controller"));
            

            return new FormVm(mi, formFactoryHtmlHelper.Url().Action(actionName, controllerName));
            
        }

        public static PropertyVm CreatePropertyVm(this HtmlHelper helper, Type objectType, string name)
        {
            return new FormFactoryHtmlHelper(helper).CreatePropertyVm(objectType, name);
        }

        public static ObjectChoices[] Choices(this HtmlHelper html, PropertyVm model)
        {
            return new FormFactoryHtmlHelper(html).Choices(model);
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