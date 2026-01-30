import os
from PIL import Image

maps_dir = r"c:\Users\wladi\source\repos\PanzerGeneral2\src\mapas"
search_size = (2400, 1800)

for map_name in ["GreenValley", "SilverCreek"]:
    map_path = os.path.join(maps_dir, map_name, "mapa.png")
    if os.path.exists(map_path):
        try:
            with Image.open(map_path) as img:
                print(f"{map_name}: {img.size}")
        except Exception as e:
            print(f"{map_name}: Error {e}")
    else:
        print(f"{map_name}: No mapa.png found")
