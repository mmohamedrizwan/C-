using InterviewSolution;


Person person1 = new Person("John", 30);
Person person2 = new Person("John", 30);
Person person3 = new Person("Ahmed", 25);

PersonComparer comparer = new PersonComparer();

// Check Equals
Console.WriteLine("p1 and p2:");
Boolean result = comparer.Equals(person1, person2);
Console.WriteLine("Equals: " + result);

// Both different object returns same hash code
Console.WriteLine("person1 HashCode: " + comparer.GetHashCode(person1));
Console.WriteLine("person2 HashCode: " + comparer.GetHashCode(person2));

Console.WriteLine();

// Compare p1 and p3
Console.WriteLine("p1 and p3:");
Console.WriteLine("Equals: " + comparer.Equals(person1, person3));

Console.WriteLine("person1 HashCode: " + comparer.GetHashCode(person1));
Console.WriteLine("person3 HashCode: " + comparer.GetHashCode(person3));

/*
 * What is happening? 
 * 
 * We created: person1(p1), person2(p2)
 * 
 * They are different objects:
 * p1 -> Person("John", 30)
 * p2 -> Person("John", 30)
 * 
 * But our comparer says:
 * comparer.Equals(p1, p2)
 * 
 * Result: True
 * 
 * Because: 
 * p1.Name == p2.Name    → True
 * p1.Age  == p2.Age     → True
 * 
 * And because they are equal, their hash codes should be the same:
 * 
 * comparer.GetHashCode(p1)
 * comparer.GetHashCode(p2)
 */

HashSetExample.Run();