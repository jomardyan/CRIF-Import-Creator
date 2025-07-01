BUILDS:
Azure Pipelines: [![Build Status](https://jomardyan.visualstudio.com/jomardyan/_apis/build/status/jomardyan.CRIF-Encrypt?branchName=master)](https://jomardyan.visualstudio.com/jomardyan/_build/latest?definitionId=6&branchName=master)

Windows: [![.NET](https://github.com/jomardyan/CRIF-Encrypt/actions/workflows/dotnetWindows.yml/badge.svg)](https://github.com/jomardyan/CRIF-Encrypt/actions/workflows/dotnetWindows.yml), Ubuntu: [![.NET](https://github.com/jomardyan/CRIF-Encrypt/actions/workflows/dotnetUbuntu.yml/badge.svg)](https://github.com/jomardyan/CRIF-Encrypt/actions/workflows/dotnetUbuntu.yml)

Create an import-ready file for the [CRIF](https://www.crif.pl/) data exchange system.

**Instructions:**

1. Save your MS Excel export as a Unicode Text file (.txt, .TXT).
2. Drag the exported file onto `CRIF-Encrypt.exe`.
3. Alternatively, you can pass the file path as an argument via the command line.
4. The program will automatically output a `.gpg` file, which is ready to be imported into the CRIF system.

**How the program works:**
1. The working file's encoding is converted to UTF-8.
2. The `^~` separator is added.
3. The file is converted to a `.DAT` file.
4. The `.DAT` file is packed into a ZIP archive as required.
5. The file is signed and encrypted. The program will prompt you to enter the signing password.

More features will be added soon...

**Program requirements:**

1. **Kleopatra (Gpg4win)** must be installed and configured according to CRIF requirements. [Download here.](https://www.gpg4win.org/download.html)
2. **.NET 6 Runtime** must be installed. [Download here.](https://dotnet.microsoft.com/download/dotnet/6.0/runtime)

---

MIT License
Copyright 2021 Hayk Jomardyan

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
