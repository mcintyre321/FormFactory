using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormFactory.RazorEngine;
using NUnit.Framework;

namespace FormFactory.Tests
{
    public class BasicPropertyTests
    {
        [Test]
        public void CanRenderAPropertyWithoutThrowingAnException()
        {
            var someObject = new SomeType() {SomeProperty = "SomeValue"};
            var properties = Properties.For(someObject);
            var annotation = new System.ComponentModel.DataAnnotations.DisplayAttribute();

            var html = VmHelper.Render(properties).ToString().Trim();
            Assert.AreEqual(expected, html);
        }

        private const string expected =
            @"<div style=""display: none"">
 
    <input id=""0d94340a-aff0-44b4-8460-477ea7ff36e9""     class=""input-large"" name=""__type"" size=""30"" type=""text"" value=""FormFactory.Tests.SomeType, FormFactory.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"" />
</div>

    <div class=""control-group valid"">
            <label class=""control-label"" for=""5df5ad07-30e9-4dae-adf5-0a4de18c48f6"">Some property</label>
        <div class=""controls"">
            
 
    <input id=""5df5ad07-30e9-4dae-adf5-0a4de18c48f6""     class=""input-large"" name=""SomeProperty"" size=""30"" type=""text"" value=""SomeValue"" />

            <span class=""help-inline"">
                <span class=""field-validation-valid"" data-valmsg-for=""SomeProperty"" data-valmsg-replace=""true"">
                    
                </span>
                
            </span>
        </div>
    </div>";

    }
    public class SomeType
    {
        public string SomeProperty { get; set; }
    }
}
