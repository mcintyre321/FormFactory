using System.Linq;
using System.Threading.Tasks;
using CsQuery;
using FormFactory.NetCore.Tests;
using FormFactory.Standalone;
using FormFactory;
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

            var html = await properties.RenderAsync();
            var annotation = new FormFactory.Attributes.DisplayAttribute();
            var actualCq = CQ.CreateFragment(html.ToString());
            var input = actualCq.Find("input").Single(el => el.GetAttribute("name") == "SomeProperty");

            Assert.AreEqual("SomeValue", input.GetAttribute("value"));
        }


    }
     
}