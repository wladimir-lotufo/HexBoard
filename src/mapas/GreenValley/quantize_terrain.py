from PIL import Image
import numpy as np
import os

# Define a strict palette (RGB)
PALETTE = {
    "Plains": (144, 238, 144),       # Light Green
    "Forest": (0, 100, 0),           # Dark Green
    "City": (169, 169, 169),         # Medium Gray
    "River": (173, 216, 230),        # Light Blue
    "Mountains": (139, 69, 19),      # Brown
    "Swamp": (47, 79, 79),           # Dark Slate Gray
    "Desert": (255, 255, 128),       # Light Yellow
    "Snow": (255, 250, 250),         # White
    "Asphalt Road": (105, 105, 105), # Dark Gray
    "Dirt Road": (210, 180, 140)     # Light Brown
}

# Pre-calculate palette values for faster lookup
PALETTE_VALUES = list(PALETTE.values())
PALETTE_NP = np.array(PALETTE_VALUES)

def closest_color_vectorized(img_array):
    """
    Finds the closest color in the palette for each pixel in the image using numpy.
    """
    h, w, c = img_array.shape
    img_flat = img_array.reshape(-1, 3) # (H*W, 3)
    
    # Initialize implementation with infinity
    min_dists = np.full(img_flat.shape[0], np.inf)
    best_indices = np.zeros(img_flat.shape[0], dtype=int)
    
    for i, color in enumerate(PALETTE_VALUES):
        diff = img_flat - color
        dists = np.sum(diff**2, axis=1)
        
        mask = dists < min_dists
        min_dists[mask] = dists[mask]
        best_indices[mask] = i
        
    # Map indices back to colors
    result_flat = PALETTE_NP[best_indices]
    return result_flat.reshape(h, w, 3).astype(np.uint8)

def main():
    base_dir = 'src/mapas/GreenValley'
    input_path = os.path.join(base_dir, 'terreno.png')
    ref_map_path = os.path.join(base_dir, 'mapa.png')
    
    if not os.path.exists(input_path):
        print(f"Error: {input_path} not found.")
        return

    print(f"Processing {input_path}...")
    try:
        img = Image.open(input_path).convert("RGB")
        
        # Check dimensions against mapa.png if it exists
        if os.path.exists(ref_map_path):
            with Image.open(ref_map_path) as ref_img:
                target_size = ref_img.size
            
            if img.size != target_size:
                print(f"Resizing terrain mask from {img.size} to {target_size} to match mapa.png")
                img = img.resize(target_size, Image.Resampling.NEAREST)
        
        img_array = np.array(img)
        
        # Quantize colors
        print("Quantizing colors...")
        new_img_array = closest_color_vectorized(img_array)
        
        new_img = Image.fromarray(new_img_array)
        new_img.save(input_path)
        print("Quantization complete.")
        
    except Exception as e:
        print(f"An error occurred: {e}")

if __name__ == "__main__":
    main()
