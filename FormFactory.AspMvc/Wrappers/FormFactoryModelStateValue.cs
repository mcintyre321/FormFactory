namespace FormFactory.AspMvc.Wrappers
{
    public class FormFactoryModelStateValue : ModelStateValue
    {
        private readonly System.Web.Mvc.ModelState _ms;

        public FormFactoryModelStateValue(System.Web.Mvc.ModelState ms)
        {
            _ms = ms;
        }

        public object AttemptedValue
        {
            get { return _ms.Value; }
        }
    }
}