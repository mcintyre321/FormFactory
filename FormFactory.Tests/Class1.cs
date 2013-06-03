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

            var html = properties.Render().ToString().Trim();
            Assert.That(string.IsNullOrWhiteSpace(html) == false);
        }

        
    }
    public class SomeType
    {
        public string SomeProperty { get; set; }
    }
}
