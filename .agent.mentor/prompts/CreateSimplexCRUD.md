---
description: Save the API file named "{API}.cs" located at folder "\APIs\{API}\", generating a simplex API CRUD Services definition.
---
# Action: CreateSimplexCRUD

- Objective: Save the API file named "{API}.cs" located at folder "\APIs\{API}\", generating a simplex API CRUD Services definition.
- Steps for each Table at script:
    1- When creating standard services or methods for a table, use the prefix `Dal` + `{Table}` + `{Suffix}`.
    2- Bellow services should be created:
      1- `Incluir` (Insert with parameters list)
      2- `Incluir` (Insert with a Model parameter)
      3- `Consultar` (Get by ID with parameters list)
      4- `Consultar` (Get by ID with a Model parameter)
      5- `Alterar` (Update with parameters list)
      6- `Alterar` (Update with a Model parameter)
      7- `Listar` (List all with parameters list)
      8- `Listar` (List all with a Model parameter)
      9- `Excluir` (Delete with parameters list)
      10- `Excluir` (Delete with a Model parameter)
    3- Connection Handling Rule:
        - When generating DAL methods, explicitly follow the pattern:
          - Check `ConexaoExterna` (null vs existing connection).
          - Use `new Conexao(TipoConexao.Conexao.WebConfig)` for internal connections.
          - Call `setMensagemErro(conexao.mErro)` on failure.
          - Use `BeginTransaction`, `Commit`, and `Rollback` appropriately.
          - Close connection in `finally` block or `if` block for internal connections.
    4- Examples: See examples at `.agent/knowledge/SampleWorkfolder/APIs/Usuario/DbUsuario.cs`
