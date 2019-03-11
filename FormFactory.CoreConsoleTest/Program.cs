using System;
using System.Threading.Tasks;
using FormFactory.Standalone;

namespace FormFactory.CoreConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }
        static async Task MainAsync(string[] args)
        {
            var someObject = new { SomeProperty = "SomeValue" };
            var properties = FF.PropertiesFor(someObject);

            var s = properties.Render();
            Console.WriteLine(s);
            Console.ReadLine();
        }
    }
}
