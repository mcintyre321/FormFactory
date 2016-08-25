using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt* in the project root for license information.
// * https://github.com/mono/aspnetwebstack/blob/master/License.txt

namespace FormFactory.UnobtrusiveValidation
{
    public class ModelClientValidationEqualToRule : ModelClientValidationRule
    {
        public ModelClientValidationEqualToRule(string errorMessage, object other)
        {
            ErrorMessage = errorMessage;
            ValidationType = "equalto";
            ValidationParameters["other"] = other;
        }
    }

    internal class ModelClientValidationMembershipPasswordRule : ModelClientValidationRule
    {
        public ModelClientValidationMembershipPasswordRule(string errorMessage, int minRequiredPasswordLength,
                                                           int minRequiredNonAlphanumericCharacters,
                                                           string passwordStrengthRegularExpression)
        {
            ErrorMessage = errorMessage;
            ValidationType = "password";

            if (minRequiredPasswordLength != 0)
            {
                ValidationParameters["min"] = minRequiredPasswordLength;
            }

            if (minRequiredNonAlphanumericCharacters != 0)
            {
                ValidationParameters["nonalphamin"] = minRequiredNonAlphanumericCharacters;
            }

            if (!String.IsNullOrEmpty(passwordStrengthRegularExpression))
            {
                ValidationParameters["regex"] = passwordStrengthRegularExpression;
            }
        }
    }

    public class ModelClientValidationRegexRule : ModelClientValidationRule
    {
        public ModelClientValidationRegexRule(string errorMessage, string pattern)
        {
            ErrorMessage = errorMessage;
            ValidationType = "regex";
            ValidationParameters.Add("pattern", pattern);
        }
    }

    public class ModelClientValidationRangeRule : ModelClientValidationRule
    {
        public ModelClientValidationRangeRule(string errorMessage, object minValue, object maxValue)
        {
            ErrorMessage = errorMessage;
            ValidationType = "range";
            ValidationParameters["min"] = minValue;
            ValidationParameters["max"] = maxValue;
        }
    }

    public class ModelClientValidationRequiredRule : ModelClientValidationRule
    {
        public ModelClientValidationRequiredRule(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ValidationType = "required";
        }
    }

    public class ModelClientValidationRule
    {
        private readonly Dictionary<string, object> _validationParameters = new Dictionary<string, object>();
        private string _validationType;

        public string ErrorMessage { get; set; }

        public IDictionary<string, object> ValidationParameters
        {
            get { return _validationParameters; }
        }

        public string ValidationType
        {
            get { return _validationType ?? String.Empty; }
            set { _validationType = value; }
        }
    }

    public class ModelClientValidationStringLengthRule : ModelClientValidationRule
    {
        public ModelClientValidationStringLengthRule(string errorMessage, int minimumLength, int maximumLength)
        {
            ErrorMessage = errorMessage;
            ValidationType = "length";

            if (minimumLength != 0)
            {
                ValidationParameters["min"] = minimumLength;
            }

            if (maximumLength != Int32.MaxValue)
            {
                ValidationParameters["max"] = maximumLength;
            }
        }
    }
}