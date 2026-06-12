# Query Pipeline in LINQ

## What is a Query Pipeline?

A Query Pipeline is a sequence of operations that process data step by step.

In LINQ, each method call (such as `Where`, `Select`, `OrderBy`) forms a stage in the pipeline. The output of one stage becomes the input for the next stage.

Think of it like a water pipeline:

```text
Data Source
     |
   Where
     |
   Select
     |
  OrderBy
     |
   Result
```

---

## Example

```csharp
List<int> numbers = new List<int>
{
    1, 2, 3, 4, 5, 6
};

var result = numbers
    .Where(x => x % 2 == 0)
    .Select(x => x * 10)
    .OrderBy(x => x);

foreach (var item in result)
{
    Console.WriteLine(item);
}
```

Output:

```text
20
40
60
```

---

## How the Pipeline Works

### Stage 1: Data Source

```csharp
numbers
```

Input:

```text
1, 2, 3, 4, 5, 6
```

---

### Stage 2: Filtering (`Where`)

```csharp
.Where(x => x % 2 == 0)
```

Keeps only even numbers.

Output:

```text
2, 4, 6
```

---

### Stage 3: Projection (`Select`)

```csharp
.Select(x => x * 10)
```

Transforms each value.

Output:

```text
20, 40, 60
```

---

### Stage 4: Sorting (`OrderBy`)

```csharp
.OrderBy(x => x)
```

Sorts the data.

Output:

```text
20, 40, 60
```

---

## Deferred Execution

Most LINQ queries are not executed immediately.

```csharp
var query = numbers.Where(x => x > 3);
```

At this point, no filtering has happened yet.

Execution occurs when the data is actually requested:

```csharp
foreach (var item in query)
{
    Console.WriteLine(item);
}
```

This behavior is called **Deferred Execution**.

---

## Query Pipeline in Entity Framework

```csharp
var employees = context.Employees
    .Where(e => e.Salary > 50000)
    .OrderBy(e => e.Name)
    .Select(e => new
    {
        e.Id,
        e.Name
    });
```

Pipeline:

```text
Employees Table
       |
     Where
       |
    OrderBy
       |
    Select
       |
    SQL Query Generated
       |
     Database
       |
     Result
```

Entity Framework converts the LINQ pipeline into SQL and executes it against the database.

---

## Common Pipeline Operators

### Filtering

```csharp
Where()
```

### Projection

```csharp
Select()
```

### Sorting

```csharp
OrderBy()
OrderByDescending()
ThenBy()
```

### Grouping

```csharp
GroupBy()
```

### Joining

```csharp
Join()
```

### Aggregation

```csharp
Count()
Sum()
Average()
Max()
Min()
```

---

## Benefits of Query Pipelines

1. Readable code
2. Reusable query logic
3. Deferred execution
4. Better performance
5. Easy data transformation
6. Works with collections, databases, XML, and APIs

---

## Summary

A Query Pipeline is a chain of LINQ operations where:

```text
Source -> Filter -> Transform -> Sort -> Aggregate -> Result
```

Each operation receives data from the previous stage, processes it, and passes it to the next stage until the final result is produced.