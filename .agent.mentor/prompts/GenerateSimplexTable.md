---
description: Update the Model file named "Mod{Table}.cs" located at folder "\Models\", based on a Sql Script input, generating a simplex Table definition.
---

# Action: GenerateSimplexTable

- Objective: Update the Model file named "Mod{Table}.cs" located at folder "\Models\", based on a Sql Script input, generating a simplex Table definition.
- Steps for each Table at script:
    1- You should insert a simplex Table notation for each table defined in the script.
    2- The simplex notation should be a first position in the file content, between c# comments `/* ... */`.
    3- Update the notation at the correspondent Modal file at "\Models\Mod{Table}.cs"
    4- Create the all table fields using the correspondent Domains definded at simplex.
    5- Example Simplex Notation:
    ```simplex
    Usuario:Table
    {
        OB PK  UUID                 id_Usuario      "Unique identifier of the User"
        OB     String(100)          nm_Nome         "Full name of the User"
        OB     String(255)          ds_Senha        "Encrypted password of the User"
        OB     DateTime(dd/mm/yyyy) dt_Inclusao     "User's inclusion date"
        OB     Enum                 ds_Tipo         "User type", A - Admin, U - User
        OB     Decimal(10,2)        nm_Salario      "User's salary"
        OB     Fk(TipoUsuario.id_TipoUsuario)   id_TipoUsuario  "TipoUsuario.id_Usuario"
    }
    ```
    6- Check if all script defined fields for each table were defined also at simplex.
    7- Check if correct syntax EBNF for each simplex element used.
    8- Check if the user want´s to create an default API with CRUD functions for each table.
    9- GenerateServiceSimplex: Execute service simplex generation for standard CRUD services for this Table
    10- Examples: See examples at `.agent/knowledge/SampleWorkfolder/APIs/Usuario/ModUsuarioConsultar.cs`