using System.Collections.Generic;
using System.Linq;
using Moq;

namespace FormFactory.NetCore.Tests
{
    //public class BestViewNameTests
    //{
    //    [TestCase("asdf", false)]
    //    [TestCase("SomeType", true)]
    //    [TestCase("FormFactory.Tests.SomeType", true)]
    //    [TestCase("SomeBaseType", true)]
    //    [TestCase("Object", true)]
    //    [TestCase("System.Object", true)]
    //    public void DivideTest(string viewname, bool viewShouldBeFound)
    //    {
    //        var bestViewName = ViewFinderExtensions.BestViewName(
    //            (IViewFinder)new DummyViewFinder("FormFactory/Property." + viewname + ".cshtml"),
    //            typeof(SomeType),
    //            "FormFactory/Property.");
    //        Assert.AreEqual(viewShouldBeFound, (bestViewName != null));
    //    }



    //    [Test]
    //    public void FindsInterface()
    //    {
    //        var fullyQualifiedViewName = "FormFactory/Property.IEnumerable." + typeof(SomeType).FullName;
    //        var viewFinder = (IViewFinder) new DummyViewFinder("FormFactory/Property.IEnumerable.Object.cshtml", fullyQualifiedViewName + ".cshtml");

    //        var bestViewName = ViewFinderExtensions.BestViewName(
    //            viewFinder,
    //            typeof(IEnumerable<SomeType>),
    //            "FormFactory/Property.");
    //        Assert.AreEqual(fullyQualifiedViewName, bestViewName);
    //    }
        
    //    [Test]
    //    public void FindsFullyQualifiedNameFirrst()
    //    {
    //        var fullyQualifiedViewName = "FormFactory/Property." + typeof(SomeType).FullName;
    //        var bestViewName = ViewFinderExtensions.BestViewName(
    //            (IViewFinder)new DummyViewFinder("FormFactory/Property.Object.cshtml", fullyQualifiedViewName + ".cshtml"),
    //            typeof(SomeType),
    //            "FormFactory/Property.");
    //        Assert.AreEqual(fullyQualifiedViewName, bestViewName);
    //    }
        
    //}

    public class DummyViewFinder : IViewFinder
    {
        private readonly string[] _viewNames;

        public DummyViewFinder(params string[] viewNames)
        {
            _viewNames = viewNames;
        }

        public IViewFinderResult FindPartialView(string partialViewName)
        {
            var exists = _viewNames.Any(v => v == partialViewName + ".cshtml");
            if (exists)
            {
                return new DummyViewFinderResult(){ View = new Mock<View>().Object};
            }
            return new DummyViewFinderResult();
        }
    }

    public class DummyViewFinderResult : IViewFinderResult
    {

        public View View { get; set; }
    }
}