# Stages, Jobs & Dependencies in Azure DevOps YAML

## The Pipeline Hierarchy

Every Azure DevOps YAML pipeline is built from four nested levels. Understanding this hierarchy is the foundation for structuring any real-world pipeline.

```
Pipeline
 └─ Stage(s)
     └─ Job(s)
         └─ Step(s)
```

| Level | What it represents | Runs on |
|---|---|---|
| **Pipeline** | The whole workflow, top to bottom | — |
| **Stage** | A major phase (e.g. Build, Test, Deploy) | One or more agents, sequentially or in parallel |
| **Job** | A unit of work within a stage | A single agent |
| **Step** | A single task or script | Inside a job, sequentially |

If you don't explicitly define stages or jobs, Azure DevOps implicitly wraps your steps in one default job - which is why simple pipelines can skip straight to `steps:`.

```yaml
# Implicit single stage, single job
steps:
- script: echo "Hello, World"
```

is equivalent to:

```yaml
stages:
  stage: Default
  jobs:
  - job: Default
    steps: 
    - script: echo "Hello, world"
```

---

## Stages

A **stages** is a logical boundary in your pipeline - typically representing a phase like `Build`, `Test`, `Deploy_QA`, or `Deploy_Prod`. Stages run **sequentially by default**, each waiting for the previous one to succeed.

```yaml
stages:
- stage: Build
  jobs:
  - job: BuildJob
    steps:
    - script: echo "Building..."

- stage: Test
  jobs:
  - job: TestJob
    steps:
    - script: echo "Testing..."

- stage: Deploy
  jobs:
  - job: DeployJob
    steps:
    - script: echo "Deploying..."
```

By default, `Test` waits for `Build` to succeed, and `Deploy` waits for `Test` - because each stage implicitly depends on the one directly before it.

### Why use stages?

- Separate concerns (build vs. test vs. deploy)
- Gate deployments with approvals/checks (via environments)
- Run parts of the pipeline in parallel
- Get a clear visual breakdown in the Azure DevOps UI (each stage shows as its own box)

---

## Jobs 

A **job** is a set of steps that run together **on the same agent**. Stages can contain multiple jobs, and by default those jobs run **in parallel** (as long as enough parallel agents/jobs are available in your plan).

```yaml
stages:
- stage: Build
  jobs:
  - job: CompileApp
    steps: 
    - scrips: echo "Compiling app..."

  - job: RunLinter
    steps: 
    - scripts: echo "Running linter..."
```

Here, `CompileApp` and `RunLinter` run **at the same time**, each on its own agent, because neither declares a dependency on the other.

### Job types

| Type | Use case |
|---|---|
| `job` | Standard job running on an agent |
| `deployment` | Special job type for deployments, tied to an **environment**, supports strategies like `runOnce`, `rolling`, `canary` |

---

## Dependencies

By default:
- **Stages** run sequentially (each depends on the one before it).
- **Jobs within a stage** run in parallel (no dependency on each other).

You override these defaults with `dependsOn`.

### Making jobs run in parallel explicitly (default behavior shown for clarity)

```yaml
jobs:
- job: A
  steps:
  - scripts: echo "Job A"

- job: B
  steps:
  - scripts: echo "Job B"
# A and B run in parallel - no dependsOn needed
```

### Making a job wait for another job

```yaml
jobs:
- job: Build
  steps:
  - scripts: echo "Building..."

- job: Test
  dependsOn: Build
  steps:
  - scripts: echo "Testing after build..." 
```

### Fan-out / fan-in pattern

```yaml
jobs:
- job: Build
  steps:
  - scripts: echo "Building..."

- job: TestLinux
  dependsOn: Build
  steps:
  - scripts: echo "Testing on Linux..."

- job: TestWindows
  dependsOn: Build
  steps:
  - scripts: echo "Testing on Windows..."

- job: Package
  dependsOn: 
  - TestLinux
  - TestWindows
  steps:
  - scripts: echo "Packaging after both test jobs finish..."
```

`Build` runs first → `TestLinux` and `TestWindows` run in parallel once `Build` finishes → `Package` waits for **both** to complete.

### Making a stage NOT wait for the previous one

```yaml
stages:
- stage: Build
  jobs:
  - job: BuildJob
    steps:
    - script: echo "Building..."
 
- stage: DeployQA
  dependsOn: Build
  jobs:
  - job: DeployJob
    steps:
    - script: echo "Deploying to QA..."
 
- stage: DeployStaging
  dependsOn: Build   # runs in parallel with DeployQA, not after it
  jobs:
  - job: DeployJob
    steps:
    - script: echo "Deploying to Staging..."
```

Both `DeployQA` and `DeployStaging` depend only on `Build`, so once `Build` finishes, they run **in parallel** with each other.

### Running a stage with no dependency at all

```yaml
stages:
- stage: Build
  jobs:
  - job: BuildJob
    steps:
    - script: echo "Building..."

- stage: Notify
  dependsOn: []   # explicitly no dependency — runs immediately, in parallel with Build
  jobs: 
  - job: NotifyJob
    steps: 
    - script: echo "Sending notification..."
```

---

### Conditions on Stages/Jobs

Combine `dependsOn` with `condition` to control execution based on outcomes, not just ordering.

```yaml
stages:
- stage: Build 
  jobs: 
  - job: BuildJob
    steps:
    - scripts: echo "Buidling..."

- stage: Deploy
  dependsOn: Build
  condition: succeeded('Build')
  jobs:
  - job: DeployJob
    steps:
    - script: echo "Deploying..."

- stage: Notify
  dependsOn: Build
  condition: failed('Build')   # only runs if Build failed
  jobs:
  - job: NotifyJob
    steps:
    - script: echo "Notifying team of failure..."
```

### Common condition functions

| Function | Meaning |
|---|---|
| `succeeded()` | Previous dependency succeeded (default condition if none specified) |
| `failed()` | Previous dependency failed |
| `succeededOrFailed()` | Runs regardless of success/failure, but not if cancelled |
| `always()` | Always runs, even if the pipeline was cancelled |
| `and(...)`, `or(...)` | Combine multiple conditions |

```yaml
- stage: Cleanup
  dependsOn:
  - Build
  - Deploy
  condition: always()
  jobs:
  - job: CleanupJob
    steps:
    - script: echo "Running cleanup no matter what happened..."
```

---