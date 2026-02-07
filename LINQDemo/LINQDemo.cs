
// A record in C# is a special type introduced in C# 9 used mainly for:
// ✔ immutable data
// ✔ value-based equality
// ✔ cleaner syntax for data containers(DTOs, models)
public record Person(String name, Int32 age);

// LINQ (Language Integrated Query) is a feature in C# that allows you to write queries directly in C# to work with:
// 1. Collections (List, Array)
// 2. Databases (LINQ to SQL, EntityFramework)
// 3. XML
// 4. Objects
// 5. Files
// Instead of writing loops and conditions manually, LINQ gives you a simple, SQL-style way to query data
class LINQDemo 
{
    public static void Run() 
    {
        List<int> numbers = new List<int> {  1, 5, 2, 8, 3, 10, 4, 7, 6, 9 };
        List<string> names = new List<string> 
        { 
            "Alice", "Bob", "Charlie", "David", "Eve", "Frank",
            "George", "Hannah", "Ivy", "Jack"
        };
        List<Person> people = new List<Person>
        {
            new Person("Alice", 25),
            new Person("Bob", 17),
            new Person("Charlie", 30),
            new Person("David", 16)
        };

        // Filter: Get even numbers
        List<Int32> evens = numbers.Where(n => n % 2 == 0).ToList();
        foreach (int n in evens)
            Console.WriteLine(n);   
        
        // Sort: Order numbers descending
        List<Int32> sortedDesc = numbers.OrderByDescending(n => n).ToList();
        Console.WriteLine(String.Join(", ", sortedDesc));

        // Select: Convert names to uppercase
        List<String> upperNames = names.Select(name => name.ToUpper()).ToList();
        foreach (String n in upperNames)
            Console.WriteLine(n);

        // Complex Filter: Get adults(18+)
        List<Person> adults = people.Where(p => p.age >= 18).ToList();
        foreach (Person p in adults)
            Console.WriteLine($"{p.name} ({p.age})");

        // Projection: Select anonymous objects
        var result = people.Select(p => new {
            p.name,
            isAdult = p.age >= 18
        });
        // new {} this code creates an anonymous object
        // It creates an object without a class definition, where:
        // * name -> comes from p.name
        // * isAdult -> a new property computed using the expression p.age >= 18
        // This is called an anonymous type.
        // anonymous objects are read-only

        foreach (var r in result)
            Console.WriteLine($"{r.name} - Adult: {r.isAdult}");

        // Distinct: Remove duplicates
        List<Int32> withDupes = new List<Int32> {1, 2, 2, 3, 3, 3, 4};
        List<Int32> unique = withDupes.Distinct().ToList();

        Console.WriteLine(String.Join(", ", unique));

        // Skip & Take: Pagination
        // Get the 3rd, 4th and 5th numbers.
        IEnumerable<int> middle = numbers.Skip(2).Take(3);
        Console.WriteLine(String.Join(", ", middle));

        // Any & All
        Boolean anyAbove9 = numbers.Any(n => n > 9);
        Boolean allPositive = numbers.Any(n => n > 0);

        Console.WriteLine($"Any > 9: {anyAbove9}");
        Console.WriteLine($"All positive: {allPositive}");

        // ComplexExample: Top 3 oldest people
        IEnumerable<Person> top3 = people.OrderByDescending(p => p.age).Take(3);

        foreach (Person p in top3)
            Console.WriteLine($"{p.name} ({p.age})");

        // Advanced Query Syntax example
        IEnumerable<String> longNames = from n in names 
                                        where n.Length > 4 
                                        orderby n 
                                        select n;

        foreach (String name in longNames)
            Console.WriteLine(name);

        // First / FirstOrDefault: Get first number greater than 8
        Console.WriteLine("-----------------First---------------------------------");
        Person pOne = people.First(p => p.age > 12);
        Console.WriteLine(pOne.name);
        // First() returns the first element that matches the condition
        // If no element exists, it throws: InvalidOperationException: Sequence contains no elements
        // FirstOrDefault() returns default value (null or default(T)) when no match is found.

        // Count: Number of names starting with 'A'
        Console.WriteLine("------------------Count Start With A--------------------");
        Int32 count = names.Count(n => n.StartsWith("A"));
        Console.WriteLine(count);

        // GroupBy: Group people by age category
        // Group into Adults and Minors
        IEnumerable<IGrouping<String, Person>> grouped = people.GroupBy(p => p.age >= 18 ? "Adult" : "Minor");

        Console.WriteLine("------------------GroupBy--------------------");
        foreach (IGrouping<String, Person> group in grouped)
        {
            Console.WriteLine(group.Key + ":");
            foreach (Person p in group)
                Console.WriteLine("  " + p.name);
        }
    }
}