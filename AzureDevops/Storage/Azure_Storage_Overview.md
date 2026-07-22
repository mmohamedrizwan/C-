# Azure Storage

Azure Storage is Microsoft's cloud storage service for storing
structured and unstructured data securely, durably, and at scale.

## Storage Types

-   **Blob Storage**: Stores unstructured data such as images, videos,
    documents, and backups.
-   **File Storage**: Managed file shares accessible via SMB/NFS.
-   **Queue Storage**: Stores messages for asynchronous communication
    between applications.
-   **Table Storage**: NoSQL key-value store for semi-structured data.
-   **Disk Storage**: Managed disks for Azure Virtual Machines.

## Key Features

-   High availability and durability
-   Automatic replication (LRS, ZRS, GRS, GZRS)
-   Encryption at rest and in transit
-   Role-based access control (RBAC)
-   Shared Access Signatures (SAS)
-   Lifecycle management for cost optimization

## Summary

Azure Storage provides scalable, secure, and cost-effective cloud
storage services for modern applications.

# How to create a Storage Account in Azure

You can create an Azure Storage Account using the **Azure Portal**, **Azure CLI**, or **Azure PowerShell**.

---

## Method 1: Create a Storage Account using the Azure Portal

1. 1. Sign in to the Azure Portal.
2. In the search bar, type **Storage accounts** and select it.
3. Click **+ Create**.
4. Enter the required details:

    - **Subscription:** Select your Azure subscription.
    - **Resource Group:** Select an existing resource group or create a new one.
    - **Storage Account Name:** Enter a globally unique name (3–24 lowercase letters and numbers).
    - **Region:** Select the Azure region.
    - **Performance:** Choose **Standard** or **Premium**.
    - **Redundancy:** Select one of the following:
     - Locally Redundant Storage (LRS)
     - Zone-Redundant Storage (ZRS)
     - Geo-Redundant Storage (GRS)
     - Read-Access Geo-Redundant Storage (RA-GRS)
5. Click **Review + Create**.
6. After validation succeeds, click **Create**.
7. Wait for the deployment to complete.
8. Click **Go to Resource** to open the storage account.