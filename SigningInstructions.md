# Code Signing Instructions

## Option 1: Commercial Certificate
1. Purchase a code signing certificate from a trusted CA (DigiCert, GlobalSign, etc.)
2. Sign your executable using SignTool.exe:
   ```
   signtool.exe sign /f "certificate.p12" /p "password" /t http://timestamp.digicert.com CRIF-Encrypt.exe
   ```

## Option 2: Self-Signed Certificate (for testing)
1. Create a self-signed certificate:
   ```
   New-SelfSignedCertificate -Subject "CN=YourCompany" -Type CodeSigning -KeySpec Signature -KeyUsage DigitalSignature -FriendlyName "Code Signing" -CertStoreLocation Cert:\CurrentUser\My
   ```
2. Export and sign the executable

## Option 3: Extended Validation (EV) Certificate
- Most trusted by antivirus software
- No warnings on first download
- Higher cost but better reputation
