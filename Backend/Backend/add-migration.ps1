Set-Location 'C:/Development/MyPrjct/test/ReactSite/Backend/Backend/Backend/'

$commandPath = $MyInvocation.MyCommand.Path
$directoryPath = [System.IO.Path]::GetDirectoryName($commandPath)
Write-Host $directoryPath
