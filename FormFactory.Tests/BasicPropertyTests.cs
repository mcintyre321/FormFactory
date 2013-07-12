using FormFactory.RazorEngine;
using NUnit.Framework;

namespace FormFactory.Tests
{
    public class BasicPropertyTests
    {
        [Test, Ignore("The ids keep changing!")]
        public void CanRenderAPropertyWithoutThrowingAnException()
        {
            var someObject = new SomeType() {SomeProperty = "SomeValue"};
            var properties = Properties.For(someObject);
            var annotation = new System.ComponentModel.DataAnnotations.DisplayAttribute();

            var html = properties.Render();
            Assert.AreEqual(expected, html);
        }

        private const string expected =
            @"<div style=""display: none"">
 
    <input id=""656e306d-d1b1-474f-a424-3190d31a4ded""     class=""input-large"" name=""__type"" size=""30"" type=""text"" value=""FormFactory.Tests.SomeType, FormFactory.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"" />
</div>


    <div class=""control-group valid"">
            <label class=""control-label"" for=""734cdeec-3876-416c-97f7-7baf4d1d5929"">Some property</label>
        <div class=""controls"">
            
 
    <input id=""734cdeec-3876-416c-97f7-7baf4d1d5929""     class=""input-large"" name=""SomeProperty"" size=""30"" type=""text"" value=""SomeValue"" />

            <span class=""help-inline"">
                <span class=""field-validation-valid"" data-valmsg-for=""SomeProperty"" data-valmsg-replace=""true"">
                    
                </span>
                
            </span>
        </div>
    </div>";

    }
}