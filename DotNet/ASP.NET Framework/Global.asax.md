# Global.asax.cs - What, Why, Purpose

## What it is

`Global.asax.cs` is the **code-behind file for `Global.asax`**, a special file in ASP.NET (Framework, `System.Web`-based) applications. It's the **application file** - entry point where you handle application-level events, not request-specific ones.

It sits at the root of the project, and the class inside it inherits from `HttpApplication`:

```csharp
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
 
public class MvcApplication : System.Web.HttpApplication
{
    protected void Application_Start()
    {
        AreaRegistration.RegisterAllAreas();
        GlobalConfiguration.Configure(WebApiConfig.Register);
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);
    }
}
```

---

## Why it exists

Before ASP.NET Core (which uses `Startup.cs` / `Program.cs`), there was no unified pipeline configuration model. `Global.asax.cs` was the place to hook into the application/request lifecycle - things you want to run once at startup, or on every request/session, regardless of which page or controller handles it.

---

## Purpose / What goes in it

| Event | When it fires | Common use |
|---|---|---|
| `Application_Start()` | Once, when the app starts (first request or app pool starts) | Route registration, DI/IoC setup, Web API config, bundling/minification, DB migrations |
| `Application_End()` | Once, when the app shuts down | Cleanup, logging shutdown |
| `Application_Error()` | Any unhandled exception | Global error logging/handling |
| `Session_Start()` | New user session begins | Initialize session variables |
| `Session_End()` | Session times out/ends | Cleanup session resources |
| `Application_BeginRequest()` | Start of every single request | URL rewriting, custom headers, logging |
| `Application_EndRequest()` | End of every single request | Logging, cleanup |
| `Application_AuthenticateRequest()` | During auth stage of pipeline | Custom authentication logic |

---

## Typical registrations done here (MVC5/Web API)
- `RouteConfig.RegisterRoutes()` — MVC routing
- `WebApiConfig.Register()` — Web API routes & config
- `FilterConfig.RegisterGlobalFilters()` — global action filters
- `BundleConfig.RegisterBundles()` — CSS/JS bundling
- `AreaRegistration.RegisterAllAreas()` — MVC areas

---

### ASP.NET Core equivalent

There is **no `Global.asax`** in ASP.NET Core. Its responsibilities are split/replaced by:

| ASP.NET (Global.asax.cs) | ASP.NET Core equivalent |
|---|---|
| `Application_Start()` | `Program.cs` (`WebApplication.CreateBuilder`, middleware setup) / `Startup.cs` `ConfigureServices()` & `Configure()` |
| Route registration | `app.MapControllers()`, `app.UseRouting()` / `[Route]` attributes |
| `Application_Error()` | Exception-handling middleware (`app.UseExceptionHandler(...)`) |
| `Application_BeginRequest`/`EndRequest` | Custom **middleware** components |
| DI/IoC setup | `builder.Services.AddScoped<...>()` etc. in `Program.cs` |