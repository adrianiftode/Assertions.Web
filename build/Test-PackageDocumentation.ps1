<#
.SYNOPSIS
    Verifies that every NuGet package produced from this repository ships XML documentation.

.DESCRIPTION
    Reads the release manifest, packs every packable project into a temporary directory
    and inspects each produced .nupkg. Every assembly under lib/ must have a sibling .xml
    file, including the shared Assertions.Web and formatter assemblies that are bundled
    into the assertion packages.

    This script is deliberately framework-neutral: it checks package contents, not API
    naming. Missing public API comments are caught by the CS1591/CS1573 build errors in
    src/Directory.Build.props.

.PARAMETER Configuration
    Build configuration used by dotnet pack.

.PARAMETER OutputDirectory
    Directory that receives the temporary packages. Defaults to a unique directory under
    the system temp path, which is removed when the script finishes.

.EXAMPLE
    ./build/Test-PackageDocumentation.ps1 -Configuration Release
#>
[CmdletBinding()]
param(
    [string] $Configuration = 'Release',

    [string] $OutputDirectory
)
$ErrorActionPreference = 'Stop'

if (-not ('System.IO.Compression.ZipFile' -as [type])) {
    Add-Type -AssemblyName System.IO.Compression.FileSystem
}

$repoRoot = Split-Path -Parent $PSScriptRoot
$manifestPath = Join-Path $repoRoot 'eng/ReleasePackages.props'

$cleanupOutput = $false
if (-not $OutputDirectory) {
    $OutputDirectory = Join-Path ([System.IO.Path]::GetTempPath()) "faw-xml-docs-$PID"
    $cleanupOutput = $true
}

if (Test-Path -LiteralPath $OutputDirectory) {
    Remove-Item -LiteralPath $OutputDirectory -Recurse -Force
}
New-Item -ItemType Directory -Path $OutputDirectory -Force | Out-Null

$failures = [System.Collections.Generic.List[string]]::new()

function Assert-PackageXml {
    param(
        [Parameter(Mandatory)]
        [string] $PackagePath
    )

    $packageName = Split-Path -Leaf $PackagePath
    $zip = $null

    try {
        $zip = [System.IO.Compression.ZipFile]::OpenRead($PackagePath)
        $assemblies = @($zip.Entries | Where-Object { $_.FullName -like 'lib/*/*.dll' })

        if ($assemblies.Count -eq 0) {
            $script:failures.Add("$packageName contains no assemblies under lib/.")
            return
        }

        foreach ($assembly in $assemblies) {
            $xmlPath = $assembly.FullName -replace '\.dll$', '.xml'
            $xmlEntry = $zip.GetEntry($xmlPath)

            if ($null -eq $xmlEntry) {
                $script:failures.Add(
                    "$packageName is missing $xmlPath (companion XML for $($assembly.FullName)).")
                continue
            }

            try {
                $reader = [System.IO.StreamReader]::new($xmlEntry.Open())
                try {
                    [xml] $doc = $reader.ReadToEnd()
                }
                finally {
                    $reader.Dispose()
                }
            }
            catch {
                $script:failures.Add("$packageName has unreadable XML documentation at $xmlPath : $_")
                continue
            }

            $members = @($doc.doc.members.member)
            if ($members.Count -eq 0) {
                $script:failures.Add("$packageName has an empty XML documentation file at $xmlPath.")
            }
        }
    }
    finally {
        if ($null -ne $zip) {
            $zip.Dispose()
        }
    }
}

try {
    [xml] $manifest = Get-Content -LiteralPath $manifestPath -Raw

    foreach ($node in $manifest.Project.ItemGroup.ReleasePackage) {
        $id = $node.Include
        $projectPath = Join-Path $repoRoot $node.ProjectPath.Replace('\', '/')

        [xml] $project = Get-Content -LiteralPath $projectPath -Raw
        $versionNode = @($project.Project.PropertyGroup.Version | Where-Object { $_ })[0]
        if ($null -eq $versionNode) {
            $failures.Add("$id has no <Version> element in its project file.")
            continue
        }
        $version = [string] $versionNode

        Write-Host "==> Packing $id $version"

        # dotnet writes progress to stderr, which would crash this script under
        # ErrorActionPreference = Stop before the exit code could be checked.
        $previousEap = $ErrorActionPreference
        $ErrorActionPreference = 'Continue'
        try {
            & dotnet pack $projectPath -c $Configuration --no-restore --nologo `
                --output $OutputDirectory 2>&1 |
                ForEach-Object { "$_" }
            $packExitCode = $LASTEXITCODE
        }
        finally {
            $ErrorActionPreference = $previousEap
        }

        if ($packExitCode -ne 0) {
            $failures.Add("dotnet pack failed for $id (exit code $packExitCode).")
        }
    }

    $packages = @(Get-ChildItem -LiteralPath $OutputDirectory -Filter '*.nupkg' -File |
        Where-Object { $_.Name -notlike '*.symbols.nupkg' })

    if ($packages.Count -eq 0) {
        $failures.Add("No .nupkg files were produced in '$OutputDirectory'.")
    }

    foreach ($package in $packages) {
        Assert-PackageXml -PackagePath $package.FullName
    }

    if ($failures.Count -gt 0) {
        throw "Package XML documentation validation failed:`n - " + ($failures -join "`n - ")
    }

    Write-Host "==> All $($packages.Count) package(s) contain XML documentation for every shipped assembly."
    $global:LASTEXITCODE = 0
}
finally {
    if ($cleanupOutput -and (Test-Path -LiteralPath $OutputDirectory)) {
        Remove-Item -LiteralPath $OutputDirectory -Recurse -Force
    }
}
