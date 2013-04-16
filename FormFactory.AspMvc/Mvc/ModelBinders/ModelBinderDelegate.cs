using System.Web.Mvc;

namespace FormFactory.AspMvc.Mvc.ModelBinders
{
    public delegate object ModelBinderDelegate(ControllerContext cc, ModelBindingContext mbc);
}