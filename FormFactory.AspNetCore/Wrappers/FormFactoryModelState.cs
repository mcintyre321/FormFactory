using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;


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