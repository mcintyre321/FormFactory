using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FormFactory.AspMvc.Wrappers
{

    static class MvcToFfMappingExtensions
    {
        public static FormFactoryModelStateErrors ToFfModelStateErrors(this ModelErrorCollection mvcErrors)
        {
            return new FormFactoryModelStateErrors(mvcErrors.Select(e => new FormFactoryModelStateError()
            {
                ErrorMessage = e.ErrorMessage
            }));
        }
    }

}