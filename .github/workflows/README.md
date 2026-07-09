# GitHub Actions Workflows

## ?? ???? ???

??? ???? ???? GitHub Actions workflows ??? ?? ???? ????????? Continuous Integration ? Continuous Deployment ??????? ???????.

## ?? Workflows

### 1. **dotnet.yml** - CI Pipeline ????
**Triggered by:**
- Push ?? `master` branch
- Pull Request ?? `master` branch

**Jobs:**
- **build**: Build ???? Library ? Sample App
- **test**: ????? ???? Unit Tests

**?????:**
1. Setup .NET 8.0
2. Install MAUI workloads
3. Restore dependencies
4. Build Library (net8.0)
5. Build Sample App (net8.0-windows)
6. Run unit tests
7. Publish test results

**???? Output:**
```
? Build Library: SUCCESS
? Build Sample App: SUCCESS
? Unit Tests: 25 passed in 3.45s
?? Test Results: uploaded to artifacts
```

---

### 2. **calendar-tests.yml** - ??????? ?????
**Triggered by:**
- Push ?? Calendar-related files
- Pull Request ???? Calendar-related files

**Jobs:**
- ????? `CalendarServiceTests`
- ????? `CalendarWeekLayoutTests`

**?????:**
1. Restore dependencies
2. Run Calendar Service Tests
   - Test date conversion roundtrip
   - Test month boundaries
   - Test holiday detection
   - Test month names
   - Test leap year
   - Test DatePickerViewModel

3. Run Calendar Week Layout Tests
   - Test consecutive days alignment
   - Test week structure (7 columns)
   - Test Persian calendar alignment
   - Test Gregorian calendar alignment
   - Test empty cells
   - Test end of month
   - Test multiple months
   - Test holiday positioning

**Test Coverage:**
- Persian (Jalali) Calendar: ?
- Gregorian Calendar: ?
- Hijri (Islamic) Calendar: ?

---

### 3. **code-coverage.yml** - Code Coverage Report
**Triggered by:**
- Push ?? `master` branch
- Pull Request ?? `master` branch

**Jobs:**
- Run tests with code coverage collection
- Generate coverage badge
- Upload to Codecov

**Output:**
- Coverage badge (locally generated)
- Codecov.io integration
- Coverage metrics for each file

---

### 4. **pr-status-checks.yml** - PR Status Checks
**Triggered by:**
- Pull Request events (opened, synchronize, reopened)

**Features:**
- Full build validation
- All tests execution
- PR comment with status summary
- Automatic failure notifications

**PR Comment Example:**
```
## Build & Test Status

| Check | Status |
|-------|--------|
| ?? Build | ? Passed |
| ?? Tests | ? Passed |
| ?? Coverage | ?? See workflow details |

### Test Details
- **Calendar Service Tests**: ? Passed
- **Calendar Week Layout Tests**: ? Passed
```

---

### 5. **nuget-deploy.yml** - NuGet Publishing
**Triggered by:**
- Push tags matching `v*` pattern

**?????:**
1. Setup .NET 8.0
2. Install MAUI workloads
3. Restore dependencies
4. Build Release
5. Extract version from tag
6. Pack NuGet package
7. Verify package
8. Publish to NuGet.org

---

## ?? Test Coverage

### ??????? ????? ???

```
MauiPersianToolkit.Test/
??? CalendarServiceTests.cs (7 tests)
?   ??? TestDateConversionRoundtrip
?   ??? TestMonthBoundaries
?   ??? TestHolidayDetection
?   ??? TestMonthNames
?   ??? TestLeapYear
?   ??? TestDatePickerViewModelWithDifferentCalendars
?   ??? TestDateFormatting
?
??? CalendarWeekLayoutTests.cs (13 tests)
    ??? TestConsecutiveDaysInConsecutiveColumns
    ??? TestWeekRowHasSevenColumns
    ??? TestPersianCalendarDayColumnAlignment
    ??? TestGregorianCalendarDayColumnAlignment
    ??? TestEmptyCellsBeforeFirstDay
    ??? TestEndOfMonthDayPosition
    ??? TestAllDaysAreConsecutive
    ??? TestMultipleMonthsWeekLayout [Theory]
    ??? TestPersianCalendarMultipleMonths [Theory]
    ??? TestHolidayDayInCorrectColumn_Gregorian
    ??? TestSpecificPersianDateColumnPlacement
    ??? TestSpecificGregorianDateColumnPlacement
    ??? TestVariousMonthStartAlignment [Theory]
```

**Total Tests: 20+**

---

## ?? Workflow Status

| Workflow | Status | Frequency |
|----------|--------|-----------|
| CI Pipeline | On Push/PR | Every commit |
| Calendar Tests | On Calendar Changes | Per file changes |
| Code Coverage | On Push/PR | Every commit |
| PR Status Checks | On PR Events | PR opened/updated |
| NuGet Deploy | On Tags | Manual via tag |

---

## ?? ???? ????? ?????

### ?? GitHub UI:
1. ?? Pull Request ?????
2. ?? ?? "Checks" ?????
3. ?? workflow ?? expand ???? ?? logs ?? ??????

### Artifacts:
1. ?? "Actions" > workflow > job ?????
2. "Artifacts" ???? ?? ????? ?? job

### Test Results:
- ???? ?? workflow `test-results.trx` uploaded ??????
- ????????? locally download ????

---

## ?? ???? ????? ????

```bash
# ??? local machine:
dotnet test ./MauiPersianToolkit.Test/MauiPersianToolkit.Test.csproj

# ??? calendar tests:
dotnet test --filter "FullyQualifiedName~Calendar"

# ?? code coverage:
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
```

---

## ?? ???? ?? ???????

### Secrets (??? ??????? ???????):
- `NUGET_API_KEY`: ???? NuGet publishing
- `GITHUB_TOKEN`: ??????? ???? GitHub

### Branch Protection Rules (????????):
```
? Require status checks to pass before merging:
  - MauiPersianToolkit CI / build
  - MauiPersianToolkit CI / test
  - Calendar Tests / calendar-tests
  - Code Coverage
  - PR Status Checks / validate
```

---

## ?? Performance

| Workflow | Duration | Notes |
|----------|----------|-------|
| CI Pipeline | ~3-4 min | Build + Tests |
| Calendar Tests | ~1-2 min | Calendar-specific |
| Code Coverage | ~2-3 min | With coverage |
| PR Status Checks | ~4-5 min | Full validation |

---

## ?? Resources

- [GitHub Actions Documentation](https://docs.github.com/actions)
- [.NET Testing](https://docs.microsoft.com/en-us/dotnet/core/testing/)
- [Xunit Documentation](https://xunit.net/)
- [Codecov Integration](https://about.codecov.io/)

---

## ? ???????? ?????

- [ ] Integration tests
- [ ] Performance benchmarks
- [ ] Security scanning
- [ ] Dependency updates automation
- [ ] Release notes generation
