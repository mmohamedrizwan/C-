# Azure Network Security Group (NSG)

## What is Azure NSG?

An **Azure Network Security Group (NSG)** is a security feature that acts as a **Virtual firewall** for Azure resources. It controls **inbound and outbound network traffic** by allowing or denying traffic based on security rules.

NSGs can be associated with:
- A **Subnet**
- A **Network Interface (NIC)** of a Virtual Machine

---

## Why do we use NSG?

- Control access to Azure Virtual Machines.
- Allow only required ports (HTTP, HTTPS, RDP, SSH).
- Block unauthorized traffic.
- Secure communication between subnets and VMs.

---

## How NSG Works

Each NSG contains **security rules**.

A rule consists of:

- **Priority** (100-4096; lower number = higher priority).
- **Source**
- **Destination**
- **Protocol** (TCP, UDP, ICMP, Any)
- **Port**
- **Action** (Allow or Deny)
- **Direction** (Inbound or Outbound)

Rules are evaluated in **priority order**. Once a rule matches, Azure stops processing additional rules.

---

## NSG Components

| Component | Description |
|-----------|-------------|
| Source | IP Address, Service Tag, or Any |
| Destination | IP Address, Subnet, or Any |
| Protocol | TCP, UDP, ICMP, or Any |
| Port | Single port or port range |
| Action | Allow or Deny |
| Direction | Inbound or Outbound |
| Priority | Lower number = Higher priority |