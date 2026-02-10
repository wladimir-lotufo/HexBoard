"""
Script to copy SaveButton configuration to other buttons
Uses SaveButton as template and replaces specific values
"""

import re
from pathlib import Path

# Button configurations
buttons = [
    {
        'file': 'newbutton.html',
        'id': 'but_New',
        'icon': 'fa-plus',
        'text': 'Novo',
        'type': 'button',
        'default_variant': 'default',
        'default_size': '',
        'title': 'NewButton'
    },
    {
        'file': 'editbutton.html',
        'id': 'but_Editar',
        'icon': 'fa-pencil',
        'text': 'Editar',
        'type': 'button',
        'default_variant': 'default',
        'default_size': 'sm',
        'title': 'EditButton'
    },
    {
        'file': 'closebutton.html',
        'id': 'but_Fechar',
        'icon': 'fa-times',
        'text': 'Fechar',
        'type': 'button',
        'default_variant': 'danger',
        'default_size': '',
        'title': 'CloseButton'
    },
    {
        'file': 'backbutton.html',
        'id': 'but_Voltar',
        'icon': 'fa-arrow-left',
        'text': 'Voltar',
        'type': 'button',
        'default_variant': 'default',
        'default_size': '',
        'title': 'BackButton'
    },
    {
        'file': 'uploadbutton.html',
        'id': 'but_Upload',
        'icon': 'fa-upload',
        'text': 'Upload',
        'type': 'submit',
        'default_variant': 'primary',
        'default_size': '',
        'title': 'UploadButton'
    },
    {
        'file': 'reportsbutton.html',
        'id': 'but_Relatorios',
        'icon': 'fa-file-text',
        'text': 'Relatórios',
        'type': 'button',
        'default_variant': 'default',
        'default_size': '',
        'title': 'ReportsButton'
    },
    {
        'file': 'programbutton.html',
        'id': 'but_Programar',
        'icon': 'fa-calendar',
        'text': 'Programar',
        'type': 'button',
        'default_variant': 'default',
        'default_size': '',
        'title': 'ProgramButton'
    }
]

base_path = Path(r'c:\Users\wladi\source\repos\PartnerShip\.agent\knowledge\DesignSystem\components\botoes')
template_file = base_path / 'savebutton.html'

# Read template
with open(template_file, 'r', encoding='utf-8') as f:
    template = f.read()

updated_count = 0
error_count = 0

for button in buttons:
    print(f"\nProcessing {button['file']}...")
    
    try:
        # Create content from template
        content = template
        
        # Replace SaveButton specific values
        content = content.replace('but_Salvar', button['id'])
        content = content.replace('fa-check', button['icon'])
        content = content.replace('>Salvar<', f">{button['text']}<")
        content = content.replace(' Salvar\\n', f" {button['text']}\\n")
        content = content.replace('SaveButton', button['title'])
        content = content.replace('type="submit"', f'type="{button["type"]}"')
        
        # Update title and description
        content = re.sub(
            r'<title>.*?</title>',
            f'<title>{button["title"]} - Design System</title>',
            content
        )
        
        # Update default variant checkbox
        if button['default_variant'] != 'primary':
            # Uncheck primary
            content = content.replace(
                'class="config-variant" value="primary" checked',
                'class="config-variant" value="primary"'
            )
            # Check correct variant
            content = content.replace(
                f'class="config-variant" value="{button["default_variant"]}">',
                f'class="config-variant" value="{button["default_variant"]}" checked>'
            )
        
        # Update default size checkbox
        if button['default_size'] == 'sm':
            # Uncheck normal
            content = content.replace(
                'class="config-size" value="" checked',
                'class="config-size" value=""'
            )
            # Check sm
            content = content.replace(
                'class="config-size" value="sm">',
                'class="config-size" value="sm" checked>'
            )
        
        # Update JavaScript default values
        content = content.replace(
            "var variant = $('.config-variant:checked').val() || 'primary';",
            f"var variant = $('.config-variant:checked').val() || '{button['default_variant']}';"
        )
        
        if button['default_size']:
            content = content.replace(
                "var size = $('.config-size:checked').val() || '';",
                f"var size = $('.config-size:checked').val() || '{button['default_size']}';"
            )
        
        # Update button class in example
        default_classes = ['btn', f"btn-{button['default_variant']}"]
        if button['default_size']:
            default_classes.append(f"btn-{button['default_size']}")
        default_class_str = ' '.join(default_classes)
        
        content = re.sub(
            r'<button id="but_Salvar"[^>]*class="[^"]*"',
            f'<button id="{button["id"]}" type="{button["type"]}" class="{default_class_str}"',
            content
        )
        
        # Save file
        output_file = base_path / button['file']
        with open(output_file, 'w', encoding='utf-8') as f:
            f.write(content)
        
        print(f"  [OK] Updated successfully!")
        updated_count += 1
        
    except Exception as e:
        print(f"  [ERROR] Error: {e}")
        error_count += 1

print(f"\n{'='*50}")
print(f"Summary:")
print(f"  Updated: {updated_count} buttons")
print(f"  Errors: {error_count} buttons")
print(f"{'='*50}")
