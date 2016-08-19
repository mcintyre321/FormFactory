using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FormFactory
{
    public interface FfHtmlHelper 
    {
        UrlHelper Url();
        string WriteTypeToString(Type type);
        ViewData ViewData { get; }
        IViewFinder ViewFinder { get; }
        //string Partial(string partialName, object vm); 
        void RenderPartial(string partialName, object model);
        PropertyVm CreatePropertyVm(Type objectType, string name);

    }

    public interface FfHtmlHelper<TViewData> : FfHtmlHelper
    {
        //string Partial(string partialName, object vm, TViewData viewData);
    }
    public interface IViewFinder
    {
        IViewFinderResult FindPartialView(string partialViewName);
    }

    public interface IViewFinderResult
    {
        View View { get; }
    }
    public class View
    {
        
    }


    public class ViewData 
    {
        public ViewData(IModelStateDictionary modelState, object model)
        {
            ModelState = modelState;
            Model = model;
        }

        public ViewData()
        {
            ModelState = new FfModelStateDictionary();
        }

        public IModelStateDictionary ModelState { get; private set; }
        public object Model { get; private set; }
    }


    public class FfModelStateDictionary : Dictionary<string, ModelState>, IModelStateDictionary
    {
        public bool IsValid => Values.SelectMany(v => v.Errors).Any(e => e != null) == false;
    }

    public interface IModelStateDictionary
    {
        bool TryGetValue(string key, out ModelState modelState);
        ModelState this[string key] { get; }
        bool ContainsKey(string key);
        bool IsValid { get; }
    }

    public class ModelState
    {
        public ModelState()
        {
                
        }

        public ModelState(FormFactoryModelStateErrors errors, FormFactoryModelStateValue value)
        {
            Errors = errors;
            Value = value;
        }

        public FormFactoryModelStateValue Value { get; private set; }
        public FormFactoryModelStateErrors Errors { get; private set; }
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

    public class FormFactoryModelStateValue
    {
        public FormFactoryModelStateValue(object attemptedValue)
        {
            AttemptedValue = attemptedValue;
        }

        public object AttemptedValue { get; private set; }
    }

    public interface UrlHelper
    {
        string Action(string actionName, string controllerName);
    }
}