# Regras e Mecânicas do Panzer General (DOS)

Este documento detalha as regras, fórmulas e estratégias do clássico jogo **Panzer General**.

## 1. Visão Geral
Panzer General é um jogo de estratégia por turnos operacionais (wargame) onde o jogador comanda exércitos (Eixo ou Aliados) em um mapa hexagonal. O objetivo é capturar cidades-chave (Objetivos) dentro de um limite de turnos.

## 2. Conceitos de Movimento

> **Nota:** O custo de movimento é acumulativo ao longo do caminho. Uma unidade com 6 pontos de movimento pode percorrer 6 hexágonos de campo aberto ou apenas 2 hexágonos de floresta (custo 3 cada).
>
> Cada unidade possui uma tabela específica de **velocidade** (hexágonos/turno) e **custo de movimento** por terreno.

## 3. Tipos de Terreno

Os diferentes tipos de terreno afetam o movimento e o combate das unidades.

### 3.1 Campo Aberto (Clear)
- Terreno plano e aberto
- Ideal para unidades mecanizadas
- Sem obstáculos ao movimento

### 3.2 Floresta (Forest)
- Vegetação densa
- Dificulta movimento de veículos
- Favorece infantaria

### 3.3 Cidade (City)
- Área urbana ou fortificada
- Movimento facilitado por estradas
- Excelente para defesa

### 3.4 Buildings (Building)
- Predios ou Casas
- Movimento facilitado por ruas
- Excelente para defesa


### 3.5 Montanha (Mountain)
- Terreno elevado e rochoso
- Intransitável para a maioria dos veículos
- Apenas tropas especializadas (Alpini) podem atravessar facilmente

### 3.5 Rio (River)
- Obstáculo aquático
- Requer pontes ou balsas para atravessar
- Penaliza defesa se atacado durante travessia

### 3.6 Pântano (Swamp)
- Terreno alagado e lamacento
- Dificulta movimento de todas as unidades
- Penaliza combate

## 4. Classes de Unidades

As unidades possuem atributos principais: **Ataque Suave (Soft)**, **Ataque Duro (Hard)**, **Ataque Aéreo**, **Ataque Naval**, **Defesa Terrestre**, **Defesa Aérea**, **Iniciativa**, **Alcance** e **Movimento**.

### 4.1 Infantaria (INF)

- **Função Principal:** Capturar cidades, combate em terreno fechado
- **Raio de Visão:** 1.0 km (visão direta melhorada em terreno fechado)
- **Raio de Ataque:** 500 m
- **Forças:** Terrenos complexos (Cidades, Florestas, Montanhas), defesa entrincheirada
- **Fraquezas:** Campo aberto contra tanques e artilharia
- **Tipo de Locomoção:** Pernas

- **Capacidade de Recursos:** 60 kg (Individual/Grupo)
- **Munição:** 10 ataques (combate sustentado)

**Velocidade, Consumo e Autonomia por Terreno:**

| Métrica       | Campo Aberto | Floresta | Cidade | Montanha | Rio | Pântano |
| :----------- | :----------: | :------: | :----: | :------: | :-: | :-----: |
| Velocidade   |   7.5 km/h   | 3.8 km/h | 7.5 km/h | 2.5 km/h |  -  | 2.5 km/h |
| Consumo      |   0.5 kg/km  |  1.0 kg/km | 0.5 kg/km|  1.5 kg/km|  -  |  2.0 kg/km|
| Autonomia    |    120 km    |   60 km  |  120 km|   40 km  |  -  |   30 km |

### 4.2 Tanques (TNK)

- **Função Principal:** Ponta de lança, ruptura de linhas, cercos
- **Raio de Visão:** 1.0 km (visão limitada por óticas do veículo)
- **Raio de Ataque:** 1.5 km
- **Forças:** Campo aberto, iniciativa alta, ataque duro
- **Fraquezas:** Cidades, terrenos fechados, defesa passiva
- **Tipo de Locomoção:** Lagarta

- **Capacidade de Combustível:** 500 Litros
- **Munição:** 8 ataques (alto calibre/consumo)

