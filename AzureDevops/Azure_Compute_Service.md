# Azure Compute Services

Azure Compute Services provide computing resources to run applications, virtual machines, containers and serverless workloads in the cloud.

## Azure Compute Services

| Service                                     | Purpose                                              | Best For      |
|---------------------------------------------|------------------------------------------------------|---------------|
| **Azure Virtual Machines (VMs)**            | Provides Windows/Linux virtual servers               | Applications requiring full control over the operating system |
| **Azure Virtual Machine Scale Sets (VMSS)** | Automatically scales a group of virtual machines     | High-availability and scalable applications |
| **Azure App Service**                       | Fully managed platform for hosting web apps and APIs | Web applications and REST APIs |
| **Azure Functions**                         | Serverless, event-driven compute service             | Background jobs, automation, and event-driven applications |
| **Azure Kubernetes Service (AKS)**          | Managed Kubernetes service                           | Running containerized and microservices-based applications |
| **Azure Container Instances (ACI)**         | Runs containers without managing servers             | Short-lived workloads and simple container deployments |
| **Azure Container Apps**                    | Serverless platform for containerized applications   | Cloud-native applications with automatic scaling |
| **Azure Batch**                             | High-performance computing service                   | Large-scale parallel and batch processing jobs |
| **Azure Service Fabric**                    | Platform for building distributed applications       | Enterprise and microservices applications |

---

## When to Use Each Service

### Azure Virtual Machines (VMs)
- Full control over the operating system.
- Support Windows and Linux.
- Best for legacy applications and custom software.

### Azure Virtual Machine Scale Sets (VMSS)
- Automatically increases or decreases the number of VMs.
- Support load balancing and high availability.
- Ideal for applications with varying traffic.

### Azure App Service
- Fully managed Platform as a Service (PaaS).
- Supports .NET, Java, Node.js, Python, PHP and more.
- No need to manage servers or operating systems.

### Azure Functions
- Serverless compute service.
- Runs code only when triggered.
- Pay only for execution time.
- Ideal for automation and event-driven workloads.

### Azure Kubernetes Service (AKS)
- Managed Kubernetes platform.
- Simplifies deployment and management of containers.
- Best for microservices architectures.

### Azure Container Instances (ACI)
- Run Docker containers instantly.
- No virtual machines or kubernetes cluster required.
- Suitable for testing, batch jobs, and simple workloads.

### Azure Container Apps
- Serverless container platform.
- Automatic scaling based on demand.
- Support microservices and APIs.

### Azure Batch
- Executes large numbers of compute-intensive tasks.
- Automatically manages virtual machines.
- Used for simulations, rendering, and data processing.

### Azure Service Fabric
- Platform for building scalable distributed systems.
- Supports both containes and traditional applications.
- Used for enterprise-grade microservices.

---

## Azure Compute Service Selection

| Requirement                       | Recommended Service            |
|-----------------------------------|--------------------------------|
| Full control over OS              | Azure Virtual Machines         |
| Automatically scale VMs           | Azure VM Scale Sets            |
| Host web applications             | Azure App Service              |
| Run serverless code               | Azure Functions                |
| Deploy Kubernetes containers      | Azure Kubernetes Service (AKS) |
| Run a single container            | Azure Container Instances      |
| Run scalable container apps       | Azure Container Apps           |
| High-performance batch processing | Azure Batch                    |
| Enterprise microservices          | Azure Service Fabric           |

---

## Summary

Azure Compute Services allow you to run applications in different ways based on your needs:

- **Virtual Machines** – Complete control over the operating system.
- **App Service** – Host web apps without managing infrastructure.
- **Azure Functions** – Execute event-driven serverless code.
- **AKS** – Deploy and manage Kubernetes clusters.
- **Container Apps** – Serverless platform for containerized applications.
- **Container Instances** – Run containers quickly without orchestration.
- **VM Scale Sets** – Automatically scale virtual machines.
- **Azure Batch** – Process large-scale compute jobs.
- **Service Fabric** – Build highly scalable distributed applications.