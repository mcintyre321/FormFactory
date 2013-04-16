using System.Web.Mvc;

namespace FormFactory.AspMvc.Wrappers
{
    public class FormFactoryViewData : ViewData
    {
        private readonly ViewDataDictionary _viewData;

        public FormFactoryViewData(ViewDataDictionary viewData)
        {
            _viewData = viewData;
        }

        public IModelStateDictionary ModelState
        {
            get { return new FormFactoryModelStateDictionary(_viewData.ModelState); }
        }

        public object Model { get; private set; }
    }
}