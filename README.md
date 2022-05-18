BUILDS: 
Azure Pipelines: [![Build Status](https://jomardyan.visualstudio.com/jomardyan/_apis/build/status/jomardyan.CRIF-Encrypt?branchName=master)](https://jomardyan.visualstudio.com/jomardyan/_build/latest?definitionId=6&branchName=master) 

Windows: [![.NET](https://github.com/jomardyan/CRIF-Encrypt/actions/workflows/dotnetWindows.yml/badge.svg)](https://github.com/jomardyan/CRIF-Encrypt/actions/workflows/dotnetWindows.yml) , Ubuntu: [![.NET](https://github.com/jomardyan/CRIF-Encrypt/actions/workflows/dotnetUbuntu.yml/badge.svg)](https://github.com/jomardyan/CRIF-Encrypt/actions/workflows/dotnetUbuntu.yml)
 
Create import file for CRIF, according compatible documentation.

**Instruction**

1. **Save MS Excel export file as Unicode Text (.txt, .TXT)**
2. **Drag Exported file onto CRIF-Encrypt.exe**
  1. **It&#39;s also possible pass the file path as an argument using CLI.**
3. **Program automatically output .gpg file, which is ready to import by CRIF system.**

**How the program work**

1. **Convert working file encoding into UTF-8**
2. **Add ^~ separator**
3. **Convert to .DAT file**
4. **Packing into ZIP Archive as it is required**
5. **Sign and encrypt (program will ask you to type sign password)**
6. **More features coming soon...**

**Program run requirement**

1. **Have installed and configured** [**Kleopatra (Gpg4win)**](https://www.gpg4win.org/download.html) **according CRIF requirements**
2. **Have Installed** [**DOT.NET 6 Runtime**](https://dotnet.microsoft.com/download/dotnet/6.0/runtime)


MIT License
Copyright 2021 Hayk Jomardyan

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.