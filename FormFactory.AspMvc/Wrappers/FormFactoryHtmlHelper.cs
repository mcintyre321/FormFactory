using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using FormFactory.ViewHelpers;
using Encoder = FormFactory.AspMvc.ModelBinders.Encoder;

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

        public HtmlHelper InnerHtmlHelper
        {
            get { return _helper; }
        }

        public string Partial(string partialName, object vm, ViewDataDictionary viewData)
        {
            return _helper.Partial(partialName, vm, viewData).ToHtmlString();
        }
        public string Partial(string partialName, object vm)
        {
            return Partial(partialName, vm, null);
        }

        public string Raw(string s)
        {
            return (_helper.Raw(s).ToHtmlString());
        }
 

        public void RenderPartial(string partialName, object model)
        {
            this._helper.RenderPartial(partialName, model);
        }

        public PropertyVm CreatePropertyVm(Type objectType, string name)
        {
            return new PropertyVm(this, objectType, name);
        }

        public ObjectChoices[] Choices(PropertyVm model) //why is this needed? HM
        {
            var html = this;
            var choices = (from obj in model.Choices.Cast<object>().ToArray()
                           let choiceType = obj == null ? model.Type : obj.GetType()
                           let properties = VmHelper.PropertiesFor(html, obj, choiceType)
                               .Each(p => p.Name = model.Name + "." + p.Name)
                               .Each(p => p.Readonly |= model.Readonly)
                               .Each(p => p.Id = Guid.NewGuid().ToString())
                           select new ObjectChoices { obj = obj, choiceType = choiceType, properties = properties, name = (obj != null ? obj.DisplayName() : choiceType.DisplayName()) }).ToArray();
            return choices;
        }

      
    }
}