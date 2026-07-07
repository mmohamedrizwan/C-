# Azure Virtual Machine (Azure VM)

An **Azure Virtual Machine (Azure VM)** is an **Infrastructure as a
Service (IaaS)** offering in Microsoft Azure that lets you create and
run Windows or Linux virtual machines in the cloud.

## Key Features

-   Supports Windows and Linux
-   Scalable CPU, memory, and storage
-   High availability
-   Pay-as-you-go pricing
-   Secure with NSGs, Microsoft Entra ID, disk encryption, and backups
-   Supports custom VM images

## Components

-   **Virtual Machine** -- Compute resource
-   **OS Disk** -- Stores the operating system
-   **Data Disks** -- Stores application data
-   **Virtual Network (VNet)** -- Private networking
-   **Network Interface (NIC)** -- Network connectivity
-   **Public IP** -- Optional internet access
-   **Network Security Group (NSG)** -- Firewall rules

# Infrastructure as a Service (IaaS)

**Infrastructure as a Service (IaaS)** is a cloud computing model that
provides virtualized infrastructure such as **virtual machines, storage,
and networking** over the internet.

The cloud provider manages the **physical hardware, networking, storage,
and virtualization**, while you manage the **operating system,
applications, runtime, middleware, and data**.

## Key Features

-   On-demand virtual machines
-   Pay-as-you-go pricing
-   Scalable infrastructure
-   Full control over the operating system
-   Suitable for custom applications

## Examples

-   Microsoft Azure -- Azure Virtual Machines
-   Amazon Web Services (AWS) -- EC2
-   Google Cloud Platform (GCP) -- Compute Engine

--- 

# How to connect

- Go to Resource groups
- Click the particular resource group
- In Overview page the vm's will be found
- Click on the particular vm then connect
- Then it will redirect us to connect page
- Download the **RDP file** and click on the remote desktop file
- Popup will open then click the connect button
- Then enter the credentials during the configuration of building the machine

---

# How to disconnect

- In Overview tab click stop

## Usage of Stopping an Azure Virtual Machine

Stopping an Azure Virtual Machine (VM) is useful when you don't need it temporarily. It helps reduce costs and frees up Azure resources.

## Why Stop a VM?

- Save compute costs when the VM is not in use.
- Perform maintenance or update safely.
- Prevent unauthorized access during downtime.
- Free Azure compute resources for other workloads.
- Shutdown development or testing environments after working hours.

## What happens when you stop a VM?

| Resource           | Status After Stopping                    |
|--------------------|------------------------------------------|
| CPU                | Stopped                                  |
| Memory (RAM)       | Released                                 |
| Operating System   | Shut down                                |
| Data on OS Disk    | Preserved                                |
| Data on Data Disks | Preserved                                |
| Public IP (Dynamic)| May be released if the VM is deallocated |
| Virtual Network    | Remains attached                         |

# Types of Stopping a VM

## 1. Stop (Shutdown from Operating System)

- Stops the operating system.
- VM still occupies Azure hardware.
- **Compute charges continue.**
- Suitable for restarting after a short period.

## 2. Stop (Deallocate)

- Stops the operating system.
- Releases the underlying Azure hardware.
- **Compute charges stop.**
- Storage and networking charges still apply.
- Dynamic Public IP may change when the VM starts again.

## Billing Impact

| Resource          | Charged After Deallocate?  |
|-------------------|----------------------------|
| Compute (CPU/RAM) | ❌ No                     |
| Managed Disk      | ✅ Yes                    |
| Snapshots         | ✅ Yes                    |
| Static Public IP  | ✅ Yes                    |
| Backup Services   | ✅ Yes                    |
| Virtual Network   | ❌ No                     |

# Why Does the Temporary D: Drive Get Lost in an Azure VM?

In Azure Virtual Machines, the **D:** drive (or another temporary drive letter depending on the VM) is a **temporary local disk**. It is **not permanent storage** and can be lost under certain conditions.

# Why Does It Happen?

The temporary disk is stored on the **physical host machine**, not on Azure Managed Disks. When the VM is moved to another host or restarted in certain ways, the temporary disk may be recreated or erased.

# When Can the Temporary Drive Be Lost?

The contents of the temporary drive can be lost during:

- VM Stop (Deallocate)
- VM Resize (change VM size)
- VM Redeployment
- Host maintenance by Azure
- Hardware failure
- Live migration to another host

