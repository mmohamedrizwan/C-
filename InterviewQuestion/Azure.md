# What is Azure App Service?

## Answer

Azure App Service is a **Platform as a Service (Paas)** offered by Microsoft Azure.

It is used to host:

- ASP.NET Core Web APIs
- Web applications
- Node.js applications
- Java applications
- Python applications

Azure manages the underlying infrastructure, operating system, patching, and platform maintenance.

## Real-Time Example

Suppose we have an ASP.NET Core Web API:

    Angular Application
            |
            v
    ASP.NET Core Web API
            |
            v
      Azure App Service
            |
            v
    PostgreSQL Database

The ASP.NET Core API can be deployed to Azure App Service.

## Important Features

- Easy deployment
- Automatic scaling
- Deployment slots
- HTTPS support
- Custom domains
- Application settings
- Environment variables
- Logging
- Monitoring
- CI/CD integration

## App Service Deployment Flow

    Developer
        |
        v
    Git Repository
        |
        v
    Azure DevOps Pipeline
        |
        +-- Restore
        |
        +-- Build
        |
        +-- Test
        |
        +-- Publish
        |
        v
    Azure App Service
        |
        v
    ASP.NET Core API

## Interview Answer

> Azure App Service is a PaaS service provided by Microsoft Azure that is used to host web applications and APIs. For a .NET application, we can deploy an ASP.NET Core Web API to App Service without managing the underlying server infrastructure. It also provides features such as scaling, deployment slots, HTTPS, logging, and CI/CD integration.

# What is Azure Virtual Machine?

## Answer

Azure Virtual Machine (VM) is an **Infrastructure as a Service (IaaS)*** offering in Microsoft Azure.

It allows us to create and manage a virtual server in the cloud.

We can install and configure:

- Windows or Linux operating systems
- .NET applications
- Web servers such as IIS
- Databases
- Other required software

Unlike Azure App Service, with a VM we have more control over the operating system and service configuration.

---

## Real-Time Example

Suppose we need to host an ASP.NET Core application and require complete control over the server.

We can create a Windows VM:

    User
      |
      v
    Internet
      |
      v
    Azure VM
      |
      +-- Windows Server
      |
      +-- IIS
      |
      +-- ASP.NET Core Application
      |
      v
    Database

---

## Real-Time Example

Suppose we need to host an ASP.NET Core application and require complete control over the server.

We can create a Windows VM:

    User
      |
      v
    Internet
      |
      v
    Azure VM
      |
      +-- Windows Server
      |
      +-- IIS
      |
      +-- ASP.NET Core Application
      |
      v
    Database

---

## Azure VM vs App Service

| Feature | Azure VM | Azure App Service |
|---|---|---|
| Service Type | IaaS | PaaS |
| OS Management | We manage | Azure manages |
| Server Control | High | Limited |
| OS Patching | We manage | Azure manages |
| Scaling | More configuration | Easier |
| Maintenance | Higher | Lower |
| Infrastructure Management | Required | Mostly managed by Azure |

---

## When Whould You Use an Azure VM?

A VM can be useful when:

- You need full OS-level access.
- You need to install custom software.
- You need specific server configurations.
- The application has legacy dependencies.
- You need more control over the infrastructure.
- The workload is not suitable for PaaS services.

---

## Example for a .NET Developer

Imagine an old .NET Framework application that requires:

- Windows Server
- IIS
- Specific Windows components
- Custom server configuration

We could host it on an Azure Windows VM:

    Azure VM
       |
       +-- Windows Server
       |
       +-- IIS
       |
       +-- .NET Framework Application
       |
       +-- Custom Configuration

---

## Advantages

### 1. Full Control

We have significant control over:

- Operating system
- Installed software
- Network configuration
- Server configuration

### 2. Supports Legacy Applications

Some applications require a specific operating system or software configuration.

### 3. Flexible Configuration

We can configure the VM according to the application's requirements.

---

## Disadvantages

- More infrastructure management
- OS patching is our responsibility
- More maintenance
- Scaling requires additional configuration
- Higher operational responsibility compared with PaaS

---

## Interview Answer

> Azure Virtual Machine is an IaaS service that provides a virtual server in Azure. It gives us more control over the operating system, installed software, and server configuration. Compared with Azure App Service, a VM requires more infrastructure management, but it is useful when we need OS-level control or need to support legacy applications.

