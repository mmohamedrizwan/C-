namespace InterviewQuestion.CodingSolution
{
    public class DuplicateNumber
    {
        public static void LINQ()
        {
            Console.WriteLine("Duplicate Number -> LINQ");

            int[] numbers = { 1, 2, 3, 2, 4, 5, 1, 6 };

            IEnumerable<int> duplicates = numbers
                        // Group the numbers by their value
                        .GroupBy(x => x)
                        // Filter the groups to only those with more than one occurrence
                        .Where(g => g.Count() > 1)
                        // Select the key (the duplicate number) from the filtered groups
                        .Select(g => g.Key);

            foreach (int duplicate in duplicates)
            {
                Console.WriteLine(duplicate);
            }
        }

        public static void HashSet()
        {
            Console.WriteLine("Duplicate Number -> HashSet");

            int[] numbers = { 1, 2, 3, 2, 4, 5, 1, 6 };
            HashSet<int> seen = new HashSet<int>();
            HashSet<int> duplicates = new HashSet<int>();

            foreach (int number in numbers)
            {
                // seen.Add(number) returns true if the number is new
                // If the number already exist, Add() returns false
                if (!seen.Add(number))
                {
                    duplicates.Add(number);
                }
            }

            foreach (int duplicate in duplicates)
            {
                Console.WriteLine(duplicate);
            }
        }
    }
}