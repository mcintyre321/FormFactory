using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Encoder = FormFactory.AspMvc.Mvc.ModelBinders.Encoder;

namespace FormFactory.AspMvc.Wrappers
{
    public class FormFactoryHtmlHelper : FormFactory.FfHtmlHelper<ViewDataDictionary>
    {
        private readonly System.Web.Mvc.HtmlHelper _helper;

        public FormFactoryHtmlHelper(System.Web.Mvc.HtmlHelper helper)
        {
            _helper = helper;
        }

        public UrlHelper Url()
        {
            return new FormFactoryUrlHelper(new System.Web.Mvc.UrlHelper(_helper.ViewContext.RequestContext));
        }

        public string WriteTypeToString(Type type)
        {
            return new Encoder().WriteTypeToString(type);
        }
       

        public ViewData ViewData { get { return new FormFactoryViewData(_helper.ViewData); } }
        public FfContext FfContext { get{return new FormFactoryContext(_helper.ViewContext.Controller.ControllerContext);} }

        public IHtmlString Partial(string partialName, object vm, ViewDataDictionary viewData)
        {
            return (FormFactory.HtmlString)_helper.Partial(partialName, vm, viewData).ToHtmlString();
        }
        public IHtmlString Partial(string partialName, object vm)
        {
            return Partial(partialName, vm, null);
        }

        public IHtmlString Raw(string s)
        {
            return new HtmlString(_helper.Raw(s).ToHtmlString());
        }
 

        public void RenderPartial(string partialName, object model)
        {
            this._helper.RenderPartial(partialName, model);
        }

        public PropertyVm CreatePropertyVm(Type objectType, string name)
        {
            return new PropertyVm(this, objectType, name);
        }
         
    }
}