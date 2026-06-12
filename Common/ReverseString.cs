public class ReverseString {
    public static void Run()
    {
        String input = "hello";
        // Important concept in C#
        // String is immutable (cannot be changed after creation)
        Char[] arr = input.ToCharArray();
        // Convert the string into an array of characters.
        // Now the array can be modified.
        // arr = ['h','e','l','l','o']
        Array.Reverse(arr);
        String result = new String(arr);
        Console.WriteLine(result);

        // String input = "hello";
        // String result = new string(input.Reverse().ToArray());
        // Reverse() returns IEnumerable<Char> but it is not an array yet, just a lazy enumerable sequence
        // Console.WriteLine(result);
    }
}