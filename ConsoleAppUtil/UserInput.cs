using System;

namespace ConsoleAppUtil
{
    public static class UserInput
    {
        /// <summary>
        ///     Gets input from the user and validates it.
        /// </summary>
        /// <param name="prompt">The message being presented to the user when asked for input.</param>
        /// <param name="errorPrompt">
        ///     The message being presented to the user when they enter invalid
        ///     input and have to try again.
        /// </param>
        /// <param name="condition">The condition that the input should satisfy.</param>
        /// <returns>The users input.</returns>
        public static string GetInput(string prompt, string errorPrompt, Predicate<string> condition)
        {
            Console.Write($"{prompt}: ");
            string input;
            while (true)
            {
                input = Console.ReadLine();
                if (condition.Invoke(input)) break;
                Console.Write($"{errorPrompt}: ");
            }

            return input;
        }

        /// <summary>
        ///     Gets input from the user and validates it.
        /// </summary>
        /// <param name="prompt">
        ///     The message being presented to the user when asked for input. This message is also used when the
        ///     user enters invalid input and has to try again.
        /// </param>
        /// <param name="condition">The condition that the input should satisfy.</param>
        /// <returns>The users input.</returns>
        public static string GetInput(string prompt, Predicate<string> condition)
        {
            return GetInput(prompt, prompt, condition);
        }
    }
}