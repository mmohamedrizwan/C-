public class TaskWhenAll
{
    public static async Task Run()
    {
        Console.WriteLine("Main Started");

        // Task 1 and Task 2 run at the same time.
        // Task 2 finishes earlier because its delay is smaller.
        // Task.WhenAll waits until both tasks finish.
        Task task1 = PrintNumbers("Task 1", 1000);
        Task task2 = PrintNumbers("Task 2", 500);

        await Task.WhenAll(task1, task2);

        Console.WriteLine("All Task Completed");
    }

    public static async Task PrintNumbers(String taskName, Int32 delay)
    {
        for (Int32 i = 1; i <= 5; i++)
        {
            Console.WriteLine($"{taskName}: {i}");
            await Task.Delay(delay);
        }
    }
}

// When should we use Task.WhenAll?
// 
// Use Task.WhenAll when:
// 1. Multiple tasks are independent of each other.
// 2. You want to execute them concurrently.
// 3. You need all results before proceeding.
//
// Example:
// 1. Calling multiple API's.
// 2. Fetching data from multiple database/services.
// 3. Sending multiple emails.
// 4. Processing multiple files.
//
// When Not to use it
// If Task B depends on the result of Task A:
// var user = await GetUserAsync();
// var orders = await GetOrdersAsync(user.Id);
// Here Task.WhenAll cannot be used because the second task requires the first task's result.