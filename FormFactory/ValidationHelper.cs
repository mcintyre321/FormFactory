using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

namespace FormFactory
{
    public static class ValidationHelper
    {

        public static MvcHtmlString AllValidationMessages(this HtmlHelper helper, string modelName)
        {
            if (HasErrors(helper, modelName))
            {
                var message = string.Join(", ", helper.ViewData.ModelState[modelName].Errors.Select(e => e.ErrorMessage));
                return new MvcHtmlString(message);
            }
            return new MvcHtmlString("");
        }

        public static bool HasErrors(this HtmlHelper helper, string modelName)
        {
            return helper.ViewData.ModelState.ContainsKey(modelName) &&
                   helper.ViewData.ModelState[modelName].Errors.Count > 0;
        }

        public static MvcHtmlString UnobtrustiveValidation(this PropertyVm property)
        {
            var sb = new StringBuilder();

            var rules = new List<ModelClientValidationRule>();

            var attributes = property.GetCustomAttributes();
            attributes.OfType<RangeAttribute>()
                .Each(a => rules.Add(new ModelClientValidationRangeRule(a.FormatErrorMessage(property.DisplayName), a.Minimum, a.Maximum))).Eval();

            attributes.OfType<RequiredAttribute>()
                .Each(a => rules.Add(new ModelClientValidationRequiredRule(a.FormatErrorMessage(property.DisplayName)))).Eval();

            attributes.OfType<StringLengthAttribute>()
                .Each(a => rules.Add(new ModelClientValidationStringLengthRule(a.FormatErrorMessage(property.DisplayName), a.MinimumLength, a.MaximumLength))).Eval();

            attributes.OfType<RegularExpressionAttribute>()
                            .Each(a => rules.Add(new ModelClientValidationRegexRule(a.FormatErrorMessage(property.DisplayName), a.Pattern))).Eval();

            if (rules.Any() == false) return new MvcHtmlString("");
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

            return new MvcHtmlString(sb.ToString());
        }
    }

}