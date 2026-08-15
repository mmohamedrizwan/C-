# Model Binding in ASP.NET & ASP.NET Core

**Model Binding** is the process of automatically mapping incoming HTTP request data (route values, query string, form data, headers, body) into action method parameters — without manually parsing the request.

This note focuses specifically on **Web API** (not MVC views).

---

## 1. ASP.NET Web API (`System.Web.Http` — .NET Framework)

### Namespace
```csharp
using System.Web.Http;
using System.Net.Http;
```

- `Request` is of type `HttpRequestMessage` (from `System.Net.Http`) - different from MVC's HttpRequestBase.
- Controllers inherit from `ApiController` (System.Web.Http).

### Binding sources
| Attribute | Source | Notes |
|---|---|---|
| `[FromUri]` | Query string / route data | Default for simple types |
| `[FromBody]` | Request body | Default for complex types; **only one `[FromBody]` param per action** |
| *(none)* | Simple types (`int`, `string`, `bool`) → from URI. Complex types → from body | Default convention |

### Example — simple types from URI
```csharp
// GET api/employee?id=5
public IHttpActionResult Get([FromUri] int id)
{
    return Ok(id);
}
```

### Example — complex type from body
```csharp
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
}
 
[HttpPost]
public IHttpActionResult Create([FromBody] Employee employee)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);
 
    // save employee
    return Ok(employee);
}
```

### Reading raw form data

`HttpRequestMessage` has no `.Form` property like MVC. You read it from the content asynchronously:
```csharp
using System.Net.Http.Formatting;
using System.Collections.Specialized;

[HttpPost]
public async Task<IHttpActionResult> Create()
{
    NameValueCollection form = await Request.Content.ReadAsFormDataAsync();
    String name = form["name"];
    return Ok(name);
}
```

### Validation
- Data Annotations (`[Required]`, `[Range]`, etc.) on the model.
- Must manually check `ModelState.Invalid` - **no automatic 400 response**.

---

## 2. ASP.NET Core Web API

### Namespace
```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
```

- `Request` is of type `HttpRequest` (from `Microsoft.AspNetCore.Http`).
- Controllers inherit from `ControllerBase` and are usually decorated with `[ApiController]`. 

### Binding sources
| Attribute | Source |
|---|---|
| `[FromRoute]` | Route data (`/api/employee/{id}`) |
| `[FromQuery]` | Query string (`?id=5`) |
| `[FromForm]` | Form fields (`multipart/form-data`, `x-www-form-urlencoded`) |
| `[FromBody]` | Request body (JSON) |
| `[FromHeader]` | HTTP headers |
| `[FromServices]` | Resolved via Dependency Injection |

### Default behaviour (no attribute)
- **Simple types** -> route data -> query string.
- **Complex types** -> request body (JSON), automatically, when using `[ApiController]`.

### Example
```csharp
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
}

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    [HttpPost("create")]
    public IActionResult Create([FromBody] Employee employee)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
 
        return Ok(employee);
    }
}
```

### Combining multiple sources in one action
```csharp
[HttpPut("{id}")]
public IActionResult Update(
    [FromRoute] int id,
    [FromBody] Employee employee,
    [FromHeader(Name = "Authorization")] string token)
{
    return Ok();
}
```

### Reading raw form data
```csharp
[HttpPost]
public IActionResult Create()
{
    if (!Request.HasFormContentType)
        return BadRequest("Not a form request");
 
    IFormCollection form = Request.Form;
    string name = form["Name"];
    return Ok(name);
}
```

### Validation
- Data Annotations on the model, checked via `ModelState.IsValid`.
- With `[ApiController]`, **invalid model state automatically returns HTTP 400** — no manual check required (though many devs check it anyway for custom error shapes).

---

## Key Differences: Web API (ASP.NET) vs Web API (ASP.NET Core)

| Feature | ASP.NET Web API | ASP.NET Core Web API |
|---|---|---|
| `Request` type | `HttpRequestMessage` | `HttpRequest` |
| Namespace | `System.Net.Http` | `Microsoft.AspNetCore.Http` |
| Base controller | `ApiController` | `ControllerBase` |
| Binding attributes | `[FromUri]`, `[FromBody]` | `[FromRoute]`, `[FromQuery]`, `[FromBody]`, `[FromForm]`, `[FromHeader]`, `[FromServices]` |
| Reading form data | `await Request.Content.ReadAsFormDataAsync()` → `NameValueCollection` | `Request.Form` → `IFormCollection` |
| Default binding for complex types | Body | Body |
| Default binding for simple types | URI (query/route) | Route → Query |
| Auto HTTP 400 on invalid model | ❌ No (manual `ModelState.IsValid` check) | ✅ Yes (with `[ApiController]`) |
| DI-based parameter binding | ❌ Not built-in | ✅ `[FromServices]` |
| Custom binder interface | `IModelBinder` (sync, `HttpActionContext`) | `IModelBinder` (async, `ModelBindingContext`) |

---

## Summary 
- **ASP.NET Web API**: Simple types default to URI (`[FromUri]`), complex types default to body (`[FromBody]`), no built-in form collection - read via `ReadAsFormDataAsync()`.
- **ASP.NET Core Web API**: richer set of binding attributes (`[FromRoute]`, `[FromQuery]`, `[FromForm]`, `[FromHeader]`, `[FromServices]`), unified pipeline, automatic model validation with `[ApiController]`.  