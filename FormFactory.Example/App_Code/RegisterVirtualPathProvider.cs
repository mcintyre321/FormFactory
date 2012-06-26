using System.Reflection;
using System.Linq;
namespace FormFactory.Example.App_Code
{
    public class RegisterVirtualPathProvider
    {
        public static void AppInitialize()
        {
			System.Web.Hosting.HostingEnvironment.RegisterVirtualPathProvider(new EmbeddedResourceVirtualPathProvider.Vpp()
            {
				{typeof(FormFactory.Logger).Assembly, @"..\..\FormFactory.Templates"}
            });
        }
    }
}