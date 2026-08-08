# Routing in ASP.NET Core Web API

## 1. What is Routing?

Routing in ASP.NET Core Web API is the process of matching an incoming HTTP request to a specific endpoint or controller action.

Routing determines:

- Which controller should handle the request
- which action method should execute
- which HTTP method is supported
- How route parameters are passed
- Whether route parameters satisfy specific constraints

For example:

```text
GET /api/products/10
```

ASP.NET Core routing identifies the appropriate action:

```csharp
[HttpGet("{id:int}")]
public IActionResult GetById(int id)
{
    return Ok(id);
}
```

The request flow is:

```text
GET /api/products/10
        |
        v
    Routing System
        |
        v
ProductsController
        |
        v
    GetById(10)
```

---

# 2. Types of Routing

ASP.NET Core supports two major routing approaches:

1. Attribute Routing
2. Conventional Routing

For modern ASP.NET Core Web API, **attribute routing is commonly used**.

---

# 3. Attribute Routing

Attribute routing defines routes directly using attributes.

Common attributes are:

```csharp
[Route]
[HttpGet]
[HttpPost]
[HttpPut]
[HttpPatch]
[HttpDelete]
```

Example:

```csharp
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/products")]
public class ProductsController: ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok();
    }
}
```

The endpoint is:

```text
GET /api/products
```

# 4. Program.cs

For a controller-based Web API, `Program.cs` can contain:

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Register controller services
builder.Services.AddControllers();

var app = builder.Build();

// Redirect HTTP requests to HTTPS
app.UseHttpsRedirection();

// Authorization middleware
app.UseAuthorization();

// Map controller endpoints
app.MapControllers();

app.Run();
```

## Important Line: AddControllers()

```csharp
builder.Services.AddControllers();
```

This registers the controller-related services required by ASP.NET Core MVC/Web API.

## Important Line: MapControllers()

```csharp
app.MapControllers();
```

This maps controller actions that use attribute routing.

For example:

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok();
    }
}
```

The route is:

```text
GET /api/products
```

---

# 5. Basic Controller Example

```csharp
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok("All products");
    }
}
```

The URL is:

```text
GET /api/products
```

---

# 6. Understanding [controller]

Consider:

```csharp
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
}
```

ASP.NET Core replaces:

```text
[controller]
```

with the controller name without the `Controller` suffix.

Therefore:

```text
ProductsController
```

becomes:

```text
products
```

So:

```csharp
[Route("api/[controller]")]
```

becomes:

```text
/api/products
```

Similarly:

```csharp
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
}
```

becomes:

```text
/api/customers
```

---

# 7. Controller-Level Route

A route can be defined at the controller level.

```csharp
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok();
    }
}
```

The route is:

```text
GET /api/products
```

---

# 8. Action-Level Route

A route can also be defined on an action.

```csharp
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    [HttpGet("details")]
    public IActionResult GetDetails()
    {
        return Ok();
    }
}
```

The final URL is:

```text
GET /api/products/details
```

The controller route:

```text
/api/products
```

and action route:

```text
/details
```

are combined.

---

# 9. HTTP Method Routing

HTTP method attributes specify which HTTP method an action handles.

## GET

```csharp
[HttpGet]
public IActionResult GetAll()
{
    return Ok();
}
```

URL:

```text
GET /api/products
```

## POST

```csharp
[HttpPost]
public IActionResult Create()
{
    return Ok();
}
```

URL:

```text
POST /api/products
```

## PUT

```csharp
[HttpPut("{id}")]
public IActionResult Update(int id)
{
    return Ok();
}
```

URL:

```text
PUT /api/products/10
```

## PATCH

```csharp
[HttpPatch("{id}")]
public IActionResult Patch(int id)
{
    return Ok();
}
```

URL:

```text
PATCH /api/products/10
```

## DELETE

```csharp
[HttpDelete("{id}")]
public IActionResult Delete(int id)
{
    return Ok();
}
```

URL:

```text
DELETE /api/products/10
```

---

# 10. Complete HTTP Method Example

```csharp
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Create()
    {
        return Ok();
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id)
    {
        return Ok();
    }

    [HttpPatch("{id:int}")]
    public IActionResult Patch(int id)
    {
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        return Ok();
    }
}
```

The endpoints are:

| HTTP Method | URL | Action |
|---|---|---|
| GET | `/api/products` | `GetAll()` |
| POST | `/api/products` | `Create()` |
| PUT | `/api/products/10` | `Update()` |
| PATCH | `/api/products/10` | `Patch()` |
| DELETE | `/api/products/10` | `Delete()` |

---

# 11. Route Parameters

A route parameter allows a value to be passed through the URL.

```csharp
[HttpGet("{id}")]
public IActionResult GetById(int id)
{
    return Ok(id);
}
```

Request:

