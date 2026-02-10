---
description: Update the Api file named "Db{Api}.cs" located at folder "\APIs\", based on simplex Service input, generating the correspondent method c# code.
---

# Action: GenerateCodeService

- Objective: Update the Api file named "Db{Api}.cs" located at folder "\APIs\", based on simplex Service input, 
    generating the correspondent method c# code.
- Steps:
    1- Update the c# code, just after each Service simplex definition (embedded in `<summary>` tags)
    2- Verify each API Service its specific for Code Generation rules.
    3- The Namaspace for API files is "namespace WarpSolutions.APIs"
    4- GenerateModelSimplex: Generate the simplex specification of Model for service return type
    5- Adjust to conform examples at `.agent/knowledge/SampleWorkfolder/APIs/Usuario/DbUsuario.cs`