using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FormFactory.Tests
{
    public class InheritanceTests
    {
        [Test]
        public void FindsInheritedProperties()
        {
            var model = new InhertitingClass();
            var propertyVmsUsingReflection = FF.GetPropertyVmsUsingReflection(model, typeof(object)).ToList();
            Assert.IsNotNull(propertyVmsUsingReflection.SingleOrDefault(_ => _.Name == nameof(BaseClass.BaseClassStringProperty)), $"Inherited property '{nameof(BaseClass.BaseClassStringProperty)}' missing.");
        }

        class BaseClass
        {
            public string BaseClassStringProperty { get; set; }
        }

        class InhertitingClass : BaseClass
        {
            public string InheritingClassStringProperty { get; set; }
        }
    }
}
