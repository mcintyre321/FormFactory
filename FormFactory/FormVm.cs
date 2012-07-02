using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using FormFactory.Attributes;

namespace FormFactory
{
    public class FormVm : IDisposable
    {
        private readonly string _actionName;
        private readonly string _controllerName;

        public FormVm(HtmlHelper html, MethodInfo mi) : this(html)
        {
            this._actionName = mi.Name;
            var controllerTypeName = mi.ReflectedType.Name;
            this._controllerName = controllerTypeName.Substring(0, controllerTypeName.LastIndexOf("Controller"));

            HtmlHelper = html;
            DisplayName = mi.Name.Sentencise();

            var inputs = new List<PropertyVm>();


            foreach (var pi in mi.GetParameters())
            {
                if (pi.GetCustomAttributes(true).Any(x => x is FormModelAttribute))
                {
                    inputs.AddRange(pi.ParameterType.GetProperties()
                                        .Select(pi2 => new PropertyVm(pi, pi2, html).Then(p => p.Name = pi.Name + "." + p.Name)));
                }
                else
                {
                    inputs.Add(new PropertyVm(pi, html));
                }
            }

            Inputs = inputs;

            ExcludePropertyErrorsFromValidationSummary = true;
        }
        public FormVm(HtmlHelper html)
        {
            Inputs = new List<PropertyVm>();
            this.DisplayName = "";
            this.HtmlHelper = html;
            ShowValidationSummary = true;
            ExcludePropertyErrorsFromValidationSummary = true;
            RenderAntiForgeryToken = Defaults.RenderAntiForgeryToken;
        }

        private string _actionUrlOverride;
        public string ActionUrl
        {
            get
            {
                return _actionUrlOverride ??
                       (
                           UseHttps.HasValue
                               ? HtmlHelper.Url().Action(_actionName, _controllerName, null,
                                                         UseHttps.Value ? "https" : "http")
                               : HtmlHelper.Url().Action(_actionName, _controllerName)
                       );
            }
            set { _actionUrlOverride = value; }
        }

        public IEnumerable<PropertyVm> Inputs { get; set; }

        public HtmlHelper HtmlHelper { get; set; }

        public string DisplayName { get; set; }

        public bool ExcludePropertyErrorsFromValidationSummary { get; set; }

        public bool ShowValidationSummary { get; set; }
        public bool RenderAntiForgeryToken { get; set; }

        public string AdditionalClasses { get; set; }

        /// <summary>
        /// Leave null to remain on current scheme.
        /// True to force https from http; False to force http from https.
        /// NOTE: Does not apply if ActionUrl has been explicitly set.
        /// </summary>
        public bool? UseHttps { get; set; }

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