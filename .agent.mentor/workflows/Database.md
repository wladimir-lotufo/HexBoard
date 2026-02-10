---
description: Generate/Update Scripts.sql and Mod{Table}.cs based on Business Entities defined in 3-BusinessEntities.
---

# Workflow: Database

- **Objective**: Generate or update the database schema scripts (`4-Database\Scripts.sql`) and the corresponding C# Models (`APIs/{Table}/Mod{Table}.cs` for new, `Models\Mod{Table}.cs` for legacy) based on the Business Entities defined in `3-BusinessEntities/`.

- **Inputs**:
    1. **Business Entities Folder**: `3-BusinessEntities\` (all keys).
    2. **Current Scripts**: `4-Database\Scripts.sql` (to append or modify).

- **Outputs**:
    1. **SQL Script**: `4-Database\Scripts.sql` updated with DDL statements.
    2. **Model Files**: `APIs/{Table}/Mod{Table}.cs` for each entity. (**NOTE**: New Entity Models should be placed in `APIs/{Table}/`).

- **Steps**:
    1. **Analyze Business Entities**:
        - Iterate through all `.md` files in `3-BusinessEntities\`.
        - For each file:
        - Identify for each entity:
            - **Name**: The table name.
            - **Attributes**: Fields, data types, and descriptions.
            - **Primary Keys**: Usually `id_{EntityName}`.
            - **Relationships**: 
                - 1-1 (One-to-One)
                - 1-N (One-to-Many)
                - 1-1* (One-to-One Optional)
                - 1-N* (One-to-Many Optional)

    2. **Generate SQL DDL**:
        - For each entity, generate the `CREATE TABLE` statement.
        - Map business types to SQL types (e.g., `Text` -> `VARCHAR`, `Number` -> `INT` or `DECIMAL`).
        - Define Primary Keys (`PRIMARY KEY`).
        - Define Foreign Keys based on relationships.
        - Add comments to columns using `MS_Description` extended properties if possible, or standard SQL comments.
        - **Update `4-Database\Scripts.sql`**: Append the new DDL or update existing definitions.

    3. **Generate Simplex Models**:
        - For each entity, execute the **GenerateSimplexTable** workflow (or equivalent logic).
        - Create/Update `APIs/{Table}/Mod{Table}.cs` (or `Models\Mod{Table}.cs` if existing).
        - Ensure the Simplex notation matches the SQL definition.
        - Verify that `Mod{Table}.cs` contains the correct properties and the mandatory `public static Mod{Table} Read(SqlDataReader reader)` method.

    4. **Generate DAL Methods (Db{Table}.cs)**:
        - When generating `Dal{Table}{Action}` methods, strictly follow the connection and transaction pattern defined in `.agent/knowledge/SampleWorkfolder/APIs/Idioma/DbIdioma.cs`.
        - **Pattern Rules**:
            - Handle external vs. internal connections (`ConexaoExterna`).
            - Use `setMensagemErro` for error reporting.
            - properly manage explicit transactions (`BeginTransaction`, `Commit`, `Rollback`).
            - Always ensure connection is closed in `finally` block or if locally created.

- **Example Usage**:
    - **Input**: `3-BusinessEntities\Campanha.md` defines "Campanha" with attributes "Nome", "DataInicio", "DataFim".
    - **Action**: Run Database workflow.
    - **Output**: 
        - `4-Database\Scripts.sql` has `CREATE TABLE Campanha (...)`.
        - `Models\ModCampanha.cs` is created with `public class ModCampanha ...`.
