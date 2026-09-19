namespace InterviewSolution
{
    /// <summary>
    /// Task vs ValueTask
    /// 
    /// Task<T>
    /// - Default choice for asynchronous programming.
    /// - Suitable for database calls, API calls, file I/O, etc.
    /// 
    /// ValueTask<T>
    /// - Used mainly as a performance optimization.
    /// - Useful when an operation frequently completes synchronously.
    /// - Can avoid allocating a Task when the result is already available.
    /// 
    /// Simple rule:
    /// Task      -> Default Choice
    /// ValueTask -> Optimization for frequently synchronous operations
    /// </summary>
    internal class TaskVsValueTask
    {
        /// <summary>
        /// Task example.
        /// 
        /// Task<T> represents an asynchronous operation
        /// that eventually returns a value.
        /// </summary>
        public static async Task<int> GetDataUsingTaskAsync()
        {
            // Simulating an asynchronous operation
            await Task.Delay(1000);

            return 100;
        }

        /// <summary>
        /// ValueTask example.
        /// 
        /// If the result is already available synchronously,
        /// ValueTask can return the result without creating a Task object.
        /// </summary>
        public static ValueTask<int> GetDataUsingValueTaskAsync(bool fromCache)
        {
            if (fromCache)
            {
                // Result is already available.
                // No asynchronous operation is required
                return new ValueTask<int>(100);
                //return ValueTask.FromResult(100);
            }

            // If the result is not available, perform the asynchronous operation.
            return LoadDataAsync();
        }

        /// <summary>
        /// Simulates an asynchronous operations such as a database or API call
        /// </summary>
        private static async ValueTask<int> LoadDataAsync()
        {
            await Task.Delay(1000);

            return 200;
        }
    }
}
