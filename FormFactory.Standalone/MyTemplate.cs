using System;
using System.Text;
using System.Threading.Tasks;

namespace FormFactory.Standalone
{
    public abstract class MyTemplate<TModel> : MyTemplate
    {
        public new TModel Model
        {
            get { return (TModel) base.Model;
                
            }
            set { base.Model = value; }
        }
    }

    public abstract class MyTemplate
    {


        public StringBuilder sb = new StringBuilder();

        // this will map to @Model (property name)
        public object Model { get; set; }
        public ViewData ViewData { get; set; }
        public virtual void WriteLiteral(object value)
        {
            if (value == null)
            {
                return;
            }

            WriteLiteral(value.ToString());
        }

        /// <summary>
        /// Writes the specified <paramref name="value"/> without HTML encoding to <see cref="Output"/>.
        /// </summary>
        /// <param name="value">The <see cref="string"/> to write.</param>
        public virtual void WriteLiteral(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                sb.Append(value);
            }
        }

        public virtual void BeginWriteAttribute(
            string name,
            string prefix,
            int prefixOffset,
            string suffix,
            int suffixOffset,
            int attributeValuesCount)
        {
            if (prefix == null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            if (suffix == null)
            {
                throw new ArgumentNullException(nameof(suffix));
            }

            Name = name;
            Prefix = prefix;
            PrefixOffset = prefixOffset;
            Suffix = suffix;
            SuffixOffset = suffixOffset;
            AttributeValuesCount = attributeValuesCount;
            Suppressed = false;
            
            // Single valued attributes might be omitted in entirety if it the attribute value strictly evaluates to
            // null  or false. Consequently defer the prefix generation until we encounter the attribute value.
            if (attributeValuesCount != 1)
            {
                WritePositionTaggedLiteral(prefix, prefixOffset);
            }
        }

        private bool IsBoolFalseOrNullValue(string prefix, object value)
        {
            return string.IsNullOrEmpty(prefix) &&
                   (value == null ||
                    (value is bool && !(bool)value));
        }

        public void WriteAttributeValue(
            string prefix,
            int prefixOffset,
            object value,
            int valueOffset,
            int valueLength,
            bool isLiteral)
        {
            if (AttributeValuesCount == 1)
            {
                if (IsBoolFalseOrNullValue(prefix, value))
                {
                    // Value is either null or the bool 'false' with no prefix; don't render the attribute.
                    Suppressed = true;
                    return;
                }

                // We are not omitting the attribute. Write the prefix.
                WritePositionTaggedLiteral(Prefix, PrefixOffset);

                if (IsBoolTrueWithEmptyPrefixValue(prefix, value))
                {
                    // The value is just the bool 'true', write the attribute name instead of the string 'True'.
                    value = Name;
                }
            }

            // This block handles two cases.
            // 1. Single value with prefix.
            // 2. Multiple values with or without prefix.
            if (value != null)
            {
                if (!string.IsNullOrEmpty(prefix))
                {
                    WritePositionTaggedLiteral(prefix, prefixOffset);
                }

                BeginContext(valueOffset, valueLength, isLiteral);

                WriteUnprefixedAttributeValue(value, isLiteral);

                EndContext();
            }
        }
        private void WriteUnprefixedAttributeValue(object value, bool isLiteral)
        {
            var stringValue = value as string;

            // The extra branching here is to ensure that we call the Write*To(string) overload where possible.
            if (isLiteral && stringValue != null)
            {
                WriteLiteral(stringValue);
            }
            else if (isLiteral)
            {
                WriteLiteral(value);
            }
            else if (stringValue != null)
            {
                Write(stringValue);
            }
            else
            {
                Write(value);
            }
        }


        public object Name { get; set; }

        private bool IsBoolTrueWithEmptyPrefixValue(string prefix, object value)
        {
            // If the value is just the bool 'true', use the attribute name as the value.
            return string.IsNullOrEmpty(prefix) &&
                   (value is bool && (bool)value);
        }
        public int PrefixOffset { get; set; }

        public string Prefix { get; set; }

        public bool Suppressed { get; set; }

        int AttributeValuesCount { get; set; }

        public virtual void EndWriteAttribute()
        {
            if (!Suppressed)
            {
                WritePositionTaggedLiteral(Suffix, SuffixOffset);
            }
        }

        public int SuffixOffset { get; set; }

        public string Suffix { get; set; }

        private void WritePositionTaggedLiteral(string value, int position)
        {
            BeginContext(position, value.Length, isLiteral: true);
            WriteLiteral(value);
            EndContext();
        }
        
        void BeginContext(int position, int length, bool isLiteral)
        {
            const string BeginContextEvent = "Microsoft.AspNetCore.Mvc.Razor.BeginInstrumentationContext";
            //
            // if (DiagnosticSource?.IsEnabled(BeginContextEvent) == true)
            // {
            //     DiagnosticSource.Write(
            //         BeginContextEvent,
            //         new
            //         {
            //             httpContext = Context,
            //             path = Path,
            //             position = position,
            //             length = length,
            //             isLiteral = isLiteral,
            //         });
            // }
        }
        public void EndContext()
        {
            const string EndContextEvent = "Microsoft.AspNetCore.Mvc.Razor.EndInstrumentationContext";
            //
            // if (DiagnosticSource?.IsEnabled(EndContextEvent) == true)
            // {
            //     DiagnosticSource.Write(
            //         EndContextEvent,
            //         new
            //         {
            //             httpContext = Context,
            //             path = Path,
            //         });
            // }
        }

        public void Write(object obj)
        {
            // replace that by a text writer for example
            sb.Append(obj);
        }

        public async virtual Task ExecuteAsync()
        {
            await Task.Yield(); // whatever, we just need something that compiles...
        }

        public MyFfHtmlHelper Html { get; set; }

        public override string ToString() => sb.ToString();
    }
}