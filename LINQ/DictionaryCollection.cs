// Why Dictionary is fast
// 👉 Internally uses Hashing
// Concept:
// Key -> Hash function -> Bucket -> Value
// 👉 Average Time Complexity:
// * Insert -> O(1)
// * Lookup -> O(1)
// * Delete -> O(1)
// How hashing works
// When you do:
// dict["apple"] = 10
// Steps:
// 1. "apple" -> GetHashCode()
// 2. Hash -> mapped to a bucket
// 3. Value stored in that bucket

public class DictionaryCollection
{
    public static void Run()
    {
        Dictionary<Int32, String> dict = new Dictionary<Int32, String>();
        dict.Add(1, "One");
        dict[2] = "Two"; // Another way to add

        // Looping through dictionary
        foreach (KeyValuePair<Int32, String> kvp in dict)
        {
            Console.WriteLine($"{kvp.Key} -> {kvp.Value}"); 
        }

        // Accessing values
        Console.WriteLine(dict[1]);
        // If key doesn't exist -> throws exception
        // Safe way:
        if (dict.ContainsKey(1))
        {
            Console.WriteLine(dict[1]);
        }
        // OR best:
        if (dict.TryGetValue(1, out String name))
        {
            Console.WriteLine(name);
        }

        // Updating values
        dict[1] = "Updated name";
        // Removing elements
        dict.Remove(2);

        // Important Properties
        Console.WriteLine(dict.Count);
        Console.WriteLine(String.Join(", ", dict.Keys));
        Console.WriteLine(String.Join(", ", dict.Values));
    }
}