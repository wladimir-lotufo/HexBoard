# Script to update button components with interactive configuration
# Based on SaveButton template

$buttons = @(
    @{
        File = "newbutton.html"
        Id = "but_New"
        Icon = "fa-plus"
        Text = "Novo"
        DefaultVariant = "default"
        DefaultSize = ""
    },
    @{
        File = "editbutton.html"
        Id = "but_Editar"
        Icon = "fa-pencil"
        Text = "Editar"
        DefaultVariant = "default"
        DefaultSize = "sm"
    },
    @{
        File = "closebutton.html"
        Id = "but_Fechar"
        Icon = "fa-times"
        Text = "Fechar"
        DefaultVariant = "danger"
        DefaultSize = ""
    },
    @{
        File = "backbutton.html"
        Id = "but_Voltar"
        Icon = "fa-arrow-left"
        Text = "Voltar"
        DefaultVariant = "default"
        DefaultSize = ""
    },
    @{
        File = "uploadbutton.html"
        Id = "but_Upload"
        Icon = "fa-upload"
        Text = "Upload"
        DefaultVariant = "primary"
        DefaultSize = ""
    },
    @{
        File = "reportsbutton.html"
        Id = "but_Relatorios"
        Icon = "fa-file-text"
        Text = "Relatórios"
        DefaultVariant = "default"
        DefaultSize = ""
    },
    @{
        File = "programbutton.html"
        Id = "but_Programar"
        Icon = "fa-calendar"
        Text = "Programar"
        DefaultVariant = "default"
        DefaultSize = ""
    }
)

$basePath = "c:\Users\wladi\source\repos\PartnerShip\.agent\knowledge\DesignSystem\components\botoes"

# Configuration section template
$configSection = @'
                                <!-- Configuration Section -->
                                <div class="ds-section">
                                    <h2 class="ds-section-title">Configuração</h2>
                                    <div style="background-color: #fff; border: 1px solid #e7eaec; border-radius: 4px; padding: 20px; margin: 20px 0;">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <h4 style="font-size: 14px; font-weight: 600; margin-bottom: 10px;">Variante</h4>
                                                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="primary" VARIANT_PRIMARY_CHECKED> Primary</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="default" VARIANT_DEFAULT_CHECKED> Default</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="success" VARIANT_SUCCESS_CHECKED> Success</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="danger" VARIANT_DANGER_CHECKED> Danger</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="warning" VARIANT_WARNING_CHECKED> Warning</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="white" VARIANT_WHITE_CHECKED> White</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="secondary" VARIANT_SECONDARY_CHECKED> Secondary</label></div>
                                            </div>
                                            <div class="col-md-3">
                                                <h4 style="font-size: 14px; font-weight: 600; margin-bottom: 10px;">Tamanho</h4>
                                                <div class="checkbox"><label><input type="checkbox" class="config-size" value="lg" SIZE_LG_CHECKED> Large (lg)</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-size" value="" SIZE_NORMAL_CHECKED> Normal</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-size" value="sm" SIZE_SM_CHECKED> Small (sm)</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-size" value="xs" SIZE_XS_CHECKED> Extra Small (xs)</label></div>
                                            </div>
                                            <div class="col-md-3">
                                                <h4 style="font-size: 14px; font-weight: 600; margin-bottom: 10px;">Posicionamento</h4>
                                                <div class="checkbox"><label><input type="checkbox" class="config-position" value="" checked> Default</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-position" value="pull-right"> Pull-right</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-position" value="pull-left"> Pull-left</label></div>
                                            </div>
                                            <div class="col-md-3">
                                                <h4 style="font-size: 14px; font-weight: 600; margin-bottom: 10px;">Estilo</h4>
                                                <div class="checkbox"><label><input type="checkbox" class="config-style" value="" checked> Normal</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-style" value="btn-block"> Block</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-style" value="btn-outline"> Outline</label></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

'@

# JavaScript template
$jsTemplate = @'

        // Configuration update logic
        $(document).ready(function() {
            // Radio-like behavior for checkboxes
            $('.config-variant').on('change', function() {
                if (this.checked) {
                    $('.config-variant').not(this).prop('checked', false);
                }
                updateButton();
            });

            $('.config-size').on('change', function() {
                if (this.checked) {
                    $('.config-size').not(this).prop('checked', false);
                }
                updateButton();
            });

            $('.config-position').on('change', function() {
                if (this.checked) {
                    $('.config-position').not(this).prop('checked', false);
                }
                updateButton();
            });

            $('.config-style').on('change', function() {
                if (this.checked) {
                    $('.config-style').not(this).prop('checked', false);
                }
                updateButton();
            });

            function updateButton() {
                var $button = $('#BUTTON_ID');
                
                // Get selected values
                var variant = $('.config-variant:checked').val() || 'BUTTON_DEFAULT_VARIANT';
                var size = $('.config-size:checked').val() || 'BUTTON_DEFAULT_SIZE';
                var position = $('.config-position:checked').val() || '';
                var style = $('.config-style:checked').val() || '';

                // Build class string
                var classes = ['btn', 'btn-' + variant];
                if (size) classes.push('btn-' + size);
                if (position) classes.push(position);
                if (style) classes.push(style);

                // Update button
                $button.attr('class', classes.join(' '));

                // Update HTML code
                updateHTMLCode(classes.join(' '));
            }

            function updateHTMLCode(classString) {
                var htmlCode = '&lt;button id="BUTTON_ID" type="BUTTON_TYPE" class="' + classString + '"&gt;\n' +
                              '    &lt;i class="fa BUTTON_ICON"&gt;&lt;/i&gt; BUTTON_TEXT\n' +
                              '&lt;/button&gt;';
                
                $('#tab_HTML .ds-code pre code').html(htmlCode);
            }
        });
