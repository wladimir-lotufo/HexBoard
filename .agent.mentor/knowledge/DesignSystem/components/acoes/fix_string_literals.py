import re
import os

# Define the files to fix
files = [
    r'c:\Users\wladi\source\repos\PartnerShip\.agent\knowledge\DesignSystem\components\acoes\actiondelete.html',
    r'c:\Users\wladi\source\repos\PartnerShip\.agent\knowledge\DesignSystem\components\acoes\actionedit.html',
    r'c:\Users\wladi\source\repos\PartnerShip\.agent\knowledge\DesignSystem\components\acoes\actionview.html',
    r'c:\Users\wladi\source\repos\PartnerShip\.agent\knowledge\DesignSystem\components\acoes\actionmove.html',
    r'c:\Users\wladi\source\repos\PartnerShip\.agent\knowledge\DesignSystem\components\acoes\actionupload.html',
]

# Pattern to match the problematic updateHTMLCode function
# This pattern matches the function with incorrect line breaks
pattern = re.compile(
    r"(            function updateHTMLCode\(classString\) \{\r?\n)"
    r"(                var htmlCode = '&lt;a href=\"#\" class=\"' \+ classString \+ '\" title=\"[^\"]+\"&gt;\r?\n)"
    r"(                ' \+\r?\n)"
    r"(                '    &lt;i class=\"fa fa-[^\"]+\"&gt;&lt;/i&gt;\r?\n)"
    r"(                ' \+\r?\n)"
    r"(                '&lt;/a&gt;';\r?\n)"
    r"(                \$\('#action-html-code'\)\.html\(htmlCode\);\r?\n)"
    r"(            \})",
    re.MULTILINE
)

def fix_file(filepath):
    """Fix the updateHTMLCode function in a file"""
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()
    
    # Extract the title and icon from the existing function
    title_match = re.search(r'title="([^"]+)"', content)
    icon_match = re.search(r'&lt;i class="fa fa-([^"]+)"&gt;', content)
    
    if not title_match or not icon_match:
        print(f"Could not find title or icon in {filepath}")
        return False
    
    title = title_match.group(1)
    icon = icon_match.group(1)
    
    # Create the corrected function
    replacement = f"""            function updateHTMLCode(classString) {{
                var htmlCode = '&lt;a href="#" class="' + classString + '" title="{title}"&gt;\\n' +
                              '    &lt;i class="fa fa-{icon}"&gt;&lt;/i&gt;\\n' +
                              '&lt;/a&gt;';
                $('#action-html-code').html(htmlCode);
            }}"""
    
    # Replace the function
    new_content = pattern.sub(replacement, content)
    
    if new_content == content:
        print(f"No changes made to {filepath}")
        return False
    
    # Write the fixed content back
    with open(filepath, 'w', encoding='utf-8') as f:
        f.write(new_content)
    
    print(f"Fixed {filepath}")
    return True

# Fix all files
for filepath in files:
    if os.path.exists(filepath):
        fix_file(filepath)
    else:
        print(f"File not found: {filepath}")

print("\\nAll files processed!")
