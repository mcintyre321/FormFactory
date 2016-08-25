using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using FormFactory.UnobtrusiveValidation;

namespace FormFactory
{
    public static class ValidationHelper
    {

        public static string AllValidationMessages(this FfHtmlHelper helper, string modelName)
        {
            if (HasErrors(helper, modelName))
            {
                var message = string.Join(", ", helper.ViewData.ModelState[modelName].Errors.Select(e => e.ErrorMessage));
                return (message);
            }
            return ("");
        }

        public static bool HasErrors(this FfHtmlHelper helper, string modelName)
        {
            return helper.ViewData.ModelState.ContainsKey(modelName) &&
                   helper.ViewData.ModelState[modelName].Errors.Any();
        }

        public static List<UnobtrusiveValidationRule> UnobtrusiveValidationRules = new List<UnobtrusiveValidationRule>()
        {
            GetRulesFromAttributes
        };



        private static IEnumerable<ModelClientValidationRule> GetRulesFromAttributes(PropertyVm property)
        {
            return property.GetCustomAttributes()
                           .SelectMany(attribute => UnobtrusiveValidationAttributeRules,
                                       (attribute, rule) => rule(property, attribute))
                           .Where(r => r != null);
        }

        public static List<UnobtrusiveValidationAttributeRule> UnobtrusiveValidationAttributeRules = new List
            <UnobtrusiveValidationAttributeRule>()
        {
            RangeAttribteRule,
            RequiredAttributeRule,
            StringLengthAttributeRule,
            RegexAttributeRule
        };

        private static ModelClientValidationRule RangeAttribteRule(PropertyVm propertyVm, object attribute)
        {
            var a = attribute as RangeAttribute;
            return (a == null)
                       ? null
                       : new ModelClientValidationRangeRule(a.FormatErrorMessage(propertyVm.DisplayName), a.Minimum,
                                                            a.Maximum);
        }

        private static ModelClientValidationRule RequiredAttributeRule(PropertyVm propertyVm, object attribute)
        {
            var a = attribute as RequiredAttribute;
            return (a == null)
                       ? null
                       : new ModelClientValidationRequiredRule(a.FormatErrorMessage(propertyVm.DisplayName));
        }

        private static ModelClientValidationRule StringLengthAttributeRule(PropertyVm propertyVm, object attribute)
        {
            var a = attribute as StringLengthAttribute;
            return (a == null)
                       ? null
                       : new ModelClientValidationStringLengthRule(a.FormatErrorMessage(propertyVm.DisplayName),
                                                                   a.MinimumLength, a.MaximumLength);
        }

        private static ModelClientValidationRule RegexAttributeRule(PropertyVm propertyVm, object attribute)
        {
            var a = attribute as RegularExpressionAttribute;
            return (a == null)
                       ? null
                       : new ModelClientValidationRegexRule(a.FormatErrorMessage(propertyVm.DisplayName), a.Pattern);
        }

        public delegate IEnumerable<ModelClientValidationRule> UnobtrusiveValidationRule(PropertyVm property);

        public delegate ModelClientValidationRule UnobtrusiveValidationAttributeRule(PropertyVm property, object attribute);

        public static string UnobtrusiveValidation(FfHtmlHelper helper, PropertyVm property)
        {
            var sb = new StringBuilder();

            var rules = UnobtrusiveValidationRules.SelectMany(r => r(property));

            if (rules.Any() == false) return ("");

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

            return (sb.ToString());
        }

        public static string UnobtrusiveValidation(this PropertyVm property)
        {
            var sb = new StringBuilder();

            var rules = UnobtrusiveValidationRules.SelectMany(r => r(property));

            if (rules.Any() == false) return ("");

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

            return (sb.ToString());
        }
    }
}