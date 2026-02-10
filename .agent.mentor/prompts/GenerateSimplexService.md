---
description: Save the Api file named "Db{Api}.cs" located at folder "\APIs\", generating the simplex Service definition (or Method).
---

# Action: GenerateSimplexService

- Objective: Save the Api file named "Db{Api}.cs" located at folder "\APIs\", generating the simplex Service definition (or Method).
- Steps:
    1- Save the API file name "Db{API}.cs" located at folder "\APIs\{API}\" with basic API structure
    2- For each Service
        1- Update the Simplex Service definition, just before each c# method definition, inside `<summary>` tags with a ` ```simplex ` block.
        2- Verify each API Service its specific for Simplex Generation rules.
        3- Generate the simplex specification of Model for services return type specific Model structures
        4- Generate default CRUD Services *CreateSimplexCRUD*
        5- GenerateModelSimplex: Generate simplex Model definition for Service return type 
    3- Examples: See examples at `.agent/knowledge/SampleWorkfolder/APIs/Usuario/DbUsuario.cs`