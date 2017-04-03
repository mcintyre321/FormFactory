
# FormFactory - dynamic HTML form engine

Visit http://formfactoryaspmvc.azurewebsites.net/ for live documentation and to see what FormFactory can do!


 
 

            
# Installation
    
## For ASP.NET MVC 5
`install-package FormFactory`
`install-package FormFactory.AspMvc`
`install-package EmbeddedResourceVirtualPathProvider *` (or you can install FormFactory.Templates if you don't want to use the EmbeddedResourceVirtualPathProvider)
            
## For ASP.NET MVC Core
`install-package FormFactory`
`install-package FormFactory.AspNetCore`
configure core to serve embedded files - see [startup.cs](https://github.com/mcintyre321/FormFactory/blob/master/FormFactory.AspNetCore.Example/Startup.cs#L36) lines 36 and 60

## For both 

Add the assets to your page
`<link href="/Content/FormFactory/FormFactory.css" rel="stylesheet" type="text/css"/>`
`<script src="/Scripts/FormFactory/FormFactory.js" type="text/javascript"></script>`
 
# How to use it

Inside an cshtml file: `@FF.PropertiesFor(someObject).Render(Html);`

`.PropertiesFor(someObject)` will reflect over the `someObject` and create an enumerable of `PropertyVm` objects, and `.Render(Html)` will render each object out  the page

See the documentation site for how to mark up your viewmodel 