```text
GET /api/products/10
```

The value:

```text
id = 10
```

is passed to:

```csharp
int id
```

---

# 12. Route Parameter with [FromRoute]

You can explicitly specify that a value comes from the route.

```csharp
[HttpGet("{id}")]
public IActionResult GetById([FromRoute] int id)
{
    return Ok(id);
}
```

Request:

```text
GET /api/products/10
```

Here:

```text
Route parameter
       |
       v
     {id}
       |
       v
[FromRoute] int id
```

---

# 13. Multiple Route Parameters

A route can contain multiple parameters.

```csharp
[HttpGet("categories/{categoryId}/products/{productId}")]
public IActionResult GetProduct(
    int categoryId,
    int productId)
{
    return Ok();
}
```

Request:

```text
GET /api/products/categories/5/products/20
```

Values:

```text
categoryId = 5
productId  = 20
```

---

# 14. Multiple Route Parameters with Constraints

```csharp
[HttpGet("categories/{categoryId:int}/products/{productId:int}")]
public IActionResult GetProduct(
    int categoryId,
    int productId)
{
    return Ok();
}
```

Valid request:

```text
GET /api/products/categories/5/products/20
```

Invalid example:

```text
GET /api/products/categories/abc/products/xyz
```

because both parameters must be integers.

---

# 15. Optional Route Parameters

A route parameter can be optional by using `?`.

```csharp
[HttpGet("{id?}")]
public IActionResult Get(int? id)
{
    return Ok(id);
}
```

Both requests can match:

```text
GET /api/products
```

and:

```text
GET /api/products/10
```

---

# 16. Route Constraints

Route constraints restrict which values can match a route.

Example:

```csharp
[HttpGet("{id:int}")]
public IActionResult GetById(int id)
{
    return Ok(id);
}
```

This matches:

```text
/api/products/10
```

but does not match:

```text
/api/products/abc
```

because `abc` is not an integer.

---

# 17. Common Route Constraints

| Constraint | Example | Description |
|---|---|---|
| `int` | `{id:int}` | Integer |
| `long` | `{id:long}` | Long integer |
| `guid` | `{id:guid}` | GUID |
| `bool` | `{value:bool}` | Boolean |
| `decimal` | `{price:decimal}` | Decimal |
| `double` | `{value:double}` | Double |
| `float` | `{value:float}` | Float |
| `min` | `{id:min(1)}` | Minimum value |
| `max` | `{id:max(100)}` | Maximum value |
| `range` | `{id:range(1,100)}` | Value range |
| `length` | `{name:length(3,20)}` | String length |
| `minlength` | `{name:minlength(3)}` | Minimum length |
| `maxlength` | `{name:maxlength(20)}` | Maximum length |

---

# 18. Integer Constraint

```csharp
[HttpGet("{id:int}")]
public IActionResult GetById(int id)
{
    return Ok(id);
}
```

Valid:

```text
GET /api/products/10
```

Invalid:

```text
GET /api/products/abc
```

---

# 19. GUID Constraint

```csharp
[HttpGet("{id:guid}")]
public IActionResult GetById(Guid id)
{
    return Ok(id);
}
```

Example:

```text
GET /api/products/7f8c8a35-8d1a-4e6f-9d5d-123456789abc
```

---

# 20. Range Constraint

```csharp
[HttpGet("{id:range(1,100)}")]
public IActionResult GetById(int id)
{
    return Ok(id);
}
```

Valid:

```text
/api/products/50
```

Invalid:

```text
/api/products/150
```

---

# 21. Combining Route Constraints

Multiple constraints can be combined.

```csharp
[HttpGet("{id:int:min(1):max(100)}")]
public IActionResult GetById(int id)
{
    return Ok(id);
}
```

This allows values from:

```text
1
```

through:

```text
100
```

---

# 22. Query String

A query string is different from a route parameter.

Example:

```text
GET /api/products?name=laptop
```

The query parameter is:

```text
name=laptop
```

Controller:

```csharp
[HttpGet]
public IActionResult Search(string name)
{
    return Ok(name);
}
```

---

# 23. [FromQuery]

You can explicitly specify that a parameter comes from the query string.

```csharp
[HttpGet("search")]
public IActionResult Search([FromQuery] string name)
{
    return Ok(name);
}
```

Request:

```text
GET /api/products/search?name=laptop
```

Value:

```text
name = laptop
```

---

# 24. Multiple Query Parameters

```csharp
[HttpGet("search")]
public IActionResult Search(
    [FromQuery] string name,
    [FromQuery] decimal minPrice,
    [FromQuery] decimal maxPrice)
{
    return Ok();
}
```

Request:

```text
GET /api/products/search?name=laptop&minPrice=1000&maxPrice=50000
```

Values:

```text
name     = laptop
minPrice = 1000
maxPrice = 50000
```

---