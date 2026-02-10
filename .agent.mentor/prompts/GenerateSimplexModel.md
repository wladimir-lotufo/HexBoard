---
description: Update the Model file "\APIs\{API}\Mod{Service}.cs", generating a simplex Model definition, for a Service return type input.
---

# Action: GenerateSimplexModel

- Objective: Update the Model file "\APIs\{API}\Mod{Service}.cs", generating a simplex Model definition, for a Service return type input.
- Steps:
    1- The simplex Model notation should be a first position in the file content, between c# comments `/* ... */`.
    2- Update the notation at the correspondent Model file at "\APIs\{API}\Mod{Service}.cs"
    3- Create the all model fields using the correspondent Domains defined for the service return type.
    4- Example Simplex Notation:
    ```simplex
    UsuarioListar:Model
    {
        OB PK  UUID                 id_Usuario      "Unique identifier of the User"
        OB     String(100)          nm_Nome         "Full name of the User"
        OB     String(255)          ds_Senha        "Encrypted password of the User"
        OB     DateTime(dd/mm/yyyy) dt_Inclusao     "User's inclusion date"
        OB     Enum                 ds_Tipo         "User type", A - Admin, U - User
        OB     Numeric(10,2)        nm_Salario      "User's salary"
        OB     FK                   id_TipoUsuario  "TipoUsuario.id_Usuario"
    }
    ```
    5- Check if all script defined fields for Model were defined also at simplex.
    6- Check if correct EBNF syntax for each simplex element used.
    7- Examples: See examples at `.agent/knowledge/SampleWorkfolder/APIs/Usuario/ModUsuarioConsultar.cs`