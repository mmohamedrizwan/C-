# `UseRouting()` in ASP.NET Core

## What is `UseRouting()`?

`UseRouting()` is a middleware that **matches an incoming HTTP request to an endpoint** (such as a controller action, minimal API, or Razor page).

It **does not execute the endpoint**. It only determines **which endpoint should handle the request**.

---

# Syntax

```csharp
app.UseRouting();
```

---

# Example

```csharp
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
```

Suppose you have the following controller:

```csharp
[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetProducts()
    {
        return Ok("Products List");
    }
}
```

If the request is:

```
GET /api/products
```

`UseRouting()` finds the matching endpoint:

```
ProductsController -> GetProducts()
```

It stores this information so that the remaining middleware (such as authentication and authorization) knows which endpoint is being accessed.

---

# Request Flow

```
Client Request
      │
      ▼
UseRouting()
      │
      │ Finds matching endpoint
      ▼
UseAuthentication()
      │
      ▼
UseAuthorization()
      │
      ▼
MapControllers()
      │ Executes the matched controller action
      ▼
Response
```

---

# Why is `UseRouting()` Needed?

Without routing, ASP.NET Core does not know which controller or action should handle the incoming request.

For example:

```
GET /api/products
```

`UseRouting()` determines:

```
URL:
GET /api/products

↓

Controller:
ProductsController

↓

Action:
GetProducts()
```

---

# Why should it come before Authentication and Authorization?`

Authentication and authorization often depend on the endpoint being accessed.

Example:

```csharp
[Authorize]
[HttpGet]
public IActionResult GetProducts()
```

`UseRouting()` first identifies that the request is for `GetProducts()`.

Then:

- `UseAuthentication()` authenticates the user.
- `UseAuthorization()` checks whether the user is allowed to access that endpoint.

Without `UseRouting()`, authorization doesn't know which endpoint's metadata (such as `[Authorize]`) to evaluate.

---

# What happens without `UseRouting()`?

```csharp
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
```

The framework cannot correctly match requests to endpoint, so controller routes won't work as expected.

---

# Difference Between `UseRouting()` and `MapControllers()`

| `UseRouting()` | `MapControllers()` |
|----------------|--------------------|
| Matches the incoming request to an endpoint. | Executes the matched controller action. |
| Does **not** execute the endpoint. | Invokes the controller action and returns the response. |
| Runs before authentication and authorization. | Runs after authentication and authorization. |

---

# Interview Questions

### Q1. What is the purpose of `UseRouting()`?

**Answer:**  
`UseRouting()` matches an incoming request to the appropriate endpoint (controller, minimal API, or Razor page). It only selects the endpoint; it does not execute it.

### Q2. Why should `UseRouting()` come before `UseAuthorization()`?

**Answer:**  
`UseAuthorization()` needs to know which endpoint is being accessed so it can evaluate endpoint metadata, such as the `[Authorize]` attribute. `UseRouting()` identifies the endpoint before authorization runs.

### Q3. Does `UseRouting()` execute the controller action?

**Answer:**  
No. It only identifies the matching endpoint. The endpoint is executed later by `MapControllers()` (or another endpoint-mapping method).