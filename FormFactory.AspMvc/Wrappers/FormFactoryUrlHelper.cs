using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace FormFactory.AspMvc.Wrappers
{
    class FormFactoryUrlHelper : FormFactory.UrlHelper
    {
        private readonly System.Web.Mvc.UrlHelper _mvcUrlHelper;

        public FormFactoryUrlHelper(System.Web.Mvc.UrlHelper mvcUrlHelper)
        {
            _mvcUrlHelper = mvcUrlHelper;
        }

        public string Action<TController>(Expression<Action<TController>> action)
            where TController : Controller
        {
            var routeValuesFromExpression = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression<TController>(action);
            var actionName = routeValuesFromExpression["action"].ToString();
            return _mvcUrlHelper.Action(actionName);
        }

        public string Action(string actionName, string controllerName)
        {
            return _mvcUrlHelper.Action(actionName, controllerName);

        }

        public string Action(string actionName, string controllerName, object routeValues, string protocol)
        {
            return _mvcUrlHelper.Action(actionName, controllerName, routeValues, protocol);
        }
    }
}