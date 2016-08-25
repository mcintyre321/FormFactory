using System;
using System.Linq;
using FormFactory.ViewHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FormFactory.AspMvc.Wrappers
{
    public class FormFactoryHtmlHelper : FormFactory.FfHtmlHelper<ViewDataDictionary>
    {
        private readonly IHtmlHelper _helper;

        public FormFactoryHtmlHelper(IHtmlHelper helper)
        {
            _helper = helper;
        }

        public UrlHelper Url()
        {
            IUrlHelper urlHelper = (IUrlHelper) _helper.ViewContext.HttpContext.RequestServices.GetService(typeof(IUrlHelper));
            return new FormFactoryUrlHelper(urlHelper);
        }

        public string WriteTypeToString(Type type)
        {
            throw new NotImplementedException("uh o9h");
            //return new Encoder().WriteTypeToString(type);
        }
       

        public ViewData ViewData { get { return new FormFactoryViewData(_helper.ViewData); } }
        public IViewFinder ViewFinder { get{return new FormFactoryContext(_helper.ViewContext);} }

        public IHtmlHelper InnerHtmlHelper
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
            this._helper.RenderPartialAsync(partialName, model, null).RunSynchronously();
        }

        public PropertyVm CreatePropertyVm(Type objectType, string name)
        {
            return new PropertyVm(objectType, name);
        }

        public ObjectChoices[] Choices(PropertyVm model) //why is this needed? HM
        {
            var html = this;
            var choices = (from obj in model.Choices.Cast<object>().ToArray()
                           let choiceType = obj == null ? model.Type : obj.GetType()
                           let properties = FF.PropertiesFor(obj, choiceType)
                               .Each(p => p.Name = model.Name + "." + p.Name)
                               .Each(p => p.Readonly |= model.Readonly)
                               .Each(p => p.Id = Guid.NewGuid().ToString())
                           select new ObjectChoices { obj = obj, choiceType = choiceType, properties = properties, name = (obj != null ? obj.DisplayName() : choiceType.DisplayName()) }).ToArray();
            return choices;
        }

      
    }
}