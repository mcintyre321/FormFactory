using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using FormFactory.AspMvc.Wrappers;
using FormFactory.ViewHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FormFactory
{
    public static class FormHelperExtension
    {

        //public static FormVm FormForAction<TController, TActionResult>(this HtmlHelper html,
        //                                                               Expression<Func<TController, TActionResult>>
        //                                                                   action)
        //    where TController : IController
        //    where TActionResult : ActionResult
        //{
        //    return FormFor(html, action.MethodInfo());
        //}

        //public static FormVm FormForAction<TController, TArg1, TActionResult>(this HtmlHelper html,
        //                                                                      Expression
        //                                                                          <
        //                                                                          Func
        //                                                                          <TController, TArg1, TActionResult>>
        //                                                                          action)
        //    where TController : IController
        //    where TActionResult : ActionResult
        //{
        //    return FormFor(html, action.MethodInfo());
        //}

        //public static FormVm FormForAction<TController, TArg1, TArg2, TActionResult>(
        //    this HtmlHelper html, Expression<Func<TController, TArg1, TArg2, TActionResult>> action)
        //    where TController : IController
        //    where TActionResult : ActionResult
        //{
        //    return FormFor(html, action.MethodInfo());
        //}

        //public static FormVm FormForAction<TController, TArg1, TArg2, TArg3, TActionResult>(
        //    this HtmlHelper html,
        //    Expression<Func<TController, TArg1, TArg2, TArg3, TActionResult>> action)
        //    where TController : IController
        //    where TActionResult : ActionResult
        //{
        //    return FormFor(html, action.MethodInfo());
        //}

        //public static FormVm FormForAction<TController, TArg1, TArg2, TArg3, TArg4, TActionResult>(
        //    this HtmlHelper html,
        //    Expression<Func<TController, TArg1, TArg2, TArg3, TArg4, TActionResult>> action)
        //    where TController : IController
        //    where TActionResult : ActionResult
        //{
        //    return FormFor(html, action.MethodInfo());
        //}

        //public static FormVm FormForAction<TController, TArg1, TArg2, TArg3, TArg4, TArg5, TActionResult>(
        //    this HtmlHelper html,
        //    Expression<Func<TController, TArg1, TArg2, TArg3, TArg4, TArg5, TActionResult>> action)
        //    where TController : IController
        //    where TActionResult : ActionResult
        //{
        //    return FormFor(html, action.MethodInfo());
        //}

        //#endregion

        //private static FormVm FormFor(HtmlHelper html, MethodInfo mi)
        //{
        //    throw new Exception("Uh oh");
        //    //var formFactoryHtmlHelper = new FormFactoryHtmlHelper(html);
        //    //var actionName = mi.Name;
        //    //var controllerTypeName = mi..ReflectedType.Name;
        //    //var controllerName = controllerTypeName.Substring(0, controllerTypeName.LastIndexOf("Controller"));
            

        //    //return new FormVm(mi, formFactoryHtmlHelper.Url().Action(actionName, controllerName));
            
        //}

        public static PropertyVm CreatePropertyVm(this IHtmlHelper helper, Type objectType, string name)
        {
            return new FormFactoryHtmlHelper(helper).CreatePropertyVm(objectType, name);
        }

        public static ObjectChoices[] Choices(this IHtmlHelper html, PropertyVm model)
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