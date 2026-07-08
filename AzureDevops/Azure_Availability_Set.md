# Azure Availability Set - Update Domain (UD) and Fault Domain (FD)

An **Availability Set** in Azure ensures that your Virtual Machines (VMs) remain highly available by distributing them across **Update Domains (UD)** and **Fault Domains (FD)**.

---

# Update Domain (UD)

An **Update Domain (UD)** is a logical group of VMs that are **Updated and rebooted together during planned Azure maintenance**.

## Purpose

- Protects against **Planned maintenance**.
- Azure updates **one Update Domain at a time**.
- Other Update Domains continue running, minimizing downtime.

## Example

Suppose you have **6 VMs** distributed across **3 Update Domains**.

| VM  | Update Domain |
|-----|---------------|
| VM1 | UD0           |
| VM2 | UD1           |
| VM3 | UD2           |
| VM4 | UD0           |
| VM5 | UD1           |
| VM6 | UD2           |

### Planned Maintenance

1. Azure updates **UD0** (VM1, VM4 restart).
2. Azure waits for them to recover.
3. Azure updates **UD1** (VM2, VM5 restart).
4. Azure waits again.
5. Azure updates **UD2** (VM3, VM6 restart).

**Result:** Not all VMs restart at the same time, so the application remains available.

---

# Fault Domain (FD)

A **Fault Domain (FD)** is a group of VMs that share the same **physical hardware infrastructure**, such as:

- Physical server
- Rack
- Power source
- Network switch

## Purpose

- Protects against **unexpected hardware failures**.
- Azure distributes VMs across different fault domains.

## Example

| VM  | Fault Domain |
|---- |--------------|
| VM1 | FD0          |
| VM2 | FD1          |
| VM3 | FD0          |
| VM4 | FD1          |

### Hardware Failure

If **Fault Domain 0 (FD0)** loses power:

- ❌ VM1 becomes unavailable.
- ❌ VM3 becomes unavailable.
- ✅ VM2 continues running.
- ✅ VM4 continues running.

**Result:** The application remains available because VMs in other fault domains continue to run.

# Visual Diagram

```text
Availability Set

             Planned Maintenance
                    │
        ┌───────────┴───────────┐
        │                       │
   Update Domain 0         Update Domain 1
   VM1      VM4            VM2      VM5
    (Updated First)        (Updated Later)

-----------------------------------------------------

Physical Hardware

Rack A (FD0)            Rack B (FD1)
VM1                     VM2
VM3                     VM4

If Rack A fails:
VM1 ❌
VM3 ❌

VM2 ✅
VM4 ✅
```
---

# Update Domain vs Fault Domain

| Feature | Update Domain (UD) | Fault Domain (FD) |
|---------|---------------------|-------------------|
| Purpose | Protects against planned maintenance | Protects against hardware failures |
| Failure Type | Azure maintenance | Power, rack, network, or server failure |
| Managed By | Azure | Physical infrastructure |
| VMs Restart Together | Yes (within one Update Domain) | No (only affected hardware fails) |
| Goal | Prevent all VMs from rebooting simultaneously | Prevent all VMs from residing on the same physical hardware |

---

# Key Points

- **Update Domain (UD)**
  - Protects against planned Azure maintenance.
  - Only one update domain is rebooted at a time.

- **Fault Domain (FD)**
  - Protects against unexpected hardware failures.
  - VMs are placed on separate physical hardware.

- **Availability Set**
  - Combines Update domains and Fault Domains to maximize VM availability and minimize downtime.

---

# How to set availability zone

- When creating a virtual machine, in instance details **Availability options** is found.
- Select the **Availability options** dropdown and click **Availability set**.
- Then select the **Availability set** select **create new**.
- Create it by giving **Name**, **Fault domains(2)** and **Update domains(5)**.