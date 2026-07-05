# DevOps

## What is DevOps?

**DevOps** is a combination of **Development (Dev)** and **Operations (Ops)**. It is set of practices, tools, and a culture that enables software development and IT operations team to work together throughout
the software development lifecycle.

The primary goal of Devops is to **deliver high-quality software faster and more reliably** through collaboration, automation, continuous integration, and continuous delivery.

---

# Objectives of DevOps

- Improve collaboration between development and operations teams.
- Automate repetitive tasks.
- Deliver software faster and more frequently.
- Increase application reliability and stability.
- Reduce deployment failures and recovery time.
- Continuously monitor applications and infrastructure.

---

# DevOps Lifecycle

```text
Plan
   ↓
Develop
   ↓
Build
   ↓
Test
   ↓
Release
   ↓
Deploy
   ↓
Operate
   ↓
Monitor
   ↺ (Feedback back to Plan)
```

---

# Stages of DevOps

## 1. Plan
- Gather business requirements.
- Create user stories and tasks.
- Plan releases.

**Common Tools**
- Azure Boards
- Jira
- Trello

## 2. Develop
- Developers write application code.
- Code is stored in a version control system.

**Common Tools**
- Git
- GitHub
- Azure Repos
- GitLab

---

## 3. Build
- Source code is compiled into an executable or package.
- Dependencies are restored.

**Common Tools**
- MSBuild
- Maven 
- Gradle
- npm

---

## 4. Test
- Automated tests verify application quality.
- Detect bugs before deployment.

**Types of Testing**
- Unit testing
- Integration testing
- Functional testing
- Performance testing

**Common Tools**
- XUnit
- NUnit
- Selenium
- JUnit

---

## 5. Release
- Prepare the application for deployment.
- Approvals and release management are performed.

**Common Tools**
- Azure Devops Release Pipelines
- Github Actions

---

## 6. Deploy
- Deploy the application to development, staging, or production environments.

**Deployment Types**
- Manual Deployment
- Automated Deployment
- Blue-Green Deployment
- Rolling Deployment
- Canary Deployment

**Common Tools**
- Azure DevOps
- Octopus Deploy
- Kubernetes

---

## 7. Operate
- Ensure the application is running correctly.
- Manage infrastructure and services.

Tasks include:
- Server Management
- Scaling
- Backup
- Security updates

---

## 8. Monitor
- Monitor application health and performance.
- Detect issues quickly.

**Common Tools**
- Azure Monitor
- Application Insights
- Prometheus
- Grafana

---

# DevOps Pipeline

```text
Developer
    ↓
Push Code
    ↓
Source Control (Git)
    ↓
Build Pipeline
(
### Example (.NET)

Source Code
↓
Compile using `dotnet build`
↓
Application is successfully built

If there is a compilation error, the pipeline stops here.
)
    ↓
Run Automated Tests
(
## Purpose
Verify that the application works correctly before deployment.

### What happens?
- Executes unit tests.
- Executes integration tests (if configured).
- Generates a test report.

### Example

Suppose your application has 150 unit tests.

Result:

- ✅ 150 Passed → Continue
- ❌ Any test failed → Stop the pipeline

This prevents broken code from being deployed.
)
    ↓
Create Build Artifact
(
## Purpose
Package the successfully built application into a deployable file.

### What is an Artifact?

An **artifact** is the output produced by the build process that can be deployed to different environments.

Examples:
- .NET → DLLs or ZIP package
- React → `build/` or `dist/` folder
- Angular → `dist/`
- Java → JAR or WAR file

### Why create an artifact?

Instead of rebuilding the application for every environment, the **same tested artifact** is reused for Development, Staging, and Production.

Example:

```
Source Code
     │
Build Once
     │
Artifact (MyApp.zip)
     │
Deploy to Dev
Deploy to Staging
Deploy to Production
```

This ensures all environments run exactly the same application.
)
    ↓
Release Pipeline
(
## Purpose
Deploy the build artifact to one or more environments.

### What happens?
- Downloads the build artifact.
- Deploys it to Development.
- Waits for approvals (optional).
- Deploys it to Staging.
- Waits for approvals.
- Deploys it to Production.

### Example

```
Artifact
   │
   ▼
Development
   │
Approval
   ▼
Staging
   │
Approval
   ▼
Production
```

The Release Pipeline focuses on **deployment**, not compilation.
)
    ↓
Deploy to Development
(Test by Developers)
    ↓
Deploy to Staging
(Test by QA & Business Users)
    ↓
Deploy to Production
(Live for Customers)
    ↓
Monitoring & Feedback
```

---

# CI/CD in DevOps

## Continuous Integration (CI)

Continuous Integration is the practice of automatically building and testing code whenever developers push changes to the repository.

### Benefits
- Detect bugs early
- Faster integration
- Improved code quality
- Reduced merge conflicts

---

## Continuous Delivery (CD)

Continuous Delivery ensures that applications are always ready for deployment. Deployments can be triggered manually.

### Benefits
- Faster releases
- Lower deployment risk
- Reliable deployments

---

## Continuous Deployment

Continuous Deployment automatically deploys every successful change to production without manual approval.

### Benefits
- Very fast releases
- Fully automated pipeline
- Immediate customer feedback