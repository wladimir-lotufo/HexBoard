# Script to move component files to category subfolders
$componentsPath = "c:\Users\wladi\source\repos\PartnerShip\.agent\knowledge\DesignSystem\components"

# Layout files
Write-Host "Moving Layout files..." -ForegroundColor Cyan
Move-Item -Path "$componentsPath\body1.html" -Destination "$componentsPath\layout\" -Force
Move-Item -Path "$componentsPath\body2.html" -Destination "$componentsPath\layout\" -Force
Move-Item -Path "$componentsPath\row.html" -Destination "$componentsPath\layout\" -Force
Move-Item -Path "$componentsPath\col.html" -Destination "$componentsPath\layout\" -Force

# Formulários files
Write-Host "Moving Formulários files..." -ForegroundColor Cyan
Move-Item -Path "$componentsPath\text.html" -Destination "$componentsPath\formularios\" -Force
Move-Item -Path "$componentsPath\select.html" -Destination "$componentsPath\formularios\" -Force
Move-Item -Path "$componentsPath\calendar.html" -Destination "$componentsPath\formularios\" -Force
Move-Item -Path "$componentsPath\checkbox.html" -Destination "$componentsPath\formularios\" -Force

# Botões files
Write-Host "Moving Botões files..." -ForegroundColor Cyan
Move-Item -Path "$componentsPath\newbutton.html" -Destination "$componentsPath\botoes\" -Force
Move-Item -Path "$componentsPath\savebutton.html" -Destination "$componentsPath\botoes\" -Force
Move-Item -Path "$componentsPath\closebutton.html" -Destination "$componentsPath\botoes\" -Force
Move-Item -Path "$componentsPath\backbutton.html" -Destination "$componentsPath\botoes\" -Force
Move-Item -Path "$componentsPath\editbutton.html" -Destination "$componentsPath\botoes\" -Force
Move-Item -Path "$componentsPath\uploadbutton.html" -Destination "$componentsPath\botoes\" -Force
Move-Item -Path "$componentsPath\programbutton.html" -Destination "$componentsPath\botoes\" -Force
Move-Item -Path "$componentsPath\reportsbutton.html" -Destination "$componentsPath\botoes\" -Force

# Ações files
Write-Host "Moving Ações files..." -ForegroundColor Cyan
Move-Item -Path "$componentsPath\actionedit.html" -Destination "$componentsPath\acoes\" -Force
Move-Item -Path "$componentsPath\actiondelete.html" -Destination "$componentsPath\acoes\" -Force
Move-Item -Path "$componentsPath\actionview.html" -Destination "$componentsPath\acoes\" -Force
Move-Item -Path "$componentsPath\actionmove.html" -Destination "$componentsPath\acoes\" -Force
Move-Item -Path "$componentsPath\actionupload.html" -Destination "$componentsPath\acoes\" -Force
Move-Item -Path "$componentsPath\actionmenu.html" -Destination "$componentsPath\acoes\" -Force

# Dados files
Write-Host "Moving Dados files..." -ForegroundColor Cyan
Move-Item -Path "$componentsPath\grid.html" -Destination "$componentsPath\dados\" -Force
Move-Item -Path "$componentsPath\gridheader.html" -Destination "$componentsPath\dados\" -Force
Move-Item -Path "$componentsPath\badge.html" -Destination "$componentsPath\dados\" -Force
Move-Item -Path "$componentsPath\label.html" -Destination "$componentsPath\dados\" -Force

# Paineis files
Write-Host "Moving Paineis files..." -ForegroundColor Cyan
Move-Item -Path "$componentsPath\filterpanel.html" -Destination "$componentsPath\paineis\" -Force
Move-Item -Path "$componentsPath\tabpanel.html" -Destination "$componentsPath\paineis\" -Force

# Modais files
Write-Host "Moving Modais files..." -ForegroundColor Cyan
Move-Item -Path "$componentsPath\modalplaceholder.html" -Destination "$componentsPath\modais\" -Force
Move-Item -Path "$componentsPath\modal2.html" -Destination "$componentsPath\modais\" -Force

# Gráficos files
Write-Host "Moving Gráficos files..." -ForegroundColor Cyan
Move-Item -Path "$componentsPath\pizzachart.html" -Destination "$componentsPath\graficos\" -Force

Write-Host "`nAll files moved successfully!" -ForegroundColor Green
Write-Host "Summary:" -ForegroundColor Yellow
Write-Host "  Layout: 5 files (header2 already moved)" -ForegroundColor White
Write-Host "  Formulários: 4 files" -ForegroundColor White
Write-Host "  Botões: 8 files" -ForegroundColor White
Write-Host "  Ações: 6 files" -ForegroundColor White
Write-Host "  Dados: 4 files" -ForegroundColor White
Write-Host "  Paineis: 2 files" -ForegroundColor White
Write-Host "  Modais: 2 files" -ForegroundColor White
Write-Host "  Gráficos: 1 file" -ForegroundColor White
