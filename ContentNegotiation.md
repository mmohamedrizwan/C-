# Content Negotiation in ASP.NET Core Web API

## What is Content Negotiation?

Content Negotiation is the process by which ASP.NET Core Web API determines the format of the response (such as JSON, XML, etc.) based on the client's request.

The client specifies the desired response format using the **Accept** HTTP header, and the server selects the most appropriate formatter to generate the response.

---

## Why is Content Negotiation Needed?

Different clients may require data in different formats:

* Web Applications → JSON
* Mobile Applications → JSON
* Legacy Systems → XML
* Third-Party Integrations → XML or other formats

Content Negotiation allows the same API endpoint to serve multiple response formats without changing the API logic.

---

## Real-Time Example

### API Endpoint

```csharp
[HttpGet("{id}")]
public IActionResult GetEmployee(int id)
{
    var employee = new Employee
    {
        Id = id,
        Name = "Rizwan",
        Department = "IT"
    };

    return Ok(employee);
}
```

---

## Scenario 1: Client Requests JSON

### Request

```http
GET /api/employees/1
Accept: application/json
```

### Response

```json
{
  "id": 1,
  "name": "Rizwan",
  "department": "IT"
}
```

ASP.NET Core selects the JSON formatter and returns JSON data.

---

## Scenario 2: Client Requests XML

### Request

```http
GET /api/employees/1
Accept: application/xml
```

### Response

```xml
<Employee>
  <Id>1</Id>
  <Name>Rizwan</Name>
  <Department>IT</Department>
</Employee>
```

ASP.NET Core selects the XML formatter and returns XML data.

---

## How Content Negotiation Works

```text
Client Request
      |
      | Accept: application/json
      v
ASP.NET Core Pipeline
      |
      | Selects JSON Formatter
      v
JSON Response
```

### Steps

1. Client sends a request.
2. API reads the Accept header.
3. ASP.NET Core checks available formatters.
4. Appropriate formatter is selected.
5. Response is serialized and sent to the client.

---

## Default Behavior

By default, ASP.NET Core supports:

* JSON (System.Text.Json)

JSON is the default response format.

---

## Enable XML Support

### Program.cs

```csharp
builder.Services.AddControllers()
                .AddXmlSerializerFormatters();
```

Now the API can return both:

* JSON
* XML

---

## Accept Header Examples

### JSON

```http
Accept: application/json
```

### XML

```http
Accept: application/xml
```

### Any Format

```http
Accept: */*
```

In this case, ASP.NET Core typically returns JSON.

---

## Unsupported Format Example

### Request

```http
GET /api/employees/1
Accept: application/pdf
```

### Default Behavior

ASP.NET Core may ignore the unsupported format and return JSON.

---

## Return 406 Not Acceptable

To force the API to reject unsupported formats:

```csharp
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
});
```

### Response

```http
406 Not Acceptable
```

---

## Content Negotiation vs Content-Type

| Header       | Purpose                             |
| ------------ | ----------------------------------- |
| Accept       | Format client wants in response     |
| Content-Type | Format client is sending in request |

### Example

```http
POST /api/employees
Content-Type: application/json
Accept: application/xml
```

Meaning:

* Request body is JSON.
* Response should be XML.

---

## Common Interview Questions

### 1. What is Content Negotiation?

Content Negotiation is the process of selecting the best response format (JSON, XML, etc.) based on the client's Accept header.

---

### 2. Which header is used for Content Negotiation?

The **Accept** header.

Example:

```http
Accept: application/json
```

---

### 3. What is the default response format in ASP.NET Core?

JSON.

---

### 4. How do you enable XML responses?

```csharp
builder.Services.AddControllers()
                .AddXmlSerializerFormatters();
```

---

### 5. What happens if the requested format is not supported?

* JSON may be returned by default.
* Or HTTP 406 Not Acceptable can be returned if configured.

---

### 6. Difference Between Accept and Content-Type?

| Accept                    | Content-Type              |
| ------------------------- | ------------------------- |
| Desired response format   | Request body format       |
| Used by client            | Used by client/server     |
| Example: application/json | Example: application/json |

---

## Interview Answer (Short Version)

Content Negotiation is a feature in ASP.NET Core Web API that determines the format of the response returned to the client. It uses the Accept header sent by the client and selects the appropriate formatter such as JSON or XML. By default, ASP.NET Core returns JSON, and XML support can be enabled using AddXmlSerializerFormatters().

### One-Line Definition

**Content Negotiation is the process of selecting the response format (JSON/XML) based on the client's Accept header.**