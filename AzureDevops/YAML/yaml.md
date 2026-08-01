# Azure DevOps YAML - Beginner's Guide

## What is YAML?

**YAML** stands for:

> **YAML Ain't Markup Language**

YAML is a **human-readable configuration language** used to define Azure DevOps pipelines.

Instead of creating pipelines manually through the Azure DevOps portal, you define everything in a YAML file named:

```text
azure-pipelines.yml
```

Azure DevOps reads this file and executes the pipeline automatically.

---

# Why Use YAML?

## Without YAML

```
text
Developer
      │
      ▼
Login to Azure DevOps
      │
      ▼
Create Pipeline
      │
      ▼
Configure Build Steps
      │
      ▼
Save Pipeline
```

The pipeline configuration exists only in Azure DevOps.

---

## With YAML 

```text
Developer
      │
      ▼
azure-pipelines.yml
      │
      ▼
Commit to Git Repository
      │
      ▼
Azure DevOps Automatically Creates and Runs the Pipeline
```

---

# Azure Pipeline File

```text
azure-pipelines.yml
```

Example project structure:

```text
MyApplication
│
├── Controllers
├── Models
├── Views
├── Program.cs
├── appsettings.json
└── azure-pipelines.yml
```

---

# Basic YAML Structure

```yaml
trigger:

pool: 

variables:

stages:

jobs:

steps:
```

Not every pipeline requires all these sections.

---

# Smallest Working Pipeline

```yaml
trigger:
    - main

pool:
    vmImage: ubuntu-latest

steps:
    - script: echo "Hello Azure DevOps"
```

### Output

```text
Hello Azure DevOps
```

---

# Pipeline Flow

```text
Git Push
    │
    ▼
Trigger Pipeline
    │
    ▼
Azure DevOps Agent
    │
    ▼
Execute Steps
    │
    ▼
Pipeline Completed
```

---

# Understanding Each Section

## 1. Trigger

The `trigger` section defines **when the pipeline should run**.

```yaml
trigger:
    - main
```

### Meaning

Whenever code is pushed to the `main` branch, Azure DevOps automatically starts the pipeline.

```text
Git Push
     │
     ▼
main Branch
     │
     ▼
Pipeline Starts
```

### Trigger Multiple Branches

```yaml
trigger:
- main
- develop
```

Pipeline runs for both branches.

---

## 2. Pool

The `pool` section specifies **which machine (agent)** will execute the pipeline.

```yaml
pool:
  vmImage: ubuntu-latest
```

Azure DevOps provides Microsoft-hosted agents.

### Common Hosted Agents

#### Ubuntu

```yaml
pool:
  vmImage: ubuntu-latest
```

#### Windows

```yaml
pool:
  vmImage: windows-latest
```

#### macOS

```yaml
pool:
  vmImage: macOS-latest
```

---

# What is an Agent?

An **Agent** is a machine that performs the pipeline tasks.

```text
Azure DevOps
      │
      ▼
Creates Agent
      │
      ▼
Downloads Source Code
      │
      ▼
Runs Build
      │
      ▼
Runs Tests
      │
      ▼
Publishes Artifacts
```

---

## 3. Variables

Variables allow you to store reusable values.

```yaml
variables:
  buildConfiguration: Release
```

Use the variable:

```yaml
steps:
- script: dotnet build --configuration $(buildConfiguration)
```

Azure DevOps replaces:

```text
$(buildConfiguration)
```

with

```text
Release
```

---

## 4. Steps

A pipeline consists of one or more steps.

```yaml
steps:

    - script: echo "Hello"
    - script: pwd
    - script: ls
```

Execution order:

```text
Step 1
   │
   ▼
Step 2
   │
   ▼
Step 3
```

---

## 5. Script

Runs command-line commands.

### Linux

```yaml
steps:
- script: echo "Hello Azure DevOps"
```

### Windows PowerShell

```yaml
steps:
- powershell: Write-Host "Hello Azure DevOps"
```

---

## 6. Display Name

Adds a friendly name to a pipeline step.

```yaml
steps:
    - script: dotnet build
      displayName: Build Application
```

Pipeline UI displays:

```text
Build Application
```

instead of

```text
script
```

---

## 7. Jobs 

A stage contains one or more Jobs.

```yaml
jobs:

- job: Build

  steps:

  - script: echo Build

- job: Test

  steps:

  - script: echo Test
```

Pipeline structure:

```text
Pipeline
│
├── Build Job
│
└── Test Job
```

Jobs can execute in parallel if they don't depend on each other.

---

## 8. Stages

Stages divide the pipeline into logical phases.

Example:

```yaml
stages:

- stage: Build

- stage: Test

- stage: Deploy
```

Execution:

```text
Build
   │
   ▼
Test
   │
   ▼
Deploy
```

---

# Complete Example

```yaml
trigger:
- main

pool:
  vmImage: windows-latest

variables:
  buildConfiguration: Release

stages:

- stage: Build

  jobs:

  - job: BuildJob

    steps:

    - script: echo Building Application
      displayName: Build

    - script: dotnet restore
      displayName: Restore Packages

    - script: dotnet build --configuration $(buildConfiguration)
      displayName: Build Project
```

---