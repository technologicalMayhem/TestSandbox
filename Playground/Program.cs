using System;
using System.Text;
using System.Threading;
using static System.Console;

namespace Playground
{
    internal class Program
    {
        private static (int, int) _windowSize = (0, 0);

        private static void Main(string[] args)
        {
            double progress = 0;
            while (true)
            {
                ShowProgress(progress);
                progress += 1;
                Thread.Sleep(500);
            }
        }

        private static void ShowProgress(double percent)
        {
            if (WindowWidth < 22 && WindowHeight < 5) return;
            if (_windowSize != (WindowWidth, WindowHeight))
            {
                _windowSize = (WindowWidth, WindowHeight);
                Clear();
                Write("Transferring...");
            }

            try
            {
                SetCursorPosition(16, 0);
                Write($"{percent}%");

                SetCursorPosition(0, WindowHeight - 2);
                var barLength = Math.Ceiling(WindowWidth * (percent / 100));
                var builder = new StringBuilder();
                for (var i = 0; i < barLength; i++) builder.Append("=");

                Write(builder.ToString().PadRight(WindowWidth));
            }
            catch (ArgumentOutOfRangeException e)
            {
                Clear();
            }
        }
    }
}