using System.Collections.Generic;

namespace FormFactory.RazorEngine
{
    public static class EmbeddedResourceRegistry
    {
        static HashSet<string> resourceKeys = new HashSet<string>();
        static EmbeddedResourceRegistry()
        {
            foreach (var item in typeof (FormFactory.VmHelper).Assembly.GetManifestResourceNames())
            {
                resourceKeys.Add(item);
            }
        }
        public static string ResolveResourcePath(string viewName)
        {
            const string searchPath = "FormFactory.Views.Shared.";
            var escapedViewName = viewName.Replace("/", ".");
            var key = searchPath + escapedViewName + ".cshtml";
            if (resourceKeys.Contains(key)) return key;
            return null;
        }
    }
}