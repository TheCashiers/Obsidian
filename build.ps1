Write-Host "========== Obsidian Build Script for Windows ==========" -ForegroundColor Cyan

$startTime = Get-Date
Write-Host "========== Start Configuring Package Dependencies ==========" -ForegroundColor Cyan
Start-Process dotnet -ArgumentList restore -NoNewWindow -Wait
Start-Process yarn -WorkingDirectory ".\src\Obsidian" -Wait -NoNewWindow
[Console]::ResetColor()

Write-Host "========== Start Building ==========" -ForegroundColor Cyan
Start-Process dotnet -ArgumentList build -WorkingDirectory ".\src\Obsidian" -NoNewWindow -Wait

Write-Host "========== Start Testing ==========" -ForegroundColor Cyan
$testProjDirs = Get-ChildItem -Path .\test\ -Directory
foreach($d in $testProjDirs){
	Start-Process dotnet -ArgumentList test -WorkingDirectory $d.FullName -NoNewWindow -Wait
}

Start-Process npm -ArgumentList "run test:cover" -WorkingDirectory ".\src\Obsidian" -Wait -NoNewWindow
[Console]::ResetColor()
$endTime = Get-Date
Write-Host "========== Obsidian Build Script Finished in $($endTime-$startTime) ==========" -ForegroundColor Cyan