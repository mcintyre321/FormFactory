using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using CsQuery;
using FormFactory.NetCore.Tests;
using FormFactory.Standalone;
using NUnit.Framework;

namespace FormFactory.Tests
{
    public class Tests
    {

        [Test]
        public async Task Test()
        {
            var someObject = new SomeType() { SomeProperty = "SomeValue" };
            var properties =  FF.PropertiesFor(someObject);

            var html = properties.Render();
            var annotation = new FormFactory.Attributes.DisplayAttribute();
            var actualCq = CQ.CreateFragment(html.ToString());
            var input = actualCq.Find("input").Single(el => el.GetAttribute("name") == "SomeProperty");

            Assert.AreEqual("SomeValue", input.GetAttribute("value"));
        }

        [Test]
        public async Task TestXElement()
        {
            var someObject = new { SomeProperty = XElement.Parse("<div id=\"yo\">hello</div>") };
            var properties =  FF.PropertiesFor(someObject);

            var html = properties.Render();
            var annotation = new FormFactory.Attributes.DisplayAttribute();
            var actualCq = CQ.CreateFragment(html.ToString());
            var input = actualCq.Find("#yo").Single();

            Assert.AreEqual("hello", input.InnerText);
        }

        [Test]
        public async Task TestForm()
        {
            var form = new FormVm
            {
                ActionUrl = "http://example.org/foo",
                DisplayName = "somelink",
                Method = "GET",
                Inputs = new List<PropertyVm>()

            };
            form.Inputs.Add(new PropertyVm(typeof(string), "_method")
            {
                DisplayName = "HttpMethod",
                Choices = new[] { "get", "put", "post", "delete" }
            });
            var html = form.Render(new MyFfHtmlHelper()).ToString();
            var actualCq = CQ.CreateFragment("<div>" + html.ToString() + "</div>");
            actualCq.Find("form").Single();
        }




    }
     
}