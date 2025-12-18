from PIL import Image
import os

# Define a strict palette (RGB)
PALETTE = {
    "Desert": (255, 255, 128),      # Light Yellow
    "Forest": (0, 100, 0),          # Dark Green
    "Plains": (144, 238, 144),      # Light Green
    "River": (173, 216, 230),       # Light Blue
    "Mountains": (139, 69, 19),     # Brown
    "Snow": (211, 211, 211),        # Light Gray
    "Dirt Road": (210, 180, 140),   # Light Brown
    "Asphalt Road": (105, 105, 105) # Dark Gray
}

def closest_color(requested_color):
    min_colors = {}
    for key, name in enumerate(PALETTE):
        r_c, g_c, b_c = PALETTE[name]
        rd = (r_c - requested_color[0]) ** 2
        gd = (g_c - requested_color[1]) ** 2
        bd = (b_c - requested_color[2]) ** 2
        min_colors[(rd + gd + bd)] = PALETTE[name]
    return min_colors[min(min_colors.keys())]

# Target file
target_file = 'src/mapas/SilverCreek/terreno.png'

if not os.path.exists(target_file):
    print(f"Error: {target_file} not found.")
    exit(1)

img = Image.open(target_file)
img = img.convert("RGB")

# Force resize to 1000x800 for SilverCreek (as per original specs)
if img.size != (1000, 800):
   img = img.resize((1000, 800))

width, height = img.size

pixels = img.load()
for x in range(width):
    for y in range(height):
        pixels[x, y] = closest_color(pixels[x, y])

img.save(target_file)
print("Quantization complete for SilverCreek.")
