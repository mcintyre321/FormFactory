using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace FormFactory
{
    public class FormVm : IDisposable
    {
        public FormVm(HtmlHelper html, MethodInfo mi, string displayName)
        {
            var controllerName = mi.ReflectedType.Name;
            controllerName = controllerName.Substring(0, controllerName.LastIndexOf("Controller"));
            HtmlHelper = html;
            DisplayName = displayName ?? mi.Name.Sentencise();
            ActionUrl = html.Url().Action(mi.Name, controllerName);
            Inputs = mi.GetParameters().Select(pi => new PropertyVm(pi, html));
        }
        public string ActionUrl { get; set; }
        public MvcHtmlString SideMessage { get; set; }
        public IEnumerable<PropertyVm> Inputs { get; set; }

        public HtmlHelper HtmlHelper { get; set; }

        public string DisplayName { get; set; }

        public FormVm Render()
        {
            RenderStart();
            RenderActionInputs();
            RenderButtons();
            return this;
        }

        public FormVm RenderButtons()
        {
            HtmlHelper.RenderPartial("Form.Actions", this);
            return this;
        }

        public FormVm RenderActionInputs()
        {
            foreach (var input in Inputs)
            {
                HtmlHelper.RenderPartial("Form.Property", input);
            }
            return this;
        }

        public FormVm RenderStart()
        {
            HtmlHelper.RenderPartial("Form.Start", this);
            return this;
        }


        public void Dispose()
        {
            HtmlHelper.RenderPartial("Form.Close", this);
        }


    }
}