using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace FormFactory.ViewHelpers
{
    public static class String
    {
        public static string GetInputTypeFromDataTypeAttribute(PropertyVm Model)
        {
            var dataAttributes = Model.GetCustomAttributes().OfType<DataTypeAttribute>().ToList();
            var inputType = "text";
            if (dataAttributes.Any(da => da.DataType == DataType.Password)) inputType = "password";
            if (dataAttributes.Any(da => da.DataType == DataType.MultilineText)) inputType = "textarea";
            return inputType;
        }

        public static string[] EscapedSuggestions(PropertyVm model)
        {
            return model.Suggestions.Cast<string>().Select(s => s.Replace("'", "''")).ToArray();
        }

        public static HtmlString GetTypeAheadAttribute(PropertyVm model)
        {
            if (model.Suggestions != null)
            {
                var suggestions = model.Suggestions.Cast<string>().ToArray();
                if (suggestions.Any())
                {
                    var escapedSuggestions = suggestions.Select(s => "\"" + s.Replace("\"", "\"\"") + "\"");
                    return new HtmlString(" data-provide=\"typeahead\" data-source='[" + string.Join(",", escapedSuggestions) + "]' autocomplete=\"off\"");
                }
            }
            return new HtmlString("");
        }
    }
    public static class Object
    
    {
        public static ObjectChoices[] Choices(this FfHtmlHelper html, PropertyVm model) //why is this needed? HM
        {
            var choices = (from obj in model.Choices.Cast<object>().ToArray()
                           let choiceType = obj == null ? model.Type : obj.GetType()
                           let properties = html.PropertiesFor(obj, choiceType)
                               .Each(p => p.Name = model.Name + "." + p.Name)
                               .Each(p => p.Readonly |= model.Readonly)
                               .Each(p => p.Id = Guid.NewGuid().ToString())
                           select new ObjectChoices { obj = obj, choiceType = choiceType, properties = properties, name = (obj != null ? obj.DisplayName() : choiceType.DisplayName()) }).ToArray();
            return choices;
        }

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
            return vm.GetCustomAttributes().OfType<DataTypeAttribute>().Any(dt => dt.CustomDataType == "Radio");
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
