# AzureWebApp@1 — Azure Web App v1 Task

**Category:** Deploy
**Purpose:** Deploys an Azure Web App for Linux or Windows.

---

## Overview

`AzureWebApp@1` is the simplest Azure Pipelines task for deploying code to an Azure App Service (Web App). By default, deployment goes to the **root application** of the Web App. For more advanced scenarios (XML parameter transforms, Web Deploy, Function Apps, WebJobs, API/Mobile apps, etc.), use `AzureRmWebAppDeployment` (`AzureRmWebAppDeployment@4`) instead.

---

## YAML Syntax

```yaml
# Azure Web App v1
# Deploy an Azure Web App for Linux or Windows.
- task: AzureWebApp@1
  inputs:
    azureSubscription: # string. Required. Azure subscription.
    appType: # 'webApp' | 'webAppLinux'. Required. App type.
    appName: # string. Required. App name.
    # Additional Deployment Options
    #deploymentMethod: 'auto' # 'auto' | 'zipDeploy' | 'runFromPackage'. Default: auto.
```

---

## Inputs

| Input | Type | Required? | Default | Description |
|---|---|---|---|---|
| `azureSubscription` | string | ✅ Required | — | The Azure Resource Manager service connection to use for deployment. |
| `appType` | string (`webApp` \| `webAppLinux`) | ✅ Required | — | Whether the target is a Windows Web App or a Linux Web App. |
| `appName` | string | ✅ Required | — | Name of an existing Azure App Service. Only apps matching the selected `appType` are listed in the UI. |
| `package` | string | ✅ Required | `$(System.DefaultWorkingDirectory)/**/*.zip` | Path to the package/folder to deploy — a zip, a war file, or an MSBuild-generated folder. Supports variables and wildcards. |
| `customDeployFolder` | string | Optional (used when package ends with `.war`) | — | Custom deploy path for a `.war` file. Empty → deploys to `/`; `ROOT` → deploys to app root; else → deploys under a custom name. |
| `deploymentMethod` | string (`auto` \| `zipDeploy` \| `runFromPackage`) | Required unless Linux/`.war`/`.jar` | `auto` | Controls how the package is pushed to the App Service. |

---

## Deployment Methods (how `auto` decides)
 
When `deploymentMethod: 'auto'` (the default) is used, the task doesn't rely on one fixed method — it looks at three things and picks the best fit:
 
1. **App type** — Windows Web App vs Linux Web App
2. **Package type** — `.zip`, `.war`, `.jar`, or a plain folder
3. **Agent OS** — Windows vs Linux/Mac

### The three underlying technologies

These are the three ways files can actually get pushed to Azure. They all get your code onto the App Service, but they work differently:

**1. Kudu REST APIs**
Kudu is App Service's built-in deployment engine (the same engine behind the `.scm.azurewebsites.net` "advanced tools" site). The task talks to it over REST to push files directly.
This is the **fallback whenever the pipeline agent itself is not Windows** — some other methods (like traditional Web Deploy/MSDeploy) work best from Windows agents, so Kudu is the universal option that works cross-platform regardless of app type.

**2. Zip Deploy**
The task zips your package/folder (if it isn't already a zip) and pushes the whole zip to Azure. Azure then **extracts the contents into the `wwwroot` folder**, replacing whatever was there before.
This is the standard method for **Linux Web Apps**, and also the general fallback for anything that doesn't match a more specific case.

**3. RunFromPackage**
Very similar to Zip Deploy - it still builds a zip package - but instead of extracting it into `wwwroot`, Azure **mounts the zip itself as the site's content, read-only**. The app runs directly out of the package: no partial-extraction downtime, and `wwwroot` becomes read-only (safer against tampering, but you can't write into it at afterward).
This is used automatically when deploying a `.jar` file.

### How `auto` chooses, in order

```
Is appType webAppLinux?        → Zip Deploy
Is the package a .war file?    → War Deploy (a Zip/Kudu-based variant for Java web archives)
Is the package a .jar file?    → RunFromPackage
None of the above?             → Zip Deploy ("Run From Zip")
Is the agent OS not Windows?   → Kudu REST APIs (overrides the above, since this depends on the agent, not the app)
```

In plain English:
- **Linux Web App?** → Zip Deploy, basically always.
- **Windows Web App with a normal zip/folder?** → Zip Deploy.
- **Deploying a `.war` (Java web archive)?** → Pushed as a War Deploy.
- **Deploying a `.jar` (standalone Java app)?** → RunFromPackage — Java apps often run better mounted as a package.
- **Pipeline agent is Linux or Mac, regardless of app type?** → Kudu REST APIs, since it's the one method guaranteed to work cross-platform.

### Why the choice matters

- **Zip Deploy** briefly makes site files inconsistent while old ones are replaced with new ones, and it physically overwrites everything in `wwwroot`.
- **RunFromPackage** avoids that swap-over risk by mounting the package atomically, but locks `wwwroot` as read-only afterward — so if some other process expects to write into the site folder at runtime, it will break.
- **Kudu** is about *how* files are transferred (protocol-level), not *where* they end up — it's the compatibility fallback tied to agent OS.If you don't want `auto` deciding, force a method explicitly:
 
```yaml
deploymentMethod: zipDeploy       # or
deploymentMethod: runFromPackage
```

---