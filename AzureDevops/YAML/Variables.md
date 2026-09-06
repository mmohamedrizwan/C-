# Variables in Azure DevOps YAML

## What are Predefined Variables?

**Predefined variables** (also called **system variables**) are built-in variables that Azure Pipelines automatically populates for every pipeline run. You never declare them yourself - they exist the moment your pipeline starts, and you can reference them in scripts, tasks, conditions, and templates.

You **do not need to define these variables yourself**.

They carry contextual information grouped roughly into these categories:

| Category | Examples of info provided |
|---|---|
| Build | Build ID, build number, reason the build was triggered |
| Repository | Repo name, repo provider, repo URI |
| Branch | Source branch, target branch (for PRs) |
| Commit | Commit SHA that triggered the run |
| Agent | Agent name, OS, working directory, temp directory |
| Pipeline | Pipeline ID, pipeline name, workspace |
| Directories | Paths like `Build.SourcesDirectory`, `Agent.BuildDirectory` |
| Pull Requests | PR ID, PR source/target branch |
| Deployment | Environment name, resource name (in deployment jobs) |

Because they're system-managed, their *values* change per run, but their *names* stay fixed - so you can rely on them the same way across every pipeline.

---

## Why Three Syntaxes Exist

Azure Pipelines processes YAML in **stages**, not all at once. Depending on *when* a value needs to be known, you use a different syntax:

| Stage | When it happens | Syntax used |
|---|---|---|
| 1. Template/compile time | Before the pipeline is even queued — while YAML templates are being expanded into one final YAML file | `${{ }}` |
| 2. Runtime (pre-task) | Right before an individual task executes, using values known by then | `$( )` |
| 3. Runtime (task execution) | *During* task execution, evaluated by the agent as the task runs | `$[ ]` |

---

## Variable Syntaxes (With Examples)

### 1. Macro Syntax - `$(Variable.Name)`

The most commonly used syntax. Azure Pipelines does a **find-and-replace as plain text** just before a task runs, swapping `$(Variable.Name)` for its actual value.

**Where it works:** task inputs, `displayName`, `script`, most string-valued YAML properties.

**Where it does *not* work:** inside `condition:` blocks, or the `variables:` block's own definitions — those need runtime expression syntax instead.

```yaml
steps:
- script: echo "Building commit $(Build.SourceVersion)"
  displayName: 'Show commit $(Build.SourceVersion)'
```

Because it's a literal text substitution, if the variable doesn't exist, Azure DevOps just leaves string `$(Variable.Name)` in place instead of failing - a common source of silent bugs.

---

### 2. Template Expression Syntax - `${{ variables.VariableName }}`

Used for **compile-time evaluation** - resolved when Azure DevOps expands your YAML (including any templates you import) into the final pipeline definition, **before any job or step actually runs**.

Because it's resolved so early, template expressions are ideal for:
- Deciding *which steps/jobs exist at all* (structural decisions)
- Parameterizing reusable templates
- Values that don't depend on anything that happens during the run

```yaml
variables:
  environment: 'production'

steps:
- script: echo "Environment: ${{ variables.environment }}"
```

```yaml
# Using template expressions to conditionally INCLUDE a step
# (this only works at compile time, not with $() or $[])
parameters:
- name: runTests
  type: boolean
  default: true

steps:
- ${{ if eq(parameters.runTests, true) }}:
  - script: echo "Running tests..."
```

**Key limitation:** template expressions can only see variables that are known *before* the pipeline runs (parameters, statically-defined `variables:` blocks). They **cannot** see variables set dynamically during a run (e.g., via `##vso[task.setvariable]`), because those don't exist yet at compile time.

---

### 3. Runtime Expression Syntax - `$[ variables.VariableName ]`

Resolved **at runtime, while the pipeline is executing** - later than macro syntax substitution, and typically used in places where an *expression* (not just plain text) is expected.

**Where it's required:** `condition:` blocks, and setting variable values that depends on other runtime logics.

```yaml
variables:
  isMain: $[eq(variables['Build.SourceBranch'], 'ref/heads/main')]

steps:
- script: echo "Deploying to production"
  condition: eq(variables.isMain, true)
```

```yaml
steps:
- script: echo "##vso[task.setvariable variable=myVar]HelloFromScript"
- script: echo "$(myVar)"   # works in a LATER step, not the same one
```

> **Note:** a variable set with `task.setvariable` in one step is available via macro syntax `$(myVar)` in **later steps**, not the same step it was set in — this trips a lot of people up.

---

## Quick Comparison Table

| Syntax | Evaluated | Typical use | Sees runtime-set variables? |
|---|---|---|---|
| `$(Variable.Name)` | Just before task runs | Script commands, task inputs, `displayName` | Yes (from earlier steps) |
| `${{ variables.Name }}` | Compile time (before pipeline starts) | Templates, conditionally including steps/jobs, parameters | No |
| `$[ variables.Name ]` | Runtime, during expression evaluation | `condition:`, computed variable values | Yes |

