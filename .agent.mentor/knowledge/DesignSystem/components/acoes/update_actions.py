"""
Batch update script for Action components
Adds interactive configuration similar to buttons but for action icons
"""

import re
from pathlib import Path

# Action configurations
actions = [
    {
        'file': 'actionedit.html',
        'icon': 'fa-pencil',
        'title': 'ActionEdit',
        'tooltip': 'Editar',
        'default_variant': 'default'
    },
    {
        'file': 'actiondelete.html',
        'icon': 'fa-trash',
        'title': 'ActionDelete',
        'tooltip': 'Excluir',
        'default_variant': 'danger'
    },
    {
        'file': 'actionview.html',
        'icon': 'fa-eye',
        'title': 'ActionView',
        'tooltip': 'Visualizar',
        'default_variant': 'info'
    },
    {
        'file': 'actionmove.html',
        'icon': 'fa-arrows',
        'title': 'ActionMove',
        'tooltip': 'Mover',
        'default_variant': 'default'
    },
    {
        'file': 'actionupload.html',
        'icon': 'fa-upload',
        'title': 'ActionUpload',
        'tooltip': 'Upload',
        'default_variant': 'primary'
    }
]

base_path = Path(r'c:\Users\wladi\source\repos\PartnerShip\.agent\knowledge\DesignSystem\components\acoes')

# Configuration HTML template
config_html = '''                                <!-- Configuration Section -->
                                <div class="ds-section">
                                    <h2 class="ds-section-title">Configuração</h2>
                                    <div style="background-color: #fff; border: 1px solid #e7eaec; border-radius: 4px; padding: 20px; margin: 20px 0;">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <h4 style="font-size: 14px; font-weight: 600; margin-bottom: 10px;">Variante</h4>
                                                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="default" VARIANT_DEFAULT_CHECKED> Default</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="primary" VARIANT_PRIMARY_CHECKED> Primary</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="success" VARIANT_SUCCESS_CHECKED> Success</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="danger" VARIANT_DANGER_CHECKED> Danger</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="warning" VARIANT_WARNING_CHECKED> Warning</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="info" VARIANT_INFO_CHECKED> Info</label></div>
                                            </div>
                                            <div class="col-md-6">
                                                <h4 style="font-size: 14px; font-weight: 600; margin-bottom: 10px;">Tamanho</h4>
                                                <div class="checkbox"><label><input type="checkbox" class="config-size" value="sm" checked> Small (sm)</label></div>
                                                <div class="checkbox"><label><input type="checkbox" class="config-size" value="xs"> Extra Small (xs)</label></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

'''

# JavaScript template
js_template = '''
        // Action configuration
        $(document).ready(function() {
            $('.config-variant').on('change', function() {
                if (this.checked) {
                    $('.config-variant').not(this).prop('checked', false);
                }
                updateAction();
            });

            $('.config-size').on('change', function() {
                if (this.checked) {
                    $('.config-size').not(this).prop('checked', false);
                }
                updateAction();
            });

            function updateAction() {
                var $action = $('#action-example');
                var variant = $('.config-variant:checked').val() || 'ACTION_DEFAULT_VARIANT';
                var size = $('.config-size:checked').val() || 'sm';

                var classes = ['btn', 'btn-' + variant, 'btn-' + size];
                $action.attr('class', classes.join(' '));

                updateHTMLCode(classes.join(' '));
            }

            function updateHTMLCode(classString) {
                var htmlCode = '&lt;a href="#" class="' + classString + '" title="ACTION_TOOLTIP"&gt;\\n' +
                              '    &lt;i class="fa ACTION_ICON"&gt;&lt;/i&gt;\\n' +
                              '&lt;/a&gt;';
                $('#action-html-code').html(htmlCode);
            }
        });
'''

updated_count = 0

for action in actions:
    print(f"\\nProcessing {action['file']}...")
    
    file_path = base_path / action['file']
    
    if not file_path.exists():
        print(f"  [SKIP] File not found")
        continue
    
    try:
        with open(file_path, 'r', encoding='utf-8') as f:
            content = f.read()
        
        # Check if already has configuration
        if 'Configuration Section' in content:
            print(f"  [SKIP] Already has configuration")
            continue
        
        # Prepare configuration HTML
        config = config_html
        for variant in ['default', 'primary', 'success', 'danger', 'warning', 'info']:
            checked = 'checked' if variant == action['default_variant'] else ''
            config = config.replace(f'VARIANT_{variant.upper()}_CHECKED', checked)
        
        # Insert configuration before "Exemplo Visual"
        pattern = r'(<div id="tab_Exemplo" class="tab-pane active">)\s+(<div class="ds-section">\\s+<h2 class="ds-section-title">Exemplo Visual</h2>)'
        if re.search(pattern, content):
            content = re.sub(pattern, f'\\1\\n{config}\\2', content)
        
        # Update example to have ID
        content = re.sub(
            r'<a href="#" class="btn btn-[^"]*"',
            f'<a href="#" id="action-example" class="btn btn-{action["default_variant"]} btn-sm"',
            content,
            count=1
        )
        
        # Add ID to HTML code block
        content = re.sub(
            r'<pre><code>',
            '<pre><code id="action-html-code">',
            content,
            count=1
        )
        
        # Prepare and insert JavaScript
        js = js_template
        js = js.replace('ACTION_DEFAULT_VARIANT', action['default_variant'])
        js = js.replace('ACTION_ICON', action['icon'])
        js = js.replace('ACTION_TOOLTIP', action['tooltip'])
        
        # Insert before closing script tag
        content = re.sub(
            r'(function copyCode\(btn\).*?\}\s+)(</script>)',
            f'\\1{js}\\n    \\2',
            content,
            flags=re.DOTALL
        )
        
        # Save file
        with open(file_path, 'w', encoding='utf-8') as f:
            f.write(content)
        
        print(f"  [OK] Updated successfully")
        updated_count += 1
        
    except Exception as e:
        print(f"  [ERROR] {e}")

print(f"\\n{'='*50}")
print(f"Summary: Updated {updated_count}/5 action components")
print(f"{'='*50}")
