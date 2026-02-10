import os

target_dir = r"C:\Users\wladi\source\repos\PartnerShip\.agent\knowledge\DesignSystem\components"

if not os.path.exists(target_dir):
    print(f"Error: Directory not found: {target_dir}")
    exit(1)

files = [f for f in os.listdir(target_dir) if f.endswith('.html')]

# Lines to add. Note the indentation matching standard HTML (4 spaces)
lines_to_add = [
    '    <link href="https://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700" rel="stylesheet" type="text/css">\n',
    '    <link rel="stylesheet" href="../../../../Vendor/animate.css/animate.min.css">\n',
    '    <link rel="stylesheet" href="../../../../Icons/pe-icon-7-stroke/css/pe-icon-7-stroke.css">\n',
    '    <link rel="stylesheet" href="../../../../Content/custom.css">\n',
    '    <link rel="stylesheet" href="../../../../Content/spinner1.css">\n',
    '    <link rel="stylesheet" href="../../../../Vendor/toastr214/build/toastr.css">\n'
]

count = 0
for filename in files:
    filepath = os.path.join(target_dir, filename)
    try:
        with open(filepath, 'r', encoding='utf-8') as f:
            lines = f.readlines()
        
        content = "".join(lines)
        if "pe-icon-7-stroke.css" in content:
            print(f"Skipping {filename} (already updated)")
            continue

        insert_idx = -1
        # Try to find Content/style.css to insert before
        for i, line in enumerate(lines):
            if "Content/style.css" in line:
                insert_idx = i
                break
        
        # Fallback: After bootstrap if style.css not found
        if insert_idx == -1:
             for i, line in enumerate(lines):
                if "bootstrap.min.css" in line:
                    insert_idx = i + 1
                    break

        if insert_idx != -1:
            # We insert in reverse order if we insert at the same index, 
            # BUT list.insert inserts *before* the index.
            # So if we iterate lines_to_add normally and insert at insert_idx, 
            # we need to increment insert_idx.
            # Or just reverse logical insert.
            
            # Let's simple string injection
            # Actually list insert is cleaner.
            # To preserve order:
            for line in reversed(lines_to_add):
                lines.insert(insert_idx, line)
            
            with open(filepath, 'w', encoding='utf-8') as f:
                f.writelines(lines)
            print(f"Updated {filename}")
            count += 1
        else:
            print(f"Warning: Could not find insertion point (style.css or bootstrap) in {filename}")

    except Exception as e:
        print(f"Error processing {filename}: {e}")

print(f"Total files updated: {count}")
