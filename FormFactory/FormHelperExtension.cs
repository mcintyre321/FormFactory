using System;
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

        public static MvcHtmlString BestProperty(this HtmlHelper html, PropertyVm vm)
        {
            var viewNameExtension = "Property";
            string partialViewName = viewNameExtension + "." + vm.Type;

            var engineResult = ViewEngines.Engines.FindPartialView(html.ViewContext.Controller.ControllerContext, partialViewName);
            var check = vm.Type;
            while (engineResult.View == null && check.BaseType != null)
            {
                check = check.BaseType;
                partialViewName = viewNameExtension + "." + check.FullName;
                engineResult = ViewEngines.Engines.FindPartialView(html.ViewContext.Controller.ControllerContext, partialViewName); ;
            }
            if (engineResult.View == null)
                partialViewName = viewNameExtension + ".System.Object";
            return html.Partial(partialViewName, vm);
        }
    }

    public class FormVm : IDisposable
    {
        public FormVm(HtmlHelper html, MethodInfo mi, string displayName)
        {
            var controllerName = mi.ReflectedType.Name;
            controllerName = controllerName.Substring(0, controllerName.LastIndexOf("Controller"));
            HtmlHelper = html;
            DisplayName = displayName ?? mi.Name.Sentencise();
            ActionUrl = html.Url().Action(mi.Name, controllerName);
            Inputs = mi.GetParameters().Select(pi => new PropertyVm(pi, html));
        }
        public string ActionUrl { get; set; }
        public MvcHtmlString SideMessage { get; set; }
        public IEnumerable<PropertyVm> Inputs { get; set; }

        public HtmlHelper HtmlHelper { get; set; }

        public string DisplayName { get; set; }

        public FormVm Render()
        {
            RenderStart();
            RenderActionInputs();
            RenderButtons();
            return this;
        }

        public FormVm RenderButtons()
        {
            HtmlHelper.RenderPartial("Form.Actions", this);
            return this;
        }

        public FormVm RenderActionInputs()
        {
            foreach (var input in Inputs)
            {
                HtmlHelper.RenderPartial("Form.Property", input);
            }
            return this;
        }

        public FormVm RenderStart()
        {
            HtmlHelper.RenderPartial("Form.Start", this);
            return this;
        }


        public void Dispose()
        {
            HtmlHelper.RenderPartial("Form.Close", this);
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

                var methodInfo = (displayOnly ? null : property.GetSetMethod()) ?? property.GetGetMethod();
                var inputVm = new PropertyVm(model, property, helper);
                if (methodInfo != null && filter(inputVm))
                {
                    if (displayOnly) inputVm.IsWritable = false;

                    helper.RenderPartial("Form.Property", inputVm);
                    continue;
                }
            }
        }
    }

    public class PropertyVm
    {
        public PropertyVm(ParameterInfo pi, HtmlHelper html)
        {
            Type = pi.ParameterType;
            Name = pi.Name;
            DisplayName = pi.Name.Sentencise();
            ModelState modelState;
            if (html.ViewData.ModelState.TryGetValue(pi.Name, out modelState))
            {
                if (modelState.Value != null)
                    Value = modelState.Value.AttemptedValue;
            }
            IsWritable = true;
            GetCustomAttributes = () => pi.GetCustomAttributes(true);
        }

        public PropertyVm(object o, PropertyInfo pi, HtmlHelper html, string displayName = null)
        {
            Type = pi.PropertyType;
            Name = pi.Name;
            DisplayName = displayName ?? Name.Sentencise();
            ModelState modelState;
            if (html.ViewData.ModelState.TryGetValue(pi.Name, out modelState))
            {
                if (modelState.Value != null)
                    Value = modelState.Value.AttemptedValue;
            }
            else if (pi.GetGetMethod() != null)
            {
                Value = pi.GetGetMethod().Invoke(o, null);
            }
            GetCustomAttributes = () => pi.GetCustomAttributes(true);
            IsWritable = pi.GetSetMethod() != null;
        }

        public Type Type { get; private set; }

        public string Name { get; private set; }
        public string DisplayName { get; private set; }

        public object Value { get; private set; }


        public Func<IEnumerable<object>> GetCustomAttributes { get; set; }

        public bool IsWritable { get; internal set; }
    }
}