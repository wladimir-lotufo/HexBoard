---
description: Gera novos (ou revisa existente) arquivo Mapa{Nome}.png (imagem realista).
---

1. Verifique se o usuário informou o nome do mapa a ser alterado/revisado, ou se ele deseja criar um novo mapa.
   1. Usuário deve informar um TerrainMap de entrada

2. Verifique com o usuário:
   - Considerar o tamanho do TerrainMask para o Mapa (mesmo tamanho)
   - Pergunte ao usuário o tipo de "tema" que ele deseja para o mapa (tres alternativas numeradas)
   - Pergunte ao usuário o nome do mapa (se for novo), Sugira tres alternativas numeradas.
   - Insira no canto inferior uma Escala (em metros por pixel)
   - Insira um Grid a cada 1 Km
   - Insira Nome de Cidades, Principais enstradas, Rios, Montanhas, Cadeias Montanhosas, Pontes, etc.

4. **IMPORTANTE - Escala do Mapa:**
   - O desenho artistico deverá estar mapeado pixel a pixel com o TerrainMask
   - Todo mapa DEVE ter uma escala definida em **Metros por Pixel**.
   - A escala deve ser salva em um arquivo `escala.txt` dentro da pasta do mapa
   - Esta escala será usada para calcular as dimensões reais do mundo (Largura da Imagem * Escala).

5. Gere uma imagem realista de um mapa (vista superior) com tamanho informado:
   - Adicione detalhes artísticos do tema solicitado no prompt.
   - **IMPORTANTE:** O desenho DEVE incluir Labels/Nomes visíveis para facilitar a comunicação:
     - Nome das Cidades (Ex: "Silver City", "Northwatch")
     - Nome dos Rios, Montanhas e Estradas principais.
     - **IMPORTANTE:** O desenho deve reproduzir as estradas entre os quarteirões nas cidades.
   - **IMPORTANTE:** O desenho DEVE incluir uma BARRA DE ESCALA no canto inferior direito.
   - Sem bordas ou molduras externas (apenas a arte do mapa).
   - Nome do arquivo: `Mapa{Nome}.png`
   - O arquivo gerado deve ser salvo em `src/mapas/{nome}/Mapa{Nome}.png`.
