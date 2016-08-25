using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormFactory.Components
{
    public class NavbarBrand
    {
        public string Text { get; set; }
        public Uri Uri { get; set; }
        public Uri ImageUri { get; set; }
    }

    public class Navbar
    {
        public Navbar()
        {
            Header = new NavbarHeader()
            {
                Brand = new NavbarBrand()
                {
                    Text = "Set Navbar.Brand.Text",
                    Uri = new Uri("/", UriKind.Relative)
                }
            };
            Components = new List<NavbarComponent>();

        }

        public NavbarHeader Header { get; set; }
        public List<NavbarComponent> Components { get; set; }

        public class NavbarHeader
        {
            public NavbarBrand Brand { get; set; }
        }

        public abstract class NavbarComponent
        {
            public virtual NavbarPosition? Position { get; set; }
            public class NavbarForm : NavbarComponent
            {
                private FormVm _form;

                public NavbarForm()
                {
                    Form = new FormVm();
                }

                public FormVm Form
                {
                    get { return _form; }
                    set
                    {
                        _form.AdditionalClasses = "navbar-form";
                        _form = value;
                    }
                }
            }

            public class Nav : NavbarComponent
            {
                public IList<NavbarNavItem> Items { get; set; }

                public abstract class NavbarNavItem
                {
                }

                public class Link : NavbarNavItem
                {
                    public string Text { get; set; }
                    public Uri Uri { get; set; }
                    public bool Active { get; set; }
                }
                
                public class Dropdown : NavbarNavItem
                {
                    public string Text { get; set; }
                    public IList<NavbarNavDropdownItem> Items { get; private set; }

                    public abstract class NavbarNavDropdownItem
                    {
                    }

                    public Dropdown(string text)
                    {
                        Text = text;
                    }

                    public class Link : NavbarNavDropdownItem
                    {
                        public string Text { get; set; }
                        public Uri Uri { get; set; }
                        public bool Active { get; set; }
                    }

                    public class Header : NavbarNavDropdownItem
                    {
                        public string Text { get; set; }
                    }

                    public class Separator : NavbarNavDropdownItem
                    {
                    }
                }
            }
            public class Button : NavbarComponent { }
            public class Text : NavbarComponent { public string Value { get; set; } }

        }
        public enum NavbarPosition
        {
            Left,
            Right
        }
    }

  
}
namespace FormFactory.Components
{
}