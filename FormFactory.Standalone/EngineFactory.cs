using System;
using System.IO;
using System.Text;
using RazorLight;
using RazorLight.Compilation;
using RazorLight.Templating;
using RazorLight.Templating.Embedded;

namespace FormFactory.Standalone
{
    public static class EngineFactory
    {



        public static RazorLightEngine CreateEmbedded(Type rootType)
        {
            return CreateEmbedded(rootType, EngineConfiguration.Default);
        }


        static RazorLightEngine CreateEmbedded(Type rootType, IEngineConfiguration configuration)
        {
            var manager = new FormFactoryTemplateMananger(new EmbeddedResourceTemplateManager(rootType));
            var core = new EngineCore(manager, configuration);
            
            IPageFactoryProvider pageFactory = new DefaultPageFactory((key) =>
            {
                ITemplateSource source = manager.Resolve(key);
                string razorTemplate = core.GenerateRazorTemplate(source, new ModelTypeInfo(typeof(FormFactoryTemplatePage<>)));
                var context = new CompilationContext(razorTemplate);

                CompilationResult compilationResult = configuration.CompilerService.Compile(context);

                return compilationResult;
            });

            IPageLookup lookup = new DefaultPageLookup(pageFactory);




            return new RazorLightEngine(core, lookup);
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
                        if (_content == null)
                        {
                            _content = _inner.Content;
                            var inherits = "@inherits FormFactory.Standalone.FormFactoryTemplatePage";
                            var sb = new StringBuilder();
                            var lines = _inner.Content.Split(Environment.NewLine.ToCharArray());
                            foreach (var readLine in lines)
                            {
                                if (readLine.StartsWith("@model "))
                                {
                                    inherits = ("@inherits FormFactory.Standalone.FormFactoryTemplatePage<" + readLine.Substring(7) + ">\r\n");
                                    continue;
                                }
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


       

    }
}