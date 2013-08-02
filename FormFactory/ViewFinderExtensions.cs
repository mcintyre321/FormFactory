using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FormFactory
{
    public static class ViewFinderExtensions
    {
        public static string BestPropertyName<THelper>(THelper html, PropertyVm vm) where THelper : FfHtmlHelper
        {
            var viewname = BestViewName(html.ViewFinder, vm.Type, "FormFactory/Property.");
           return viewname;
        }
        public static string BestPartialName<THelper>(THelper helper, object model, Type type = null, string prefix = null)where THelper : FfHtmlHelper
        {
            if (type == null) type = model.GetType();
            return BestViewName(helper.ViewFinder, type, prefix);
        }
        public static string BestViewName<THelper>(THelper helper, Type type, string prefix = null)where THelper : FfHtmlHelper
        {
            return BestViewName(helper.ViewFinder, type, prefix);
        }
        
        public static IList<Func<Type, string>> SearchPathRules = new List<Func<Type, string>>()
        {
            t => t.FullName,
            t => t.FullName.StartsWith(t.Assembly.GetName().Name + ".") ? t.FullName.Substring(t.Assembly.GetName().Name.Length + 1) : t.FullName,
            t => t.Name
        };

        private static string CleanPath(string fileName)
        {
            return Path.GetInvalidFileNameChars()
                       .Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
        }

        public static string BestViewName(IViewFinder cc, Type type, string prefix = null)
        {
            return SearchPathRules
                .Select(r => BestViewName(cc, type, prefix, r))
                    .FirstOrDefault(v => v != null);
        }

        public static string BestViewName(IViewFinder cc, Type type, string prefix, Func<Type, string> getNameIn)
        {
            if (type == null) return null;
            var viewName = PartialViewNameInner(cc, type, prefix, getNameIn);
            viewName = viewName ?? BestViewName(cc, type.GetEnumerableType(), prefix  + "IEnumerable.");
            //viewName = viewName ?? "FormFactory/Property.System.Object"; //must be some unknown object exposed as an interface

            return viewName;
        }

        private static string PartialViewNameInner(IViewFinder cc, Type type, string prefix, Func<Type, string> getNameIn)
        {
            var getName = getNameIn ?? (t => t.FullName);
            var check = Nullable.GetUnderlyingType(type) ?? type;

            Func<Type, string> getPartialViewName =
                t => prefix + CleanPath(getName(t));
            string partialViewName = getPartialViewName(check);

            var engineResult = cc.FindPartialView(partialViewName);
            while (engineResult.View == null && check.BaseType != null)
            {
                check = check.BaseType;
                partialViewName = getPartialViewName(check);
                engineResult = cc.FindPartialView(partialViewName);
                ;
            }

            if (engineResult.View == null)
            {
                return null;
            }
            return partialViewName;
        }
    }
}
