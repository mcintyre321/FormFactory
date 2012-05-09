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
        public FormVm(HtmlHelper html, MethodInfo mi) : this(html)
        {
            var controllerName = mi.ReflectedType.Name;
            controllerName = controllerName.Substring(0, controllerName.LastIndexOf("Controller"));
            ActionUrl = html.Url().Action(mi.Name, controllerName);

            HtmlHelper = html;
            DisplayName = mi.Name.Sentencise();

            var inputs = new List<PropertyVm>();


            foreach (var pi in mi.GetParameters())
            {
                inputs.Add(new PropertyVm(pi, html));
            }

            Inputs = inputs;

            ExcludePropertyErrorsFromValidationSummary = true;
        }
        public FormVm(HtmlHelper html)
        {
            Inputs = new List<PropertyVm>();
            this.ActionUrl = "";
            this.DisplayName = "";
            this.SideMessage = new MvcHtmlString("");
            this.HtmlHelper = html;
            ShowValidationSummary = true;
            ExcludePropertyErrorsFromValidationSummary = true;
        }

        public string ActionUrl { get; set; }
        public MvcHtmlString SideMessage { get; set; }
        public IEnumerable<PropertyVm> Inputs { get; set; }

        public HtmlHelper HtmlHelper { get; set; }

        public string DisplayName { get; set; }

        public bool ExcludePropertyErrorsFromValidationSummary { get; set; }

        public bool ShowValidationSummary { get; set; }

        public FormVm Render()
        {
            RenderStart();
            RenderActionInputs();
            RenderButtons();
            return this;
        }

        public FormVm RenderButtons()
        {
            HtmlHelper.RenderPartial("FormFactory/Form.Actions", this);
            return this;
        }

        public FormVm RenderActionInputs()
        {
            foreach (var input in Inputs)
            {
                HtmlHelper.RenderPartial("FormFactory/Form.Property", input);
            }
            return this;
        }

        public FormVm RenderStart()
        {
            HtmlHelper.RenderPartial("FormFactory/Form.Start", this);
            return this;
        }


        public void Dispose()
        {
            HtmlHelper.RenderPartial("FormFactory/Form.Close", this);
        }


    }
}