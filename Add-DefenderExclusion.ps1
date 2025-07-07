# Windows Defender Exclusion Script for CRIF-Encrypt
# Run as Administrator

Write-Host "Adding CRIF-Encrypt to Windows Defender exclusions..." -ForegroundColor Green

# Get the current directory where the script is located
$currentDir = Split-Path -Parent $MyInvocation.MyCommand.Path

# Add process exclusion
try {
    Add-MpPreference -ExclusionProcess "$currentDir\CRIF-Encrypt.exe"
    Write-Host "✓ Added process exclusion for CRIF-Encrypt.exe" -ForegroundColor Green
} catch {
    Write-Host "✗ Failed to add process exclusion: $($_.Exception.Message)" -ForegroundColor Red
}

# Add path exclusion
try {
    Add-MpPreference -ExclusionPath $currentDir
    Write-Host "✓ Added path exclusion for: $currentDir" -ForegroundColor Green
} catch {
    Write-Host "✗ Failed to add path exclusion: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host "`nExclusions added successfully!" -ForegroundColor Cyan
Write-Host "Note: This script must be run as Administrator to work properly." -ForegroundColor Yellow

# Check if we're running as admin
$isAdmin = ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator")

if (-not $isAdmin) {
    Write-Host "`nWARNING: This script was not run as Administrator!" -ForegroundColor Red
    Write-Host "Right-click on this script and select 'Run as Administrator' for it to work." -ForegroundColor Yellow
}

pause
