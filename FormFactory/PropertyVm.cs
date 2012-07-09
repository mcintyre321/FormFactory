using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using FormFactory.Attributes;

namespace FormFactory
{
    public class PropertyVm
    {
        static PropertyVm()
        {
            TypeSlug = t => t.AssemblyQualifiedName;
        }


        //this property is used to render the Type of an object choice
        public static Func<Type, string> TypeSlug { get; set; }

        public PropertyVm(ParameterInfo pi, HtmlHelper html)
            : this(html, pi.ParameterType, pi.Name)
        {
            ModelState modelState;
            if (html.ViewData.ModelState.TryGetValue(pi.Name, out modelState))
            {
                if (modelState.Value != null)
                    Value = modelState.Value.AttemptedValue;
            }
            Readonly = !true;
            IsHidden = pi.GetCustomAttributes(true).OfType<DataTypeAttribute>()
                .Any(x => x.CustomDataType == "Hidden");
            ShowLabel = pi.GetCustomAttributes(true).OfType<NoLabelAttribute>().Any() == false;
            LabelOnRight = pi.GetCustomAttributes(true).OfType<LabelOnRightAttribute>().Any();
            PlaceholderText = pi.GetCustomAttributes(true).OfType<PlaceholderAttribute>().FirstOrDefault().Maybe(x => x.Text);
            GetCustomAttributes = () => pi.GetCustomAttributes(true);

            var descriptionAttr = pi.GetCustomAttributes(true).OfType<DisplayAttribute>()
                .FirstOrDefault(x => !string.IsNullOrEmpty(x.Name));
            DisplayName = descriptionAttr != null ? descriptionAttr.Name : pi.Name.Sentencise();
        }
        public PropertyVm(ParameterInfo modelParamInfo, PropertyInfo pi, HtmlHelper html)
            : this(html, pi.PropertyType, pi.Name)
        {
            ModelState modelState;
            if (html.ViewData.ModelState.TryGetValue(modelParamInfo.Name + "." + pi.Name, out modelState))
            {
                if (modelState.Value != null)
                    Value = modelState.Value.AttemptedValue;
            }
            else if (html.ViewData.Model != null && (html.ViewData.Model.GetType() == modelParamInfo.ParameterType))
            {
                Value = pi.GetValue(html.ViewData.Model, new object[0]);
            }
            Readonly = !true;
            IsHidden = pi.GetCustomAttributes(true).OfType<DataTypeAttribute>()
                .Any(x => x.CustomDataType == "Hidden");
            ShowLabel = pi.GetCustomAttributes(true).OfType<NoLabelAttribute>().Any() == false;
            LabelOnRight = pi.GetCustomAttributes(true).OfType<LabelOnRightAttribute>().Any();
            PlaceholderText = pi.GetCustomAttributes(true).OfType<PlaceholderAttribute>().FirstOrDefault().Maybe(x => x.Text);
            GetCustomAttributes = () => pi.GetCustomAttributes(true);

            var descriptionAttr = pi.GetCustomAttributes(true).OfType<DisplayAttribute>()
                .FirstOrDefault(x => !string.IsNullOrEmpty(x.Name));
            DisplayName = descriptionAttr != null ? descriptionAttr.Name : pi.Name.Sentencise();

            // check to see if we're dealing with an enum or enumerable of enum
            var checkType = TypeHelper.GetEnumerableType(this.Type) ?? this.Type;
            checkType = Nullable.GetUnderlyingType(checkType) ?? checkType;
            if (checkType.IsEnum)
            {
                SetChoicesForEnumType(checkType);
            }
        }

