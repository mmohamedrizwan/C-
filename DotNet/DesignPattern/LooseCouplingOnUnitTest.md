# Improve Unit Testing

Interface make it easy to replace real dependencies with mock objects during testing.

## Real Service

```csharp
public class EmailNotification: INotification
{
    public void Send(string message)
    {
        Console.WriteLine("Email Sent");
    }
}
```

## Mock Service

```csharp
public class FakeNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine("Fake Notification");
    }
}
```

## Unit Test

```csharp
var orderService = new OrderService(new FakeNotification());

orderService.PlaceOrder();
```

**Explanation**

- During testing, you don't send a real email or SMS.
- Instead, you use a fake or mock implementation.
- This makes tests faster, more reliable, and independent of external systems.