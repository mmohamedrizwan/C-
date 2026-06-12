// C# supports:
// 1. Compile-time polymorphism (Method Overloading)
// 2. Run-time polymorphism (Method Overriding)

// 1. Method Overloading (Compile-time)
// Same method name, different parameters.
 
class Calculator
{
    public Int32 Add(Int32 a, Int32 b)
    {
        return a + b;
    }

    public Double Add(Double a, Double b)
    {
        return a + b;
    }
}

// 2. Method Overriding (Run-time)

// Base class
class Animal
{
    public virtual void Speak()
    {
        Console.WriteLine("Animal is speaking");
    }
}

// Derived class
class Dog: Animal
{
    public override void Speak()
    {
        Console.WriteLine("Dog barks");
    }
}

class Cat: Animal
{
    public override void Speak()
    {
        Console.WriteLine("Cat meows");
    }
}

// Real-Time Example (C#)

// Payment processing system:

public abstract class Payment
{
    public abstract void Pay(Decimal amount);
}

public class CreditCardPayment: Payment
{
    public override void Pay(Decimal amount)
    {
        Console.WriteLine($"Paid {amount} using Credit Card");
    }
}

public class UPIPayment: Payment
{
    public override void Pay(Decimal amount)
    {
        Console.WriteLine($"Paid {amount} using UPI");
    }
}

public class Polymorphism
{
    public static void Run()
    {
        // The compiler decides which method to call.
        Calculator calc = new Calculator();
        Console.WriteLine(calc.Add(10, 20));
        Console.WriteLine(calc.Add(10.5, 20.5));

        // Even though the reference type is Animal, the actual object determines which method runs.
        Animal animal1 = new Dog();
        Animal animal2 = new Cat();
        animal1.Speak();
        animal2.Speak();

        // Correct - Target-Typed new (C# 9+)
        // Since the compiler already knows the type from the left side:
        List<Payment> payments = new()
        {
            new CreditCardPayment(),
            new UPIPayment()
        };
        // The compiler treat it as:
        // List<Payment> payments = new List<Payment>()
        // {
        //  new CreditCardPayment(),
        //  new UPIPayment()
        // };

        foreach (Payment payment in payments)
        {
            payment.Pay(1000);
        }
    }
}