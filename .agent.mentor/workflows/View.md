---
description: Generate a {View}.cshtml file from a prototype HTML with Simplex notation
---

# Workflow: View

- **Objective:** Generate the View file `\Views\{Controller}\{ViewName}.cshtml` from a prototype HTML file, including Simplex View definition at the beginning with updated UIElements list.

- **Inputs:**
    1. **View Name:** The name of the view to be generated (e.g., `PainelDeCampanhas`, `DetalheUsuario`)
    2. **Controller Name:** The name of the controller this view belongs to (if not provided, ask the user)
    3. **Prototype File:** The HTML prototype file located in `5-Prototypes\{ViewName}.html`
    4. **Current Scripts**: `4-Database\Scripts.sql` (to verify fields and relationships to be used).

- **Steps:**
    1. **Verify Controller Name:** 
        - If the controller name is not provided, ask the user which controller this view belongs to
        - Verify that the controller folder exists at `\Views\{Controller}\`
    
    2. **Analyze Prototype and Data Requirements:**
        - Read the prototype HTML file from `5-Prototypes\{ViewName}.html`.
        - Identify all UI components used in the prototype.
        - Determine the underlying data entities required.
        - **Execute /Database Workflow**: If new tables or entities are needed, run the `/Database` workflow first to generate schema and basic models.
    
    3. **Generate Simplex View Definition:**
        - Place the Simplex View definition at the very beginning of the .cshtml file.
        - Enclose the Simplex notation within HTML comments (`<!-- ... -->` for multi-line).
        - Format the notation clearly with proper indentation.
        - Create the Simplex notation for the View following the syntax defined in `Simplex.md` and `Platform.md`.
        - Include the View signature: `void {ViewName}() :View { ... }`
        - List all UIComponents found in the prototype, referencing UIElements defined in `Simplex.md` and `Platform.md`.
        - For each UIComponent, use the appropriate Simplex notation:
            - `Text(Field) dataname;` for text inputs
            - `Select(Service) dataname;` for dropdowns
            - `Calendar(Field) dataname;` for date pickers
            - `Grid(Service) dataname;` for data tables
            - `Button() dataname;` for buttons
            - And other UIComponents as defined in Platform.md
        - **Define Events**:
            - Generate all Events (user interactions) that should be handled according to Inputs.
            - For each event, generate the corresponding `ActionResult` or `JsonResult` method in the View signature.
    
    4. **Generate View Model (Mod{ViewName}.cs):**
        - **Location**: Create the C# Model class in `Views\{Controller}\Mod{ViewName}.cs`. **(IMPORTANT: View Models must reside in the View's folder)**.
        - Represents the specific data structure needed for the View (ViewModel).
        - Include properties for all data fields used in the View.
        - Ensure it matches the Simplex View definition.

    5. **Execute /Api Workflow:**
        - For the entities identified in Step 2, run the `/Api` workflow.
        - This will ensure `APIs/{TableName}/Db{TableName}.cs` and `APIs/{TableName}/Mod{TableName}.cs` (Entity Models) exist and are up to date.
        - **Note**: Entity Models (from `/Api`) are different from View Models (from Step 4). View Models may contain or reference Entity Models.

    6. **Update Controller (Controllers\{Controller}Controller.cs):**
        - Open or create `Controllers\{Controller}Controller.cs`.
        - **Dependencies**: Ensure the Controller is using the namespaces for the generated APIs (`WarpSolutions.APIs.{TableName}`) and View Models (`WarpSolutions.Views.{Controller}`).
        - Add an `ActionResult` method for the View: `public ActionResult {ViewName}()`.
        - Implement the logic to populate the `Mod{ViewName}` model, potentially calling API methods like `db.Dal{Table}Listar()`.
        - Implement `JsonResult` or `ActionResult` methods for the Events defined.
        - Reference the **Controller** workflow for detailed steps.

    7. **Convert Prototype to Razor/CSHTML:**
        - Convert the HTML prototype to ASP.NET MVC Razor syntax according Platform.md .cshtml file ## SAMPLES 
        - Use HTML as defined in Platform **Mockup Html Example** to get the correct HTML to be rendered for each Simplex UIElement.
        - **Standard Layout for View Panels**:
            - Use `id="viw_{ViewName}" class="content-full"`.
            - Use `Header2` structure for titles.
            - Wrap content in `hpanel`.
            - **Lists/Grids**:
                - **Do not** use server-side `@foreach` for grids.
                - Use `Newtonsoft.Json` to serialize the Model to a JS variable (`var jsonModel = ...`).
                - Implement a Client-Side `RefreshGrid()` function in JavaScript that clones a template row (`<tr id="template" style="display:none">`) and populates it.
        - **Standard Pattern for Modals**:
            - If the View is a Form/Detail view opened via Modal:
            - Set `Layout = null;`.
            - Wrap content in `<div class="modal-content">`.
            - Use `Ajax.BeginForm` or standard `<form>` with JS interception for submission.
        - Add proper model binding using `@model` directive (e.g., `@model WarpSolutions.Views.{Controller}.Mod{ViewName}`).
        - Include validation helpers (e.g., `@Html.ValidationMessageFor`).
        - Ensure all `id` attributes are placed as the first property in HTML elements.
    
    8. **For each simplex Event insert the JavaScript Code for it:**
        - Follow samples from Platform.md ## SAMPLES    
        - **Grid Initialization**: Call `RefreshGrid()` in `$(document).ready`.
        - **Modal Handling**: Use `loadViewModal(url)` or `$("#modalBody").load(url)` pattern.
        - **Form Submission**: Implement `but_Salvar_Click` to handle AJAX posting and JSON response processing (`OK` vs `ERRO`).
        - Check if is necessary to insert a corresponding method in the Controller.
        - Check the simplex logic for the API.Methods necessary for this funcionality.
            - Check the API.Methods if there is a suitable method for this functionality.
            - If there is no suitable method, create a new API.Method (refer back to `/Api` workflow).
    
    9. **Add Required Scripts and Styles:**
        - Include all necessary script bundles in the `@section Scripts` block.
        - Include all necessary style bundles in the `@section Styles` block.
    
    10. **Save the View File:**
        - Save the complete .cshtml file to `\Views\{Controller}\{ViewName}.cshtml`.
        - Ensure the file follows the standard template (`StdView` or `StdModal`) as appropriate.
    
    11. **Verify Completeness and Coherence:**
        - **Run /Tasks Workflow**: Review common tasks to ensure nothing was missed.
        - Ensure all UIComponents from the prototype are represented in the Simplex notation.
        - Verify that all interactive elements have corresponding Events defined.
        - Check that the Razor syntax is correct and follows MVC conventions.
        - Confirm all required scripts and styles are referenced.
        - Verify that `Mod{ViewName}.cs` (View Model), `Db{Table}.cs` (API), and `Controllers\{Controller}Controller.cs` are correctly updated and consistent.

- **Output:** 
    - A complete .cshtml View file at `\Views\{Controller}\{ViewName}.cshtml`
    - A Model file at `\Views\{Controller}\Mod{ViewName}.cs`
    - Updated Controller at `\Controllers\{Controller}Controller.cs`
    - Ensure all UIComponents follow the Mockup Html Examples from Platform.md and `SampleWorkfolder`
    - Reference UIElements defined in both Simplex.md and Platform.md

- **Example Usage:**
    - Input: View Name = "PainelDeCampanhas", Controller = "Campanha"
    - Output: `\Views\Campanha\PainelDeCampanhas.cshtml` with:
        - Simplex View definition listing all UI components
        - Razor markup for campaign dashboard using `hpanel`
        - Client-side Rendered Grid using `jsonModel`
        - Filters panel
        - All required scripts and styles