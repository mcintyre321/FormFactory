using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace FormFactory.Standalone
{
    public class MyFfHtmlHelper : FfHtmlHelper
    {
        private FfRazorProjectFileSystem fs;

        public MyFfHtmlHelper()
        {
            fs = new FfRazorProjectFileSystem();

        }

        public string Raw(string s)
        {
            return s;
        }

        public ViewData ViewData => new ViewData();

        public IViewFinder ViewFinder => new ViewFinder();

        public PropertyVm CreatePropertyVm(Type objectType, string name)
        {
            throw new NotImplementedException();
        }

        public void RenderPartial(string partialName, object model)
        {
            throw new NotImplementedException();
        }

        public UrlHelper Url()
        {
            throw new NotImplementedException();
        }

        public string WriteTypeToString(Type type)
        {
            throw new NotImplementedException();
        }

        public MvcHtmlString Partial(string viewPath, object model)
        {
            var templateType = compiledViews.GetOrAdd(viewPath, s => new Lazy<Type>(() => GetTemplateType(s)))
                .Value;
            var template = (MyTemplate)Activator.CreateInstance(templateType);

            template.Model = model;
            template.Html = this;
            template.ExecuteAsync().Wait();
            return new MvcHtmlString(template.sb.ToString());
        }
        static readonly ConcurrentDictionary<string, Lazy<Type>> compiledViews = new ConcurrentDictionary<string, Lazy<Type>>();
        private Type GetTemplateType(string viewPath)
        {
            var namespaceName = typeof(MyTemplate<>).Namespace + Guid.NewGuid().ToString().Replace("-", "");

            var engine = RazorProjectEngine.Create(RazorConfiguration.Default, fs, (builder) =>
            {
                builder.SetBaseType("FormFactory.Standalone.MyTemplate<TModel>");
                ModelDirective.Register(builder);
                InheritsDirective.Register(builder);
                builder.SetNamespace(namespaceName); // define a namespace for the Template class
            });


            var item = fs.GetItem(viewPath);

            var codeDocument = engine.Process(item);
            var cs = codeDocument.GetCSharpDocument();


            var tree = CSharpSyntaxTree.ParseText(cs.GeneratedCode);


            var loadedAssemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies()
                .Select(Assembly.Load)
                .Where(a => a.FullName == "FormFactory, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")
                .Select(a => MetadataReference.CreateFromFile(a.Location));
            new object[] { }.OfType<string>();
            string dllName = "hello" + Guid.NewGuid().ToString();

            var compilation = CSharpCompilation.Create(dllName, new[] {tree},
                new[]
                {
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location), // include corlib
                    MetadataReference.CreateFromFile(typeof(RazorCompiledItemAttribute).Assembly
                        .Location), // include Microsoft.AspNetCore.Razor.Runtime
                    MetadataReference.CreateFromFile(typeof(MyTemplate<>).Assembly
                        .Location), // this file (that contains the MyTemplate base class)
                    MetadataReference.CreateFromFile(typeof(System.Linq.EnumerableExecutor).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Linq.Expressions.Expression).Assembly.Location),

                    // for some reason on .NET core, I need to add this... this is not needed with .NET framework
                    MetadataReference.CreateFromFile(Path.Combine(Path.GetDirectoryName(typeof(object).Assembly.Location),
                        "System.dll")),
                    MetadataReference.CreateFromFile(Path.Combine(Path.GetDirectoryName(typeof(object).Assembly.Location),
                        "System.Runtime.dll")),
                    MetadataReference.CreateFromFile(Path.Combine(Path.GetDirectoryName(typeof(object).Assembly.Location),
                        "System.Core.dll")),
                    MetadataReference.CreateFromFile(Path.Combine(Path.GetDirectoryName(typeof(object).Assembly.Location),
                        "System.Linq.dll")),

                    // as found out by @Isantipov, for some other reason on .NET Core for Mac and Linux, we need to add this... this is not needed with .NET framework
                    MetadataReference.CreateFromFile(Path.Combine(Path.GetDirectoryName(typeof(object).Assembly.Location),
                        "netstandard.dll"))
                }.Concat(loadedAssemblies),
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)); // we want a dll

            var stream = new MemoryStream();
            var emitResult = compilation.Emit(stream);

            if (!emitResult.Success)
            {

                var errro = (viewPath + "<br/>" +
                             string.Join("<br/>", emitResult.Diagnostics.Select(x => x.GetMessage())));

                throw new Exception(errro);
            }

            stream.Seek(0, SeekOrigin.Begin);
            var asm = Assembly.Load(stream.ToArray());

            // the generated type is defined in our custom namespace, as we asked. "Template" is the type name that razor uses by default.
            return  asm.GetType(namespaceName + ".Template");
        }
    }
}