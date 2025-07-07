# Antivirus False Positive Information

## Why is CRIF-Encrypt flagged as a virus?

CRIF-Encrypt is a legitimate file processing and encryption tool that may be flagged by antivirus software due to its functionality:

### Behaviors that trigger antivirus detection:
- **File encryption operations**: The application uses GPG to encrypt files
- **File system operations**: Creates, reads, writes, and deletes files and directories
- **Process execution**: Calls external GPG executable for encryption
- **Archive creation**: Creates ZIP files from processed data

### This is a FALSE POSITIVE - the application is safe

## Solutions:

### 1. Add to Antivirus Exclusions (Recommended)
- **Windows Defender**: Run `Add-DefenderExclusion.ps1` as Administrator
- **Other Antivirus**: Add the CRIF-Encrypt folder to your antivirus exclusions

### 2. Digital Signature Verification
- Check the digital signature of the executable (if signed)
- Verify the publisher information matches "Hayk Jomardyan" or "JHC"

### 3. Source Code Verification
- This is open-source software: https://github.com/jomardyan/CRIF-Import-Creator
- You can review the source code and compile it yourself

## Technical Details:
- **Application**: CRIF Import Creator v17
- **Purpose**: Process and encrypt CRIF import files for secure transmission
- **Technology**: .NET 6.0 C# Console Application
- **Dependencies**: GPG (GNU Privacy Guard) for encryption

## If you still have concerns:
1. Review the source code on GitHub
2. Scan with multiple antivirus engines (VirusTotal)
3. Run in a sandboxed environment first
4. Contact the developer: https://github.com/jomardyan

**This software is provided as-is and is used for legitimate business file processing.**
