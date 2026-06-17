using System.Text;

public class StringConcept
{
    public static void Run()
    {
        // Why StringBuilder?
        // A string in C# is immutable, meaning once created, it cannot be changed.
        // When you modify a string, a new object is created in memory.

        String name = "Rizwan";

        Console.WriteLine($"Value: {name}");
        Console.WriteLine($"HashCode: {name.GetHashCode()}");

        name += " Ahmed";

        Console.WriteLine($"Value: {name}");
        Console.WriteLine($"HashCode: {name.GetHashCode()}");

        // Notice the hash code changes because a new string object is created.

        // Memory:
        // Heap
        // "Rizwan"          ← Old Object
        // "Rizwan Ahmed"    ← New Object
        // The old object becomes eligible for Garbage Collection.

        // Performance Problem
        // Imagine:

        String result = "";

        for (Int32 i = 1; i <= 1000; i++)
        {
            result += i;
        }

        // Every iteration creates a new string.
        // Iteration 1 -> New Object
        // Iteration 2 -> New Object
        // Iteration 3 -> New Object
        // ...
        // Iteration 10000 -> New Object

        // This causes:
        // 1. High memory usage.
        // 2. More Garbage Collection.
        // 3. Slower performance.

        // StringBuilder Solution
        // StringBuilder modifies the same memory buffer instead of creating new objects repeatedly.

        StringBuilder report = new StringBuilder();

        for (Int32 i = 0; i <= 5; i++)
        {
            report.AppendLine($"Employee {i}");
        }

        Console.WriteLine(report.ToString());

        // Benefit:
        // 1. Uses the same buffer.
        // 2. Better performance.

        // Common StringBuilder Methods
        // Append
        StringBuilder sb = new StringBuilder();
        sb.Append("Hello");
        sb.Append(" World");
        Console.WriteLine(sb); // Hello World
        sb.Clear();
        // AppendLine
        sb.AppendLine("Line 1");
        sb.AppendLine("Line 2");
        Console.WriteLine(sb);
        sb.Clear();
        // Insert
        sb.Append("Rizwan");
        sb.Insert(0, "Mr. ");
        Console.WriteLine(sb); // Mr. Rizwan
        // Replace
        sb.Replace("Rizwan", "Ahmed");
        Console.WriteLine(sb); // Mr. Ahmed
        // Remove
        sb.Remove(0, 4);
        Console.WriteLine(sb); // Ahmed

        // When should you use String?
        // Use string when:
        // 1. Few modification
        // 2. Simple text values
        // 3. Readability is important

        // When should you use StringBuilder?
        // 1. Many concatenations
        // 2. Loops
        // 3. Large text generations
        // 4. Reports
        // 5. CSV creation
        // 6. Log generation

        // Why String is Immutable?
        // 1. Thread safety
        // Multiple thread can safely read the same string without synchronization.
        //
        // 2. Security
        // Strings are often used for:
        // - Connection strings
        // - URLs
        // - File Paths
        // - Configuration values
        // Imagine:
        // string connectionString = "Server=Prod;Database=MainDB";
        // If String were mutable, some code change it unexpectedly.
        // Immutability ensures that once created, the content cannot be altered.
        //
        // 3. String Interning Optimization
        // C# stores identical string literals only once in memory.
        String a = "Hello";
        String b = "Hello";
        // Both variables reference the same object.
        Console.WriteLine(Object.ReferenceEquals(a, b)); // True
        // If strings were mutable:
        // a[0] = "X";
        // Then b would unexpectedly become ""Xello".
        // Immutability makes string interning safe.
        //
        // 4. Reliable Hash Codes
        // Strings are frequently used as keys in collections.
        // The dictionary depends on the string's hash code.
        // If the string could change after insertion:
        // users["Admin"] -> "SuperAdmin"
        // The hash code would change and the dictionary lookup would fail.
        // Immutability guarantees consistent hash codes.
        // 
        // 5. Better Memory Management
        // Because strings are immutable, .NET can reuse identical strings.
        string s1 = "Hello";
        string s2 = "Hello";
        string s3 = "Hello";
        // All three variables can point to the same memory location.
        // Heap
        // +---------+
        // | "Hello" |
        // +---------+
        // ^  ^  ^
        // |  |  |
        // s1 s2 s3
        // This saves memory.
    }
}