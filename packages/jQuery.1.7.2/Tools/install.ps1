param($installPath, $toolsPath, $package, $project)

# VS 11 and above supports the new intellisense JS files
$supportsJsIntelliSenseFile = [System.Version]::Parse($dte.Version).Major -ge 11

if (-not $supportsJsIntelliSenseFile) {
    Write-Host "IntelliSense JS files are not supported by your version of VS: " + $dte.Version
    exit
}

# Extract the version number from the jquery file in the package's content\scripts folder
$packageScriptsFolder = Join-Path $installPath Content\Scripts
$jqueryFileName = Join-Path $packageScriptsFolder "jquery-*.js" | Get-ChildItem -Exclude "*.min.js","*-vsdoc.js" | Split-Path -Leaf
$jqueryFileName -match "jquery-((?:\d+\.)?(?:\d+\.)?(?:\d+\.)?(?:\d+)).js"
$ver = $matches[1]

# Determine the project scripts folder path
$projectFolderPath = $project.Properties.Item("FullPath").Value
$projectScriptsFolderPath = Join-Path $projectFolderPath Scripts

# Get the project item for the scripts folder
try {
    $scriptsFolderProjectItem = $project.ProjectItems.Item("Scripts")
}
catch {
    exit
}

# Delete the vsdoc file from the project
try {
    $vsDocProjectItem = $scriptsFolderProjectItem.ProjectItems.Item("jquery-$ver-vsdoc.js")
    $vsDocProjectItem.Delete()
}
catch {
    exit
}

# Copy the intellisense file to the project from the tools folder
$intelliSenseFileName = "jquery-$ver.intellisense.js"
$intelliSenseFileSourcePath = Join-Path $toolsPath $intelliSenseFileName
try {
    $scriptsFolderProjectItem.ProjectItems.AddFromFileCopy($intelliSenseFileSourcePath)
}
catch {
    # This will throw if the file already exists, so we need to catch here
}