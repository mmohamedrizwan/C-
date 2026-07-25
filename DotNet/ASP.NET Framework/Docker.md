# Can we use Docker for ASP.NET Web Application (.NET Framework)?

## Short Answer

**Yes, but with a limitation.**

You **can use Docker** to **build, run, and deploy** an ASP.NET Framework application, but you **cannot use Docker to create a new ASP.NET Framework project**.

---

# Why?

ASP.NET Framework project templates are part of **Visual Studio**, not the **.NET CLI (`dotnet`)**.

Therefore, commands like:

```bash
dotnet new webapi
```

or

```bash
dotnet new mvc
```

work only for **ASP.NET Core / .NET 5+** and **not** for **ASP.NET Framework 4.x**.

---

# What Docker Can Do

Docker can be used to:

- Build an existing ASP.NET Framework project using **MSBuild**
- Restore NuGet packages
- Run the application inside a **Windows container**
- Deploy the application

Example:

```cmd
docker run --rm ^
-v %cd%:C:\src ^
-w C:\src ^
mcr.microsoft.com/dotnet/framework/sdk:4.8 ^
msbuild MyApp.sln /p:Configuration=Release
```