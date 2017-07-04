function Print-Section ([String]$Content){
    Write-Host "============= $($Content) =============" -ForegroundColor Cyan
}

function Run-BuildTask{ 
param([String]$Command, 
      [String[]]$Arguments, 
      [String]$Dir)
    if($Arguments.Count -eq 0){
        Start-Process $Command -WorkingDirectory $Dir -NoNewWindow -Wait
    }
    else{
        Start-Process $Command -ArgumentList $Arguments -WorkingDirectory $Dir -NoNewWindow -Wait
    }
    [Console]::ResetColor()
}

Print-Section "Obsidian Build Script for Windows"

$webProjectDir = [System.IO.Path]::Combine( $PSScriptRoot ,"src","Obsidian")
$testDir =  [System.IO.Path]::Combine( $PSScriptRoot ,"test")
$startTime = Get-Date

Print-Section "Start Configuring Package Dependencies (Stage 1 of 3)"
Run-BuildTask dotnet -Arguments restore -Dir $PSScriptRoot
Run-BuildTask yarn -Dir $webProjectDir

Print-Section "Start Building (Stage 2 of 3)"
Run-BuildTask dotnet -Arguments build -Dir $webProjectDir

Print-Section "Start Testing (Stage 3 of 3)"
$testProjDirs = Get-ChildItem -Path $testDir -Directory
foreach($d in $testProjDirs){
	Run-BuildTask dotnet -Arguments test -Dir $d.FullName
}

Run-BuildTask npm -Arguments "run test:cover" -Dir $webProjectDir

$endTime = Get-Date
Print-Section "Obsidian Build Script Finished in $($endTime-$startTime)"