---

## 10. Custom (User-Defined) Variables

Alongside predefined variables, you can declare your own variables directly in the YAML file. These behave like ordinary key-value pairs and can be overriden at queue time (unless locked down).

```yaml
variables:
  buildConfiguration: 'Release'
  appName: 'MyWebApp'

steps:
- script: echo "Building $(appName) in $(buildConfiguration) mode"
```

### Scoping variables to a stage or job

Variables can be defined at the **pipeline**, **stage**, or **job** level. A variable defined at a narrower scope overrides one defined at a wider scope for that scope only.

```yaml
variables:
  globalVar: 'from pipeline'

stages:
- stage: Build
  variables:
    stageVar: 'from stage'
  jobs:
  - job: BuildJob
    variables:
      jobVar: 'from job'
    steps:
    - script: echo "$(globalVar) | $(stageVar) | $(jobVar)"
```

### Setting a variable from within a script

Use the `task.setvariable` logging command to set a variable during a run. Remember — it becomes available starting the **next** step.

```yaml
steps:
- bash: |
    echo "##vso[task.setvariable variable=myVar]HelloWorld"
  displayName: 'Set Variable'

- script: echo "Value is $(myVar)"
  displayName: 'Use variable (next step)'
```

To make a variable available to **later stages/jobs** (not just later steps), mark it as an output variable:

```yaml
jobs:
- job: JobA
  steps: 
  - bash: echo "##vso[task.setvariable variable=myOutputVar;isOutput=true]HelloFromJobA"
    name: setvarStep

- job: JobB
  dependsOn: JobA
  variables:
    passedVar: $[ dependencies.JobA.outputs['setvarStep.myOutputVar'] ]
  steps:
  - script: echo "Received: $(passedVar)"
```

---

# 11. Variable Groups

**Variable groups** let you share values (including secrets) across multiple pipelines. They're created in **Pipelines -> Library** in the Azure DevOps portal and linked into a pipeline's YAML.

```yaml
variables:
- group: my-shared-variable-group

steps:
- script: echo "Shared value is $(sharedVariableName)"
```

You can combine a variable group with pipeline-specific variables:

```yaml
variables:
- group: my-shared-variable-group
- name: localVar
  value: 'only for this pipeline'

steps:
- script: echo "$(sharedVariableName) and $(localVar)"
```

---

## 12. Secret Variables

Secrets (API keys, passwords, tokens) should never be hardcoded in YAML. Instead, mark them as **secret** in the pipeline settings UI or a variable group, and they'll be encrypted at rest and masked in logs.

- Secret variables are **not** available via `${{}}` template expressions (compile-time) - for security, secrets only exist at runtime.
- They **must** be explicitly mapped to an environment variable to be used in a script:

```yaml
steps:
- script: echo "Using secret: $(mySecret)"
  env:
    mySecret: $(mySecretVariable)
```

Azure DevOps automatically masks secret values in the pipeline logs, replacing them with '***'.

---

## 13. Full Worked Example

Putting it all together - predefined variables, custom variables, a condition, and a variable group:

```yaml
trigger:
- main

variables:
- group: release-secrets
- name: buildConfiguration
  value: 'Release'
- name: isMain
  value: $[eq(variables['Build.SourceBranch'], 'refs/heads/main')]

pool:
  vmImage: 'ubuntu-latest'

steps:
- script: |
    echo "Pipeline: $(Build.DefinitionName)"
    echo "Build ID: $(Build.BuildId)"
    echo "Branch: $(Build.SourceBranch)"
    echo "Config: $(buildConfiguration)"
  displayName: 'Show build info'

- script: echo "Deploying to Production"
  displayName: 'Deploy'
  condition: eq(variables.isMain, true)
  env:
    deployKey: $(mySecretFromGroup)
```

---

## Summary Cheat Sheet

| Concept | Key point |
|---|---|
| Predefined variables | Built-in, no declaration needed, cover build/repo/agent/PR info |
| Macro `$( )` | Text substitution, resolved just before a task runs |
| Template `${{ }}` | Compile-time only, can't see secrets or runtime-set values |
| Runtime `$[ ]` | Used in `condition:`, resolved as the pipeline executes |
| Custom variables | Declared in `variables:` block, can be scoped to pipeline/stage/job |
| `task.setvariable` | Sets a variable mid-run; visible from the *next* step onward |
| Output variables | Pass a variable across jobs/stages via `dependencies.<job>.outputs[...]` |
| Variable groups | Shared variables/secrets managed in Library, linked via `group:` |
| Secret variables | Encrypted, masked in logs, unavailable at compile time |