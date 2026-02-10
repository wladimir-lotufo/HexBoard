# PowerShell script to apply TabPanel structure to Design System components
# This script automates the transformation of linear component documentation into tabbed interface

$componentsDir = "c:\Users\wladi\source\repos\PartnerShip\.agent\knowledge\DesignSystem\components"
$skipFiles = @("header2.html", "text.html", "select.html", "calendar.html", "checkbox.html", "label.html", "welcome.html")

# Get all HTML files
$htmlFiles = Get-ChildItem -Path $componentsDir -Filter "*.html" | Where-Object { $skipFiles -notcontains $_.Name }

$updated = 0
$skipped = 0
$failed = 0

Write-Host "Starting Design System Component Standardization..." -ForegroundColor Cyan
Write-Host "Component directory: $componentsDir`n" -ForegroundColor Gray

foreach ($file in $htmlFiles) {
    try {
        $content = Get-Content -Path $file.FullName -Raw -Encoding UTF8
        
        # Skip if already has TabPanel
        if ($content -match 'pnl_.*Tabs' -and $content -match 'nav nav-tabs') {
            Write-Host "  ✓ Skipped (already has TabPanel): $($file.Name)" -ForegroundColor Yellow
            $skipped++
            continue
        }
        
        # Extract component name from filename
        $componentName = $file.BaseName -replace '\.html$', ''
        $componentId = (Get-Culture).TextInfo.ToTitleCase($componentName)
        
        # Find the content section
        $pattern = '(?s)(<!-- Content Section -->.*?<div id="PanelBody" class="panel-body tooltip-demo">)(.*?)(</div>\s*</div>\s*</div>\s*</div>\s*</div>\s*<script)'
        
        if ($content -match $pattern) {
            $before = $matches[1]
            $oldContent = $matches[2]
            $after = $matches[3]
            
            # Extract sections from old content
            $exampleMatch = [regex]::Match($oldContent, '(?s)<div class="ds-example">(.*?)</div>(?:\s*<!--[^>]*-->)?')
            $codeMatch = [regex]::Match($oldContent, '(?s)<pre><code>(.*?)</code></pre>')
            $nomenclatureMatch = [regex]::Match($oldContent, '(?s)<h2 class="ds-section-title">Padrão de Nomenclatura</h2>\s*<ul class="ds-list">(.*?)</ul>')
            $variationsMatch = [regex]::Match($oldContent, '(?s)<h2 class="ds-section-title">Variações</h2>\s*<ul class="ds-list">(.*?)</ul>')
            $librariesMatch = [regex]::Match($oldContent, '(?s)<h2 class="ds-section-title">Bibliotecas Necessárias</h2>\s*<ul class="ds-list">(.*?)</ul>')
            
            if (!$exampleMatch.Success -or !$codeMatch.Success) {
                Write-Host "  ✗ Failed: Could not extract sections from $($file.Name)" -ForegroundColor Red
                $failed++
                continue
            }
            
            $exampleHtml = $exampleMatch.Groups[1].Value.Trim()
            $codeTemplate = $codeMatch.Groups[1].Value.Trim()
            $nomenclature = if ($nomenclatureMatch.Success) { $nomenclatureMatch.Groups[1].Value.Trim() } else { "" }
            $variations = if ($variationsMatch.Success) { $variationsMatch.Groups[1].Value.Trim() } else { "" }
            $libraries = if ($librariesMatch.Success) { $librariesMatch.Groups[1].Value.Trim() } else { "" }
            
            # Generate new TabPanel content
            $newContent = @"
                        
                        <!-- TabPanel -->
                        <ul id="pnl_${componentId}Tabs" class="nav nav-tabs">
                            <li class="active">
                                <a data-toggle="tab" href="#tab_Exemplo">
                                    <i class="fa fa-eye text-primary fa-lg"></i> Exemplo
                                </a>
                            </li>
                            <li>
                                <a data-toggle="tab" href="#tab_HTML">
                                    <i class="fa fa-code text-primary fa-lg"></i> HTML
                                </a>
                            </li>
                            <li>
                                <a data-toggle="tab" href="#tab_Propriedades">
                                    <i class="fa fa-cog text-primary fa-lg"></i> Propriedades
                                </a>
                            </li>
                        </ul>

                        <div class="tab-content">
                            <br />
                            
                            <!-- Tab Exemplo -->
                            <div id="tab_Exemplo" class="tab-pane active">
                                <div class="ds-section">
                                    <h2 class="ds-section-title">Exemplo Visual</h2>
                                    <div class="ds-example">
$exampleHtml
                                    </div>
                                </div>
                            </div>

                            <!-- Tab HTML -->
                            <div id="tab_HTML" class="tab-pane">
                                <div class="ds-section">
                                    <h2 class="ds-section-title">Template HTML</h2>
                                    <div class="ds-code">
                                        <div class="ds-code-header">
                                            <span class="ds-code-title">HTML</span>
                                            <button class="ds-code-copy" onclick="copyCode(this)">Copiar</button>
                                        </div>
                                        <pre><code>$codeTemplate</code></pre>
                                    </div>
                                </div>
                            </div>

                            <!-- Tab Propriedades -->
                            <div id="tab_Propriedades" class="tab-pane">
                                <div class="ds-section">
                                    <h2 class="ds-section-title">Padrão de Nomenclatura</h2>
                                    <ul class="ds-list">
$nomenclature
                                    </ul>
                                </div>

                                <div class="ds-section">
                                    <h2 class="ds-section-title">Variações</h2>
                                    <ul class="ds-list">
$variations
                                    </ul>
                                </div>

                                <div class="ds-section">
                                    <h2 class="ds-section-title">Bibliotecas Necessárias</h2>
                                    <ul class="ds-list">
$libraries
                                    </ul>
                                </div>
                            </div>

                        </div>
                        <!-- End TabPanel -->

"@
            
            # Reconstruct the file
            $replacement = '$1' + $newContent + '$3'
            $newFullContent = $content -replace $pattern, $replacement
            
            # Write back to file
            Set-Content -Path $file.FullName -Value $newFullContent -Encoding UTF8 -NoNewline
            
            Write-Host "  ✓ Updated: $($file.Name)" -ForegroundColor Green
            $updated++
        }
        else {
            Write-Host "  ✗ Failed: Could not match pattern in $($file.Name)" -ForegroundColor Red
            $failed++
        }
    }
    catch {
        Write-Host "  ✗ Error processing $($file.Name): $_" -ForegroundColor Red
        $failed++
    }
}

Write-Host "`n$('='*60)" -ForegroundColor Cyan
Write-Host "Standardization Complete!" -ForegroundColor Cyan
Write-Host "Total files: $($htmlFiles.Count)" -ForegroundColor Gray
Write-Host "Updated: $updated" -ForegroundColor Green
Write-Host "Skipped: $skipped" -ForegroundColor Yellow
Write-Host "Failed: $failed" -ForegroundColor Red
Write-Host "$('='*60)" -ForegroundColor Cyan
