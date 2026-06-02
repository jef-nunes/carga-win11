$ProjectFile = ".\CargaWin11.csproj"
$OutputDir = ".\publish"

dotnet publish `
    $ProjectFile `
    -c Release `
    -r win-x64 `
    --self-contained true `
    -p:PublishSingleFile=true `
    -o $OutputDir

Write-Host ""
Write-Host "Build concluído."
Write-Host "Executável gerado em: $OutputDir"