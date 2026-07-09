# Branch Protection Configuration

## ?? ???? ????? Branch Protection Rules

### ?????:
1. ?? GitHub repository ?????
2. Settings > Branches
3. "Add rule" ???? ????
4. Branch name pattern: `master`

---

## ? ????? ??? Settings

### 1. Require Status Checks to Pass Before Merging

**Status checks to require:**
- `MauiPersianToolkit CI / build`
- `MauiPersianToolkit CI / test`
- `Calendar Tests / calendar-tests`
- `Code Coverage / coverage`
- `PR Status Checks / validate`

```json
{
  "required_status_checks": {
    "strict": true,
    "contexts": [
      "MauiPersianToolkit CI / build",
      "MauiPersianToolkit CI / test",
      "Calendar Tests / calendar-tests",
      "PR Status Checks / validate"
    ]
  }
}
```

### 2. Require Code Reviews

```json
{
  "required_pull_request_reviews": {
    "dismiss_stale_reviews": true,
    "require_code_owner_reviews": false,
    "required_approving_review_count": 1
  }
}
```

### 3. Require Branches to be Up to Date

```json
{
  "require_branches_to_be_up_to_date": true
}
```

### 4. Require Status Checks to be Up to Date

```json
{
  "require_status_checks_strict": true
}
```

---

## ?? ??????? ?????

### Dismiss Stale Pull Request Approvals
- ? Enable: ??? ?? push ????? approvals ????? dismiss ????

### Require Code Owner Approval
- ? Disable (optional): ??? ????? CODEOWNERS ???? ?????

### Restrict Who Can Push
- ? Enable: ??? admins ????????? push ???? (optional)

### Require Conversation Resolution
- ? Enable: ???? comments ???? resolve ????

### Require Signed Commits
- ? Optional: ??? GPG signing ?????

---

## ?? Using the GitHub UI

### Step-by-Step:

1. Go to Settings ? Branches
2. Click "Add Rule"
3. Enter `master` in "Branch name pattern"
4. Enable:
   - ?? Require a pull request before merging
   - ?? Require approvals
   - ?? Dismiss stale pull request approvals when new commits are pushed
   - ?? Require status checks to pass before merging
   - ?? Require branches to be up to date before merging
   - ?? Require conversation resolution before merging

5. Select Status Checks:
   - ?? MauiPersianToolkit CI / build
   - ?? MauiPersianToolkit CI / test
   - ?? Calendar Tests / calendar-tests
   - ?? PR Status Checks / validate

6. Click "Create"

---

## ?? Checklist ???? PR

????? ????? PR? ????? ???? ??:

- [ ] ???? tests passed ????? (GitHub UI show ??????)
- [ ] build ???? ???
- [ ] calendar tests ??? ??? ???????
- [ ] code coverage ???? ?? ????? ?? ??? ???
- [ ] PR description ???? ???
- [ ] commits ??????? ????????? ???????

---

## ?? Merge Process

```
1. Create PR
   ?
2. GitHub Actions trigger automatically
   ??? CI Pipeline runs
   ??? Calendar Tests run
   ??? Code Coverage runs
   ??? PR Status Checks run
   ?
3. All checks PASS (green checkmarks)
   ?
4. Request code review
   ?
5. Review approved
   ?
6. Merge when ready
   ?
7. Delete branch (GitHub offers this)
```

---

## ?? Expected Results

### ? All Checks Pass:
```
MauiPersianToolkit CI / build ?
MauiPersianToolkit CI / test ?
Calendar Tests / calendar-tests ?
PR Status Checks / validate ?
Code Coverage ?
Reviews ? (if required)
```

### ? If Any Check Fails:
```
The "Merge" button will be disabled
Red X will show on the PR
Details will be in Actions tab
```

---

## ?? Troubleshooting

### Build Failed
- Check the "MauiPersianToolkit CI / build" logs
- Usually due to compilation errors
- Fix the code and push again

### Tests Failed
- Check "MauiPersianToolkit CI / test" logs
- See which specific tests failed
- Run locally: `dotnet test`

### Status Check Pending
- Wait for all jobs to complete
- Check "Actions" tab for progress
- Status checks usually complete within 5-10 minutes

### Status Check Missing
- Ensure the branch protection rule references the correct check names
- Check .github/workflows/dotnet.yml for the exact job name

---

## ?? For Repository Maintainers

### Recommended Settings:

```yaml
Branch: master
Rules:
  - Require status checks to pass: YES
    - require_latest: YES
    - Checks:
      - MauiPersianToolkit CI / build
      - MauiPersianToolkit CI / test
      - Calendar Tests / calendar-tests
      - PR Status Checks / validate
  
  - Require reviews: YES
    - number: 1
    - dismiss_stale: YES
  
  - Require conversation resolution: YES
  - Allow force pushes: NO
  - Allow deletions: NO
  - Require linear history: YES (optional)
```

---

## ?? Support

??? ????? ??????:
1. GitHub Actions logs ?? ????? ????
2. Local build ?? test ????: `dotnet build`
3. Tests ?? local ???? ????: `dotnet test`
4. Issue ??? ???? ??:
   - Workflow name
   - Job name
   - Error message
   - Git commit hash
