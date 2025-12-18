---
description: Gera novos (ou revisa existente) arquivo mapa.png (imagem realista).
---

1. Verifique se o usuário informou o nome do mapa a ser alterado/revisado, ou se ele deseja criar um novo mapa.

2. Verifique com o usuário:
   - o tamanho do mapa que ele deseja (gere tres alternativas)
   - Algum tipo de "tema" que ele deseja para o mapa (tres alternativas de tema)
   - Sugira tres alternativas de nomes para este novo mapa (se for novo)

3. Crie uma pasta dentro da pasta src/mapas com o nome sugerido pelo usuário (se não existir).

4. **IMPORTANTE - Escala do Mapa:**
   - Todo mapa DEVE ter uma escala definida
   - Pergunte ao usuário qual escala deseja (sugestões: 1:25000, 1:50000, 1:100000)
   - A escala deve ser salva em um arquivo `escala.txt` dentro da pasta do mapa
   - Formato do arquivo: apenas o número da escala (ex: "50000" para 1:50000)

5. Gere uma imagem realista de um mapa (vista superior) com tamanho informado:
   - Prompt Base: "A realistic top-down view map. High resolution, no borders. Cities must have NO outer walls. Urban areas consist of clusters of buildings with internal streets that are directly connected to the main regional roads. 80% asphalt roads (dark gray), 20% dirt roads (light brown). Integration between city streets and highways must be seamless."
   - Adicione detalhes do tema solicitado no prompt.
   - Sem bordas ou molduras.
   - Nome do arquivo: `mapa.png`
   - O arquivo gerado deve ser salvo em `src/mapas/{nome}/mapa.png`.

6. Confirme a geração e solicite ao usuário para executar o workflow `/newmask` para gerar a máscara de terreno correspondente.