'@

$updatedCount = 0
$errorCount = 0

foreach ($button in $buttons) {
    $filePath = Join-Path $basePath $button.File
    Write-Host "`nProcessing $($button.File)..." -ForegroundColor Cyan
    
    if (-not (Test-Path $filePath)) {
        Write-Host "  File not found!" -ForegroundColor Red
        $errorCount++
        continue
    }
    
    try {
        $content = Get-Content $filePath -Raw -Encoding UTF8
        
        # Check if already has configuration
        if ($content -match "Configuration Section") {
            Write-Host "  Already has configuration, skipping..." -ForegroundColor Yellow
            continue
        }
        
        # Prepare configuration section with correct defaults
        $config = $configSection
        
        # Set variant checkboxes
        $config = $config -replace 'VARIANT_PRIMARY_CHECKED', $(if ($button.DefaultVariant -eq 'primary') { 'checked' } else { '' })
        $config = $config -replace 'VARIANT_DEFAULT_CHECKED', $(if ($button.DefaultVariant -eq 'default') { 'checked' } else { '' })
        $config = $config -replace 'VARIANT_SUCCESS_CHECKED', $(if ($button.DefaultVariant -eq 'success') { 'checked' } else { '' })
        $config = $config -replace 'VARIANT_DANGER_CHECKED', $(if ($button.DefaultVariant -eq 'danger') { 'checked' } else { '' })
        $config = $config -replace 'VARIANT_WARNING_CHECKED', $(if ($button.DefaultVariant -eq 'warning') { 'checked' } else { '' })
        $config = $config -replace 'VARIANT_WHITE_CHECKED', ''
        $config = $config -replace 'VARIANT_SECONDARY_CHECKED', ''
        
        # Set size checkboxes
        $config = $config -replace 'SIZE_LG_CHECKED', ''
        $config = $config -replace 'SIZE_NORMAL_CHECKED', $(if ($button.DefaultSize -eq '') { 'checked' } else { '' })
        $config = $config -replace 'SIZE_SM_CHECKED', $(if ($button.DefaultSize -eq 'sm') { 'checked' } else { '' })
        $config = $config -replace 'SIZE_XS_CHECKED', ''
        
        # Find and insert configuration section
        $pattern = '(<div id="tab_Exemplo" class="tab-pane active">)\s+(<div class="ds-section">\s+<h2 class="ds-section-title">Exemplo Visual</h2>)'
        
        if ($content -match $pattern) {
            $replacement = "`$1`n$config`$2"
            $content = $content -replace $pattern, $replacement
            
            # Prepare JavaScript
            $js = $jsTemplate
            $js = $js -replace 'BUTTON_ID', $button.Id
            $js = $js -replace 'BUTTON_DEFAULT_VARIANT', $button.DefaultVariant
            $js = $js -replace 'BUTTON_DEFAULT_SIZE', $button.DefaultSize
            $js = $js -replace 'BUTTON_ICON', $button.Icon
            $js = $js -replace 'BUTTON_TEXT', $button.Text
            
            # Determine button type
            $buttonType = if ($button.Id -like "*Salvar*" -or $button.Id -like "*Upload*") { "submit" } else { "button" }
            $js = $js -replace 'BUTTON_TYPE', $buttonType
            
            # Insert JavaScript before closing script tag
            $jsPattern = '(function copyCode\(btn\).*?\}\s+)(</script>)'
            if ($content -match $jsPattern) {
                $content = $content -replace $jsPattern, "`$1$js`n    `$2"
            }
            
            # Save file
            Set-Content -Path $filePath -Value $content -Encoding UTF8 -NoNewline
            Write-Host "  Updated successfully!" -ForegroundColor Green
            $updatedCount++
        } else {
            Write-Host "  Pattern not found, skipping..." -ForegroundColor Red
            $errorCount++
        }
    }
    catch {
        Write-Host "  Error: $_" -ForegroundColor Red
        $errorCount++
    }
}

Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "Summary:" -ForegroundColor Cyan
Write-Host "  Updated: $updatedCount buttons" -ForegroundColor Green
Write-Host "  Errors: $errorCount buttons" -ForegroundColor $(if ($errorCount -gt 0) { 'Red' } else { 'Green' })
Write-Host "========================================" -ForegroundColor Cyan
