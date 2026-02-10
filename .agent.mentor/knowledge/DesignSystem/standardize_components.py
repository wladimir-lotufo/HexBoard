import os
import re
from pathlib import Path

# Base directory for components
COMPONENTS_DIR = r"c:\Users\wladi\source\repos\PartnerShip\.agent\knowledge\DesignSystem\components"

def extract_component_info(content):
    """Extract component name and description from existing content"""
    # Try to find existing header
    header_match = re.search(r'<h[14][^>]*>([^<]+)</h[14]>', content)
    desc_match = re.search(r'<(?:p|small)[^>]*>([^<]+)</(?:p|small)>', content)
    
    component_name = header_match.group(1) if header_match else "Component"
    description = desc_match.group(1) if desc_match else "Component description"
    
    return component_name, description

def extract_body_content(content):
    """Extract the main content sections (everything after header, before scripts)"""
    # Find the start of content sections (after header)
    content_start = None
    
    # Look for ds-section or the first section after header
    section_match = re.search(r'<div class="ds-section">', content)
    if section_match:
        content_start = section_match.start()
    
    # Find where scripts start
    script_match = re.search(r'<script src=', content)
    content_end = script_match.start() if script_match else len(content)
    
    if content_start:
        body_content = content[content_start:content_end].strip()
        
        # Robustly strip closing divs by balancing tags
        # Count open vs close divs in the extracted content
        open_divs = len(re.findall(r'<div\b', body_content))
        close_divs = len(re.findall(r'</div>', body_content))
        
        excess_closing = close_divs - open_divs
        
        if excess_closing > 0:
            # Create a regex to match exactly 'excess_closing' divs at the end
            # We match the last N </div> tags, possibly separated by whitespace
            pattern = r'(\s*</div>){' + str(excess_closing) + r'}\s*$'
            body_content = re.sub(pattern, '', body_content, count=1)
            
    else:
        # Fallback: extract everything between body tags
        body_match = re.search(r'<body>(.*?)<script', content, re.DOTALL)
        if body_match:
            body_content = body_match.group(1).strip()
            # Remove old header if present
            body_content = re.sub(r'<div class="ds-content-header">.*?</div>\s*', '', body_content, flags=re.DOTALL)
            body_content = re.sub(r'<div[^>]*class="[^"]*hpanel[^"]*"[^>]*>.*?</div>\s*</div>', '', body_content, flags=re.DOTALL, count=1)
            
            # Apply same balancing logic for fallback
            open_divs = len(re.findall(r'<div\b', body_content))
            close_divs = len(re.findall(r'</div>', body_content))
            excess_closing = close_divs - open_divs
            if excess_closing > 0:
                pattern = r'(\s*</div>){' + str(excess_closing) + r'}\s*$'
                body_content = re.sub(pattern, '', body_content, count=1)
        else:
            body_content = ""
            
    return body_content

def extract_scripts(content):
    """Extract all script tags from the end of the file"""
    scripts_match = re.search(r'(<script.*?</script>.*?)</body>', content, re.DOTALL)
    if scripts_match:
        return scripts_match.group(1).strip()
    return ""

def extract_head_content(content):
    """Extract content from head tag (title, links, styles)"""
    head_match = re.search(r'<head>(.*?)</head>', content, re.DOTALL)
    if head_match:
        head_content = head_match.group(1).strip()
        # Remove body style definition
        head_content = re.sub(r'body\s*\{\s*background-color:\s*#fff;\s*padding:\s*20px;\s*\}', '', head_content, flags=re.DOTALL)
        # Also remove the one with overflow-y if present (seen in header2.html)
        head_content = re.sub(r'body\s*\{\s*overflow-y:\s*auto;\s*padding-bottom:\s*20px;\s*background-color:\s*#f3f3f4;\s*\}', '', head_content, flags=re.DOTALL)
        # Clean up empty style tags if any (optional, but good for cleanliness if style becomes empty)
        # For now, just removing the body rule is sufficient.
        return head_content
    return ""

def create_standardized_html(component_name, description, head_content, body_content, scripts):
    """Create standardized HTML structure"""
    return f'''<!DOCTYPE html>
<html lang="pt-BR">

<head>
{head_content}
</head>

<body>
    <div id="viw_Component" class="content-full">
        <div class="row">
            <div class="col-lg-12">
                <div class="hpanel">
                    <!-- Header Section -->
                    <div id="PanelHeader" class="panel-heading">
                        <div class="row">
                            <div class="col-lg-12">
                                <h4 class="font-extra-bold text-primary">{component_name}</h4>
                                <small class="text-muted">{description}</small>
                            </div>
                        </div>
                    </div>
                    
                    <!-- Content Section -->
                    <div id="PanelBody" class="panel-body tooltip-demo">
{body_content}
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    {scripts}
</body>

</html>'''

def process_component_file(filepath):
    """Process a single component file"""
    print(f"Processing: {filepath.name}")
    
    try:
        with open(filepath, 'r', encoding='utf-8') as f:
            content = f.read()
        
        # Extract parts
        component_name, description = extract_component_info(content)
        head_content = extract_head_content(content)
        body_content = extract_body_content(content)
        scripts = extract_scripts(content)
        
        # Create new content
        new_content = create_standardized_html(
            component_name, 
            description, 
            head_content, 
            body_content, 
            scripts
        )
        
        # Write back
        with open(filepath, 'w', encoding='utf-8') as f:
            f.write(new_content)
        
        print(f"  [OK] Successfully updated {filepath.name}")
        return True
        
    except Exception as e:
        print(f"  [ERROR] Error processing {filepath.name}: {str(e)}")
        return False

def main():
    """Main function to process all component files"""
    components_path = Path(COMPONENTS_DIR)
    
    if not components_path.exists():
        print(f"Error: Directory not found: {COMPONENTS_DIR}")
        return
    
    # Get all HTML files except welcome.html (special case)
    html_files = [f for f in components_path.glob("*.html") if f.name != "welcome.html"]
    
    print(f"Found {len(html_files)} component files to process\n")
    
    success_count = 0
    fail_count = 0
    
    for filepath in sorted(html_files):
        if process_component_file(filepath):
            success_count += 1
        else:
            fail_count += 1
    
    print(f"\n{'='*60}")
    print(f"Processing complete!")
    print(f"  Success: {success_count}")
    print(f"  Failed: {fail_count}")
    print(f"{'='*60}")

if __name__ == "__main__":
    main()
