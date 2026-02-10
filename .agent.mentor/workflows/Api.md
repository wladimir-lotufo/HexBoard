---
description: Create or update the API corresponding to an Entity or Table
---
# Action: Api

- Inputs: 
    1. **Table or Entity name**: (TableName).
    2. **Model File**: `APIs/{Table}/Mod{Table}.cs` (for new) or `Models\Mod{Table}.cs` (for legacy) (Primary Source for Return Types).
    3. **Entity Definition**: `3-BusinessEntities\{TableName}.md` (Context).
    4. **Current Scripts**: `4-Database\Scripts.sql` (to verify fields and relationships to be used).

- Objective: Create or update `APIs/{TableName}/Db{TableName}.cs` and associated Mod files.

- Steps:
    1.  **Analyze the Table/Entity**:
        - Locate the table definition in `4-Database/Scripts.sql` or `3-BusinessEntities/{TableName}.md`.
        - Identify columns, data types, and primary key.
        - **Verify Model**: Check `APIs/{Table}/Mod{Table}.cs` (or `Models\Mod{Table}.cs` for legacy) to ensure it matches the table structure. Models for new APIs should always be in `APIs/{Table}/`.

    2.  **Create/Update `Db{TableName}.cs`**:
        - File path: `APIs/{TableName}/Db{TableName}.cs`.
        - If new, create the file with the standard structure:
          ```csharp
          using System;
          using System.Collections.Generic;
          using System.Data.SqlClient;
          using WarpSolutions.Models;

          namespace WarpSolutions.APIs.{TableName}
          {
              public partial class Db{TableName} : Erros
              {
                  // Methods will go here
              }
          }
          ```

    3.  **Implement CRUD Methods**:
        - For each operation (Insert, GetById, Update, Delete, ListAll), generate:
            - **Simplex Notation**: Inside `/// <summary>` blocks with a ` ```simplex ` block, strictly following the format in `Simplex.md` and examples in `.agent/knowledge/SampleWorkfolder/APIs/Usuario/DbUsuario.cs`.
            - **C# Method**: Implement the method matching the Simplex signature.
                - Use `SqlCommand` and `SqlDataReader`.
                - Handle transactions and connections (`Conexao` class).
                - Use `try/catch` blocks for error handling.
                - Follow the exact coding style of `.agent/knowledge/SampleWorkfolder/APIs/Usuario/DbUsuario.cs`.
        - **Method Naming Convention**: `Dal{TableName}{Operation}` (e.g., `DalCampanhaIncluir`, `DalCampanhaConsultar`).

    4.  **Create/Update Mod Files**:
        - Identify return types from the methods (e.g., `Mod{TableName}`, `List<Mod{TableName}>`).
        - Check if these Mod classes exist in `Models/`.
        - If not, or if they need updates (e.g. missing properties), create/update them to match the table schema.
        - **Mandatory**: Ensure Mod classes have a generic static `Read` method for mapping from `SqlDataReader`:
          ```csharp
          public static Mod{Table} Read(SqlDataReader reader) { ... }
          ```
