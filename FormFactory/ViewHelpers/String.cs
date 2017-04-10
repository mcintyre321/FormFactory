using System;
using System.Collections.Generic;
using FormFactory.Attributes;
using System.Linq;
using System.Text;

namespace FormFactory.ViewHelpers
{
    public static class String
    {
        public static string GetInputTypeFromDataTypeAttribute(PropertyVm Model)
        {
            var dataAttributes = Model.GetCustomAttributes().ToArray();
            var inputType = "text";
            if (dataAttributes.OfType<PasswordAttribute>().Any()) inputType = "password";
            if (dataAttributes.OfType<MultilineTextAttribute>().Any()) inputType = "textarea";
            return inputType;
        }

        public static string[] EscapedSuggestions(PropertyVm model)
        {
            return model.Suggestions.Cast<string>().Select(s => s.Replace("'", "''")).ToArray();
        }

        public static string GetTypeAheadAttribute(PropertyVm model)
        {
            if (model.Suggestions != null)
            {
                var suggestions = model.Suggestions.Cast<string>().ToArray();
                if (suggestions.Any())
                {
                    var escapedSuggestions = suggestions.Select(s => "\"" + s.Replace("\"", "\"\"") + "\"");
                    return (" data-provide=\"typeahead\" data-source='[" + string.Join(",", escapedSuggestions) + "]' autocomplete=\"off\"");
                }
            }
            return ("");
        }
    }
    public static class Object
    
    {
        

        public static int GetSelectedIndex(ObjectChoices[] choices)
        {
            return choices
                .Select((choice, index) => new { index, choice })
                .Where(pair => pair.choice.obj.IsSelected())
                .Select(pair => pair.index)
                .FirstOrDefault();
        }
        public static bool UseRadio(PropertyVm vm)
        {
            return vm.GetCustomAttributes().OfType<RadioAttribute>().Any();
        }
    }

    public class ObjectChoices
    {
        public object obj { get; set; }

        public Type choiceType { get; set; }

        public IEnumerable<PropertyVm> properties { get; set; }

        public string name { get; set; }
    }
}
