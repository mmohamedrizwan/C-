# ASP.NET Core Default Middleware Pipeline

In ASP.NET Core, the request pipeline is built using **middleware**. Middleware components are executed in the order they are added to the pipeline.

## Default Middlware Order

```csharp
var app = builder.Build();

// Error handling 
app.UseExceptionHandler("/error");
app.UseHsts();

// Redirect HTTP to HTTPS
app.UseHttpsRedirection();

// Serve static files
app.UseStaticFiles();

// Enable routing
app.UseRouting();

// Authenticate the user
app.UseAuthentication();

// Authorize the user
app.UseAuthorization();

// Map controller endpoints
app.MapControllers();

// Start the application
app.Run();
```

---

# Middleware Execution Flow

```
Incoming Request
        │
        ▼
Exception Handling
       │
       ▼
HTTPS Redirection
       │
       ▼
Static Files
       │
       ▼
Routing
       │
       ▼
Authentication
       │
       ▼
Authorization
       │
       ▼
Controller / Endpoint
       │
       ▼
Outgoing Response
```

---

# Middleware Description

| Middleware | Purpose |
|------------|---------|
| `UseExceptionHandler()` | Handles unhandled exceptions and returns an error response. |
| `UseHsts()` | Adds the HTTP Strict Transport Security (HSTS) header in production. |
| `UseHttpsRedirection()` | Redirects HTTP requests to HTTPS. |
| `UseStaticFiles()` | Serves static files such as CSS, JavaScript, and images. |
| `UseRouting()` | Matches the incoming request to an endpoint. |
| `UseAuthentication()` | Authenticates the current user (JWT, Cookies, OAuth, etc.). |
| `UseAuthorization()` | Checks whether the authenticated user has permission to access the resource. |
| `MapControllers()` | Maps HTTP requests to controller actions. |
| `Run()` | Starts the application and terminates the middleware pipeline. |

--- 

# Important Interview Points

- Middleware executes **in the order it is registered**.
- The **response travels back in reverse order** through the middleware.
- `UseRouting()` **must be called before** `UseAuthentication()` and `UseAuthorization()`.
- `UseAuthentication()` **must be called before** `UseAuthorization()`.
- `MapControllers()` maps the request to the appropriate controller action.
- `Run()` is the terminal middleware that starts processing incoming requests.

---

# Interview Question

### Q: Why should `UseAuthentication()` come before `UseAuthorization()`?

**Answer:**

- `UseAuthentication()` validates the user's identity and creates the `User` object.
- `UseAuthorization()` checks whether the authenticated user has permission to access the requested resource.
- If `UseAuthorization()` is executed first, the user has not yet been authenticated, so authorization will fail.