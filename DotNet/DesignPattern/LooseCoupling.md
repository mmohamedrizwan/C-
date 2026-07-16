# Achieve Loose Coupling

Loose coupling means one class **does not depend on a specific implementation**.

## Tight Coupling (Not Recommended)

```csharp
public class OrderService 
{
    private EmailNotification notification = new EmailNotification();

    public void PlaceOrder()
    {
        notification.Send("Order Placed");
    }
}
```

Problem:

- `OrderService` works only with `EmailNotification`.
- If you want SMS or WhatsApp notifications, you must modify `OrderService`.

---

## Loose Coupling (Recommended)

```csharp
public interface INotification
{
    void Send(String message);
}

public class EmailNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine("Email Sent");
    }
}

public class SmsNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine("SMS Sent");
    }
}

public class OrderService
{
    private readonly INotification _notification;

    public OrderService(INotification notification)
    {
        _notification = notification;
    }

    public void PlaceOrder()
    {
        _notification.Send("Order Placed");
    }
}
```

Now you can use either implementation without changing `OrderService`.

```csharp
OrderService order1 = new OrderService(new EmailNotification());
OrderService order2 = new OrderService(new SmsNotification());
```

**Benefit**

- Easier to extend.
- Easier to maintain.
- Follows the Dependency Inversion Principle (DIP).