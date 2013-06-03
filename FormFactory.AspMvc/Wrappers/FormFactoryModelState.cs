using System.Collections.Generic;
using System.Web.Mvc;

namespace FormFactory.AspMvc.Wrappers
{
    public class FormFactoryModelState : ModelState
    {
        private readonly System.Web.Mvc.ModelState _ms;

        public FormFactoryModelState(System.Web.Mvc.ModelState ms)
        {
            _ms = ms;
        }

        public FormFactoryModelStateValue Value { get { return new FormFactoryModelStateValueWrapper(_ms); } }
        public FormFactoryModelStateErrors Errors { get{return _ms.Errors == null ? null : new  FormFactoryModelStateErrorsWrapper(_ms.Errors);}}
    }

    public class FormFactoryModelStateErrorsWrapper : FormFactoryModelStateErrors
    {
        public FormFactoryModelStateErrorsWrapper(ModelErrorCollection errors)
            :base(Transform(errors))
        {
            
        }

        private static IEnumerable<FormFactoryModelStateError> Transform(ModelErrorCollection errors)
        {
            foreach (var error in errors)
            {
                yield return new FormFactoryModelStateError()
                {
                    ErrorMessage = error.ErrorMessage
                };
            }
        }
    }
}