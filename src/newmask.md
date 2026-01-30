---
description: Gera novos (ou revisa existente) arquivo Terrain{Nome}.png que é uma máscara de terreno baseado nas regras definidas pelo usuário.
---

1. Gere a imagem de máscara de terreno:
   - Gere a imagem `src/mapas/{nome}/Terrain{Nome}.png` 
   - Pergunte sobre caracteristicas como:
     - 1 Percentual dos tipos de Terreno: %Planicie, % Floresta, % Cidade, % Rio, % Montanha, % Pantanal, % Deserto, % Neve
     - 2 Percentual de Estarads de Asfalto e de Estradas de Terra.
     - 3 Quantidades e tamenhos de cidades
     - 4 Tamanho do Mapa em pixels
     - 5 Escala do mapa: Metros/Pixel
   
   - Regras: 
        1- A Terrain Mask for a top-down view map. 
        2- High resolutionA Terrain Mask for a top-down view map. High resolution, no borders. 
        3- Cities must have NO outer walls. Urban areas consist of clusters of buildings with internal straighe or curved streets
        4- Integration between city streets and highways must be seamless."   
   
   - **MAPPING RULES (RGB):**
     - Plains/Grass: (144, 238, 144) - Light Green
     - Forest: (0, 100, 0) - Dark Green
     - City/Buildings: (169, 169, 169) - Medium Gray. **IMPORTANT:** Cities must be composed of distinct "Building Blocks" (irregular polygons) separated by a CLEAR GRID of Streets (Dark Gray). Do not create solid gray blobs. The street network must cut through the city.
     - River: (173, 216, 230) - Light Blue
     - Mountain: (139, 69, 19) - Brown
     - Swamp: (47, 79, 79) - Dark Slate Gray
     - Desert: (255, 255, 128) - Light Yellow
     - Snow: (255, 250, 250) - White
     - Asphalt Road: (105, 105, 105) - Dark Gray (Solid Line)
     - Dirt Road: (210, 180, 140) - Light Brown (Solid Line)
   
   - Salve em `src/mapas/{nome}/Terrain{Nome}.png`.

2. Verifique as dimensões finais e confirme o sucesso.
