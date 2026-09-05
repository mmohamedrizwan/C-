# Can Async Methods Be Used in Constructors in C#?

## Short Answer

**No. C# constructors cannot be declared as `async`.**

You cannot write:

```csharp
public async MyClass()
{
    await SomeMethodAsync();
}
```

This will not compile - the compiler does not permit the `async` modifier on a constructor.

## Why It's Disallowed
 
Two separate C# rules run into each other here.

**Rule 1 - `async` methods can only return specific types.**
When a method is marked `async`, the compiler only allows it to return `void`, `Task`, `Task<T>`, or `ValueTask<T>`. This exists because the point of `async` is that the caller can `await` it — `Task` and `Task<T>` are the objects that represent "work that will finish later" and let the caller wait on it, get a result, or catch an exception once it's done.

**Rule 2 — A constructor's return type is fixed to the class itself.**
`new MyClass()` always hands back a `MyClass` reference, immediately and synchronously. A constructor doesn't get to choose what it returns — that's baked into the language.


# `ref` vs `out` in C#

Both `ref` and `out` pass arguments by reference instead of by value, but they differ in intent and rules.

## `ref`

- Variable must be initialized **before** being passed in.
- The method can read the existing value and optionally change it.
- Use when the method needs both an input and way to modify it.

```csharp
void Increment(ref int x)
{
    x++;
}
 
int a = 5;
Increment(ref a);
// a is now 6
```

## `out`

- Variable does **not** need to be initialized before the call.
- The method **must** assign a value before it returns - the compiler enforces this.
- Any value passed in is ignored; it's purely for output.
- Use when the method's whole purpose is to produce a value (or several).

```csharp
bool TryParse(string input, out int result)
{
    if (int.TryParse(input, out result))
    {
        return true;
    }
    result = 0;
    return false;
}

int number;
TryParse("42", out number);
// number is now 42
```

## Quick Comparison
 
| | `ref` | `out` |
|---|---|---|
| Must be initialized before call | Yes | No |
| Must be assigned inside method | No | Yes |
| Typical use | Modify an existing value | Return extra value(s) |
| Common example | Swap functions, in-place mutation | `TryParse`, `TryGetValue` |


# `return` Inside `finally` in C#

## Key Point

**`finally` should be used for cleanup, not for returning values.**

A `return` statement inside `finally` can **override a previous return value or suppress an exception**.

## 1. What Does "Suppress an Exception" Mean?

Suppressing an exception means that an exception occurs, but instead of being passed to the caller, it gets **hidden** because the `finally` block contains a `return` statement.

Consider this example:

```csharp
public int GetNumber()
{
    try
    {
        throw new Exception("Something went wrong");
    }
    finally
    {
        return 10;
    }
}
```

Normally, we expect:

```text
try
↓
Exception occurs
↓
finally executes
↓
Exception is thrown to the caller
```

But because finally contains:

```csharp
return 10;
```

the return takes precedence.

The method returns:

```text
10
```

and the exception:

```text
Something went wrong
```

is suppressed.

## 2. Example With try Returning a Value

A return inside finally can also override a return from try.

```csharp
public int GetNumber()
{
try
{
return 10;
}
finally
{
return 20;
}
}
```

What value will this method return?

```text
20
```

The return 20 inside finally overrides the return 10 from try.

### Flow

```text
try
↓
return 10
↓
finally
↓
return 20
↓
20 is returned
```

## 3. Example With an Exception

Consider:

```csharp
public int Divide()
{
    try
    {
        int x = 10;
        int y = 0;

        return x / y;  
    }  
    finally  
    {  
        return 100;  
    }  
}
```

The following line causes a DivideByZeroException:

```csharp
return x / y;
```

Normally, the exception would be thrown.

However, finally contains:

```csharp
return 100;
```

Therefore, the method returns:

```text
100
```

and the exception is suppressed.


# A struct is passed to a method that mutates it. After calling the method with the struct argument, what is printed and why does the original struct remain unchanged?

## The Code

```csharp
struct S
{
    public int x;
}

void Mutate(S s)
{
    s.x = 5;
}

S s = new S();
s.x = 0;
Mutate(s);
Console.WriteLine(s.x); // 0
```

## Output 

```
0
```

## Why 

`S` is a `struct`, and structs in C# are **value types**. When you pass `s` into `Mutate(s)` without `ref`, `out`, or `in`, C# copies the entire struct into the method's parameter. Inside `Mutate`, the parameter `s` is a brand-new, independent copy sitting in its own memory — completely disconnected from the `s` in the caller's scope.

So `s.x = 5;` inside `Mutate` only changes the copy's `x` field. Once `Mutate` returns, that copy is discarded, and the original `s` back in the caller is untouched — still `0`.

## Fixing It: Use `ref`
 
To actually mutate the original, pass it by reference:
 
```csharp
void Mutate(ref S s)
{
    s.x = 5;
}
 
Mutate(ref s);
Console.WriteLine(s.x); // 5
```
 
`ref` tells the compiler "don't copy — give the method direct access to the original variable's memory."

## The Core Distinction
 
- **Structs (value types):** copied by default when passed to a method. Mutations inside the method don't affect the original unless you use `ref`.
- **Classes (reference types):** passing an object passes a reference to the same underlying instance, so mutations to its fields *do* propagate back to the caller — no `ref` needed.


# Queue vs Stack

## Queue - First In, First Out (FIFO)

Items are processed in the order they were added - the first item added is the first one removed. Think of a line of people waiting: whoever got in line first get served first.

**Key operations:**
- `Enqueue(item)` - add to the back
- `Dequeue()` - remove from the front

**Example scenerio:** A print spooler. Documents sent to the printer should print in the order they were submitted - the first document queued should be the first one printed, regardless of how many more get added while it's still working through the list.

## Stack — Last In, First Out (LIFO)

Items are processed in reverse order of insertion - the most recently added item is the first one removed. Think of a stack of plates: you take from top, which is also where you last placed one.

**Key operations:**
- `Push(item)` - add to the top
- `Pop()` - remove from the top

**Example scenerio:** The "undo" feature in a text editor. Each edit action gets pushed onto a stack; when the user hits Ctrl + Z, you pop the most recent action off and reverse it - you always undo the *last* thing done first, not the oldest.

## Quick Comparison
 
| Aspect | Queue | Stack |
|---|---|---|
| Order | FIFO (First In, First Out) | LIFO (Last In, First Out) |
| Add | `Enqueue` (back) | `Push` (top) |
| Remove | `Dequeue` (front) | `Pop` (top) |
| Real-world analogy | Waiting line | Stack of plates |
| Common use cases | Task scheduling, message queues, breadth-first search (BFS) | Undo/redo, expression evaluation, backtracking, depth-first search (DFS), call stack |
