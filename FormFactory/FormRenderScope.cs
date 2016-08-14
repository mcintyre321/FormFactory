using System;

namespace FormFactory
{
    public class FormRenderScope : IDisposable {
        public FfHtmlHelper HtmlHelper { get; set; }
        public FormVm FormVm { get; private set; }

        public FormRenderScope(FfHtmlHelper htmlHelper, FormVm formVm)
        {
            HtmlHelper = htmlHelper;
            FormVm = formVm;
        }

        public FormRenderScope Render()
        {
            RenderStart();
            RenderActionInputs();
            RenderButtons();
            return this;
        }

        public FormRenderScope RenderButtons()
        {
            HtmlHelper.RenderPartial("FormFactory/Form.Actions", this.FormVm);
            return this;
        }

        public FormRenderScope RenderActionInputs()
        {
            foreach (var input in FormVm.Inputs)
            {
                HtmlHelper.RenderPartial("FormFactory/Form.Property", input);
            }
            return this;
        }

        public FormRenderScope RenderStart()
        {
            HtmlHelper.RenderPartial("FormFactory/Form.Start", this.FormVm);
            return this;
        }


        public void Dispose()
        {
            HtmlHelper.RenderPartial("FormFactory/Form.Close", this.FormVm);
        }
   
    }
}