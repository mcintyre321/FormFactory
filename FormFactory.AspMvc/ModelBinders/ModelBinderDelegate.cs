using System.Web.Mvc;

namespace FormFactory.AspMvc.ModelBinders
{
    public delegate object ModelBinderDelegate(ControllerContext cc, ModelBindingContext mbc);
}