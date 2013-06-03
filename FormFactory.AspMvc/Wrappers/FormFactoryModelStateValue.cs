namespace FormFactory.AspMvc.Wrappers
{
    public class FormFactoryModelStateValueWrapper : FormFactoryModelStateValue
    {
        private readonly System.Web.Mvc.ModelState _ms;

        public FormFactoryModelStateValueWrapper(System.Web.Mvc.ModelState ms)
        {
            _ms = ms;
        }

        public object AttemptedValue
        {
            get { return _ms.Value.AttemptedValue; }
        }
    }
}