**Velocidade, Consumo e Autonomia por Terreno:**

| Métrica       | Campo Aberto | Floresta | Cidade | Montanha | Rio | Pântano |
| :----------- | :----------: | :------: | :----: | :------: | :-: | :-----: |
| Velocidade   |   10 km/h    |  5 km/h  | 10 km/h |    -     |  -  | 3.3 km/h |
| Consumo      |   2.5 L/km   |  5.0 L/km  | 2.5 L/km | Bloqueado |  -  |  8.0 L/km |
| Autonomia    |    200 km    |   100 km   |  200 km  |     0     |  -  |   62 km  |

### 4.3 Antitanque (AT)

- **Função Principal:** Defesa contra blindados
- **Raio de Visão:** 1.5 km (óticas de mira de longo alcance)
- **Forças:** Defesa alta contra tanques (especialmente entrincheirado)
- **Fraquezas:** Ataque ofensivo baixo, infantaria, artilharia
- **Tipo de Locomoção:** Roda (rebocado) ou Lagarta (autopropulsado)

- **Capacidade de Combustível:** 300 Litros
- **Munição:** 6 ataques (projéteis pesados)

**Velocidade, Consumo e Autonomia por Terreno (Roda):**

| Métrica       | Campo Aberto | Floresta | Cidade | Montanha | Rio | Pântano |
| :----------- | :----------: | :------: | :----: | :------: | :-: | :-----: |
| Velocidade   |   3.8 km/h   | 2.5 km/h | 7.5 km/h |    -     |  -  | 1.9 km/h |
| Consumo      |   3.0 L/km   |  6.0 L/km  | 2.0 L/km | Bloqueado |  -  |  7.5 L/km |
| Autonomia    |    100 km    |   50 km    |  150 km  |     0     |  -  |   40 km  |

### 4.4 Reconhecimento (RCN)

- **Função Principal:** Olheiros, movimento rápido
- **Raio de Visão:** 3.0 km (excelente ótica e mobilidade para escolta)
- **Raio de Ataque:** 1.0 km
- **Forças:** Visão estendida, movimento rápido, reconhecimento em força-se em etapas)
- **Fraquezas:** Combate direto contra qualquer unidade pesada
- **Tipo de Locomoção:** Roda (veículos leves)

- **Capacidade de Combustível:** 200 Litros
- **Munição:** 12 ataques (armas leves)

**Velocidade, Consumo e Autonomia por Terreno:**

| Métrica       | Campo Aberto | Floresta | Cidade | Montanha | Rio | Pântano |
| :----------- | :----------: | :------: | :----: | :------: | :-: | :-----: |
| Velocidade   |   12.5 km/h  | 6.3 km/h | 12.5 km/h |    -     |  -  | 4.2 km/h |
| Consumo      |   1.0 L/km   |  2.5 L/km  | 1.0 L/km | Bloqueado |  -  |  4.0 L/km |
| Autonomia    |    200 km    |   80 km    |  200 km  |     0     |  -  |   50 km  |

### 4.5 Artilharia (ART)

- **Função Principal:** Suporte, supressão
- **Raio de Visão:** 1.5 km (observadores avançados e cálculos balísticos)
- **Raio de Ataque:** 15.0 km
- **Forças:** Ataque à distância, supressão, apoio indiretoaliação), causa **Supressão**
- **Fraquezas:** Defesa nula se atacada diretamente
- **Tipo de Locomoção:** Roda (rebocada) ou Lagarta (autopropulsada)

- **Capacidade de Combustível:** 250 Litros
- **Munição:** 5 ataques (barragem intensa, requer reabastecimento frequente)

**Velocidade, Consumo e Autonomia por Terreno (Roda):**

| Métrica       | Campo Aberto | Floresta | Cidade | Montanha | Rio | Pântano |
| :----------- | :----------: | :------: | :----: | :------: | :-: | :-----: |
| Velocidade   |   2.5 km/h   | 1.3 km/h | 5 km/h |    -     |  -  | 1 km/h  |
| Consumo      |   4.0 L/km   |  8.0 L/km  | 3.0 L/km | Bloqueado |  -  |  12.0 L/km|
| Autonomia    |    62 km     |   31 km    |  83 km   |     0     |  -  |   20 km  |

