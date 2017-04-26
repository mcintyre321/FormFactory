using System.Linq;
using FormFactory.Standalone;
using NUnit.Framework;

namespace FormFactory.NetFramework.Tests
{
    public class BasicPropertyTests
    {

        [Test]
        public void CanRenderAPropertyWithoutThrowingAnException()
        {
            var someObject = new SomeType() { SomeProperty = "SomeValue" };
            var properties = Properties.For(someObject);
            ////var annotation = new FormFactory.Attributes.DisplayAttribute();
            var helper = new RazorTemplateHtmlHelper();
            var html = properties.Render(helper);
            var actualCq = CsQuery.CQ.CreateFragment(html.ToString());
            var input = actualCq.Find("input").Single(el => el.GetAttribute("name") == "SomeProperty");

            Assert.AreEqual("SomeValue", input.GetAttribute("value"));
        }


    }
}