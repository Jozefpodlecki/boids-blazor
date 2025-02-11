$source = "node_modules\html2canvas-pro\dist\html2canvas-pro.min.js"
$destination = "wwwroot\lib\html2canvas-pro.min.js"

$destinationFolder = Split-Path $destination
if (-not (Test-Path $destinationFolder)) {
    New-Item -ItemType Directory -Path $destinationFolder | Out-Null
}

Copy-Item -Path $source -Destination $destination -Force

Write-Host "File copied to $destination"