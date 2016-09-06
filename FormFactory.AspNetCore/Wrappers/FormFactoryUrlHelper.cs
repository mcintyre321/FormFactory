using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;


namespace FormFactory.AspMvc.Wrappers
{
    class FormFactoryUrlHelper : FormFactory.UrlHelper
    {
        private readonly IUrlHelper _mvcUrlHelper;

        public FormFactoryUrlHelper(IUrlHelper mvcUrlHelper)
        {
            _mvcUrlHelper = mvcUrlHelper;
        }

        //public string Action<TController>(Expression<Action<TController>> action)
        //    where TController : Controller
        //{
        //    var routeValuesFromExpression = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression<TController>(action);
        //    var actionName = routeValuesFromExpression["action"].ToString();
        //    return _mvcUrlHelper.Action(actionName);
        //}

        public string Action(string actionName, string controllerName)
        {
            return _mvcUrlHelper.Action(actionName, controllerName);
        }
 
    }
}