### 4.6 Antiaérea (AA)

- **Função Principal:** Proteção contra aviões
- **Raio de Visão:** 5.0 km (radares e óticas antiaéreas)
- **Forças:** Abate aviões inimigos, protege unidades adjacentes
- **Fraquezas:** Combate terrestre
- **Tipo de Locomoção:** Roda ou Lagarta

- **Capacidade de Combustível:** 300 Litros
- **Munição:** 15 ataques (alta cadência)

**Velocidade, Consumo e Autonomia por Terreno (Roda):**

| Métrica       | Campo Aberto | Floresta | Cidade | Montanha | Rio | Pântano |
| :----------- | :----------: | :------: | :----: | :------: | :-: | :-----: |
| Velocidade   |   3.8 km/h   | 2.5 km/h | 7.5 km/h |    -     |  -  | 1.9 km/h |
| Consumo      |   3.0 L/km   |  6.0 L/km  | 2.5 L/km | Bloqueado |  -  |  7.5 L/km |
| Autonomia    |    100 km    |   50 km    |  120 km  |     0     |  -  |   40 km  |

### 4.7 Caças (FTR)

- **Função Principal:** Superioridade aérea
- **Raio de Visão:** 10.0 km (visão aérea)
- **Forças:** Abate bombardeiros, protege unidades terrestres de ataques aéreos
- **Fraquezas:** Defesa antiaérea terrestre
- **Tipo de Locomoção:** Aéreo
- **Alcance de Missão:** 6-12 hexágonos (varia por modelo)
- **Capacidade de Combustível:** 800 Litros
- **Munição:** 6 surtidas (combate aéreo limitado)

**Velocidade, Consumo e Autonomia por Terreno:**

| Métrica       | Todos os tipos |
| :----------- | :------------: |
| Velocidade   |   450 km/h     |
| Consumo      |   1.0 L/km     |
| Autonomia    |    800 km      |

> Unidades aéreas não são afetadas por terreno, mas possuem alcance limitado de missão.

### 4.8 Bombardeiros Táticos (TB)

- **Função Principal:** Ataque ao solo (casca-grossa)
- **Raio de Visão:** 8.0 km (foco no solo)
- **Forças:** Destruir tanques e navios
- **Fraquezas:** Caças inimigos, AA
- **Tipo de Locomoção:** Aéreo
- **Alcance de Missão:** 4-8 hexágonos (varia por modelo)
- **Capacidade de Combustível:** 1000 Litros
- **Munição:** 4 surtidas (bombas pesadas)

**Velocidade, Consumo e Autonomia por Terreno:**

| Métrica       | Todos os tipos |
| :----------- | :------------: |
| Velocidade   |   350 km/h     |
| Consumo      |   1.5 L/km     |
| Autonomia    |    666 km      |

> Unidades aéreas não são afetadas por terreno, mas possuem alcance limitado de missão.

### 4.9 Bombardeiros Estratégicos (STR)

- **Função Principal:** Ataque à economia e supressão
- **Raio de Visão:** 12.0 km (alta altitude)
- **Forças:** Reduz munição/combustível, zera entrincheiramento (cidades)
- **Fraquezas:** Caças inimigos, AA
- **Tipo de Locomoção:** Aéreo
- **Alcance de Missão:** 8-16 hexágonos (varia por modelo)
- **Capacidade de Combustível:** 2500 Litros
- **Munição:** 2 surtidas (carga massiva)

**Velocidade, Consumo e Autonomia por Terreno:**

| Métrica       | Todos os tipos |
| :----------- | :------------: |
| Velocidade   |   400 km/h     |
| Consumo      |   1.2 L/km     |
| Autonomia    |   2080 km      |

> Unidades aéreas não são afetadas por terreno, mas possuem alcance limitado de missão.

### 4.10 Tropas de Montanha (Alpini)

