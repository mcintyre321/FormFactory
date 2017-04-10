using FormFactory.Attributes;
using FormFactory.Components;
using System;
using FormFactory.Attributes;

namespace FormFactory.Example.Models.Examples
{
    public class BootstrapComponents
    {
        public Navbar Navbar { get; set; } = new Navbar()
        {
            Components =
            {
                new Navbar.NavbarComponent.Nav()
                {
                    Items =
                    {
                        new Navbar.NavbarComponent.Nav.Link()
                        {
                            Text = "Some link",
                            Uri = new Uri("/somelink"),
                            Active = true
                        },
                        new Navbar.NavbarComponent.Nav.Dropdown("A Dropdown")
                        {
                            Items =
                            {
                                new Navbar.NavbarComponent.Nav.Dropdown.Header()
                                {
                                    Text = "A menu header"
                                },
                                new Navbar.NavbarComponent.Nav.Dropdown.Link()
                                {
                                    Text = "Some Action",
                                    Uri = new Uri("/someaction", UriKind.Relative)
                                },
                                new Navbar.NavbarComponent.Nav.Dropdown.Separator(),
                                new Navbar.NavbarComponent.Nav.Dropdown.Link()
                                {
                                    Text = "Some other Action",
                                    Uri = new Uri("/someaction", UriKind.Relative)
                                },
                            }
                        }
                    }
                },
                new Navbar.NavbarComponent.NavbarForm()
                {
                    Position = Navbar.NavbarPosition.Left,
                    Form = new FormVm() { Inputs =
                    {
                        new PropertyVm(typeof(string),"q") { GetCustomAttributes = () => new[] { (object) new DisplayAttribute(){Prompt = "Search"}}}
                    }}
                },
                new Navbar.NavbarComponent.Text()
                {
                    Position = Navbar.NavbarPosition.Right,
                    Value = "<p class=\"navbar-text navbar-right\">Signed in as <a href=\"#\" class=\"navbar-link\">Mark Otto</a></p>"
                }
            }
        };
    }
}

