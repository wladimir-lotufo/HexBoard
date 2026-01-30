import numpy as np
from PIL import Image
import os
from scipy.spatial import cKDTree

# Defined Palette from /newmask
PALETTE = {
    "Plains": (144, 238, 144),
    "Forest": (0, 100, 0),
    "City": (169, 169, 169),
    "River": (173, 216, 230),
    "Mountain": (139, 69, 19),
    "Swamp": (47, 79, 79),
    "Desert": (255, 255, 128),
    "Snow": (255, 250, 250),
    "Asphalt Road": (105, 105, 105),
    "Dirt Road": (210, 180, 140)
}

# Create a list of colors and a mapping back to names (for debugging if needed, or just values)
color_values = list(PALETTE.values())
color_names = list(PALETTE.keys())

def quantize_image(image_path, output_path):
    print(f"Processing {image_path}...")
    try:
        img = Image.open(image_path).convert("RGB")
        img_array = np.array(img)
        
        # Flatten image for processing
        flat_img = img_array.reshape(-1, 3)
        
        # Build KDTree for nearest neighbor search
        tree = cKDTree(color_values)
        
        # Query nearest color for each pixel
        # k=1 returns (distances, indices)
        _, indices = tree.query(flat_img, k=1)
        
        # Map indices back to colors
        quantized_flat = np.array(color_values)[indices]
        
        # Reshape back to original image dimensions
        quantized_img_array = quantized_flat.reshape(img_array.shape).astype(np.uint8)
        
        # Create new image
        result_img = Image.fromarray(quantized_img_array)
        
        # Save
        result_img.save(output_path)
        print(f"Saved quantized image to {output_path}")
        
    except Exception as e:
        print(f"Error processing image: {e}")

if __name__ == "__main__":
    target_dir = r"c:\Users\wladi\source\repos\PanzerGeneral2\src\mapas\IronRidge"
    target_file = os.path.join(target_dir, "TerrainIronRidge.png")
    
    if os.path.exists(target_file):
        quantize_image(target_file, target_file)
    else:
        print(f"File not found: {target_file}")
