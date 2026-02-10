---
description: Cria uma nova tarefa e a adiciona ao arquivo tarefas.json com status Todo
---

1. Solicite ao usuário a descrição da nova tarefa, caso não tenha sido fornecida.
2. Leia o arquivo `c:\Users\wladi\source\repos\PartnerShip\.agent\data\tarefas.json`.
3. Analise o JSON para encontrar o maior ID atual.
4. Crie um novo objeto de tarefa com:
    - `id`: (maior ID encontrado + 1)
    - `descricao`: (descrição fornecida pelo usuário)
    - `status`: "Todo"
5. Adicione este objeto à lista de tarefas.
6. Salve o arquivo `c:\Users\wladi\source\repos\PartnerShip\.agent\data\tarefas.json` com a lista atualizada.
