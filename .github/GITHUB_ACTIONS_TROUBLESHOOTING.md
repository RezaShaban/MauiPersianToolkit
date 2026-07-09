# GitHub Actions Troubleshooting Guide

## ?? ?? ?????? GitHub Actions

### ? ????: NETSDK1112 Runtime Pack

```
Error: The runtime pack for Microsoft.NETCore.App.Runtime.win-x64 was not downloaded
```

#### ? ??:

**????:**
- MAUI ???? Windows ???? ?? runtime pack ????
- Platform-specific build ???? ?? RuntimeIdentifier ????

**?????? ????? ????? ???:**

1. **dotnet.yml**
```yaml
- name: Restore with RuntimeIdentifier
  run: dotnet restore -r win-x64

- name: Build Sample App (Windows)
  run: dotnet build ./PersianUISamples/PersianUISamples.csproj `
    -c Debug `
    --no-restore `
    -f net8.0-windows10.0.19041.0 `
    -r win-x64
```

2. **pr-status-checks.yml**
```yaml
- name: Restore with RuntimeIdentifier
  run: dotnet restore -r win-x64

- name: Build Library
  run: dotnet build ./MauiPersianToolkit/MauiPersianToolkit.csproj `
    -c Release `
    --no-restore `
    -f net8.0
```

3. **calendar-tests.yml**
```yaml
# No runtime pack needed (test project only)
- name: Restore dependencies
  run: dotnet restore
```

---

### ?? ????? ????

| Workflow | Runtime | Status |
|----------|---------|--------|
| dotnet.yml | `win-x64` | ? Fixed |
| pr-status-checks.yml | Library only (net8.0) | ? Fixed |
| calendar-tests.yml | Test only | ? No runtime needed |
| code-coverage.yml | Test only | ? No runtime needed |

---

## ?? Workflow ?????? ??????

### Q: ??? Sample App build ????? ???????
**A:** Sample App ???? Windows ???? ?? `win-x64` RuntimeIdentifier ????.
- ? ??: ????? ???? `-r win-x64` ?? build command

### Q: ??? Test project ?? Runtime ???? ?????
**A:** ???. Test project ??? `net8.0` ??? ? runtime-specific ????.
- ? ?????: ??? library build ????? ?? sample app

### Q: ??? code coverage ???? ??? ???????
**A:** ???. ????? ???? MSBuild properties ???? coverage collection.
- ? ?????: ????? ???? summary display

### Q: ????? test results ??????
**A:** Artifacts ? Test Reporter page.
- ? ?????: dorny/test-reporter action

---

## ? ??????? ????? ???

### 1. dotnet.yml
```diff
+ - name: Restore with RuntimeIdentifier
+   run: dotnet restore -r win-x64

+ - name: Install MAUI workloads
+   (added in test job)
```

### 2. pr-status-checks.yml
```diff
+ - name: Restore with RuntimeIdentifier
+   run: dotnet restore -r win-x64

+ - name: Build Library
+   (separate from Sample App)

- Build (both projects)
+ Build Library (net8.0 only)
```

### 3. calendar-tests.yml
```diff
- matrix (removed)
+ Test logging (added)
+ Test Reporter (added)
```

### 4. code-coverage.yml
```diff
- Install Coverlet Tool
+ Use MSBuild properties

+ Display Coverage Summary
```

---

## ?? ???? ??? ???? ????

```bash
# ???? RuntimeIdentifier
dotnet restore
dotnet build ./MauiPersianToolkit/MauiPersianToolkit.csproj -f net8.0

# ?? RuntimeIdentifier
dotnet restore -r win-x64
dotnet build ./PersianUISamples/PersianUISamples.csproj -f net8.0-windows10.0.19041.0 -r win-x64

# Tests
dotnet test ./MauiPersianToolkit.Test/MauiPersianToolkit.Test.csproj

# With Coverage
dotnet test ./MauiPersianToolkit.Test/MauiPersianToolkit.Test.csproj `
  /p:CollectCoverage=true `
  /p:CoverletOutputFormat=cobertura `
  /p:CoverletOutput=./coverage/
```

---

## ?? Workflow Status

| Workflow | Status | Notes |
|----------|--------|-------|
| MauiPersianToolkit CI | ? Fixed | Build + Test |
| PR Status Checks | ? Fixed | Validation + Comments |
| Calendar Tests | ? Fixed | Calendar-specific |
| Code Coverage | ? Fixed | Coverage reporting |

---

## ?? ??? ????? ???? ???? ????:

1. **Check workflow logs:**
   - GitHub Actions ? workflow name ? failed step

2. **Common issues:**
   - ? `NETSDK1112` ? ????? ???? `-r win-x64`
   - ? `dotnet workload` ? ??????? ?? MAUI installation
   - ? `test not found` ? check filter syntax

3. **Debugging locally:**
   ```bash
   # Exactly match CI commands
   dotnet workload install maui
   dotnet restore
   dotnet restore -r win-x64
   dotnet build ...
   dotnet test ...
   ```

---

## ?? ???? ???

- ? ????? `--no-restore` ??????? ???? ??? ?? restore
- ? `-f net8.0` ???? library (platform-agnostic)
- ? `-f net8.0-windows10.0.19041.0 -r win-x64` ???? Sample App
- ? Test project ??? RuntimeIdentifier ???? ?????
- ? Coverage collection ??????? ???

---

## ?? ????? ?????? ????

```
? dotnet.yml: Build + Test successful
? pr-status-checks.yml: PR validation successful
? calendar-tests.yml: Calendar tests passed
? code-coverage.yml: Coverage reported
```

Happy Building! ??
