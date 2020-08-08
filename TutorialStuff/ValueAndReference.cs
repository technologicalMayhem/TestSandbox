using System;

namespace FileSned
{
    internal static partial class Program
    {
        private static void Main()
        {
            ValueVsReference();
        }

        private static void ValueVsReference()
        {
            var test = new Test();
            test.Increase(5, 6);
            Console.WriteLine(test.Number);
        }

        private static void IncreaseInt(int i)
        {
            i++;
        }

        private class Test
        {
            public int Number { get; private set; } = 5;

            /// <summary>
            ///     Increases the <c>Number</c> value by a given amount.
            /// </summary>
            /// <param name="amount">The amount to increase it by.</param>
            public void Increase(int amount)
            {
                Number += amount;
            }

            /// <summary>
            ///     Increases the <c>Number</c> value by a given amount multiple times.
            /// </summary>
            /// <param name="amount">The amount to increase it by.</param>
            /// <param name="times">The amount of times to increase it.</param>
            public void Increase(int amount, int times)
            {
                for (var i = 0; i < times; i++) Number += amount;
            }
        }
    }
}