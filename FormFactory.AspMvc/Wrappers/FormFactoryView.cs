using System.Web.Mvc;

namespace FormFactory.AspMvc.Wrappers
{
    public class FormFactoryView : View
    {
        private readonly IView _view;

        public FormFactoryView(IView view)
        {
            _view = view;
        }
    }
}