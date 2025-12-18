---
description: Gera novos (ou revisa existente) arquivo terreno.png (máscara de terreno) baseado no mapa.png existente.
---

1. Verifique se o mapa informado existe (`src/mapas/{nome}/mapa.png`). Se não, pare e avise o usuário.

2. Gere a imagem de máscara de terreno baseada no `mapa.png`:
   - Use a imagem `src/mapas/{nome}/mapa.png` como referência (input image).
   - Prompt: "Create a flat segmentation map (terrain mask) of the provided map image. Replace realistic textures with SOLID FLAT COLORS (no shading, no gradients, no borders) corresponding to the terrain type at that location at pixel level. Make road lines SOLID and CONTINUOUS.
   - **MAPPING RULES (RGB):**
     - Plains/Grass: (144, 238, 144) - Light Green
     - Forest: (0, 100, 0) - Dark Green
     - City/Buildings: (169, 169, 169) - Medium Gray
     - River: (173, 216, 230) - Light Blue
     - Mountain: (139, 69, 19) - Brown
     - Swamp: (47, 79, 79) - Dark Slate Gray
     - Desert: (255, 255, 128) - Light Yellow
     - Snow: (255, 250, 250) - White
     - Asphalt Road: (105, 105, 105) - Dark Gray (Solid Line)
     - Dirt Road: (210, 180, 140) - Light Brown (Solid Line)
   - The result should be a posterized map of terrain types. Maintain exact layout."
   - Nome do arquivo: `terreno` (vai gerar `terreno.png`).
   - Salve em `src/mapas/{nome}/terreno.png`.

3. Execute o script Python para Quantização (forçar cores exatas):
   
   ```python
   from PIL import Image
   import os
   
   # Official Palette
   PALETTE = {
       "Campo Aberto": (144, 238, 144),
       "Floresta": (0, 100, 0),
       "Cidade": (169, 169, 169),
       "Estrada Asfalto": (105, 105, 105),
       "Estrada Terra": (210, 180, 140),
       "Rio": (173, 216, 230),
       "Montanha": (139, 69, 19),
       "Pântano": (47, 79, 79),
       "Deserto": (255, 255, 128),
       "Neve": (255, 250, 250)
   }
   
   def closest_color(requested_color):
       min_diff = float('inf')
       best_color = (0,0,0)
       rc, gc, bc = requested_color
       
       for name, (r, g, b) in PALETTE.items():
           diff = (r - rc)**2 + (g - gc)**2 + (b - bc)**2
           if diff < min_diff:
               min_diff = diff
               best_color = (r, g, b)
       return best_color

   # Path logic (adjust as needed for workflow context)
   # Assumes running from root, taking map name as arg or hardcoded for now in the snippet step
   # You should replace 'GreenValley' with the actual map name in the logic below when running interactively
   
   map_name = "{NOME_DO_MAPA}" # Workflow agent replace this
   path = f'src/mapas/{map_name}/terreno.png'
   
   if not os.path.exists(path):
       print(f"File not found: {path}")
       exit()
       
   img = Image.open(path).convert("RGB")
   width, height = img.size
   pixels = img.load()
   
   for x in range(width):
       for y in range(height):
           pixels[x, y] = closest_color(pixels[x, y])
           
   img.save(path)
   print(f"Quantization complete for {map_name}.")
   ```

4. Verifique as dimensões finais e confirme o sucesso.
