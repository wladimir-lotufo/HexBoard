---
description: Gera ou atualiza o arquivo src/index.html com as funcionalidades de mapa interativo, seletor de mapas, detecção de terreno, e renderização de unidades militares.
---

**IMPORTANTE - Carregar Estado do Jogo:**
- Ao iniciar a aplicação, carregar automaticamente o arquivo `src/dados.json`
- O mapa definido em `dados.json` (campo `mapa.nome`) deve ser selecionado automaticamente
- As unidades definidas no JSON devem ser renderizadas sobre o mapa nas coordenadas especificadas

**IMPORTANTE - Controles de Interação:**
- O arrasto do mapa deve ser feito com o **botão direito do mouse** (não o botão esquerdo)
- O botão esquerdo fica livre para seleção de unidades e outras interações futuras
- Desabilitar o menu de contexto (context menu) no botão direito

1. Crie a estrutura HTML básica em `src/index.html`:
   - `html` e `body` devem ocupar 100% da tela, sem margens (`overflow: hidden`, background preto).
   - Adicione um elemento `<img>` com id `main-map` para exibir o mapa, esticado para cobrir toda a tela (`object-fit: fill`, `width: 100%`, `height: 100%`).
   - Adicione uma `div` com id `tooltip` (inicialmente oculta, `position: absolute`) para mostrar informações ao passar o mouse.
   - Adicione um container de UI (`#ui-container`) posicionado de forma absoluta no canto superior esquerdo para os controles.
   - Adicione um elemento para exibir a escala do mapa (`#scale-display`).

2. **Sistema de Coordenadas 2D:**
   - O mapa usa um sistema de coordenadas contínuo (não hexagonal)
   - Origem (0,0) fica no **canto inferior esquerdo** do mapa
   - Posições são medidas em **metros**
   - Implementar funções de conversão:
     - `worldToCanvas(x, y)`: converte coordenadas do mundo (metros) para pixels do canvas
     - `canvasToWorld(x, y)`: converte pixels do canvas para coordenadas do mundo (metros)
   - Fórmula de conversão Y: `canvasY = mapHeight - worldY * (canvasHeight / mapHeightMeters)`

3. **Carregar Estado do Jogo (dados.json):**
   - Ao iniciar, carregar o arquivo `src/dados.json`
   - Estrutura esperada (com Logística detalhada):
     ```json
     {
       "mapa": { "nome": "GreenValley", ... },
       "unidades": [
         {
           "id": 1,
           "tipo": "INF",
           "time": 0,
           "pais": "USA",       // Código do país (USA, GER, etc)
           "nome": "1ª Divisão",
           "posicaoX": 2500,
           "posicaoY": 3000,
           "rota": [],
           "raioVisao": 1000,
           "raioAtaque": 500,
           "combustivelAtual": 60,
           "combustivelMax": 60,
           "municaoAtual": 10,
           "municaoMax": 10
         }
       ]
     }
     ```

4. **Renderizar Unidades e Cartão de Detalhes:**
   - **Cartão de Detalhes (Tooltip Rico):**
     - Ao passar o mouse (`mouseenter`), abrir um cartão flutuante (`.unit-card`) com HTML rico.
     - **Conteúdo:** Bandeira, Nome, Tipo, Barras de Combustível e Munição.
   - **Menu Global do Mapa (Right-Click no Mapa):**
     - Ao clicar com botão direito fora de uma unidade:
       1. Interromper movimento de Pan se for um clique rápido.
       2. Abrir menu flutuante `#global-menu`.
     - **Funcionalidades:**
       - **Seletor de Mapa:** Dropdown para troca de territórios.
       - **Send Command:** Serializa o `gameState` atual (com rotas planejadas) para o clipboard (JSON).
   
   - **Menu de Ação de Unidade (Right-Click na Unidade):**
     - Ao clicar com botão direito na unidade:
       1. Interromper o Pan do mapa.
       2. **Fechar o Cartão de Detalhes (Unit Detail)** se estiver aberto.
       3. Abrir menu flutuante (`#action-menu`) na posição do mouse.
     - **UI:** Botões compactos (apenas ícones SVG), sem texto visível.
     - **Tooltip dos Botões:** Exibir nome da ação ("Move", "Attack") ao passar o mouse sobre o botão (tooltip CSS).
     - Fechar menu ao clicar fora.

   - **Planejamento de Rota (Move Action):**
     - Ao clicar em "Move", iniciar modo de planejamento (`isPlanningRoute`).
     - **Cursor:** Mudar para `crosshair`.
     - **Visualização (Canvas Overlay):**
       - Desenhar linhas conectando os checkpoints existentes da unidade (`unit.rota`).
       - **Linha Elástica:** Desenhar linha pontilhada dinâmica do último ponto (ou unidade) até o cursor do mouse.
     - **Interação:**
       - **Clique Esquerdo:** Adicionar novo Checkpoint (converter mouse -> coordenadas mundo e salvar em `unit.rota`).
       - **Clique Direito / Esc:** Finalizar planejamento e restaurar cursor.
     - Checkpoints devem ser desenhados visualmente (pequenos quadrados/ícones).

   - Converter coordenadas usando `worldToCanvas()`.

