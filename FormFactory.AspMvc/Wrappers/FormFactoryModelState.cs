namespace FormFactory.AspMvc.Wrappers
{
    public class FormFactoryModelState : ModelState
    {
        private readonly System.Web.Mvc.ModelState _ms;

        public FormFactoryModelState(System.Web.Mvc.ModelState ms)
        {
            _ms = ms;
        }

        public ModelStateValue Value { get { return new FormFactoryModelStateValue(_ms); } }
    }
}