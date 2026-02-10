# Script to fix ALL component files with correct path depth (5 levels, not 4)
# From: components/[subfolder]/file.html to project root needs ../../../../../

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
        
        # Replace ../../../../ with ../../../../../ (add one more level)
        $newContent = $content -replace '"\.\./\.\./\.\./\.\./','"..\/..\/..\/..\/..\/'
        
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
Write-Host "All paths corrected to 5 levels!" -ForegroundColor Green
