// Lock is one of the most important synchronization mechanisms in C#.
// Why do we need lock?
// Imagine two threads trying to update the same bank account balance at the same time.
// Without lock

class BankAccount1
{
    public int Balance = 1000;

    public void Withdraw(int amount)
    {
        Balance = Balance - amount;
    }
}

// Suppose:
// 1. Thread 1 withdraws 100
// 2. Thread 2 withdraws 200
// Current balance = 1000

// What can happen?
// | Step         | Thread 1 | Thread 2 |
// | ------------ | -------- | -------- |
// | Read Balance | 1000     | 1000     |
// | Calculate    | 900      | 800      |
// | Write        | 900      | 800      |

// Final balance becomes: 800
// But the correct balance should be: 1000 - 100 - 200 = 700
// This problem is called a Race Condition.
// Multiple threads are racing to modify the same data.

// Solution

class BankAccount2
{
    private readonly Object _lockObj = new Object();

    public Int32 Balance = 1000;

    public void Withdraw(Int32 amount)
    {
        lock (_lockObj)
        {
            Balance -= amount;
        }
    } 
}

// Now only one thread can enter the critical section at a time.

public class Lock
{
    static Int32 counter = 0;
    static Int32 visitorCount = 0;
    static Object _lockObj = new Object();

    // Every time a user visits your website, you increment a counter.
    // This is correct, but every increment requires:
    // 1. Acquiring a lock.
    // 2. Releasing a lock.
    // That adds overhead.
    // Using Interlocked: same result, but faster.
    // Why is Interlocked faster?
    // With lock:
    // lock(lockObj)
    // {
    // counter++;
    // }
    // Internally:
    // Acquire lock
    // Read counter
    // Add 1
    // Write counter
    // Release lock
    // With Interlocked:
    // Internally, the CPU performs the increment as a single atomic operation.
    // Increment Counter Atomically
    // No lock object.
    // No waiting.
    // No blocking.
    static void  IncrementVisitor()
    {
        for (Int32 i = 0; i < 100000; i++)
        {
            Interlocked.Increment(ref visitorCount);
        }
    }

    static void Increment()
    {
        for (Int32 i = 0; i < 100000; i++)
        {
            lock (_lockObj)
            {
                counter++;
            }
        }
    }

    public static void Run()
    {
        Thread t1 = new Thread(IncrementVisitor);
        Thread t2 = new Thread(IncrementVisitor);

        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();

        // t1.Join() and t2.Join() are used to make the main thread wait until t1 and t2 finish their work

        Console.WriteLine(visitorCount);
    }
}

// Use Interlocked when:
// 1. Incrementing counters
// 2. Decrementing counters
// 3. Updating a single numeric value atomically

// Use lock when:
// 1. Multiple statements must execute as one unit
// 2. Updating multiple variable together
// 3. Working with collections (List, Dictionary, Queue etc.)
// 4. Protecting complex business logic

// Why do we write private readonly Object _lockobj = new Object();
// We use a private readonly object because the lock statement in C# requires a reference type object as 
// the synchronization token. Internally lock uses Monitor.Enter() and Monitor.Exit() on that object. 
// A dedicated private readonly object is the safest choice because it prevents external code from locking
// on the same object and ensures the lock references does not change during the lifetime of the class.

// Why not int, bool, etc.?
// Because lock needs  a reference object shared by all threads.

// Why create a separate object?
// we do this:
// private readonly object _lockObj = new object();
// because we need a dedicated object whose only purpose is locking.
// 
// Think of it like a room key:
// 1. The object itself does not store the business data.
// 2. It just acts as a lock token.
// 3. Threads compete to acquire that token.

// Why object specifically?
// Because object is:
// 1. Simple.
// 2. Lightweight.
// 3. Meant to be used as a generic reference.
// 4. Commonly used as a lock token.