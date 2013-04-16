using System;

namespace FormFactory
{
    public interface FfHtmlHelper
    {
        void RenderPartial(string partialName, object model);
        UrlHelper Url();
        string WriteTypeToString(Type type);
        ViewData ViewData { get; }
        FfContext FfContext { get; }
        IHtmlString Partial(string partialName, object vm);
        IHtmlString UnobtrusiveValidation(PropertyVm model);
    }
    public interface FfContext
    {
        ViewResult FindPartialView(string partialViewName);
    }

    public interface ViewResult
    {
        View View { get; }
    }
    public class View
    {
        
    }


    public interface ViewData
    {
        IModelStateDictionary ModelState { get; }
        object Model { get; }
    }

    public interface IModelStateDictionary
    {
        bool TryGetValue(string key, out ModelState modelState);
    }

    public interface ModelState
    {
        ModelStateValue Value { get; }
    }

    public interface ModelStateValue
    {
        object AttemptedValue { get;  }
    }

    public interface UrlHelper
    {
        string Action(string actionName, string controllerName);
        string Action(string actionName, string controllerName, object routeValues, string protocol);
    }

    public interface RouteValueDictionary
    {
    }
}