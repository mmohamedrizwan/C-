# Abstract Class vs Interface in C#

## Difference b/w Abstract Class and Interface

| Feature | Abstract Class | Interface |
|---------|----------------|-----------|
| Purpose | Provides a base class with common implementation | Defines a contract that classes must implement |
| Methods | Can have abstract and concrete methods | Can declare methods (also supports default implementations in modern C#) |
| Fields | ✅ Yes | ❌ No instance fields |
| Properties | ✅ Yes | ✅ Yes |
| Constructors | ✅ Yes | ❌ No |
| Access Modifiers | Supports public, protected, private, etc. | Members are public by default (except newer default implementations) |
| State | Can maintain state using fields | Cannot maintain instance state |
| Multiple Inheritance | ❌ Only one abstract class can be inherited | ✅ Multiple interfaces can be implemented |
| Performance | Slightly faster in some scenarios | Negligible overhead |

---

# Abstract Class Example

```csharp
public abstract class Animal 
{
    public String Name { get; set; }

    public void Sleep()
    {
        Console.WriteLine($"{Name} is sleeping");
    }

    public abstract void MakeSound();
}

public class Dog : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Bark");
    }
}

class Program 
{
    static void Main()
    {
        Dog dog = new Dog
        {
            Name = "Tommy"
        };

        dog.Sleep();
        dog.MakeSound();
    }
}
```

### Output

```
Tommy is sleeping.
Bark
```

### When to Use Abstract Class

- **Share common implementation**
    - When multiple derived classes need the same method implementation.
- **Share common properties and methods**
    - Define common properties and methods once in the base class so all derived class inherit them.
- **Use constructors**
    - Abstract classes can have constructors to initialize common data for derived classes.
- **Maintain common state**
    - Abstract classes can contain fields and properties to store data shared by all derived classes.
- **Force derived classes to implement required methods**
    - Use abstract methods when every derived class must provide its own implementation.

```csharp
public abstract class Employee
{
    public string Name { get; set; }

    public void Login()
    {
        Console.WriteLine($"{Name} logged in.");
    }

    public abstract decimal CalculateSalary();
}

public class PermanentEmployee : Employee
{
    public override decimal CalculateSalary()
    {
        return 60000;
    }
}

public class ContractEmployee : Employee
{
    public override decimal CalculateSalary()
    {
        return 30000;
    }
}
```

--- 

# Interface Example

```csharp
public interface IAnimal
{
    void MakeSound();
}

public class Dog : IAnimal
{
    public void MakeSound()
    {
        Console.WriteLine("Bark");
    }
}

public class Cat : IAnimal
{
    public void MakeSound()
    {
        Console.WriteLine("Meow");
    }
}
```

### Usage

```csharp
IAnimal animal = new Dog();
animal.MakeSound();
```

### Output

```
Bark
```

### When to Use Interface

- Define a contract.
- Support multiple inheritance.
- Achieve loose coupling.
- Enable Dependency Injection.
- Improve Unit testing.

--- 

# Real-Time Example

## Abstract Class

```csharp
public abstract class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void Login()
    {
        Console.WriteLine("Employee Logged In");
    }

    public abstract decimal CalculateSalary();
}

public class PermanentEmployee : Employee
{
    public override decimal CalculateSalary()
    {
        return 60000;
    }
}

public class ContractEmployee : Employee
{
    public override decimal CalculateSalary()
    {
        return 30000;
    }
}
```

**Explanation**

- All employees have `Id`, `Name`, and `Login()`.
- Salary calculation differs for each employee type.
- Abstract class allows sharing common code while forcing subclasses to implement `CalculateSalary()`.

---

## Interface

```csharp
public interface INotification
{
    void Send(string message);
}

public class EmailNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"Email: {message}");
    }
}

public class SmsNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"SMS: {message}");
    }
}
```

**Explanation**

- Both Email and SMS notifications implement the same contract.
- Each class provides its own implementation.

---

# Using Both Together

```csharp
public abstract class Employee
{
    public abstract void Work();
}

public interface ILogger
{
    void Log(string message);
}

public class Developer : Employee, ILogger
{
    public override void Work()
    {
        Console.WriteLine("Writing Code");
    }

    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}
```

---

# Key Differences

| Abstract Class | Interface |
|----------------|-----------|
| Can contain implementation | Defines a contract |
| Can have constructors | Cannot have constructors |
| Can have fields | Cannot have instance fields |
| Supports state | No instance state |
| Single inheritance | Multiple implementation |
| Used for closely related classes | Used for unrelated classes sharing common behavior |

---

# Interview Answer 

**Abstract Class**

- Used when classes share common implementation and state.
- Can have constructor, fields and implemented methods.
- Supports only single inheritance.

**Interface**

- Used to define a contract.
- Support multiple inheritance.
- Commonly used with Dependency Injection for loose coupling.

**Rule of Thumb**

- Use an **Abstract Class** when you need **shared implementation and state**.
- Use an **Interface** when you need **a common contract, flexibility and loose coupling**.