namespace InterviewSolution
{
    /// <summary>
    /// ReferenceEquals() -> "Are these the exact same object?"
    /// Equals()          -> "Are these considered equal?"
    /// ==                -> "What equality rule does this type/operator define?"
    /// </summary>
    internal class Equality
    {
        class Person
        {
            public String Name { get; set; }
        }

        record PersonRecord(string Name);

        public static void Run()
        {
            // ============================================================
            // 1. ReferenceEquals() with a normal class
            // ============================================================

            Person p1 = new Person { Name = "Rizwan" };
            Person p2 = new Person { Name = "Rizwan" };

            // p1 and p2 contain references to two different objects.
            //
            // p1 ─────→ Person Object #1
            //            Name = "Rizwan"
            //
            // p2 ─────→ Person Object #2
            //            Name = "Rizwan"

            // ReferenceEquals() checks whether two references point to the exact same object.
            Console.WriteLine(ReferenceEquals(p1, p2)); // False

            // Now p2 points to the same object as p1.
            p2 = p1;

            // p1 ─────┐
            //         ↓
            //     Person Object #1
            //         ↑
            // p2 ─────┘

            Console.WriteLine(ReferenceEquals(p1, p2)); // True

            // ============================================================
            // 2. Equals() with a normal class
            // ============================================================

            Person p3 = new Person { Name = "Rizwan" };
            Person p4 = new Person { Name = "Rizwan" };

            // Person does not override Equals().
            // Therefore, the default equality behavior is reference-based.

            Console.WriteLine(p3.Equals(p4)); // False

            // ============================================================
            // 3. == with a normal class
            // ============================================================

            // For a normal class that does not overload ==,
            // == generally compares object references.

            Console.WriteLine(p3 == p4); // False

            p4 = p3;

            Console.WriteLine(p3 == p4); // True

            // ============================================================
            // 4. string equality
            // ============================================================

            String s1 = new String('a', 3);
            String s2 = new String('a', 3);

            // s1 and s2 are different string objects,
            // but both contain the same value: "aaa".

            Console.WriteLine(s1 == s2);                // True
            Console.WriteLine(s1.Equals(s2));           // True
            Console.WriteLine(ReferenceEquals(s1, s2)); // False

            // ============================================================
            // 5. Records
            // ============================================================

            // Records provide value-based equality by default.
            //
            // Example:
            //
            PersonRecord r1 = new PersonRecord("Rizwan");
            PersonRecord r2 = new PersonRecord("Rizwan");
            //
            Console.WriteLine(r1 == r2); // True
        }
    }
}
