using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FormFactory
{
    public static class ViewFinderExtensions
    {
        public static string BestProperty(this FfHtmlHelper html, PropertyVm vm)
        {
            var viewname = html.FfContext.BestViewName(vm.Type, "FormFactory/Property.");
            viewname = viewname ?? html.FfContext.BestViewName(vm.Type.GetEnumerableType(), "FormFactory/Property.IEnumerable.");
            viewname = viewname ?? "FormFactory/Property.System.Object"; //must be some unknown object exposed as an interface
            return html.Partial(viewname, vm);
        }
        public static string BestViewName(this FfHtmlHelper helper, Type type, string prefix = null)
        {
            return BestViewName(helper.FfContext, type, prefix);
        }
        public static string BestPartial(FfHtmlHelper helper, object model, Type type = null, string prefix = null)
        {
            if (type == null) type = model.GetType();
            return helper.Partial(BestViewName(helper.FfContext, type, prefix), model);
        }
        
        public static IList<Func<Type, string>> SearchPathRules = new List<Func<Type, string>>()
        {
            t => t.FullName,
            t => t.FullName.StartsWith(t.Assembly.GetName().Name + ".") ? t.FullName.Substring(t.Assembly.GetName().Name.Length + 1) : t.FullName,
            t => t.Name
        };
        public static string BestViewName(this FfContext cc, Type type, string prefix = null)
        {
            return SearchPathRules
                .Select(r => BestViewName(cc, type, prefix, r))
                    .FirstOrDefault(v => v != null);
        }

        public static string BestViewName(FfContext cc, Type type, string prefix, Func<Type, string> getNameIn)
        {
            if (type == null) return null;
            var getName = getNameIn ?? (t => t.FullName);
            var check = Nullable.GetUnderlyingType(type) ?? type;

            Func<Type, string> getPartialViewName =
                t => prefix + getName(t);
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
