using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormFactory.Standalone;

namespace FormFactory.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        public static async Task MainAsync(string[] args)
        {
            var someObject = new { SomeProperty = "SomeValue" };
            var properties = FF.PropertiesFor(someObject);

            var s = await properties.RenderAsync();
            Console.WriteLine(s);
            Console.ReadLine();
        }
    }
}
