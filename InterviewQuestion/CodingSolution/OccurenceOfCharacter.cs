
namespace InterviewQuestion.CodingSolution
{
    public class OccurenceOfCharacter
    {
        public static void LINQ()
        {
            Console.WriteLine("Occurrence of Character -> LINQ");

            string input = "hello world";

            IEnumerable<IGrouping<char, char>> result = input.GroupBy(c => c);

            foreach (IGrouping<char, char> group in result)
            {
                Console.WriteLine($"Character: {group.Key}, Occurrences: {group.Count()}");
            }
        }

        public static void Dictionary()
        {
            Console.WriteLine("Occurrence of Character -> Dictionary");
            
            string input = "hello world";
            
            Dictionary<char, int> charCount = new Dictionary<char, int>();

            foreach (char c in input)
            {
                if (c == ' ') { continue; }

                if (charCount.ContainsKey(c))
                {
                    charCount[c]++;
                }
                else
                {
                    charCount[c] = 1;
                }
            }

            foreach (var kvp in charCount)
            {
                Console.WriteLine($"Character: {kvp.Key}, Occurrences: {kvp.Value}");
            }
        }
    }
}