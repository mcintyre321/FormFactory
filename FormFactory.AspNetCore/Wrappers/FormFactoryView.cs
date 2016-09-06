

using Microsoft.AspNetCore.Mvc.ViewEngines;

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