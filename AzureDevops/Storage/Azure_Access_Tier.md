# Azure Storage Account Access Tier

The **Access Tier** in Azure Storage determines the cost of storing and accessing **blob data** based on how frequently the data is used. It helps optimize storage costs.

> **Note:** Access tiers apply only to **Blob Storage** (BlobStorage and StorageV2 accounts).They do not apply to Azure Files, Queues, or Tables.

## Access Tier Types

| Access Tier | Description | Best For | Storage Cost | Access Cost |
|-------------|-------------|----------|--------------|-------------|
| **Hot** | Optimized for data that is accessed frequently. | Active files, images, application data | Higher | Lower |
| **Cool** | Optimized for data that is accessed infrequently (at least once every 30 days). | Backups, short-term archives | Lower | Higher |
| **Cold** | Optimized for data accessed rarely (at least once every 90 days). | Long-term backups, compliance data | Lower than Cool | Higher than Cool |
| **Archive** | Lowest-cost storage for data that is rarely accessed and stored for at least 180 days. Data must be rehydrated before it can be read. | Long-term archival, legal records | Lowest | Highest |

## Comparison

| Feature | Hot | Cool | Cold | Archive |
|---------|-----|------|-------|---------|
| Frequent Access | ✅ Yes | ❌ No | ❌ Rarely | ❌ Almost Never |
| Storage Cost | High | Low | Lower | Lowest |
| Data Retrieval Cost | Low | Medium | High | Highest |
| Minimum Retention Period | None | 30 days | 90 days | 180 days |
| Immediate Access | ✅ Yes | ✅ Yes | ✅ Yes | ❌ No (rehydration required) |

## When to Use Each Tier

- **Hot Tier**
  - Frequently accessed files
  - Website content
  - Application data
  - Active datasets

- **Cool Tier**
  - Monthly backups
  - Disaster recovery files
  - Older media files
  - Data accessed occasionally

- **Cold Tier**
  - Long-term backups
  - Compliance records
  - Data retained for months with infrequent access

- **Archive Tier**
  - Historical records
  - Financial documents
  - Legal and regulatory archives
  - Data retained for years with very rare access

## Changing the Access Tier

You can change the access tier of a blob at any time:

- **Hot → Cool/Cold/Archive**
- **Cool → Hot/Cold/Archive**
- **Cold → Hot/Cool/Archive**
- **Archive → Hot/Cool/Cold** (requires rehydration before the data can be accessed)

> **Note:** Changing tiers may incur transaction and early deletion charges if the minimum retention period has not been met.

## Best Practice

Choose the access tier based on how often your data is accessed:

- **Hot** → Frequently used data
- **Cool** → Occasionally accessed data
- **Cold** → Rarely accessed data
- **Archive** → Long-term archival with minimal access

---

# Understanding Transaction and Early Deletion Charges in Azure Blob Storage

When you change the access tier or delete a blob, Azure may charge additional fees depending on how long the blob has been stored in its current tier.

## Early Deletion Charges

Each lower-cost access tier has a **minimum retention period**:

| Access Tier | Minimum Retention Period |
|--------------|--------------------------|
| **Cool** | 30 days |
| **Cold** | 90 days |
| **Archive** | 180 days |

If you **delete a blob** or **move it to another tier** before the minimum retention period ends, Azure charges you for the **remaining days**.

### Example 1: Cool → Hot

- **Day 1:** Upload a blob to the **Cool** tier.
- **Day 10:** Change the blob to the **Hot** tier.

The Cool tier requires a **30-day minimum retention period**.

Since the blob remained in the Cool tier for only **10 days**, Azure charges for the **remaining 20 days**.

### Example 2: Cold → Delete

- **Day 1:** Move a blob to the **Cold** tier.
- **Day 40:** Delete the blob.

The Cold tier has a **90-day minimum retention period**.

Because the blob was stored for only **40 days**, Azure charges for the **remaining 50 days**.

### Example 3: Archive → Hot

- **Day 1:** Move a blob to the **Archive** tier.
- **Day 60:** Rehydrate it back to the **Hot** tier.

The Archive tier requires a **180-day minimum retention period**.

Since the blob remained in Archive for only **60 days**, Azure charges for the **remaining 120 days**.

In addition, you'll also pay:
- A **rehydration charge** to restore the blob to an online tier.
- A **data retrieval/transaction charge**.

---

# Transaction Charges

Azure also charges for operations performed on blobs.

Examples of operations that incur transaction charges include:

- Uploading a blob
- Downloading a blob
- Reading a blob
- Changing the access tier
- Listing blobs in a container
- Deleting a blob

| Operation | Transaction Charge |
|------------|--------------------|
| Upload a blob | Yes |
| Download a blob | Yes |
| Change Hot → Cool | Yes |
| Change Cool → Hot | Yes |
| List blobs | Yes |
| Delete a blob | Yes |

> **Note:** The exact transaction cost varies based on the storage tier and Azure region.

---

# Summary

- **Transaction charges** are fees for operations such as uploading, downloading, reading, deleting, or changing a blob's access tier.
- **Early deletion charges** apply when you **delete a blob** or **move it to another access tier** before the minimum retention period is met.

| Access Tier | Minimum Retention Period |
|--------------|--------------------------|
| **Cool** | 30 days |
| **Cold** | 90 days |
| **Archive** | 180 days |

Azure enforces these minimum retention periods to ensure that lower-cost storage tiers are used for long-term storage rather than short-term workloads.