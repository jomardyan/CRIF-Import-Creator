# CRIF-Import-Creator

[![Build and Release](https://github.com/jomardyan/CRIF-Import-Creator/actions/workflows/build-and-release.yml/badge.svg)](https://github.com/jomardyan/CRIF-Import-Creator/actions/workflows/build-and-release.yml)
[![CI Build](https://github.com/jomardyan/CRIF-Import-Creator/actions/workflows/ci.yml/badge.svg)](https://github.com/jomardyan/CRIF-Import-Creator/actions/workflows/ci.yml)


Create an import-ready file for the [CRIF](https://www.crif.pl/) data exchange system.

## Quick Start

### Download
Get the latest release from the [Releases page](https://github.com/jomardyan/CRIF-Import-Creator/releases).

**Recommended:** Download `CRIF-Encrypt-vX.X.X-win-x64-standalone.zip` - No .NET installation required!

### Usage

1. Save your MS Excel export as a Unicode Text file (.txt, .TXT).
2. Drag the exported file onto `CRIF-Encrypt.exe`.
3. Alternatively, you can pass the file path as an argument via the command line:
   ```bash
   CRIF-Encrypt.exe "path/to/your/file.txt"
   ```
4. The program will automatically output a `.gpg` file, which is ready to be imported into the CRIF system.

## 🔧 How It Works
1. The working file's encoding is converted to UTF-8.
2. The `^~` separator is added.
3. The file is converted to a `.DAT` file.
4. The `.DAT` file is packed into a ZIP archive as required.
5. The file is signed and encrypted. The program will prompt you to enter the signing password.


## 📋 Requirements

### For Standalone Versions (Recommended)
- Windows 10/11 (64-bit or 32-bit)
- **Kleopatra (Gpg4win)** must be installed and configured according to CRIF requirements. [Download here.](https://www.gpg4win.org/download.html)

### For Framework-Dependent Version
- **.NET 6 Runtime** must be installed. [Download here.](https://dotnet.microsoft.com/download/dotnet/6.0/runtime)
- **Kleopatra (Gpg4win)** must be installed and configured according to CRIF requirements. [Download here.](https://www.gpg4win.org/download.html)

## Development

### Building from Source
```bash
git clone https://github.com/jomardyan/CRIF-Import-Creator.git
cd CRIF-Import-Creator
dotnet build CRIF-Encrypt/CRIF-Encrypt.csproj --configuration Release
```

### GitHub Actions
This repository includes automated build and release workflows. See [.github/WORKFLOWS.md](.github/WORKFLOWS.md) for details.

## What's New in v17
- Optimized async/await implementation for better performance
- Enhanced error handling with colored console output  
- Centralized configuration management
- Improved file processing performance
- Better user experience and feedback
- 📦 Multiple build targets (standalone and framework-dependent)

## 📝License

MIT License
Copyright 2021 Hayk Jomardyan

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
