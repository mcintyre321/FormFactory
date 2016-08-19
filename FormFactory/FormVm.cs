using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FormFactory.Attributes;
using FormFactory.Components;

namespace FormFactory
{
    public class FormVm : IHasDisplayName
    {

        public FormVm(MethodInfo mi, string actionUrl)  
        {

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
            Buttons.Add(new PropertyVm(typeof(SubmitButton), "") { Value = new FormFactory.Components.SubmitButton()});
            this.DisplayName = mi.Name.Sentencise(true);
            ExcludePropertyErrorsFromValidationSummary = true;


            this.ActionUrl = actionUrl;
        }
        public FormVm()
        {
            this.DisplayName = "";
            ShowValidationSummary = true;
            ExcludePropertyErrorsFromValidationSummary = true;
            RenderAntiForgeryToken = Defaults.RenderAntiForgeryToken;
        }

        public string ActionUrl { get; set; }
        public string Method { get; set; }
         
        public IList<PropertyVm> Inputs { get; set; } = new List<PropertyVm>();
        public IList<PropertyVm> Buttons { get; set; } = new List<PropertyVm>();


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

       

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

