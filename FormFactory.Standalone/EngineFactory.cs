using System;
using System.IO;
using System.Text;
using RazorLight.Templating;
using RazorLight.Templating.Embedded;
using RazorLight;
using RazorLight.Compilation;

namespace FormFactory.Standalone
{
    public static class EngineFactory
    {



        public static IRazorLightEngine CreateEmbedded(Type rootType)
        {
            return CreateEmbedded(rootType, EngineConfiguration.Default);
        }


        public static IRazorLightEngine CreateEmbedded(Type rootType, IEngineConfiguration configuration)
        {
            ITemplateManager manager = new FormFactoryTemplateMananger(new EmbeddedResourceTemplateManager(rootType));
            var dependencies = CreateDefaultDependencies(manager, configuration);

            return new RazorLightEngine(dependencies.Item1, dependencies.Item2);
        }

        public class FormFactoryTemplateMananger : ITemplateManager
        {
            private readonly EmbeddedResourceTemplateManager _inner;

            public ITemplateSource Resolve(string key)
            {
                return new FormFactoryTemplateSource(_inner.Resolve(key));
            }

            public class FormFactoryTemplateSource : ITemplateSource
            {
                private string _content;
                private readonly ITemplateSource _inner;

                public TextReader CreateReader()
                {
                    return new StringReader(Content);
                }

                public string Content
                {
                    get
                    {
                        if (_content != null)
                        {
                            _content = _inner.Content;
                            var inherits = "@inherits FormFactory.Standalone.FormFactoryTemplateBase";
                            var sb = new StringBuilder();
                            var lines = _inner.Content.Split(Environment.NewLine.ToCharArray());
                            foreach (var readLine in lines)
                            {
                                if (readLine == "@using FormFactory.AspMvc")
                                {
                                    sb.AppendLine("@using FormFactory.Standalone");
                                    continue;
                                }
                                sb.AppendLine(readLine);
                            }
                            _content = inherits + sb.ToString();
                        }
                        return _content;
                    }
                }

                public string FilePath => _inner.FilePath;

                public string TemplateKey => _inner.TemplateKey;

                public FormFactoryTemplateSource(ITemplateSource inner)
                {
                    _inner = inner;
                }
            }

            public Type RootType => _inner.RootType;

            public FormFactoryTemplateMananger(EmbeddedResourceTemplateManager inner)
            {
                _inner = inner;
            }
        }


        private static Tuple<IEngineCore, IPageLookup> CreateDefaultDependencies(
            ITemplateManager manager,
            IEngineConfiguration configuration)
        {
            IEngineCore core = new EngineCore(manager, configuration);

            IPageFactoryProvider pageFactory = new DefaultPageFactory((key) =>
            {
                ITemplateSource source = manager.Resolve(key);

                string razorTemplate = core.GenerateRazorTemplate(source, new ModelTypeInfo(typeof(FormFactoryTemplateBase)));
                var context = new CompilationContext(razorTemplate, configuration.Namespaces);

                CompilationResult compilationResult = configuration.CompilerService.Compile(context);

                return compilationResult;
            });

            IPageLookup lookup = new DefaultPageLookup(pageFactory);

            return new Tuple<IEngineCore, IPageLookup>(core, lookup);
        }

    }
}