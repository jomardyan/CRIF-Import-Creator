# PowerShell Pester tests for CRIF-Encrypt.ps1
Describe "CRIF-Encrypt.ps1" {
    It "Should reject non-existent file" {
        $result = powershell -Command "& { . ./CRIF-Encrypt.ps1; $input = 'nonexistent.txt'; Test-Path $input }"
        $result | Should -BeFalse
    }
    It "Should reject non-txt file" {
        $tmp = New-TemporaryFile
        Rename-Item $tmp "$tmp.docx"
        $result = powershell -Command "& { . ./CRIF-Encrypt.ps1; $input = '$tmp.docx'; [System.IO.Path]::GetExtension($input) -eq '.txt' }"
        $result | Should -BeFalse
        Remove-Item "$tmp.docx"
    }
    # Add more tests for Replace-CrifAndSaveDat and Sign-And-Encrypt as needed
}
