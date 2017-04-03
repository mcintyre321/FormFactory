using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace FormFactory
{
    public static class EmbeddedResourceRegistry
    {
        static readonly ConcurrentDictionary<string, Assembly> resourceKeys = new ConcurrentDictionary<string, Assembly>();
        static EmbeddedResourceRegistry()
        {
            var assembly = typeof(FormFactory.FF).Assembly;
            AddResourcesFromAssemblyToRegistry(assembly);
        }

        public static void AddResourcesFromAssemblyToRegistry(Assembly assembly)
        {
            foreach (var item in assembly.GetManifestResourceNames())
            {
                resourceKeys.AddOrUpdate(item, assembly, (s, assembly1) => assembly);
            }
        }

        public static string ResolveResourcePath(string viewName)
        {
            const string searchPath = "FormFactory.Views.Shared.";
            var escapedViewName = viewName.Replace("/", ".");
            var key = searchPath + escapedViewName + ".cshtml";
            if (resourceKeys.ContainsKey(key)) return key;
            return null;
        }
        public static Stream ResolveResourceStream(string name)
        {
            Assembly assembly;
            var resourcePath = ResolveResourcePath(name);
            if (resourceKeys.TryGetValue(resourcePath, out assembly))
            {
                return assembly.GetManifestResourceStream(resourcePath);
            }
            return null;
        }
    }
}