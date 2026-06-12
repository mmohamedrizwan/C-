// How to create a console app using docker
// docker run --rm -v "${PWD}:/app" -w /app mcr.microsoft.com/dotnet/sdk:latest dotnet new console -n MyApp

// In older .NET versions, you needed this:
using System;

class Program 
{
    static void Main(String[] args) 
    {
        LINQ.Run();
    }
}

// Starting from .NET 6, Microsoft made console apps simpler:
// Console.WriteLine("Hello, World!");
// This is called top-level statements.

// What are top-level statements?
// They allow you to write code in Program.cs without a class and Main method.
// Behind the scenes, the compiler automatically generates:
//
// internal class Program 
// {
//      static void Main(String[] args) 
//      {
//          Console.WriteLine("Hello, World!");
//      }
// }

// How to run the application using docker 
// docker run --rm -v "${PWD}/LINQDemo:/app" -w /app mcr.microsoft.com/dotnet/sdk:latest dotnet run
// | Flag                                  | Meaning                                                     |
// | ------------------------------------- | ----------------------------------------------------------- |
// | `--rm`                                | Delete container after it finishes (no leftovers)           |
// | `-v "${PWD}/LINQDemo:/app"`           | Mount your local folder `LINQDemo` into container at `/app` |
// | `-w /app`                             | Set working directory to `/app`                             |
// | `mcr.microsoft.com/dotnet/sdk:latest` | Use .NET SDK image                                          |
// | `dotnet run`                          | Runs your C# project inside container                       |
