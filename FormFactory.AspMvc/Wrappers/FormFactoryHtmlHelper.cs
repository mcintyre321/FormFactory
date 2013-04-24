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


        public IHtmlString UnobtrusiveValidation(PropertyVm model)
        {
            var sb = new StringBuilder();

            var rules = ValidationHelper.UnobtrusiveValidationRules.SelectMany(r => r(model));

            if (rules.Any() == false) return  (HtmlString)"";

            sb.Append("data-val=\"true\" ");
            foreach (var rule in rules)
            {
                var prefix = string.Format(" data-val-{0}", rule.ValidationType);
                sb.AppendFormat(prefix + "=\"{0}\" ", rule.ErrorMessage);
                foreach (var parameter in rule.ValidationParameters)
                {
                    sb.AppendFormat(prefix + "-{0}=\"{1}\" ", parameter.Key, parameter.Value);
                }
            }

            return (HtmlString) (sb.ToString());
        }
    }
}