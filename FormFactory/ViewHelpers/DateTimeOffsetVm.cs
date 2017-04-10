using System;
using FormFactory.Attributes;
using System.Linq;

namespace FormFactory.ViewHelpers
{
    public class DateTimeOffsetVm
    {
        public DateTimeOffsetVm(PropertyVm model)
        {
            var dateAttr = model
                .GetCustomAttributes()
                .FirstOrDefault(a => a is DateAttribute || a is DateTimeAttribute);
            var isDate = dateAttr is DateAttribute;
            var displayFormatAttribute = model.GetCustomAttributes().OfType<DisplayFormatAttribute>().SingleOrDefault();
            stringFormat = (displayFormatAttribute != null ? displayFormatAttribute.DataFormatString : null) ??
                               (isDate ? "dd MMM yyyy" : "g");
            behaviour = isDate ? "datepicker" : "datetimepicker";
            if (model.Value is string) valueAsString = model.Value as string;
            else
            {
                var dateTimeOffset = model.Value as DateTimeOffset?;
                if (dateTimeOffset == null)
                {
                    valueAsString = "";
                }
                else
                {
                    valueAsString = ((DateTimeOffset?) model.Value).Value.ToString(stringFormat);
                }
            }
        }

        public string behaviour { get; set; }

        public string stringFormat { get; set; }

        public string valueAsString { get; set; }
    }
}