---

# How to change the size of the vm machine

- Find the **Availability + scale** in the sidenav and click on **Size**
- Click the **VM Size (B2s)** then click the resize button

## Azure Virtual Machine (VM) Sizes

Azure Virtual Machines are available in different **VM families**, each designed for specific workloads such as general-purpose applications, databases, AI/ML, high-performance computing, and storage-intensive applications.

## Azure VM Families

| VM Family | Purpose | Common Series |
|-----------|---------|---------------|
| **A-Series** | Entry-level, development, testing | Av2 |
| **B-Series (Burstable)** | Low-cost VMs with occasional CPU bursts | B1s, B2s, B4ms |
| **D-Series (General Purpose)** | Balanced CPU and memory for most applications | Dv5, Dsv5, Dasv5, Dv6, Dv7 |
| **DC-Series** | Confidential Computing (secure workloads) | DCasv5, DCadsv5 |
| **F-Series (Compute Optimized)** | High CPU performance | Fsv2, Fasv7 |
| **FX-Series** | Very high-performance compute workloads | FX, FXmsv2 |
| **E-Series (Memory Optimized)** | High memory for databases and analytics | Ev5, Esv5, Easv5 |
| **Eb-Series** | Memory optimized with higher storage throughput | Ebsv5, Ebsv6 |
| **EC-Series** | Confidential Computing with high memory | ECasv5, ECasv6 |
| **M-Series** | Extremely large memory for SAP HANA and enterprise databases | Msv3, Mdsv3 |
| **L-Series (Storage Optimized)** | High disk IOPS and throughput | Lsv3, Lsv4 |
| **NC-Series (GPU)** | AI, Machine Learning, HPC | NCv3, NC A100, NC H100 |
| **ND-Series (GPU)** | Deep Learning training | ND A100, ND H100, ND MI300X |
| **NV-Series (GPU)** | Graphics rendering, VDI, CAD | NVv4, NVadsA10 |
| **NG-Series (GPU)** | Cloud gaming and Virtual Desktop | NGads V620 |
| **NP-Series (FPGA)** | FPGA acceleration for specialized workloads | NP-Series |

## Commonly Used VM Sizes

| VM Size | vCPUs | Memory | Typical Use |
|---------|------:|-------:|-------------|
| Standard_B1s | 1 | 1 GB | Small test VM |
| Standard_B2s | 2 | 4 GB | Development |
| Standard_D2s_v5 | 2 | 8 GB | Web applications |
| Standard_D4s_v5 | 4 | 16 GB | Application servers |
| Standard_D8s_v5 | 8 | 32 GB | Production workloads |
| Standard_E2s_v5 | 2 | 16 GB | SQL Server |
| Standard_E4s_v5 | 4 | 32 GB | Databases |
| Standard_F4s_v2 | 4 | 8 GB | Compute-intensive applications |
| Standard_L8s_v3 | 8 | 64 GB | Big Data, NoSQL |
| Standard_NC4as_T4_v3 | 4 | 28 GB | AI and GPU workloads |

## VM Family Selection Guide

| VM Family | Best For |
|-----------|----------|
| **A-Series** | Basic development and testing |
| **B-Series** | Low-cost workloads with occasional CPU bursts |
| **D-Series** | General-purpose applications (most common) |
| **E-Series** | Databases and memory-intensive applications |
| **F-Series** | Compute-intensive workloads |
| **L-Series** | Storage-intensive applications |
| **NC-Series** | AI, Machine Learning, HPC |
| **ND-Series** | Deep Learning training |
| **NV-Series** | Graphics rendering and Virtual Desktop Infrastructure (VDI) |
| **M-Series** | Large enterprise databases (SAP HANA) |
| **DC/EC-Series** | Confidential Computing and secure workloads |

## Quick Summary

- **A-Series** → Entry-level VMs
- **B-Series** → Burstable VMs
- **D-Series** → General Purpose
- **E-Series** → Memory Optimized
- **F-Series** → Compute Optimized
- **L-Series** → Storage Optimized
- **NC-Series** → AI & Machine Learning
- **ND-Series** → Deep Learning
- **NV-Series** → Graphics & Visualization
- **M-Series** → Large Memory
- **DC/EC-Series** → Confidential Computing