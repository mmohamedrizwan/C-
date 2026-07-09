# Azure Virtual Network (VNet)

## What is Azure Virtual Network?

**Azure Virtual Network (VNet)** is the fundamental networking service in Microsoft Azure. It allows Azure resources, such as Virtual Machines, databases, and applications, to securely communicate with 
each other, the internet, and on-premises networks.

---

## Key Features

### 1. Network Isolation
- Creates a private network in Azure.
- Define your own IP address range using CIDR notation (e.g., `10.0.0.0/16`).

### 2. Subnets
- Divide a VNet into multiple logical networks called **subnets**.
- Example:
  - Frontend Subnet
  - Application Subnet
  - Database Subnet

### 3. Internet Connectivity
- Azure resources can communicate with the internet using Public IP address or Azure-managed outbound connectivity.

### 4. Secure Communication
- Use **Network Security Groups (NSGs)** to control inbound and outbound traffic.
- Use **Application Security Groups (ASGs)** for application-level security.

### 5. Hybrid Connectivity
Connect your on-premises network to Azure using:
- VPN Gateway
- ExpressRoute

### 6. VNet Peering
- Connect multiple VNets using Azure's private backbone network.
- Supports communication across Azure regions.

In one sentence: A VNet is the network, while VNet Peering is the link that connects two networks.

### 7. Private Access to Azure Services

**Private Access to Azure Services** means that Azure resources communicate with Azure services **over Microsoft's private network instead of the public internet**.

#### Example

Suppose you have:

- An Azure Virtual Machine (VM)
- An Azure Storage Account

There are two ways the VM can access the storage account.

#### 1. Public Access (Over the Internet)

```text
Azure VM
    │
    ▼
Public Internet
    │
    ▼
Azure Storage
```

- Use the storage account's public endpoint.
- Traffic travels over the public internet (encrypted using HTTPS).
- Public network access must be enabled.

#### 2. Private Access

```text
Azure VM
    │
    ▼
Azure Virtual Network (VNet)
    │
    ▼
Private Endpoint
    │
    ▼
Azure Storage
```

- Traffic stays within Microsoft's private Azure network.
- No public internet is allowed.
- The storage account can disabled public network access.
- Provides a more secure connection.

#### Azure Features for Private Access

- **Private Endpoint (Azure Private Link)** – Assigns a private IP address to an Azure service within your Virtual Network.
- **Service Endpoints** – Extends your Virtual Network identity to Azure services while using the service's public endpoint.
- **Virtual Network (VNet)** – Provides a private network for Azure resources.

#### Why Do We Need Service Endpoint and Private Endpoint?

A common question is:

> **If Virtual Machines can communicate using a Virtual Network (VNet), why do we need Service Endpoints and Private Endpoints?**

The answer depends on **what you're communicating with**.

#### Case 1: VM to VM Communication

Suppose you have two Virtual Machines in the same VNet.

```text
VNet
│
├── VM1 (10.0.1.4)
│
└── VM2 (10.0.2.5)
```

- Both VMs are inside the same Virtual Network.
- They communicate using their **private IP addresses**.
- No additional configuration is required.

**VNet alone is sufficient.**

✅ VNet Required  
❌ Service Endpoint Not Required  
❌ Private Endpoint Not Required

#### Case 2: VM to Azure Storage

Now replace VM2 with an Azure Storage Account.

```text
VNet
│
└── VM

Azure Storage Account
```

Although the VM is inside the VNet, the **Azure Storage Account is not**.

Azure Storage is a **Platform as a Service (PaaS)** offering managed by Microsoft. It exists outside your VNet.

Therefore, the VM needs a secure way to connect to it.

This is where **Service Endpoints** and **Private Endpoints** are used.

---

# Service Endpoint

A **Service Endpoint** extends your Virtual Network identity to an Azure service.

```text
VNet
│
└── VM
      │
      │ Service Endpoint
      ▼
Azure Storage Account
(Public Endpoint)
```

### How it works

- The Storage Account still uses its **public endpoint**.
- Azure recognizes that the request comes from your VNet.
- You can configure the Storage Account to accept traffic only from your VNet.

### Key Points

- Uses the service's **public endpoint**.
- Traffic travels over the Azure backbone network.
- Easier to configure.
- Does not assign a private IP to the Azure service.

---

# Private Endpoint

A **Private Endpoint** creates a **private IP address inside your VNet** for an Azure service.

```text
VNet
│
├── VM
│
└── Private Endpoint (10.0.1.10)
        │
        ▼
Azure Storage Account
```

### How it works

- Azure assigns a private IP address inside your VNet.
- The VM connects to the Storage Account using this private IP.
- The Storage Account's public access can be disabled.

### Key Points

- Uses a **private IP address**.
- No public endpoint is required.
- Most secure option.
- Recommended for sensitive workloads.

---

# Why Not Put Azure Storage Inside the VNet?

Azure services such as:

- Azure Storage
- Azure SQL Database
- Azure Key Vault
- Azure App Service

are **PaaS (Platform as a Service)** offerings.

Microsoft manages their infrastructure, networking, scaling, and maintenance. They are **not deployed inside your VNet** like Virtual Machines.

To securely access these services, Azure provides:

- **Service Endpoint** – Secure access through the service's public endpoint.
- **Private Endpoint** – Secure access through a private IP address inside your VNet.

---

# Summary

- **Virtual Network (VNet)** is used to enable communication between Azure resources that are inside the same network, such as VM-to-VM communication.
- **Service Endpoint** securely connects a VNet to an Azure PaaS service while the service continues to use its public endpoint.
- **Private Endpoint** creates a private IP address for an Azure PaaS service inside your VNet, allowing access without using a public endpoint.
- For communication between **VMs**, only a **VNet** is required.
- For communication from a **VM to Azure PaaS services** (Storage, SQL Database, Key Vault, etc.), use a **Service Endpoint** or, preferably, a **Private Endpoint**.
