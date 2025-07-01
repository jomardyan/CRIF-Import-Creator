# GitHub Actions Workflows

This repository includes several GitHub Actions workflows for automated building, testing, and releasing of the CRIF-Encrypt application.

## 🔄 Available Workflows

### 1. **CI Build** (`ci.yml`)
- **Trigger:** Every push/PR to master/main branch
- **Purpose:** Continuous integration validation
- **Outputs:** Quick validation build
- **Retention:** 3 days

### 2. **Build and Release** (`build-and-release.yml`)
- **Trigger:** 
  - Push to master/main
  - Git tags starting with `v*`
  - Manual trigger
- **Purpose:** Full build with multiple targets and automatic releases
- **Outputs:** 
  - Framework-dependent (requires .NET 6.0)
  - Self-contained x64 (standalone)
  - Self-contained x86 (standalone)
- **Retention:** 30 days

### 3. **Manual Release** (`manual-release.yml`)
- **Trigger:** Manual workflow dispatch only
- **Purpose:** Create custom releases with specific versions
- **Features:**
  - Custom version input
  - Optional debug builds
  - Pre-release option
  - Automatic git tagging
- **Retention:** 90 days

## 🚀 Creating a Release

### Automatic Release (Recommended)
1. Create and push a git tag:
   ```bash
   git tag v1.0.4
   git push origin v1.0.4
   ```
2. The workflow will automatically build and create a GitHub release

### Manual Release
1. Go to **Actions** tab in GitHub
2. Select **Manual Release** workflow
3. Click **Run workflow**
4. Fill in the parameters:
   - **Version**: e.g., `1.0.4`
   - **Create git tag**: ✅ (recommended)
   - **Pre-release**: ❌ (for stable releases)
   - **Include debug**: ❌ (unless needed for troubleshooting)
5. Click **Run workflow**

## 📦 Build Outputs

Each workflow produces different build artifacts:

### Release Builds
- **Framework Dependent** (`win-x64-framework-dependent.zip`)
  - Requires .NET 6.0 Runtime
  - Smaller file size
  - Best for environments with .NET already installed

- **Standalone x64** (`win-x64-standalone.zip`) ⭐ **Recommended**
  - No .NET installation required
  - 64-bit Windows systems
  - Single executable file

- **Standalone x86** (`win-x86-standalone.zip`)
  - No .NET installation required
  - 32-bit compatibility
  - Larger file size

### Debug Builds (Manual Release Only)
- **Debug Standalone** (`win-x64-standalone-debug.zip`)
  - Includes debug symbols
  - For troubleshooting issues
  - Larger file size

## 🛠️ Workflow Features

### Build Optimizations
- **Single File Publishing**: All dependencies bundled into one executable
- **Trimming**: Removes unused code to reduce file size
- **Native Libraries**: Includes native dependencies for self-contained builds

### Quality Assurance
- **Multi-target builds**: Ensures compatibility across different environments
- **Artifact validation**: Verifies executable creation
- **Error handling**: Continues on test failures (tests are optional)

### Release Management
- **Automatic versioning**: Reads version from project file
- **Comprehensive release notes**: Includes download options and system requirements
- **Artifact retention**: Different retention periods based on workflow type

## 🔧 Configuration

### Environment Variables
- `DOTNET_VERSION`: .NET SDK version (currently 6.0.x)
- `PROJECT_PATH`: Path to the main project file
- `BUILD_CONFIGURATION`: Build configuration (Release/Debug)

### Customization
To modify the workflows:

1. **Change .NET version**: Update `DOTNET_VERSION` in workflow files
2. **Add new target platforms**: Add additional `dotnet publish` commands
3. **Modify retention**: Change `retention-days` values
4. **Update build configurations**: Modify `BUILD_CONFIGURATION` or add new configurations

## 📋 Prerequisites

### Repository Setup
- Repository must contain the CRIF-Encrypt project
- `CRIF-Encrypt/CRIF-Encrypt.csproj` file must exist
- Repository must have Actions enabled

### Permissions
- `GITHUB_TOKEN` is automatically provided
- Workflows need write permissions for releases (usually enabled by default)

## 🐛 Troubleshooting

### Common Issues

1. **Build Failures**
   - Check .NET version compatibility
   - Verify project file path
   - Review build logs in Actions tab

2. **Release Creation Failures**
   - Ensure repository has release permissions
   - Check if tag already exists
   - Verify `GITHUB_TOKEN` permissions

3. **Artifact Upload Issues**
   - Check file paths in workflow
   - Verify artifact naming conventions
   - Review storage quota limits

### Debug Steps
1. Check the **Actions** tab for detailed logs
2. Review individual step outputs
3. Download artifacts to verify build output
4. Use manual release with debug builds if needed

## 🔄 Workflow Status

You can check the status of workflows:
- **Repository main page**: Shows workflow badges
- **Actions tab**: Detailed workflow run history
- **Pull requests**: CI status checks
- **Releases page**: Automatic releases from successful builds
