using System;
using System.Threading.Tasks;

namespace FileSned
{
    internal static partial class Program
    {
        // private static Task Main() => Async();

        private static async Task Async()
        {
            var data = SomeLongReadOperation();
            Console.WriteLine("Hello");
            var s = await data;
            Console.WriteLine(s);
        }

        private static async Task<string> SomeLongReadOperation()
        {
            await Task.Delay(3000);
            return "World!";
        }
    }
}