using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Razor.Language;

namespace FormFactory.Standalone
{
    public class FfRazorProjectFileSystem : RazorProjectFileSystem
    {
        public override IEnumerable<RazorProjectItem> EnumerateItems(string basePath)
        {
            throw new NotImplementedException();
        }

        public override RazorProjectItem GetItem(string path)
        {
            return new MyRazorProjectItem(path);
        }
    }
}