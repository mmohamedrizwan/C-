namespace InterviewSolution
{
    /// <summary>
    /// Thread vs Task vs await
    /// 
    /// Thread:
    /// A Thread is an actual execution path managed by the operating system.
    /// It executes code.
    /// 
    /// Task:
    /// A Task represents an asynchronous operation or unit of work that may complete
    /// in the future. A Task is Not a Thread.
    /// 
    /// For CPU-bound work, Task.Run() commonly schedules the work on a ThreadPool thread.
    /// 
    /// await:
    /// await asynchronously waits for a Task to complete. It does not block
    /// the current thread while waiting.
    /// 
    /// For I/O-bound operations such as HTTP or database calls, async/await allows the thread
    /// to be released while I/O operation is in progress.
    /// </summary>
    internal class TaskVsThread
    {
        public static async Task Run()
        {
            // ----------------------------------------
            // 1. THREAD
            // ----------------------------------------

            // A Thread is an actual path of execution.
            Thread thread = new Thread(() =>
            {
                Console.WriteLine("Running on another thread");
            });
            // Explicitly create and start a thread.
            thread.Start();

            /**
             * 
             * Thread
             * ↓
             * Actual execution resource
             * ↓
             * Executes code
             * 
             * Creating many threads can be expensive because each thread 
             * consumes memory and has scheduling overhead.
             */

            // ----------------------------------------
            // 2. TASK
            // ----------------------------------------

            // A Task represents an operation that may complete in the future.
            Task task = Task.Run(() => 
            {
                Console.WriteLine("Running Task work");

                // Simulating CPU/blocking work for 2 seconds
                Thread.Sleep(2000);

                Console.WriteLine("Task work completed");
            });

            Console.WriteLine("Main continues...");

            /*
             * Task is NOT a Thread.
             *
             * In this example, Task.Run() schedules the work on a
             * ThreadPool thread.
             *
             * Task.Run()
             *   ↓
             * Task
             *   ↓
             * Task Scheduler
             *   ↓
             * ThreadPool thread
             *   ↓
             * Executes the delegate
             */

            // ----------------------------------------
            // 3. AWAIT
            // ----------------------------------------

            await task;

            Console.WriteLine("Task finished");

            /*
             * await does NOT create a new thread.
             *
             * await means:
             *
             * "I need to continue this method after this Task completes,
             * but don't synchronously block the current thread while waiting."
             *
             * Flow:
             *
             * Task is running
             *       ↓
             * await task
             *       ↓
             * Task not completed?
             *       ↓
             * Run() yields
             *       ↓
             * Thread can do other work
             *       ↓
             * Task completes
             *       ↓
             * Run() continues
             *       ↓
             * "Task finished"
             */
        }
    }
}
