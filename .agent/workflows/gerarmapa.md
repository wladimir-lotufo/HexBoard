---
description: Gera novos (ou revisa existente) arquivos mapa.png e terreno.png com as especificações de terreno e dimensões corretas.
---

1. Verifique se o usuário informou o nome do mapa a ser alterado/revisado, ou se ele deseja criar um novo mapa.

2. Verifique com o usuário:
   - o tamanho do mapa que ele deseja (gere tres alternativas)
   - os tipos de terreno que ele deseja (gere 5 tipos de combinacoes de terreno)
   - Algum tipo de "tema" que ele deseja para o mapa (tres alternativas de tema)
   - Sugira tres alternativas de nomes para este novo mapa

3. Crie uma pasta dentro da pasta src/mapas com o nome sugerido pelo usuário, e salve or arquivos gerados dentro dela

4. **IMPORTANTE - Escala do Mapa:**
   - Todo mapa DEVE ter uma escala definida
   - Pergunte ao usuário qual escala deseja (sugestões: 1:25000, 1:50000, 1:100000)
   - A escala deve ser salva em um arquivo `escala.txt` dentro da pasta do mapa
   - Formato do arquivo: apenas o número da escala (ex: "50000" para 1:50000)
   - Esta escala será usada para converter pixels em metros no sistema de coordenadas

5. Gere uma imagem realista de um mapa (vista superior) com tamanho informado:
   - Prompt: ""A realistic top-down view map. High resolution, no borders. Cities must have NO outer walls. Urban areas consist of clusters of buildings with internal streets that are directly connected to the main regional roads. 80% asphalt roads (dark gray), 20% dirt roads (light brown). Integration between city streets and highways must be seamless."
   - Sem bordas ou molduras para não afetar as dimensões do mapa e terreno que devenm ser as mesmas
   - Nome do arquivo: `mapa.png`
   - O arquivo gerado deve ser salvo em `src/mapas/{nome}/mapa.png`.

6. Gere a imagem de máscara de terreno baseada no mapa gerado:
   - Use a imagem `src/mapas/{nome}/mapa.png` como referência (input image).
   - Prompt: "Create a flat segmentation map (terrain mask) of the provided map image. Replace realistic textures with SOLID FLAT COLORS (no shading, no gradients, no borders) corresponding to the terrain type at that location at pixel level. Make road lines SOLID and CONTINUOUS. Remove texture noise from roads. Dirt roads: solid colored. Asphalt roads: solid colored. 
   - A imagem Terreno deve ter a mesma dimensão que a imagem de Mapa
   - Follow this color mapping STRICTLY: 
        - Desert: Light Yellow (255, 255, 128) 
        - Forest: Dark Green (0, 100, 0) 
        - Plains: Light Green (144, 238, 144) 
        - River: Light Blue (173, 216, 230) 
        - Mountains: Brown (139, 69, 19) 
        - Snow: Light Gray (211, 211, 211) 
        - Dirt Road: Light Brown (210, 180, 140) (MUST be a continuous solid line, do not mix with asphalt) 
        - Asphalt Road: Dark Gray (105, 105, 105) (MUST be a continuous solid line, do not mix with dirt)
        - Building: Dark Brown (should be small connected squares)
        - The result should be a posterized map of terrain types. Maintain the exact layout and geography of the original image. Roads should be clear, continuous, and single-colored per segment.
   - Nome do arquivo: `terreno`
   - O arquivo gerado deve ser salvo em `src/terreno.png`.

7. Execute o seguinte script Python para forçar as cores exatas (Quantização) e remover ruído:
   ```python
   from PIL import Image
   import numpy as np

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
       "Building": (105, 105, 105)      # Dark Gray
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

   img = Image.open('src/terreno.png')
   img = img.convert("RGB")
   width, height = img.size
   
   # Process (can be slow pixel-by-pixel, for 1000x800 it's fine for a workflow script)
   # Optimizing with numpy would be better but this is dependency-light
   pixels = img.load()
   for x in range(width):
       for y in range(height):
           pixels[x, y] = closest_color(pixels[x, y])
           
   img.save('src/terreno.png')
   print("Quantization complete.")
   ```

8. Redimensione a imagem `terreno.png` para 1000x800 pixels.
   - Use python para garantir a dimensão exata.

9. Verifique se os arquivos `src/mapa.png` e `src/terreno.png` existem e estão com as dimensões corretas.
