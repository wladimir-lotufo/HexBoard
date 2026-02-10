# Script to update all button components with configuration section
# This script adds interactive configuration to all button components

$buttons = @(
    @{File="newbutton.html"; Id="but_Novo"; Icon="fa-plus"; Text="Novo"},
    @{File="editbutton.html"; Id="but_Editar"; Icon="fa-pencil"; Text="Editar"},
    @{File="closebutton.html"; Id="but_Fechar"; Icon="fa-times"; Text="Fechar"},
    @{File="backbutton.html"; Id="but_Voltar"; Icon="fa-arrow-left"; Text="Voltar"},
    @{File="uploadbutton.html"; Id="but_Upload"; Icon="fa-upload"; Text="Upload"},
    @{File="reportsbutton.html"; Id="but_Relatorios"; Icon="fa-file-text"; Text="Relatórios"},
    @{File="programbutton.html"; Id="but_Programar"; Icon="fa-calendar"; Text="Programar"}
)

$basePath = "c:\Users\wladi\source\repos\PartnerShip\.agent\knowledge\DesignSystem\components\botoes"

$configSection = @'
                                <!-- Configuration Section -->
                                <div class="ds-section">
                                    <h2 class="ds-section-title">Configuração</h2>
                                    <div style="background-color: #fff; border: 1px solid #e7eaec; border-radius: 4px; padding: 20px; margin: 20px 0;">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <h4 style="font-size: 14px; font-weight: 600; margin-bottom: 10px;">Variante</h4>
                                                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="primary" checked> Primary</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="default"> Default</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="success"> Success</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="danger"> Danger</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="warning"> Warning</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="white"> White</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="secondary"> Secondary</label></div>
                                            </div>
                                            <div class="col-md-3">
                                                <h4 style="font-size: 14px; font-weight: 600; margin-bottom: 10px;">Tamanho</h4>
                                                <div class="checkbox"><label><input type="checkbox" class="config-size" value="lg"> Large (lg)</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-size" value="" checked> Normal</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-size" value="sm"> Small (sm)</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-size" value="xs"> Extra Small (xs)</label></div>
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

$jsScript = @'

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
                var $button = $('BUTTON_SELECTOR');
                
                // Get selected values
                var variant = $('.config-variant:checked').val() || 'primary';
                var size = $('.config-size:checked').val() || '';
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
                var htmlCode = 'HTML_TEMPLATE';
                
                $('#tab_HTML .ds-code pre code').html(htmlCode);
            }
        });
'@

foreach ($button in $buttons) {
    $filePath = Join-Path $basePath $button.File
    Write-Host "Processing $($button.File)..." -ForegroundColor Cyan
    
    if (Test-Path $filePath) {
        $content = Get-Content $filePath -Raw -Encoding UTF8
        
        # Check if already has configuration
        if ($content -match "Configuration Section") {
            Write-Host "  Already has configuration, skipping..." -ForegroundColor Yellow
            continue
        }
        
        # Find the tab exemplo section and add configuration before "Exemplo Visual"
        $pattern = '(\s+<!-- Tab Exemplo -->\s+<div id="tab_Exemplo" class="tab-pane active">)\s+(<div class="ds-section">\s+<h2 class="ds-section-title">Exemplo Visual</h2>)'
        
        if ($content -match $pattern) {
            $replacement = "`$1`n$configSection`$2"
            $content = $content -replace $pattern, $replacement
            
            # Add JavaScript
            $buttonSelector = "#$($button.Id)"
            $htmlTemplate = "&lt;button id=`"$($button.Id)`" type=`"submit`" class=`"" + classString + "`"&gt;\\n" +
                          "    &lt;i class=`"fa $($button.Icon)`"&gt;&lt;/i&gt; $($button.Text)\\n" +
                          "&lt;/button&gt;"
            
            $customJs = $jsScript -replace 'BUTTON_SELECTOR', $buttonSelector
            $customJs = $customJs -replace 'HTML_TEMPLATE', $htmlTemplate
            
            # Find the end of copyCode function and add new script
            $jsPattern = '(function copyCode\(btn\).*?\}\s+)(</script>)'
            if ($content -match $jsPattern) {
                $content = $content -replace $jsPattern, "`$1$customJs`n    `$2"
            }
            
            # Save file
            Set-Content -Path $filePath -Value $content -Encoding UTF8 -NoNewline
            Write-Host "  Updated successfully!" -ForegroundColor Green
        } else {
            Write-Host "  Pattern not found, skipping..." -ForegroundColor Red
        }
    } else {
        Write-Host "  File not found!" -ForegroundColor Red
    }
}

Write-Host "`nAll buttons processed!" -ForegroundColor Green
