using System.Linq;
using CsQuery;
using FormFactory.Standalone;
using Xunit;

namespace FormFactory.NetCore.Tests
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var someObject = new SomeType() { SomeProperty = "SomeValue" };
            var properties = Properties.For(someObject);
            ////var annotation = new FormFactory.Attributes.DisplayAttribute();
            var helper = new RazorTemplateHtmlHelper();
            var html = properties.Render(helper);
            var actualCq = CQ.CreateFragment(html.ToString());
            var input = actualCq.Find("input").Single(el => el.GetAttribute("name") == "SomeProperty");

            Assert.Equal("SomeValue", input.GetAttribute("value"));
        }


    }
}