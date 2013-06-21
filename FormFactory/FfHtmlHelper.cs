using System;
using System.Collections;
using System.Collections.Generic;

namespace FormFactory
{
    public interface FfHtmlHelper 
    {
        UrlHelper Url();
        string WriteTypeToString(Type type);
        ViewData ViewData { get; }
        FfContext FfContext { get; }
        string Partial(string partialName, object vm); 
        void RenderPartial(string partialName, object model);
        PropertyVm CreatePropertyVm(Type objectType, string name);
    }

    public interface FfHtmlHelper<TViewData> : FfHtmlHelper
    {
        string Partial(string partialName, object vm, TViewData viewData);
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
        ModelState this[string key] { get; }
        bool ContainsKey(string key);
    }

    public interface ModelState
    {
        FormFactoryModelStateValue Value { get; }
        FormFactoryModelStateErrors Errors { get; } 
    }

    public class FormFactoryModelStateErrors : IEnumerable<FormFactoryModelStateError>
    {
        private IEnumerable<FormFactoryModelStateError> _items;

        public FormFactoryModelStateErrors(IEnumerable<FormFactoryModelStateError> items)
        {
            _items = items;
        }

        public IEnumerator<FormFactoryModelStateError> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class FormFactoryModelStateError
    {
        public string ErrorMessage { get; set; }
    }

    public interface FormFactoryModelStateValue
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