param($installPath, $toolsPath, $package, $project)

function Get-Checksum($file) {
    $cryptoProvider = New-Object "System.Security.Cryptography.MD5CryptoServiceProvider"
	
    $fileInfo = Get-Item "$file"
	trap { ;
	continue } $stream = $fileInfo.OpenRead()
    if ($? -eq $false) {
		#Write-Host "Couldn't open file for reading"
        return $null
	}
    
    $bytes = $cryptoProvider.ComputeHash($stream)
    $checksum = ''
	foreach ($byte in $bytes) {
		$checksum += $byte.ToString('x2')
	}
    
	$stream.Close() | Out-Null
    
    return $checksum
}

# Extract the version number from the jquery file in the package's content\scripts folder
$packageScriptsFolder = Join-Path $installPath Content | Join-Path -ChildPath Scripts
$jqueryFileName = Join-Path $packageScriptsFolder "jquery-*.js" | Get-ChildItem -Exclude "*.min.js","*-vsdoc.js" | Split-Path -Leaf
$jqueryFileName -match "jquery-((?:\d+\.)?(?:\d+\.)?(?:\d+\.)?(?:\d+)).js"
$ver = $matches[1]

# Determine the file paths
$projectFolder = $project.Properties.Item("FullPath").Value
$intelliSenseFileName = "jquery-$ver.intellisense.js"
$projectScriptsFolder = Join-Path $projectFolder Scripts
$projectIntelliSenseFilePath = Join-Path $projectScriptsFolder $intelliSenseFileName
$origIntelliSenseFilePath = Join-Path $toolsPath $intelliSenseFileName

if (Test-Path $projectIntelliSenseFilePath) {
    if ((Get-Checksum $projectIntelliSenseFilePath) -eq (Get-Checksum $origIntelliSenseFilePath)) {
        # The intellisense file in the project matches the file in the tools folder, delete it
        
        try {
            # Get the project item for the scripts folder
            $scriptsFolderProjectItem = $project.ProjectItems.Item("Scripts")
        
            # Get the project item for the intellisense file
            $intelliSenseFileProjectItem = $scriptsFolderProjectItem.ProjectItems.Item($intelliSenseFileName)
        }
        catch {
            # The item wasn't found
            exit
        }

        # Delete the project item
        $intelliSenseFileProjectItem.Delete()
    }
    else {
        $projectScriptsFolderLeaf = Split-Path $projectScriptsFolder -Leaf
        Write-Host "Skipping '$projectScriptsFolderLeaf\$intelliSenseFileName' because it was modified." -ForegroundColor Magenta
    }
}
else {
    # The intellisense file was not found in project
}