- **Função Principal:** Combate em terreno montanhoso
- **Raio de Visão:** 2.0 km (vantagem de altitude)
- **Forças:** Movimento irrestrito em montanhas, defesa em altitude
- **Fraquezas:** Campo aberto contra tanques
- **Tipo de Locomoção:** Pernas (especializada)

- **Capacidade de Recursos:** 80 kg (Mulas/Equipamento extra)
- **Munição:** 10 ataques

**Velocidade, Consumo e Autonomia por Terreno:**

| Métrica       | Campo Aberto | Floresta | Cidade | Montanha | Rio | Pântano |
| :----------- | :----------: | :------: | :----: | :------: | :-: | :-----: |
| Velocidade   |   6.3 km/h   | 3.1 km/h | 6.3 km/h | 6.3 km/h |  -  | 2.1 km/h |
| Consumo      |   0.5 kg/km  |  0.8 kg/km | 0.5 kg/km|  0.4 kg/km|  -  |  1.5 kg/km|
| Autonomia    |    160 km    |   100 km   |  160 km  |  200 km   |  -  |   53 km  |

## 5. Efeitos do Terreno no Combate

O terreno afeta os modificadores de combate (Iniciativa e Entrincheiramento). Para velocidade e custos de movimento, veja as tabelas individuais de cada unidade na seção 4.

### 5.1 Campo Aberto (Clear)
- **Efeito no Combate:** Ideal para Tanques. Sem bônus de defesa
- **Iniciativa Cap.:** Normal (sem limite)
- **Entrincheiramento Base:** 1

### 5.2 Floresta
- **Efeito no Combate:** Bônus para Infantaria. Penaliza Tanques
- **Iniciativa Cap.:** Baixa
- **Entrincheiramento Base:** 2

### 5.3 Cidade / Fortificação
- **Efeito no Combate:** **Defesa Robusta**. Infantaria brilha aqui. Tanques sofrem penalidade severa
- **Iniciativa Cap.:** Muito Baixa
- **Entrincheiramento Base:** 3

### 5.4 Montanha
- **Efeito no Combate:** Bônus extremo de defesa. Visão bloqueada
- **Iniciativa Cap.:** Muito Baixa
- **Entrincheiramento Base:** 3

### 5.5 Rio
- **Efeito no Combate:** Defesa penalizada se atacado "do rio" (unidade na ponte/balsa)
- **Iniciativa Cap.:** Baixa
- **Entrincheiramento Base:** 0

### 5.6 Pântano
- **Efeito no Combate:** Penalidade severa de defesa e ataque
- **Iniciativa Cap.:** Baixa
- **Entrincheiramento Base:** 1

> **Nota sobre Rugged Defense (Defesa Robusta):** Unidades entrincheiradas em terrenos complexos (Cidades, Florestas) podem desencadear uma "Rugged Defense", onde o atacante sofre perdas desproporcionais e o ataque para.

## 6. Regras de Combate

O combate é resolvido comparando os valores de Ataque do agressor contra a Defesa do defensor.

### Fórmulas e Passos do Combate:

1.  **Verificação de Surpresa**: Se uma unidade entra em um hexágono ocupado por uma unidade não detectada (Ambush), o atacante perde sua vez e sofre dano massivo.

2.  **Cálculo da Iniciativa**:
    *   Base = Iniciativa da Unidade + Experiência (1 estrela = +1 iniciativa).
    *   **Terreno**: Terrenos fechados limitam a iniciativa (nivelando Tanques e Infantaria).
    *   Quem tiver maior iniciativa ataca primeiro e causa dano.
    *   Se a diferença for alta, o perdedor pode nem conseguir revidar (for eliminado ou suprimido antes).

3.  **Tipo de Ataque**:
    *   Se o alvo é "Soft" (Infantaria, Canhões), usa-se **Soft Attack**.
    *   Se o alvo é "Hard" (Tanques, APCs), usa-se **Hard Attack**.

4.  **Cálculo de Dano e Supressão**:
    *   **Dano Real (Kills)**: Reduz a força da unidade permanentemente.
    *   **Supressão**: Pontos de força ficam "atordoados" neste turno. Eles não revidam e aumentam a chance de rendição. Artilharia e Bombardeiros causam muita supressão.

