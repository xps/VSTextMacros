# New version can be passed as a parameter, otherwise the current version is simply incremented
Param(
    [Parameter(Mandatory = $false, Position = 1)]
    [string] $newVersion
)

# Replace helper function
function replace($filePath, $find, $replace) {
    "Updating $filePath"
    $contents = Get-Content $filePath
    $contents -replace $find, $replace | Set-Content $filePath -Encoding UTF8
}

# Get current version
$manifest = Get-Content "source.extension.vsixmanifest"
$identity = $manifest | select-string "<Identity"
$currentVersion = [regex]::match($identity, "(\d+\.\d+)").Groups[1].Value
"Current version: $currentVersion"

# New version
if (-not $newVersion) {
    $parts = $currentVersion.Split(".")
    $newVersion = $parts[0] + "." + (([int]$parts[1]) + 1)
}
"New version: $newVersion"

# Update files
replace "source.extension.vsixmanifest" "`"$currentVersion`"" "`"$newVersion`""
replace "VSTextMacrosPackage.cs" "`"$currentVersion`"" "`"$newVersion`""
replace "Properties/AssemblyInfo.cs" "`"$currentVersion.*`"" "`"$newVersion.*`""

"Now add what's new in:"
" - ReadMe.md"
" - Documentation/ReleaseNotes.txt"