# What is Azure Storage Account?

## Answer

An **Azure Storage Account** is a container that provides access to Azure Storage services.

It can be used to store different types of data such as:

- Images
- Videos
- Documents
- Files
- Logs
- Backup data
- Messages
- Structured NoSQL data

A storage account provides a unique namespace in Azure for accessing and managing storage data.

---

## Types of Azure Storage

An Azure Storage Account can provide different storage services:

### 1. Blob Storage

Used to store unstructured data such as:

- Images
- Videos
- PDFs
- Documents
- Backups

Example:

    Storage Account
          |
          v
       Container
          |
          +-- image1.jpg
          +-- image2.jpg
          +-- report.pdf

---

### 2. Azure Files

Provides managed file shares that can be accessed using standard file-sharing protocols.

Example:

    Application
         |
         v
    Azure Files
         |
         +-- Shared Documents
         +-- Reports
         +-- Files

---

### 3. Queue Storage

Used for storing messages that can be processed asynchronously.

Example:

    ASP.NET Core API
          |
          v
    Queue Storage
          |
          v
    Background Worker

The API can add a message to the queue, and another service can process it later.

---

### 4. Table Storage

Azure Table Storage is a NoSQL key-value store suitable for storing structured, non-relational data.

Example:

    Table
    --------------------------------
    PartitionKey | RowKey | Name
    --------------------------------
    Users        | 101    | John
    Users        | 102    | David
    --------------------------------

---

# Real-Time Example in a .NET Application

Suppose we have an employee management application.

The application allows users to upload profile pictures.

The architecture can be:

    Angular Application
           |
           v
    ASP.NET Core Web API
           |
           v
    Azure Blob Storage
           |
           +-- employee-101.jpg
           +-- employee-102.jpg
           +-- employee-103.jpg

The database can store the URL or identifier of the image rather than storing the actual image binary data.

---

# Storage Account Structure

A simplified Blob Storage structure looks like:

    Storage Account
          |
          v
      Container
          |
          v
         Blob

Example:

    mycompanystorage
          |
          +-- employee-images
          |       |
          |       +-- user101.jpg
          |       +-- user102.jpg
          |
          +-- documents
                  |
                  +-- report.pdf
                  +-- invoice.pdf

---

# Important Storage Account Features

## High Availability

Azure Storage provides options to replicate data so that it remains available if infrastructure failures occur.

## Scalability

Storage accounts can handle large amounts of data and traffic.

## Security

Storage can be secured using mechanisms such as:

- Microsoft Entra ID
- Managed Identity
- Shared Access Signatures (SAS)
- Storage account keys
- Network restrictions

## Encryption

Azure Storage provides encryption for data at rest.

---

# How Does a .NET Application Access Azure Storage?

A .NET application can use the Azure SDK to interact with Azure Storage.

For example, for Blob Storage, the application can use the Azure Storage Blobs SDK.

Conceptually:

    ASP.NET Core API
          |
          v
    Azure SDK
          |
          v
    Azure Blob Storage

The application can perform operations such as:

- Upload blob
- Download blob
- Delete blob
- Check whether a blob exists

---

# Example Use Case

Suppose a user uploads a PDF document.

The flow can be:

    Angular
       |
       | Upload PDF
       v
    ASP.NET Core API
       |
       | Upload
       v
    Azure Blob Storage
       |
       v
    PDF stored in container

The database might contain:

    DocumentId
    FileName
    BlobName
    UploadedBy
    UploadedDate

The actual PDF is stored in Blob Storage.

---

# Storage Account vs Blob Storage

This is an important interview question.

### Storage Account

A **Storage Account** is the overall Azure storage resource.

### Blob Storage

**Blob Storage** is one of the storage services provided through a Storage Account.

Think of it like:

    Storage Account
          |
          +-- Blob Storage
          +-- File Storage
          +-- Queue Storage
          +-- Table Storage

---

# Interview Answer

> An Azure Storage Account is a top-level Azure resource that provides access to storage services such as Blob Storage, Azure Files, Queue Storage, and Table Storage. In a .NET application, I can use Blob Storage to store files such as images and documents, while storing their metadata or references in the database.