using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using FormFactory.Examples.Shared.Examples;

namespace FormFactory.Examples.Shared
{
    public class ExampleRenderer
    {
         
        public static string Index(Func<object, string> render)
        {


            var ex = typeof(NestedFormsExample);
            var exampleTypes = ex.Assembly.GetTypes()
                .Where(t => t.Namespace == ex.Namespace && t.Name.Contains("Example"))
                .OrderBy(t => t.Name);
            var sb = new StringBuilder();
            foreach (var exampleType in exampleTypes)
            {


                var instance = Activator.CreateInstance(exampleType);
                sb.Append(render(instance));


            }

            return sb.ToString();
        }

        static Hashtable Cache = new Hashtable();

        private string GetSourceForType(Type type)
        {
            var html = Cache[type.Name + ".cs"] as string;
            if (html == null)
            {
                var address =
                    "https://raw.github.com/mcintyre321/FormFactory/master/FormFactory.AspMvc.Example/Models/Examples/" +
                    type.Name + ".cs";
                try
                {
                    html = new HttpClient().GetAsync(address).Result.Content.ReadAsStringAsync().Result;
                    html = string.Join(Environment.NewLine,
                        html.Split(Environment.NewLine.ToCharArray())
                            .Where(line => line.Trim().StartsWith("using ") == false));
                    Cache[type.Name + ".cs"] = html;
                }
                catch (Exception)
                {
                    return "Could not get source from " + address;
                }
            }

            return html;

        }

        public class IndexModel
        {
            public IOrderedEnumerable<Tuple<Type, string>> exampleTypes { get; set; }
        }


    }
}