from PIL import Image
import numpy as np
import os

# Define a strict palette (RGB)
PALETTE = {
    "Desert": (255, 255, 128),       # Light Yellow
    "Forest": (0, 100, 0),           # Dark Green
    "Plains": (144, 238, 144),       # Light Green
    "River": (173, 216, 230),        # Light Blue
    "Mountains": (139, 69, 19),      # Brown
    "Snow": (211, 211, 211),         # Light Gray
    "Dirt Road": (210, 180, 140),    # Light Brown
    "Asphalt Road": (105, 105, 105), # Dark Gray
    "Building": (105, 105, 105)      # Dark Gray (same as asphalt for now, or use a distinct one if needed)
}

# Pre-calculate palette values for faster lookup
PALETTE_VALUES = list(PALETTE.values())
PALETTE_NP = np.array(PALETTE_VALUES)

def closest_color_vectorized(img_array):
    """
    Finds the closest color in the palette for each pixel in the image using numpy.
    """
    # img_array shape is (H, W, 3)
    # PALETTE_NP shape is (N, 3)
    
    # Reshape image to (H*W, 1, 3) and palette to (1, N, 3) for broadcasting
    # This creates a difference matrix of shape (H*W, N, 3)
    # However, for large images this consumes too much memory.
    # We can iterate over the palette instead.
    
    h, w, c = img_array.shape
    img_flat = img_array.reshape(-1, 3) # (H*W, 3)
    
    # Calculate distances
    # We want to find index of min distance for each pixel
    # dist = sum((pixel - palette_color)^2)
    
    # Initialize implementation with infinity
    min_dists = np.full(img_flat.shape[0], np.inf)
    best_indices = np.zeros(img_flat.shape[0], dtype=int)
    
    for i, color in enumerate(PALETTE_VALUES):
        # color is (3,)
        # dists is (H*W,)
        diff = img_flat - color
        dists = np.sum(diff**2, axis=1)
        
        mask = dists < min_dists
        min_dists[mask] = dists[mask]
        best_indices[mask] = i
        
    # Map indices back to colors
    result_flat = PALETTE_NP[best_indices]
    return result_flat.reshape(h, w, 3).astype(np.uint8)

def main():
    input_path = 'src/mapas/GreenValley/terreno.png'
    
    if not os.path.exists(input_path):
        print(f"Error: {input_path} not found.")
        return

    print(f"Processing {input_path}...")
    try:
        img = Image.open(input_path)
        img = img.convert("RGB")
        
        # Resize to 1000x800 strictly
        img = img.resize((1000, 800), Image.Resampling.LANCZOS)
        
        img_array = np.array(img)
        
        # Quantize colors
        new_img_array = closest_color_vectorized(img_array)
        
        new_img = Image.fromarray(new_img_array)
        new_img.save(input_path)
        print("Quantization and resizing complete.")
        
    except Exception as e:
        print(f"An error occurred: {e}")

if __name__ == "__main__":
    main()
