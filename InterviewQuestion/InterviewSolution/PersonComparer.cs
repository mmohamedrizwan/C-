namespace InterviewSolution
{
    class Person
    {
        public String Name { get; set; }
        public Int32 Age { get; set; }

        public Person(String name, Int32 age)
        {
            Name = name;
            Age = age;
        }
    }

    /// <summary>
    /// IEqualityComparer<T> allows us to define custom equality logic for a type without modifying the
    /// type itself(You don't have to change the Person class to define how two Person objects should be compared). 
    /// It provides Equals() to determine whether two objects are equal and GetHashCode() to generate the hash code 
    /// </summary>
    internal class PersonComparer: IEqualityComparer<Person>
    {
        public bool Equals(Person x, Person y)
        {
            // Both references point to the same object
            if (ReferenceEquals(x, y))
                return true;

            if (x is null || y is null)
                return false;


            return String.Equals(x.Name, y.Name, StringComparison.Ordinal)
                && x.Age == y.Age;
        }

        /// <summary>
        /// GetHashCode() returns a hash value used by hash-based collections such as HashSet and Dictionary
        /// to quickly locate objects. The collection uses the hash code to identify the bucket where an object 
        /// may exist, and then uses Equals() to determine actual equality. If two objects are equal according to
        /// Equals(), they must return the same hash code.
        /// </summary>
        /// <param name="obj">Person</param>
        /// <returns>One Hash Number</returns>
        public Int32 GetHashCode(Person obj)
        {
            return HashCode.Combine(obj.Name, obj.Age);
        }
    }
}
