# Automating Azure Resource Deployment Across Multiple Resource Groups

## Scenerio
Your organization has multiple departments, and each department requires the deployment of the **same set of Azure resource** across different **Resource Groups**. The deployment process needs to be automated to ensure consistency and reduce manual effort.

## Recommended Solution

**Use Azure Resource Manager (ARM) Templates** or **Azure Bicep** (Microsoft's recommended Infrastructure as Code language).

## Why ARM Templates/Bicep?

- Deploy the same infrastructure repeatedly with consistency.
- Eliminate manual configuration errors.
- Reuse the same template across multiple resource groups.
- Customize deployments using parameter files.
- Integrate with CI/CD pipelines (Azure DevOps, GitHub Actions)

# Question

**TRUE or FALSE:** You can prevent accidental deletion of Azure resources by adding a **Delete Lock**.

## Answer

**✅ True**

## Explanation

Azure **Resource Locks** help protect critical resources from accidental changes.

There are two types of locks:

1. **Delete Lock (CanNotDelete)**
    - Prevents authorized users from deleting the resource.
    - Users can still read and modify the resource.

2.  **Read-Only Lock (ReadOnly)**
    - Prevents both deletion and modification.
    - Allows only read operations.

A **Delete Lock** is commonly used to safeguard important resources such as Virtual Machines, Storage Accounts, and Resource Groups from accidental deletion.

## Benefits

- ✔ Prevents accidental deletion
- ✔ Protects critical resources
- ✔ Can be applied at the Subscription, Resource Group, or Resource level
- ✔ Helps improve governance and resource protection

## Exam Answer

**Answer:** **True**

A **Delete Lock (CanNotDelete)** prevents Azure resources from being accidentally deleted while still allowing authorized users to modify them.

# Question

**TRUE or FALSE:** A resource can have multiple **Read-Only** or **Delete** locks.

## Answer

**✅ True**

## Explanation

An Azure resource can have **multiple resource locks** applied to it.

The two types of locks are:

- **ReadOnly** – Prevents modification and deletion.
- **CanNotDelete (Delete Lock)** – Prevents deletion but allows modifications.

If multiple locks are applied, **the most restrictive lock takes precedence**. For example:

- If a resource has both a **ReadOnly** lock and a **CanNotDelete** lock, the **ReadOnly** lock is enforced.

## Key Points

- ✔ Multiple locks can exist on a resource.
- ✔ Locks can be applied at the Subscription, Resource Group, or Resource level.
- ✔ The most restrictive lock is effective.

## Exam Answer

**Answer:** **True**

A resource can have multiple **ReadOnly** or **CanNotDelete** locks, and Azure enforces the **most restrictive** lock.

# Azure Region Pair

## Definition

An **Azure Region Pair** is a relationship between two Azure regions within the same geographical area. Region pairs support business
continuity, disaster recovery and certain coordinated Azure platform updates.

## Example

- **Primary Region:** Central India
- **Paired Region** South India

If the Central India region experiences a major outage, resources deployed in South India can help restore application availability,
provided disaster recovery mechanisms are configured.

## Why Do We Use Azure Region Pairs?

- **Disaster Recovery:** Helps recover applications and data during regional outages.
- **Business Continuity:** Helps minimize service disruption.
- **Planned Updates:** Azure coordinates certain platform updates across paired regions to reduce the risk of simultaneous disruption.
- **Data Residency:** Certain services use region pairs to support geographic data-residency requirements.

## Key Points

- A region pair consists of two Azure regions within the same geography.
- Both regions are generally located within the same geographic area.
- Region pairing does not automatically replicate all Azure resources.
- Replication, backup, and failover must be configured based on application requirements.
- Some Azure regions do not have a traditional paired region.
- Region pairs and Availability Zones are different concepts.

## Region Pair vs Availability Zone

| Feature | Region Pair | Availability Zone |
|---|---|---|
| Location | Two separate Azure regions | Separate physical locations within one region |
| Main purpose | Regional disaster recovery | High availability within a region |
| Protects against | Major regional outages | Datacenter or zone failures |
| Example | Central India and South India | Multiple zones within Central India |

## Real-Time Example

Suppose an organization hosts an e-commerce application in Central India.

1. The application and database run in Central India.
2. The organization configures database replication and recovery resources in South India.
3. Central India experiences a regional outage.
4. The operations team initiates the configured failover process.
5. The application can resume service from South India, depending on the recovery configuration and service availability.

# Important Interview Point

**Azure Region Pairing does not mean automatic disaster recovery.** You must configure the required replication, backups, recovery resources, 
and failover procedures to meet your Recovery Time Objective (RTO) and Recovery Point Objective (RPO).

## Official Documentation

[Azure Region Pairs — Microsoft Learn](https://learn.microsoft.com/en-us/azure/reliability/regions-paired)