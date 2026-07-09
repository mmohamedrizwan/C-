# Azure Functions

**Azure Functions** is a **serverless compute service** provided by
Microsoft Azure that lets you run small pieces of code (called
**functions**) without managing servers or infrastructure.

Instead of creating and maintaining an entire application server, you
simply write a function that performs a specific task, and Azure runs it
whenever it is triggered.

## How Azure Functions Work

A function consists of:

-   **Trigger** -- The event that starts the function.
-   **Code** -- Your business logic.
-   **Bindings** -- Easy connections to Azure services for input and
    output.


## Azure Functions vs Traditional Web Apps

  Azure Functions                        Traditional Web App
  -------------------------------------- --------------------------------
  Event-driven                           Continuously running
  No server management                   Server management required
  Automatic scaling                      Scaling must be configured
  Pay per execution (Consumption plan)   Pay for allocated resources
  Best for small event-driven tasks      Best for full web applications

## Summary

Azure Functions is ideal for running code in response to events without
managing servers. It is commonly used for APIs, automation,
integrations, and background processing in cloud-native applications.