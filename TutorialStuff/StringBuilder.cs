using System;
using System.Diagnostics;
using System.Text;

namespace FileSned
{
    internal static partial class Program
    {
        //private static void Main() => StringBuilder();

        private static void StringBuilder()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var frMemory = WithoutBuilder();
            stopwatch.Stop();
            var frTime = stopwatch.Elapsed.TotalSeconds;
            stopwatch.Restart();
            var srMemory = WithBuilder();
            stopwatch.Stop();
            var srTime = stopwatch.Elapsed.TotalSeconds;
            Console.WriteLine($"First Run:  {frTime}s, {frMemory} bytes\nSecond Run: {srTime}s, {srMemory} bytes");
        }

        private static long WithBuilder()
        {
            const string phrase = "lalalalalalalalalalalalalalalalalalalalalalalalalalalalalala";
            var manyPhrases = new StringBuilder();
            for (var i = 0; i < 10000; i++) manyPhrases.Append(phrase);

            Console.WriteLine("tra" + manyPhrases);
            return GC.GetTotalMemory(true);
        }

        private static long WithoutBuilder()
        {
            var text = "";
            const string phrase = "lalalalalalalalalalalalalalalalalalalalalalalalalalalalalala";
            for (var i = 0; i < 10000; i++) text += phrase;

            Console.WriteLine("tra" + text);
            return GC.GetTotalMemory(true);
        }
    }
}