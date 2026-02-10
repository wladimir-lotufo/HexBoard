# Script to fix relative paths in all component files
# Reverts from ../../../../../ (5 levels) back to ../../../../ (4 levels)
# Also fixes escaped slashes

$componentsPath = "c:\Users\wladi\source\repos\PartnerShip\.agent\knowledge\DesignSystem\components"
$folders = @("layout", "formularios", "botoes", "acoes", "dados", "paineis", "modais", "graficos")

$totalFiles = 0
$updatedFiles = 0

foreach ($folder in $folders) {
    $folderPath = Join-Path $componentsPath $folder
    $htmlFiles = Get-ChildItem -Path $folderPath -Filter "*.html"
    
    Write-Host "Processing folder: $folder" -ForegroundColor Cyan
    
    foreach ($file in $htmlFiles) {
        $totalFiles++
        $content = Get-Content -Path $file.FullName -Raw -Encoding UTF8
        
        # Replace escaped slashes back to normal format with 4 levels
        $newContent = $content -replace '\.\.\\/\.\.\\/\.\.\\/\.\.\\/\.\.\\/','../../../../'
        
        # Check if any changes were made
        if ($content -ne $newContent) {
            Set-Content -Path $file.FullName -Value $newContent -Encoding UTF8 -NoNewline
            $updatedFiles++
            Write-Host "  Updated: $($file.Name)" -ForegroundColor Green
        } else {
            Write-Host "  No changes: $($file.Name)" -ForegroundColor Gray
        }
    }
}

Write-Host ""
Write-Host "Summary:" -ForegroundColor Yellow
Write-Host "  Total files processed: $totalFiles" -ForegroundColor White
Write-Host "  Files updated: $updatedFiles" -ForegroundColor Green
Write-Host "  Files unchanged: $($totalFiles - $updatedFiles)" -ForegroundColor Gray
Write-Host ""
Write-Host "All relative paths fixed successfully!" -ForegroundColor Green
