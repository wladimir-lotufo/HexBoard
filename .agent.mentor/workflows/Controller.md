---
description: Updates the {ControllerName}Controller.cs file based on the View Prototype and LeanInception.md definitions.
---

# Workflow: Update Controller

- **Goal**: Update the `{ControllerName}Controller.cs` file in the `Controllers` folder.
- **Inputs**:
    1. **Controller Name**: The name of the controller (e.g., `Campanhas`).
    2. **View Name**: The name of the view (e.g., `PainelDeCampanhas`).
    3. **Prototype File**: Path to the HTML prototype (e.g., `5-Prototypes/PainelDeCampanhas.html`).
    4. **View Definition**: `2-ViewsAndFeatures/{View Name}.md`.
    5. **Current Scripts**: `4-Database\Scripts.sql` (to verify fields and relationships to be used). 

- **Steps**:
    1. **Analyze Requirements**:
        - Read `2-ViewsAndFeatures/{View Name}.md` to understand the functionality defined for `{View Name}`.
        - Read the **Prototype File** to identify UI events (buttons, filters) that require server-side interaction.
    2. **Analyze Standards**:
        - Read `.agent/knowledge/Platform.md` section `## SAMPLES` and examine `.agent/knowledge/SampleWorkfolder/Controllers/Seguranca2Controller.cs` to understand the standard Controller structure and coding patterns.
    3. **Update Controller**:
        - Locate or create `Controllers/{Controller Name}Controller.cs`.
        - **Load Method**: Create/Update the `ActionResult` method for the View (e.g., `public ActionResult {View Name}()`).
            - It must initialize the view model (`WarpSolutions.Views.{Controller}.Mod{ViewName}`) as required by the prototype/requirements.
            - It may interact with APIs using Entity Models (`WarpSolutions.APIs.{Table}.Mod{Table}`).
            - **Connection Handling**:
              - When querying data (without transactions), use:
                ```csharp
                // Instancia nossa Conexao
                Conexao conexao = new Conexao(TipoConexao.Conexao.WebConfig);
                
                // Se existe erro na conexao move o erro para a classe de acesso 
                if (conexao.ExisteErro())
                {
                    setMensagemErro(conexao.mErro);
                    return View(model); // Or appropriate return
                }
                ```
              - This ensures connections are properly handled for read-only operations.
        - **Event Methods**: Create/Update `JsonResult` or `ActionResult` methods for each event identified in the prototype (e.g., `Save`, `Delete`, `Filter`).
            - Use the logic defined in `2-ViewsAndFeatures/{View Name}.md`.
            - **Standard Return Pattern**: Methods responding to AJAX calls (like Save/Delete) must return JSON:
              - Success: `return Json(new { Resultado = "OK" });`
              - Failure: `return Json(new { Resultado = "ERRO", Mensagens = new[] { ex.Message } });`
            - Follow the pattern: `public JsonResult {MethodName}({Parameters})`.
    4. **Connect View**:
        - Ensure the Controller methods align with the View's Simplex definition and AJAX calls.
