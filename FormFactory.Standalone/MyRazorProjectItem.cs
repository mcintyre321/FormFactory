using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Razor.Language;

namespace FormFactory.Standalone
{
    public class MyRazorProjectItem : RazorProjectItem
    {
        private readonly string _name;

        public MyRazorProjectItem(string name)
        {
            _name = name;
        }
        public override Stream Read()
        {
            return typeof(FF).Assembly.GetManifestResourceStream(FilePath);
        }

        public override string BasePath => "";
        public override string FilePath => "FormFactory.Views.Shared." + _name.Replace('/', '.') + ".cshtml";
        public override string PhysicalPath => null;
        public override bool Exists => typeof(FF).Assembly.GetManifestResourceNames().Contains(_name);
    }
}