5. Implemente os Controles de UI:
   - Dentro de `#ui-container`, adicione um `<select id="map-selector">`.
   - Popule o select com as opções baseadas nas pastas existentes em `src/mapas` (ex: SilverCreek, GreenValley).
   - Adicione uma instrução visual: "Press ENTER to toggle view".
   - **Exibir Escala:** Mostrar a escala do mapa atual (ex: "Escala: 1:50000")
   - **Barra de Escala Visual:** Desenhar uma barra mostrando distâncias (ex: "0 — 1km — 2km")

6. Implemente a Lógica JavaScript para Detecção de Terreno:
   - Defina a Paleta de Cores Estrita (RGB exato) para mapear nomes de terreno:
     ```javascript
     const terrainPalette = {
         "255,255,128": "Deserto",
         "0,100,0":     "Floresta",
         "144,238,144": "Planície",
         "173,216,230": "Rio",
         "139,69,19":   "Montanhas",
         "211,211,211": "Neve",
         "210,180,140": "Estrada de Terra",
         "105,105,105": "Estrada de Asfalto"
     };
     ```
   - Crie um `<canvas>` off-screen para carregar a imagem `terreno.png`.
   - Implemente a função `getTerrainAt(x, y)`:
     - Deve considerar o redimensionamento da imagem na tela (`getBoundingClientRect`).
     - Deve ler o pixel correspondente no canvas oculto.
     - Deve buscar a cor exata ("r,g,b") na paleta.

7. Implemente a Lógica de Navegação e Visualização:
   - Estado global: `currentMapFolder`, `isShowingTerrain` (boolean), `gameState` (dados.json).
   - Função `loadMap(folderName)`:
     - Atualiza a imagem principal.
     - Carrega a nova máscara `terreno.png` no canvas oculto.
     - Carrega o arquivo `escala.txt` da pasta do mapa.
     - Atualiza a exibição da escala.
   - Evento `change` no Dropdown: Chama `loadMap`.
   - Evento `keydown` (Enter): Alterna `isShowingTerrain` e atualiza a fonte da imagem principal (`mapa.png` <-> `terreno.png`).
   - Evento `mousemove` na imagem: 
     - Chama `getTerrainAt` e atualiza o tooltip com nome do terreno.
     - Se mouse sobre unidade, mostrar informações da unidade.

8. Garanta que o estilo visual (CSS) permita que os controles fiquem sobre o mapa (z-index alto) e que o tooltip siga o mouse sem interferir nos eventos (`pointer-events: none`).


9. **Processamento de Turno (Game Engine):**
   - Utilizar script Python `src/game_engine.py`.
   - **Requisitos:** Biblioteca `Pillow` instalada (`pip install Pillow`).
   - **Fluxo:**
     1. O JSON do jogo deve conter o objeto `parametros: { "horasPorTurno": 1.0 }`.
     2. Copiar JSON do navegador (botão "Send") ou salvar arquivo.
     3. Executar engine: `python src/game_engine.py --input dados.json --output dados_atualizados.json`
     4. **Lógica Avançada:**
        - Carrega `src/mapas/GreenValley/terreno.png`.
        - Verifica cor do terreno a cada passo de movimento.
        - Aplica tabela de custos (Velocidade/Combustível) por tipo de unidade (INF, TNK, etc).
     5. Atualizar jogo com novo JSON.

