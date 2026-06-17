# Optimistic Concurrency in EF Core

## What is Optimistic Concurrency?

Optimistic Concurrency is a mechanism used to prevent one user's changes from accidentally overwriting another user's changes when multiple users modify the same data simultaneously.

It assumes that:

> Data conflicts are rare, so records should not be locked while users are editing them.

Instead of locking data, the application checks whether the data has changed before saving updates.

---

# Why Do We Need Optimistic Concurrency?

Consider an Employee table:

| Id | Name   | Salary |
| -- | ------ | ------ |
| 1  | Rizwan | 50000  |

Two users open the same record at the same time.

### User A

Reads:

```text
Salary = 50000
```

### User B

Reads:

```text
Salary = 50000
```

---

### User A Updates Salary

```text
Salary = 55000
```

Database becomes:

```text
Salary = 55000
```

---

### User B Updates Salary Later

```text
Salary = 60000
```

Database becomes:

```text
Salary = 60000
```

---

## Problem

User A's update is lost.

Final value:

```text
60000
```

Expected:

```text
User A should not lose their changes.
```

This issue is called:

> Lost Update Problem

---

# How Optimistic Concurrency Solves This

A special column called RowVersion is added.

| Id | Name   | Salary | RowVersion |
| -- | ------ | ------ | ---------- |
| 1  | Rizwan | 50000  | 1          |

Whenever a row is updated, RowVersion changes automatically.

---

# Example Workflow

## Initial State

| Id | Salary | RowVersion |
| -- | ------ | ---------- |
| 1  | 50000  | 1          |

---

## User A Reads

```text
Salary = 50000
RowVersion = 1
```

---

## User B Reads

```text
Salary = 50000
RowVersion = 1
```

---

## User A Saves

```text
Salary = 55000
```

Database:

| Salary | RowVersion |
| ------ | ---------- |
| 55000  | 2          |

---

## User B Saves

User B still has:

```text
RowVersion = 1
```

Database has:

```text
RowVersion = 2
```

Mismatch detected.

EF Core throws:

```csharp
DbUpdateConcurrencyException
```

No overwrite occurs.

---

# Implementing Optimistic Concurrency in EF Core

## Entity Model

```csharp
public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Salary { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }
}
```

---

## What Does [Timestamp] Mean?

```csharp
[Timestamp]
```

Tells EF Core:

> Use this property as a concurrency token.

Whenever the row changes, EF Core verifies that the original RowVersion still matches the database value.

---

# SQL Server Table

```sql
CREATE TABLE Employees
(
    Id INT PRIMARY KEY,
    Name VARCHAR(100),
    Salary DECIMAL(18,2),
    RowVersion ROWVERSION
)
```

---

# What SQL Does EF Core Generate?

Suppose the original RowVersion is:

```text
ABC123
```

EF Core generates:

```sql
UPDATE Employees
SET Salary = 55000
WHERE Id = 1
AND RowVersion = ABC123
```

Notice:

```sql
AND RowVersion = ABC123
```

This check is the key to Optimistic Concurrency.

---

# Successful Update

Database RowVersion:

```text
ABC123
```

SQL:

```sql
UPDATE Employees
SET Salary = 55000
WHERE Id = 1
AND RowVersion = ABC123
```

Rows affected:

```text
1
```

Update succeeds.

---

# Failed Update

Database RowVersion:

```text
XYZ789
```

SQL:

```sql
UPDATE Employees
SET Salary = 55000
WHERE Id = 1
AND RowVersion = ABC123
```

Rows affected:

```text
0
```

EF Core detects:

```text
Record was modified by another user.
```

Exception thrown:

```csharp
DbUpdateConcurrencyException
```

---

# Handling the Exception

```csharp
try
{
    await _context.SaveChangesAsync();
}
catch (DbUpdateConcurrencyException)
{
    return Conflict(
        "The record was modified by another user.");
}
```

---

# API Response

```http
409 Conflict
```

Example:

```json
{
    "message": "The record was modified by another user."
}
```

---

# Banking Example

Current Balance:

```text
1000
```

---

## User A Withdraws

```text
-200
```

Expected:

```text
800
```

---

## User B Deposits

```text
+500
```

Expected:

```text
1500
```

Without concurrency handling, one operation could overwrite the other.

Optimistic Concurrency ensures conflicts are detected before data corruption occurs.

---

# Optimistic vs Pessimistic Concurrency

| Optimistic Concurrency         | Pessimistic Concurrency           |
| ------------------------------ | --------------------------------- |
| No lock while editing          | Locks record immediately          |
| Better performance             | Lower performance                 |
| Most common in web apps        | Common in highly critical systems |
| Detects conflicts at save time | Prevents conflicts using locks    |
| Scales well                    | Less scalable                     |

---

# Optimistic Concurrency Flow

```text
Read Data
    ↓
Modify Data
    ↓
Check Version
    ↓
Save Changes
```

---

# Pessimistic Concurrency Flow

```text
Lock Record
    ↓
Modify Data
    ↓
Save Changes
    ↓
Unlock Record
```

---

# Real-Time Scenarios

## Employee Management System

Two HR users update the same employee record.

Without concurrency:

```text
One update overwrites another.
```

With concurrency:

```text
Conflict detected.
```

---

## E-Commerce Inventory

Two users purchase the last product simultaneously.

Concurrency checking prevents invalid inventory updates.

---

## Banking Applications

Two transactions update the same account.

Concurrency checking ensures balance consistency.

---

# Common Interview Question

### Question

Two users open the same employee record.

User A updates salary.

User B updates designation.

How will you prevent one user from overwriting another user's changes?

### Answer

I would implement Optimistic Concurrency using a RowVersion column. EF Core includes the original RowVersion value in the UPDATE statement. If another user has already modified the record, the RowVersion changes and EF Core throws a DbUpdateConcurrencyException. The application can then notify the user to refresh the data before saving again.

---

# Key Interview Points

1. Optimistic Concurrency prevents Lost Updates.
2. No database lock is maintained while users edit data.
3. EF Core commonly uses RowVersion.
4. Use `[Timestamp]` attribute.
5. EF Core throws `DbUpdateConcurrencyException`.
6. Return HTTP 409 Conflict from APIs.
7. Best suited for web applications.
8. Better scalability than pessimistic locking.
9. Most commonly asked concurrency topic in .NET interviews.
10. RowVersion is automatically updated by SQL Server.

---

# One-Line Interview Definition

> Optimistic Concurrency is a technique that prevents accidental overwriting of data by verifying that a record has not changed since it was originally read, typically using a RowVersion column and throwing a DbUpdateConcurrencyException when a conflict occurs.