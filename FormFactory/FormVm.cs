using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FormFactory.Attributes;

namespace FormFactory
{
    public class FormVm : IHasDisplayName
    {

        public FormVm(FfHtmlHelper html, MethodInfo mi)  
        {
            var _actionName = mi.Name;
            var controllerTypeName = mi.ReflectedType.Name;
            var _controllerName = controllerTypeName.Substring(0, controllerTypeName.LastIndexOf("Controller"));

            var inputs = new List<PropertyVm>();


            foreach (var pi in mi.GetParameters())
            {
                if (pi.GetCustomAttributes(true).Any(x => x is FormModelAttribute))
                {
                    inputs.AddRange(pi.ParameterType.GetProperties()
                                        .Select(pi2 => new PropertyVm(pi, pi2).Then(p => p.Name = pi.Name + "." + p.Name)));
                }
                else
                {
                    inputs.Add(new PropertyVm(pi));
                }
            }

            Inputs = inputs;
            this.DisplayName = mi.Name.Sentencise(true);
            ExcludePropertyErrorsFromValidationSummary = true;


            this.ActionUrl = UseHttps.HasValue
                ? html.Url().Action(_actionName, _controllerName, null, UseHttps.Value ? "https" : "http")
                : html.Url().Action(_actionName, _controllerName);
        }
        public FormVm()
        {
            Inputs = new List<PropertyVm>();
            this.DisplayName = "";
            ShowValidationSummary = true;
            ExcludePropertyErrorsFromValidationSummary = true;
            RenderAntiForgeryToken = Defaults.RenderAntiForgeryToken;
        }

        public string ActionUrl { get; set; }
        public string Method { get; set; }
        public string EncType { get; set; } = "multipart/form-data";
        public IEnumerable<PropertyVm> Inputs { get; set; }


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

       

        public override string ToString()
        {
            return base.ToString();
        }
    }
}