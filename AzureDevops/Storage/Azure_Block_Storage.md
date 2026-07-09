# Azure Block Storage

**Azure Block Storage** is provided through **Azure Managed Disks**,
which are block-level storage volumes used by Azure Virtual Machines
(VMs). Data is stored in fixed-size blocks, providing high performance,
low latency, and reliable storage.

## Disk Types

-   **Premium SSD** -- High-performance production workloads
-   **Standard SSD** -- Cost-effective with consistent performance
-   **Standard HDD** -- Backup and infrequent access
-   **Ultra Disk** -- Mission-critical, I/O-intensive workloads

## Local Redundant Storage (LRS) vs Zone-Redundant Storage (ZRS)

| Feature             | Local Redundant Storage (LRS)                | Zone-Redundant Storage (ZRS) |
|---------------------|----------------------------------------------|------------------------------|
| Data Copies         | 3 copies                                     | 3 copies                     |
| Location            | Single datacenter                            | Multiple Availability Zones in the same region |
| Protection          | Hardware failures                            | Hardware failures + Availability Zone failures |
| Regional Protection | ❌ No                                        | ❌ No                       |
| Availability        | Lower than ZRS                               | Higher than LRS              |
| Cost                | Lower                                        | Higher                       |
| Best For            | Development, testing, non-critical workloads | Production applications requiring high availability |

## LRS (Local Redundant Storage)

- Stores **3 copies** of your data in a **single datacenter**.
- Protects against disk and server failures.
- Does **not** protect against datacenter or Availability Zone failures.
- Lowest-cost redundancy option.

## ZRS (Zone-Redundant Storage)

- Stores **3 copies** across **three Availability Zones** in the same Azure region.
- Protects against the failure of an entire Availability Zone.
- Provides higher availability than LRS.
- Suitable for mission-critical production applications.

## Example

Suppose your storage account is in **East US**:

- **LRS:** All 3 copies are stored in one datacenter in East US.
- **ZRS:** The 3 copies are distributed across three different Availability Zones within East US.

## Summary

- **LRS:** Best for cost-sensitive workloads that only need protection from hardware failures.
- **ZRS:** Best for production workloads requiring resilience against Availability Zone failures.

# How to

- **Search Virtual Machine** -> **Create Virtual Machine** -> **Go to Disks** -> **Select OS disk type**