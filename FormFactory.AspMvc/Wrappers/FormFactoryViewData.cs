using System.Web.Mvc;

namespace FormFactory.AspMvc.Wrappers
{
    public class FormFactoryViewData : ViewData
    {
        public FormFactoryViewData(ViewDataDictionary viewData) : base(new FormFactoryModelStateDictionary(viewData.ModelState), viewData.Model)
        {
        }
    }
}