# CRIF-Encrypt PowerShell Script
# Interactive script to replicate the C# app logic

function Show-Intro {
    Write-Host "(c) Hayk Jomardyan 2022. All rights reserved.  v16"
    Write-Host "https://github.com/jomardyan/CRIF-Import-Creator"
    Write-Host "Initializing, please wait..."
    Start-Sleep -Milliseconds 100
}

function Remove-FirstAndLastLines {
    param([string[]]$lines)
    if ($lines.Length -le 2) { return @() }
    return $lines[1..($lines.Length-2)]
}

function Replace-CrifAndSaveDat {
    param(
        [string]$fileName,
        [string]$directory
    )
    $fileBaseName = [System.IO.Path]::GetFileNameWithoutExtension($fileName)
    $text = Get-Content $fileName -Raw
    $text = $text -replace "\t", "^~"
    $lines = $text -split "`r?`n"
    $lines = Remove-FirstAndLastLines $lines
    $datDir = Join-Path $directory $fileBaseName
    $datDir = Join-Path $datDir $fileBaseName
    if (!(Test-Path $datDir)) { New-Item -ItemType Directory -Path $datDir | Out-Null }
    $datOutput = Join-Path $datDir ("$fileBaseName.dat")
    $lines | Set-Content $datOutput
    Write-Host "Saved: $datOutput"
    Start-Sleep -Milliseconds 500
    $zipPath = Join-Path $directory $fileBaseName
    $zipPath = Join-Path $zipPath ("$fileBaseName.zip")
    Compress-Archive -Path $datDir\* -DestinationPath $zipPath -Force
    Remove-Item $datDir -Recurse -Force
    Write-Host "ZIP PATH: $zipPath"
}

function Sign-And-Encrypt {
    param(
        [string]$inputDirectory,
        [string]$fileNameWithoutExtension
    )
    $zipPath = Join-Path $inputDirectory $fileNameWithoutExtension
    $zipPath = Join-Path $zipPath ("$fileNameWithoutExtension.zip")
    $cmd = "gpg.exe -v -se -r CRIF-SWO-PROD --passphrase '' --local-user 0x9F674BC8 '$zipPath'"
    Write-Host "Sign and encrypt..."
    Invoke-Expression $cmd
    return $LASTEXITCODE -eq 0
}

Show-Intro

$filePath = Read-Host "Please enter the path to the .txt file (exported UNICODE TEXT from Excel)"
if (!(Test-Path $filePath)) {
    Write-Host "File does not exist. Exiting..."
    exit
}
if (![System.IO.Path]::GetExtension($filePath).Equals(".txt", [System.StringComparison]::OrdinalIgnoreCase)) {
    Write-Host "Input is not a .txt file. Use the exported UNICODE TEXT file from Excel."
    exit
}

$inputText = Get-Content $filePath -Raw
$inputDirectory = [System.IO.Path]::GetDirectoryName($filePath) + [System.IO.Path]::DirectorySeparatorChar
$fileNameWithoutExtension = [System.IO.Path]::GetFileNameWithoutExtension($filePath)
$fileWithExtension = $inputDirectory + $fileNameWithoutExtension + [System.IO.Path]::GetExtension($filePath)
Set-Content $fileWithExtension $inputText
Replace-CrifAndSaveDat -fileName $fileWithExtension -directory $inputDirectory
Start-Sleep -Milliseconds 500
if (-not (Sign-And-Encrypt -inputDirectory $inputDirectory -fileNameWithoutExtension $fileNameWithoutExtension)) {
    Write-Host "An error occurred during signing and encryption."
    exit
}
Write-Host "--------------SIGN AND ENCRYPT FINISHED-------------------"
Write-Host "---------All operations completed successfully------------"
Read-Host "Type any key to exit..."