5.  **Entrincheiramento (Entrenchment)**:
    *   Unidades paradas ganham nível de entrincheiramento a cada turno.
    *   Cada nível aumenta a defesa efetiva.
    *   Ataques repetidos reduzem o nível de entrincheiramento.

6.  **Retirada e Rendição**:
    *   Se uma unidade sofrer muita supressão ou dano superior à sua organização, ela tenta recuar.
    *   Se não houver hexágono livre para recuar (cercada), ela se **Rende** (é destruída e dá muito prestígio ao atacante).

## 7. Mecânicas de Jogo

*   **Prestígio**: A "moeda" do jogo. Ganho ao capturar objetivos e vencer cenários rapidamente. Usado para comprar unidades, reforçar (elites ou normais) e atualizar equipamentos.
*   **Combustível e Munição**: Unidades têm estoques limitados. Se o combustível acabar, a unidade para. Se a munição acabar, ela não ataca e defende mal. Reabastecimento ocorre automaticamente se a unidade não se mover e não estiver cercada.
*   **Clima**:
    *   **Sol**: Tudo normal.
    *   **Nublado**: Aviões não atacam, spotting aéreo reduzido.
    *   **Chuva/Neve**: Movimento reduzido, sem aviões, rios podem congelar (neve).

## 8. Estratégias Fundamentais

1.  **Armas Combinadas**: Nunca ataque com apenas um tipo de unidade. Use Artilharia para suprimir (amolecer) o alvo, depois Tanques ou Infantaria para finalizar.
2.  **Cercos (Encirclement)**: Corte as rotas de fuga do inimigo antes de atacar. Unidades cercadas se rendem, eliminando-as completamente sem lutas prolongadas.
3.  **Use o Terreno**: Coloque Infantaria nas cidades/florestas e Tanques nas planícies. Nunca envie tanques para dentro de cidades defendidas sem apoio pesado de artilharia/pioneiros.
4.  **Preserve a Experiência**: Unidades experientes (estrelas) são muito mais fortes. É melhor recuar e reforçar uma unidade experiente do que deixá-la morrer e comprar uma novata. Use "Reforços de Elite" (Elite Replacements) nos intervalos de cenários para manter as estrelas.
5.  **Reconhecimento é Vida**: Mova seus Recon cars (RCN) passo a passo ("phased movement") para evitar emboscadas. Saber onde o inimigo está antes de mover seus tanques pesados evita surpresas desagradáveis.
## 5. Notas de Implementação

- O sistema utiliza coordenadas cartesianas (metros) e não hexágonos discretos.
- "Hexágono" é apenas uma abstração visual de escala (1 hex ~= 1-2 km).
- Combustível e Munição devem ser reabastecidos em cidades ou por unidades de suprimento (future feature).

## 6. Automação e Motor de Jogo (Game Engine)

O jogo utiliza um motor externo em Python (`src/game_engine.py`) para processar os turnos de forma realista.

### 6.1 Fluxo do Turno
1.  **Planejamento:** O jogador define as rotas das unidades na interface web.
2.  **Exportação:** O estado do jogo (`dados.json`) é exportado via clipboard ou arquivo.
3.  **Processamento:** O script processa 1 turno (padrão 1 hora):
    -   Lê o mapa de terreno (`terreno.png`) pixel a pixel.
    -   Aplica modificadores de velocidade e custo de combustível baseados no terreno (ex: Floresta, Estrada).
    -   Move as unidades ao longo da rota planejada até o limite de tempo.
4.  **Atualização:** O novo estado é carregado de volta no jogo.

### 6.2 Modificadores de Terreno (Implementados)
O motor aplica automaticamente os seguintes fatores, sobrescrevendo estimativas manuais:

-   **Estrada:** Bônus de velocidade (~110-120%) e economia de combustível.
-   **Floresta:** Penalidade severa para Tanques (50% vel), leve para Infantaria.
-   **Rio:** Bloqueio ou penalidade extrema (exceto pontes/estradas).
-   **Cidade:** Movimento tático (reduzido para veículos).
