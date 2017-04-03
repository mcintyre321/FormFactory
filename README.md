Please visit http://formfactoryaspmvc.azurewebsites.net/ for documentation and to see what FormFactory can do!

![Screengrab of documentation](https://snag.gy/dEr2yF.jpg)

Reflection form generation

Because you shouldn't have to update your html when you update your object model

Convention based rendering

Specify custom templates for rendering different object types.

Twitter bootstrap support

Styled for bootstrap (by default)

Embeddable

FormFactory.RazorEngine can be used in non ASP projects like console apps, services or WebAPI projects.

What is it?
FormFactory renders complex object forms automatically. It refects over an object model or method signature, and builds an intermediate model representing the form and properties. These models are then rendered using customisable templates.

FormFactory can build complex nested forms with rich content pickers. By following a few simple code conventions, properties with multiple choices and suggested values can be written in a few lines of code.

How to use it
//In a cshtml file
@FF.PropertiesFor(someObject).Render(Html);
            
Installation
1. Install the library
For ASP.NET MVC 5
install-package FormFactory
install-package FormFactory.AspMvc
install-package EmbeddedResourceVirtualPathProvider *
            
For ASP.NET MVC Core
install-package FormFactory
install-package FormFactory.AspNetCore
Then configure core to serve embedded files ( see startup.cs lines 36 and 60)
* You can install FormFactory.Templates if you don't want to use the EmbeddedResourceVirtualPathProvider
2. Add the assets to your page
<link href="/Content/FormFactory/FormFactory.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/FormFactory/FormFactory.js" type="text/javascript"></s
 
