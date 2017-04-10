using System;
using System.Collections;
using System.Collections.Generic;
using FormFactory.Attributes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using FormFactory.AspMvc.Wrappers;

namespace FormFactory
{
    public static class AspMvcValidationHelper
    {

        public static MvcHtmlString AllValidationMessages(this System.Web.Mvc.HtmlHelper helper, string modelName)
        {
            if (HasErrors(helper, modelName))
            {
                var message = string.Join(", ", helper.ViewData.ModelState[modelName].Errors.Select(e => e.ErrorMessage));
                return new MvcHtmlString(message);
            }
            return new MvcHtmlString("");
        }

        public static bool HasErrors(this System.Web.Mvc.HtmlHelper helper, string modelName)
        {
            return helper.ViewData.ModelState.ContainsKey(modelName) &&
                   helper.ViewData.ModelState[modelName].Errors.Count > 0;
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
            return (a == null) ? null : new ModelClientValidationRangeRule(a.FormatErrorMessage(propertyVm.DisplayName), a.Minimum, a.Maximum);
        }

        private static ModelClientValidationRule RequiredAttributeRule(PropertyVm propertyVm, object attribute)
        {
            var a = attribute as RequiredAttribute ;
            return (a == null) ? null : new ModelClientValidationRequiredRule(a.FormatErrorMessage(propertyVm.DisplayName));
        }

        private static ModelClientValidationRule StringLengthAttributeRule(PropertyVm propertyVm, object attribute)
        {
            var a = attribute as StringLengthAttribute;
            return (a == null) ? null : new ModelClientValidationStringLengthRule(a.FormatErrorMessage(propertyVm.DisplayName), a.MinimumLength, a.MaximumLength);
        }
        private static ModelClientValidationRule RegexAttributeRule(PropertyVm propertyVm, object attribute)
        {
            var a = attribute as RegularExpressionAttribute;
            return (a == null) ? null : new ModelClientValidationRegexRule(a.FormatErrorMessage(propertyVm.DisplayName), a.Pattern);
        }

        public delegate IEnumerable<ModelClientValidationRule> UnobtrusiveValidationRule(PropertyVm property);
        public delegate ModelClientValidationRule UnobtrusiveValidationAttributeRule(PropertyVm property, object attribute);




        public static MvcHtmlString UnobtrusiveValidation(this HtmlHelper helper, PropertyVm property)
        {
            var unobtrusiveValidation = ValidationHelper.UnobtrusiveValidation(new FormFactoryHtmlHelper(helper), property);

            return new MvcHtmlString(unobtrusiveValidation);
        }
    }

}