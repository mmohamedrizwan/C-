# REST API vs Azure Service Bus in Microservices

## Overview

In a Microservices Architecture, services need to communicate with each other.

There are two common communication patterns:

1. REST API Communication (Synchronous)
2. Azure Service Bus Communication (Asynchronous)

Understanding when to use each approach is a common interview topic.

---

# 1. REST API Communication

## What is REST Communication?

REST communication is a synchronous communication model where one service directly calls another service and waits for a response.

```text
Order Service
      |
      | HTTP Request
      ↓
Customer Service
      |
      | HTTP Response
      ↓
Order Service
```

The caller cannot continue processing until a response is received.

---

## Real-Time Example

Suppose an Order Service needs customer information before creating an order.

### Flow

```text
Create Order
      ↓
Call Customer Service
      ↓
Receive Customer Details
      ↓
Continue Processing
```

---

## Customer Service API

```csharp
[HttpGet("{id}")]
public async Task<IActionResult> GetCustomer(int id)
{
    var customer = await _customerRepository.GetByIdAsync(id);

    return Ok(customer);
}
```

---

## Order Service Calling Customer Service

```csharp
public async Task<CustomerDto> GetCustomerAsync(int customerId)
{
    var response = await _httpClient.GetAsync(
        $"api/customers/{customerId}");

    response.EnsureSuccessStatusCode();

    return await response.Content
        .ReadFromJsonAsync<CustomerDto>();
}
```

---

## Advantages of REST

### Simple to Implement

Easy to understand and develop.

### Immediate Response

The caller gets a response instantly.

### Easy Debugging

Requests and responses are visible.

### Suitable For

* Login
* User Validation
* Product Search
* Customer Details
* Balance Check

---

## Disadvantages of REST

### Tight Coupling

If Customer Service is unavailable:

```text
Order Service
      ↓
Customer Service ❌
```

Order creation may fail.

---

### Scalability Issues

Large numbers of requests can overload services.

---

### Dependency Problems

The calling service depends on the availability of the target service.

---

# 2. Azure Service Bus Communication

## What is Azure Service Bus?

Azure Service Bus is a message broker that allows services to communicate asynchronously.

Instead of directly calling another service, a service sends a message to a queue or topic.

Other services consume that message later.

---

## Architecture

```text
Order Service
      ↓
Azure Service Bus
      ↓
Inventory Service
Email Service
Billing Service
Analytics Service
```

---

## Real-Time Example

When an order is placed:

* Update Inventory
* Send Email
* Generate Invoice
* Update Analytics

Instead of calling all services directly, publish an event.

---

# Publisher Example

## Create Message

```csharp
var message = new ServiceBusMessage(
    JsonSerializer.Serialize(new
    {
        OrderId = 101,
        CustomerId = 1,
        Amount = 500
    }));
```

---

## Send Message

```csharp
await sender.SendMessageAsync(message);
```

---

# Consumer Example

## Inventory Service

```csharp
processor.ProcessMessageAsync += async args =>
{
    var body = args.Message.Body.ToString();

    Console.WriteLine(body);

    await args.CompleteMessageAsync(args.Message);
};
```

---

# Service Bus Flow

```text
Order Created
      ↓
Publish Event
      ↓
Azure Service Bus
      ↓
Inventory Service
      ↓
Update Inventory
```

---

# What Happens If Inventory Service Is Down?

```text
Order Service
      ↓
Azure Service Bus
      ↓
Inventory Service ❌
```

Message remains in the queue.

When Inventory Service comes back online:

```text
Azure Service Bus
      ↓
Inventory Service
```

The message is processed automatically.

No data is lost.

---

# Real-World E-Commerce Example

Suppose a user places an order.

The system must:

* Update Inventory
* Send Confirmation Email
* Generate Invoice
* Send SMS
* Update Analytics

---

## Bad Approach (REST Everywhere)

```text
Order Service
      ↓
Inventory API
      ↓
Email API
      ↓
Invoice API
      ↓
SMS API
      ↓
Analytics API
```

Problem:

If one service fails:

```text
Entire Process Fails
```

---

## Better Approach (Event-Driven)

```text
Order Service
      ↓
Azure Service Bus
      ↓
Inventory Service

Email Service

Invoice Service

SMS Service

Analytics Service
```

Services work independently.

---

# REST vs Azure Service Bus

| Feature             | REST API              | Azure Service Bus |
| ------------------- | --------------------- | ----------------- |
| Communication Style | Synchronous           | Asynchronous      |
| Wait for Response   | Yes                   | No                |
| Coupling            | Tight                 | Loose             |
| Reliability         | Lower                 | Higher            |
| Scalability         | Moderate              | High              |
| Fault Tolerance     | Limited               | Excellent         |
| Real-Time Response  | Immediate             | Delayed           |
| Complexity          | Simple                | Moderate          |
| Retry Mechanism     | Custom Implementation | Built-In          |
| Message Persistence | No                    | Yes               |

---

# When to Use REST

Use REST when an immediate response is required.

Examples:

## Login

```text
User Login
      ↓
Authentication Service
      ↓
Return JWT Token
```

---

## Product Details

```text
Product Service
      ↓
Return Product Information
```

---

## Customer Validation

```text
Order Service
      ↓
Customer Service
      ↓
Valid Customer?
```

---

# When to Use Azure Service Bus

Use Azure Service Bus when the operation can happen later.

Examples:

## Send Email

```text
Order Created
      ↓
Email Queue
      ↓
Email Service
```

---

## Generate Invoice

```text
Order Created
      ↓
Invoice Queue
      ↓
Invoice Service
```

---

## Inventory Update

```text
Order Created
      ↓
Inventory Queue
      ↓
Inventory Service
```

---

## Analytics Processing

```text
Order Created
      ↓
Analytics Queue
      ↓
Analytics Service
```

---

# Hybrid Architecture (Most Common)

Modern enterprise applications use both REST and Service Bus.

Example:

```text
Order Service
      ↓ REST
Customer Service

Order Service
      ↓ Service Bus
Email Service

Order Service
      ↓ Service Bus
Inventory Service

Order Service
      ↓ Service Bus
Analytics Service
```

---

# Why Hybrid Architecture?

## Customer Validation

Need immediate response.

Use:

```text
REST API
```

---

## Email Sending

User should not wait.

Use:

```text
Azure Service Bus
```

---

## Inventory Update

Can be processed asynchronously.

Use:

```text
Azure Service Bus
```

---

# Interview Scenario

## Question

Which is better for Microservices: REST API or Azure Service Bus?

## Answer

Neither is universally better.

REST API is suitable for synchronous request-response communication where an immediate response is required.

Azure Service Bus is suitable for asynchronous event-driven communication where services should remain loosely coupled and resilient to failures.

In real-world microservice architectures, both are typically used together.

For example:

* Customer validation → REST API
* Email notifications → Azure Service Bus
* Inventory updates → Azure Service Bus
* Analytics processing → Azure Service Bus

This combination provides both responsiveness and scalability.

---

# Key Interview Points

1. REST is synchronous communication.
2. Azure Service Bus is asynchronous communication.
3. REST requires immediate response.
4. Service Bus allows eventual processing.
5. REST creates tighter coupling.
6. Service Bus creates loose coupling.
7. Service Bus provides better fault tolerance.
8. Messages are persisted in queues.
9. Failed services can process messages later.
10. Most enterprise microservice applications use both approaches together.

---

# One-Line Interview Definition

> REST API is used for synchronous request-response communication, whereas Azure Service Bus is used for asynchronous event-driven communication that improves scalability, reliability, and loose coupling in microservice architectures.