// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.ObjectPool;

namespace FormFactory.Standalone
{
    public class RazorContextBuilder
    {

        internal static Lazy<IServiceScopeFactory> ServiceScopeFactory { get;  } = new Lazy<IServiceScopeFactory>(InitializeServices);

        static IServiceScopeFactory InitializeServices()
        {
            // Initialize the necessary services
            var services = new ServiceCollection();
            ConfigureDefaultServices(services);

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<IServiceScopeFactory>();
        }

       

        private static void ConfigureDefaultServices(IServiceCollection services)
        {
            IFileProvider fileProvider = new EmbeddedFileProvider(typeof(FF).Assembly);
            services.AddSingleton<IHostingEnvironment>(new HostingEnvironment
            {
                ApplicationName =  "FormFactory",
                WebRootFileProvider = fileProvider,
            });
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.FileProviders.Clear();
                options.FileProviders.Add(fileProvider);
                var previous = options.CompilationCallback;
                options.CompilationCallback = (context) =>
                {
                    previous?.Invoke(context);

                    var assembly = typeof(RazorContextBuilder).GetTypeInfo().Assembly;
                    var assemblies = assembly.GetReferencedAssemblies().Select(x => MetadataReference.CreateFromFile(Assembly.Load(x).Location))
                        .ToList();
                    var others = new[]
                    {
                        "mscorlib",
                        "System.Private.Corelib",
                        "Microsoft.AspNetCore.Razor",


                        "System.Linq",
                       // "System.Xml.Linq",
                        "System.Threading.Tasks",
                        "System.Runtime",
                        "System.Dynamic.Runtime",
                        "Microsoft.AspNetCore.Razor.Runtime",
                        "Microsoft.AspNetCore.Mvc",
                        "Microsoft.AspNetCore.Razor",
                        "Microsoft.AspNetCore.Mvc.Razor",
                        "Microsoft.AspNetCore.Html.Abstractions",
                        "System.Text.Encodings.Web",
                        "System.Collections",

                        "FormFactory",
                        "FormFactory.AspNetCore"
                    };
                    others.ToList().ForEach(a =>
                    {
                        var load = Assembly.Load(new AssemblyName(a));
                        assemblies.Add(MetadataReference.CreateFromFile(load.Location));
                    });
                   
                    context.Compilation = context.Compilation.AddReferences(assemblies);

                };
            });
            var diagnosticSource = new DiagnosticListener("Microsoft.AspNetCore");
            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            services.AddSingleton<DiagnosticSource>(diagnosticSource);
            services.AddLogging();
            services.AddMvc();
            services.AddTransient<RazorViewToStringRenderer>();
        }
    }
}
