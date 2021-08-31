BUILDS: 
Azure Pipelines: [![Build Status](https://jomardyan.visualstudio.com/jomardyan/_apis/build/status/jomardyan.CRIF-Encrypt?branchName=master)](https://jomardyan.visualstudio.com/jomardyan/_build/latest?definitionId=6&branchName=master) 

Windows: [![.NET](https://github.com/jomardyan/CRIF-Encrypt/actions/workflows/dotnetWindows.yml/badge.svg)](https://github.com/jomardyan/CRIF-Encrypt/actions/workflows/dotnetWindows.yml) , Ubuntu: [![.NET](https://github.com/jomardyan/CRIF-Encrypt/actions/workflows/dotnetUbuntu.yml/badge.svg)](https://github.com/jomardyan/CRIF-Encrypt/actions/workflows/dotnetUbuntu.yml)
 
Create CRIF compatible import file according documentation using EXCEL Unicode export.

**Instruction**

1. **Save Ms Excel export file as Unicode Text (.txt, .TXT)**
2. **Drag Exported file onto CRIF-Encrypt.exe**
  1. **It&#39;s also possible pass the file path as an argument using CL.**
3. **Program automatically output .gpg file, which is ready to import by CRIF system.**

**How the program work**

1. **Connvert encoding into UTF-8**
2. **Add ^~ separator**
3. **Convert to .DAT file**
4. **Packing into ZIP Archive**
5. **Sign and encrypt (program will ask you to type sign password)**
6. **More features soon...**

**Program run requirement**

1. **Have installed and configured** [**Kleopatra (Gpg4win)**](https://www.gpg4win.org/download.html) **according CRIF requirements**
2. **Have Installed** [**DOT.NET 5 Runtime**](https://dotnet.microsoft.com/download/dotnet/5.0/runtime)
