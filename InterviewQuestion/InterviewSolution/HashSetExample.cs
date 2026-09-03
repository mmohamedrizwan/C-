namespace InterviewSolution
{
    internal class HashSetExample
    {
        public static void Run()
        {
            Person p1 = new Person("Rizwan", 25);
            Person p2 = new Person("Rizwan", 25);
            Person p3 = new Person("Ahmed", 30);

            ISet<Person> people = new HashSet<Person>(new PersonComparer());
            people.Add(p1);
            people.Add(p2);
            people.Add(p3);

            Console.WriteLine("People count: " + people.Count);
        }

       
    }
}
