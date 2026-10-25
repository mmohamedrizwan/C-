using System.Text;

namespace InterviewQuestion.CodingSolution
{
    public class ReverseString
    {
        /// <summary>
        /// In C#, Strings are immutable, meaning that once a string is created, it cannot be changed. 
        /// When you concatenate strings using the `+` operator, a new string is created each time, which can lead to performance issues, especially in loops. 
        /// This method demonstrates reversing a string using concatenation, which is not the most efficient way to do it.
        /// 
        /// For example:
        /// - Iteration 1: "" + 't' → "t"
        /// - Iteration 2: "t" + 'e' → "te"
        /// - Iteration 3: "te" + 'n' → "ten"
        /// 
        /// As the string grows, each concatenation creates a new string, leading to increased memory usage and slower performance.
        /// </summary>
        public static void Concatenation()
        {
            String input = "Hello, World!";
            String result = String.Empty;

            for (int i = input.Length - 1; i >= 0; i--)
            {
                result += input[i];
            }

            Console.WriteLine($"Original: {input}");
            Console.WriteLine($"Reversed: {result}");
        }

        public static void Char()
        {
            String input = "hello world";
            Char[] result = new Char[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                result[i] = input[input.Length - 1 - i];
            }

            Console.WriteLine($"Original: {input}");
            Console.WriteLine($"Reversed: {new string(result)}");
        }

        /// <summary>
        /// Demonstrates reversing a string using a StringBuilder and outputs the original and 
        /// reversed strings to the console.
        /// </summary>
        public static void StringBuilder()
        {
            String input = "hello world";
            StringBuilder result = new StringBuilder();

            for (int i = input.Length - 1; i >= 0; i--)
            {
                result.Append(input[i]);
            }

            Console.WriteLine($"Original: {input}");
            Console.WriteLine($"Reversed: {result.ToString()}");
        }
    }
}