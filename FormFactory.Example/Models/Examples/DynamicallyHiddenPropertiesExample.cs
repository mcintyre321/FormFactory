using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FormFactory.Example.Models.Examples
{
    /// <summary>
    /// Return false from a _show method or property to exclude a property 
    /// when using Html.PropertiesFor(Model)
    /// </summary>
    public class DynamicallyHiddenPropertiesExample
    {
        public string Property1 { get; set; } = "This won't be shown";
        public bool Property1_show() => false;

        public string Property2 { get; set; } = "This won't be shown either";
        public bool Property2_show { get; set; } = false;

        public string Property3 { get; set; } = "This will be shown";
        public bool Property3_show() => true;
    }
}