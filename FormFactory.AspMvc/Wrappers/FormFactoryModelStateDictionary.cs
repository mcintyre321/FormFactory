using System.Web.Mvc;

namespace FormFactory.AspMvc.Wrappers
{
    public class FormFactoryModelStateDictionary : IModelStateDictionary
    {
        private readonly ModelStateDictionary _modelState;

        public FormFactoryModelStateDictionary(ModelStateDictionary modelState)
        {
            _modelState = modelState;
        }

        public bool TryGetValue(string key, out ModelState modelState)
        {
            System.Web.Mvc.ModelState ms;
            var result = _modelState.TryGetValue(key, out ms);
            if (result)
            {
                modelState = new FormFactoryModelState(ms);
            }
            else
            {
                modelState = null;
            }
            return result;
        }
    }
}