        private void SetChoicesForEnumType(Type enumType)
        {
            Func<FieldInfo, string> getName = fieldInfo => Enum.GetName(enumType, (int) fieldInfo.GetValue(null));
            Func<FieldInfo, string> getDisplayName = fieldInfo =>
                                                         {
                                                             var attr =
                                                                 fieldInfo.GetCustomAttributes(
                                                                     typeof (DisplayAttribute), true).Cast
                                                                     <DisplayAttribute>().FirstOrDefault();
                                                             return attr != null ? attr.Name : null;
                                                         };
            Func<FieldInfo, string> getLabel = fieldInfo => getDisplayName(fieldInfo) ?? getName(fieldInfo).Sentencise();

            this.Choices = enumType.GetFields(BindingFlags.Static | BindingFlags.GetField |
                                              BindingFlags.Public).Select(x => new Tuple<string, string>(getName(x), getLabel(x)));
        }

        public object Source { get; set; }
        public PropertyVm(object model, PropertyInfo pi, HtmlHelper html) :
            this(html, pi.PropertyType, pi.Name)
        {
            Source = model;
            ModelState modelState;
            if (html.ViewData.ModelState.TryGetValue(pi.Name, out modelState))
            {
                if (modelState.Value != null)
                    Value = modelState.Value.AttemptedValue;
            }
            else if (pi.GetGetMethod() != null && model != null)
            {
                Value = pi.GetGetMethod().Invoke(model, null);
            }
            if (model != null)
            {
                MethodInfo choices = model.GetType().GetMethod(pi.Name + "_choices");
                if (choices != null)
                {
                    Choices = (IEnumerable)choices.Invoke(model, null);
                }
                MethodInfo suggestions = model.GetType().GetMethod(pi.Name + "_suggestions");
                if (suggestions != null)
                {
                    Suggestions = (IEnumerable)suggestions.Invoke(model, null);
                }
                var setter = pi.GetSetMethod();
                var getter = pi.GetGetMethod();
                Readonly = !(setter != null);
                Value = getter == null ? null : getter.Invoke(model, null);
            }
            GetCustomAttributes = () => pi.GetCustomAttributes(true);
            Readonly = !(pi.GetSetMethod() != null);
            IsHidden = pi.GetCustomAttributes(true).OfType<DataTypeAttribute>()
                .Any(x => x.CustomDataType == "Hidden");
            ShowLabel = pi.GetCustomAttributes(true).OfType<NoLabelAttribute>().Any() == false;
            LabelOnRight = pi.GetCustomAttributes(true).OfType<LabelOnRightAttribute>().Any();
            PlaceholderText = pi.GetCustomAttributes(true).OfType<PlaceholderAttribute>().FirstOrDefault().Maybe(x => x.Text);

            var descriptionAttr = pi.GetCustomAttributes(true).OfType<DisplayAttribute>()
                .FirstOrDefault(x => !string.IsNullOrEmpty(x.Name));
            DisplayName = descriptionAttr != null ? descriptionAttr.Name : pi.Name.Sentencise();
        }

        public PropertyVm(HtmlHelper html, Type type, string name)
        {
            Type = type;
            Name = name;
            GetId = () => Name.Replace(".", "_");

            ModelState modelState;
            if (html.ViewData.ModelState.TryGetValue(name, out modelState))
            {
                if (modelState.Value != null)
                    Value = modelState.Value.AttemptedValue;
            }
            Html = html;
            GetCustomAttributes = () => new object[] { };
            ShowLabel = true;
            LabelOnRight = false;
        }

        protected internal HtmlHelper Html { get; set; }

        public Func<string> GetId { private get; set; }
        public string Id
        {
            get { return GetId(); }
            set { GetId = () => value; }
        }
        public Type Type { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public object Value { get; set; }


        public Func<IEnumerable<object>> GetCustomAttributes { get; set; }

        public bool Readonly { get; set; }
        public bool Disabled { get; set; }


        public IEnumerable Choices { get; set; }
        public IEnumerable Suggestions { get; set; }

        public bool IsHidden { get; set; }
        public bool ShowLabel { get; set; }
        public bool LabelOnRight { get; set; }
        public string PlaceholderText { get; set; }
    }



    static class Extensions
    {
        public static U Maybe<T, U>(this T t, Func<T, U> f) where T : class
        {
            return (t == null) ? default(U) : f(t);
        }
    }
}