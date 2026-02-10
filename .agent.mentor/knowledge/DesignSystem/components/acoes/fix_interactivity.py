import re
import os

# Define the files and their default values
files_config = {
    r'c:\Users\wladi\source\repos\PartnerShip\.agent\knowledge\DesignSystem\components\acoes\actiondelete.html': {
        'variant_default': 'danger',
        'size_default': 'sm'
    },
    r'c:\Users\wladi\source\repos\PartnerShip\.agent\knowledge\DesignSystem\components\acoes\actionedit.html': {
        'variant_default': 'default',
        'size_default': 'sm'
    },
    r'c:\Users\wladi\source\repos\PartnerShip\.agent\knowledge\DesignSystem\components\acoes\actionview.html': {
        'variant_default': 'info',
        'size_default': 'sm'
    },
    r'c:\Users\wladi\source\repos\PartnerShip\.agent\knowledge\DesignSystem\components\acoes\actionmove.html': {
        'variant_default': 'default',
        'size_default': 'sm'
    },
    r'c:\Users\wladi\source\repos\PartnerShip\.agent\knowledge\DesignSystem\components\acoes\actionupload.html': {
        'variant_default': 'primary',
        'size_default': 'sm'
    },
}

# Pattern to find the old event handlers
old_pattern = re.compile(
    r"(\s+)// Action configuration\r?\n"
    r"\s+\$\(document\)\.ready\(function\(\) \{\r?\n"
    r"\s+\$\('\.config-variant'\)\.on\('change', function\(\) \{\r?\n"
    r"\s+if \(this\.checked\) \{\r?\n"
    r"\s+\$\('\.config-variant'\)\.not\(this\)\.prop\('checked', false\);\r?\n"
    r"\s+\}\r?\n"
    r"\s+updateAction\(\);\r?\n"
    r"\s+\}\);\r?\n"
    r"\r?\n"
    r"\s+\$\('\.config-size'\)\.on\('change', function\(\) \{\r?\n"
    r"\s+if \(this\.checked\) \{\r?\n"
    r"\s+\$\('\.config-size'\)\.not\(this\)\.prop\('checked', false\);\r?\n"
    r"\s+\}\r?\n"
    r"\s+updateAction\(\);\r?\n"
    r"\s+\}\);\r?\n"
    r"\r?\n"
    r"\s+function updateAction\(\) \{",
    re.MULTILINE
)

def create_new_handlers(variant_default, size_default, indent="        "):
    return f"""{indent}// Action configuration
{indent}$(document).ready(function() {{
{indent}    $('.config-variant').on('change', function(event) {{
{indent}        if (this.checked) {{
{indent}            $('.config-variant').each(function() {{
{indent}                if (this !== event.target) {{
{indent}                    $(this).prop('checked', false);
{indent}                }}
{indent}            }});
{indent}        }} else if ($('.config-variant:checked').length === 0) {{
{indent}            $('.config-variant[value="{variant_default}"]').prop('checked', true);
{indent}        }}
{indent}        updateAction();
{indent}    }});

{indent}    $('.config-size').on('change', function(event) {{
{indent}        if (this.checked) {{
{indent}            $('.config-size').each(function() {{
{indent}                if (this !== event.target) {{
{indent}                    $(this).prop('checked', false);
{indent}                }}
{indent}            }});
{indent}        }} else if ($('.config-size:checked').length === 0) {{
{indent}            $('.config-size[value="{size_default}"]').prop('checked', true);
{indent}        }}
{indent}        updateAction();
{indent}    }});

{indent}    function updateAction() {{"""

def fix_file(filepath, variant_default, size_default):
    """Fix the event handlers in a file"""
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()
    
    # Create the new handlers
    new_handlers = create_new_handlers(variant_default, size_default)
    
    # Replace the old handlers
    new_content = old_pattern.sub(new_handlers, content)
    
    if new_content == content:
        print(f"No changes made to {filepath}")
        return False
    
    # Write the fixed content back
    with open(filepath, 'w', encoding='utf-8') as f:
        f.write(new_content)
    
    print(f"✓ Fixed {os.path.basename(filepath)}")
    return True

# Fix all files
print("Fixing action button interactivity...\n")
for filepath, config in files_config.items():
    if os.path.exists(filepath):
        fix_file(filepath, config['variant_default'], config['size_default'])
    else:
        print(f"✗ File not found: {filepath}")

print("\n✓ All files processed!")
