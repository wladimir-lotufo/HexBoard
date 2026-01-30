import os
from PIL import Image

# Source: The generated artifact
# I need to find the specific file in the artifact directory since the timestamp is variable
artifact_dir = r"C:\Users\wladi\.gemini\antigravity\brain\63d0ceee-a738-489f-b61d-0bdf062d928f"
target_dir = r"c:\Users\wladi\source\repos\PanzerGeneral2\src\mapas\IronRidge"
target_file = os.path.join(target_dir, "TerrainIronRidge.png")
target_size = (2400, 1800)

# Find the latest generated image (starts with terrain_ironridge)
generated_files = []
for f in os.listdir(artifact_dir):
    if f.startswith("terrain_ironridge") and f.endswith(".png"):
        full_path = os.path.join(artifact_dir, f)
        generated_files.append((full_path, os.path.getmtime(full_path)))

# Sort by time, descending
generated_files.sort(key=lambda x: x[1], reverse=True)

if generated_files:
    generated_file = generated_files[0][0]

if generated_file:
    print(f"Found generated image: {generated_file}")
    try:
        with Image.open(generated_file) as img:
            print(f"Original size: {img.size}")
            # Resize
            resized_img = img.resize(target_size, Image.Resampling.LANCZOS)
            
            # Ensure target directory exists
            os.makedirs(target_dir, exist_ok=True)
            
            # Save
            resized_img.save(target_file)
            print(f"Saved resized image to: {target_file}")
            print(f"New size: {resized_img.size}")
            
    except Exception as e:
        print(f"Error processing image: {e}")
else:
    print("No generated image found in artifact directory.")
