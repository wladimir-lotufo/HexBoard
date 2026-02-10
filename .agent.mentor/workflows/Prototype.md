---
description: Create or Review a prototype HTML file for a View based on User Story, Feature Name, and User Journeys.
---

# Workflow: Prototype

- **Objective:** Create a prototype HTML file named `{View}.html` inside the appropriate feature folder within `5-Prototypes`, using Simplex UI Elements **mockup html example** as defined in Simplex.md and Platform.md. The prototype should enable users to execute activities defined in the User Journey and integrate seamlessly with the index.html iframe structure.

- **Inputs:**
    1. **User Story:** The name of the user story to be prototyped (e.g., `US001-Acessar-Painel-Campanhas`, `US002-Listar-Campanhas`, etc.)
    2. **Feature Name:** The feature/module this view belongs to (e.g., `Configurar Campanha`, `Filtrar Solicitações`, `Visualizar Ofertas`)
    3. **User Journeys:** Located in `1-LeanInception\Lean Inception.md` under section `# Journeys` - Contains the journeys that involve this View.
    4. **View Definition:** `2-ViewsAndFeatures/{ViewName}.md`.
    5. **Current Scripts**: `4-Database\Scripts.sql` (to verify fields and relationships to be used). 

- **Steps:**
    1. **Analyze User Story:** Read the user story :
        - Main functionalities
        - Target users/personas
    
    2. **Identify Related User Journeys:** Find all user journeys in `1-LeanInception\Lean Inception.md` section `# Journeys` that reference this view
    
    3. **Identify Database Entities:** From User Story: 
        1- identify all tables and fields mentioned in:
            - The user story
            - The related user journeys
            - `3-BusinessEntities/` (check associated entity files)
            - Extract field types, constraints, and relationships
        2- If there new fields plan to consider these chages in database and genrete the prototype accordingly
    
    4. **Design UI Components:** Based on the functionalities and user journeys, determine:
        - Identify the necessary views to accomplish the user journey
        - Split 'List View' and 'Details View' in separate files
        - Identify the best suitable Simplex UIElement for each functionality as defined in `Simplex.md`, 'Platform.md' and examples in `.agent/knowledge/DesignSystem`
        - Use the Platform.md **Mockup Html Example** and examples in `.agent/knowledge/SampleWorkfolder/Views` to render proper HTML for each UIElement
        - Actions and interactions needed
        - Navigation flows
    
    5. **Create Feature Folder (if needed):** 
        - If `5-Prototypes\{FeatureName}\` doesn't exist, create it
        - Example: `5-Prototypes\Campanhas\` for campaign-related views
    
    6. **Create Implementation Plan with Simplex View Definition:**
        - Create or Update the `implementation_plan.md`
        - Include a section `## Simplex View Definition`
        - Write the Simplex definition inside a `simplex` code block, based on `Platform.md` and `Simplex.md`
        - Present this plan to the user for approval before coding
    
    7. **Create HTML Prototype:** Generate `5-Prototypes\{FeatureName}\{ViewName}.html` with:
        - **DO NOT include** header, sidebar, or footer (these are in index.html)
        - **include** references to local styles using relative paths (e.g., `../../Content/style.css`, `../../Vendor/bootstrap/dist/css/bootstrap.min.css`)
        - **IMPORTANT**: Verify the correct version of scripts in `../../Scripts/` (e.g., use `jquery-3.6.0.min.js` if available, checking the directory first).
        - **DO NOT create** a new `Styles.css` file. Use the project's existing styles.
        - Add `overflow-y: auto` and `padding-bottom: 20px` to body for iframe scrolling
        - Include page header with title and breadcrumb navigation
        - Implement UIComponents using HTML5, CSS3, and vanilla JavaScript
        - Use existing CSS classes from project styles (page-header, content-wrapper, btn, card, form-control, etc.)
        - Include all interactive elements needed for the user journey
        - Add placeholder data that represents realistic scenarios
        - Ensure all actions from the user journey are represented
    
    8. **Implement Prototype Interactivity:** Add JavaScript to:
        - Mock necessary data
        - Simulate data operations (CRUD) using `sessionStorage` to persist data between page reloads (e.g., creating an item in one view and listing it in another).
        - **Handle Detail/Edit Modes**:
          - Use a single HTML file for both "Create" and "Edit/View" modes if possible.
          - Check for an `id` query parameter (e.g., `?id=1`) on page load.
          - If `id` exists, switch the form to "Edit" or "Read-Only" mode, populate fields with mock data, and adjust buttons (e.g., hide Save, change Title).
        - Keep data in sync within the toher related views As List and Detail, where de Details should be updated when the List is updated
        - Show/hide modals and dynamic content
        - Validate inputs
        - Provide user feedback (success/error messages)
        - Handle navigation between screens within the same feature folder (e.g., clicking a row in the Grid redirects to `Detalhe.html?id=X`).
    
    9. **Update Navigation:** Update `5-Prototypes\index.html`:
        - **Register the View in `menuStructure`:**
            - Locate the `menuStructure` object in the script section.
            - Identify the appropriate **Persona** (e.g., 'GP', 'AD').
            - Identify or create the **Journey** (Parent Menu, e.g., 'PARTNERSHIP').
            - Identify or create the **Sub-Journey** (e.g., 'Criar e Publicar Campanha').
            - Add the new **Functionality** (View) to the `items` array:
                ```javascript
                { label: 'Label do Menu', url: '{FeatureName}/{ViewName}.html' }
                ```
        - **DO NOT** manually modify the HTML list structure (`<ul>`/`<li>`) as it is generated dynamically.
    
    10. **Verify Completeness:** Ensure the prototype:
        - Covers all steps in the related user journeys
        - Displays all relevant database fields
        - Provides all actions mentioned in the view definition
        - Is visually consistent with the platform design
        - Works correctly when loaded in index.html iframe
        - Is fully functional for demonstration purposes

    11. **Output:** 
        - A complete, interactive HTML prototype files at `5-Prototypes\{FeatureName}\{View}.html`
        - Updated navigation in `5-Prototypes\index.html` (via `menuStructure`)

- **Design Guidelines:**
    - Use external styles via relative paths (e.g., `../../Content/*.css*`, `../../Vendor/*.css*`)
    - **Required CSS**:
        - `../../Vendor/fontawesome/css/font-awesome.min.css`
        - `../../Vendor/bootstrap/dist/css/bootstrap.min.css`
        - `../../Content/style.css`
        - `../../Content/style-custom.css`
        - And others as needed from `_Layout.cshtml` bundles
    - Use existing CSS classes (btn, card, form-control, page-header, etc.)
    - DO NOT duplicate layout elements (header, sidebar, footer)
    - Implement responsive design using existing media queries
    - Add smooth transitions and hover effects
    - Use Font Awesome icons
    - Apply modern UI patterns (cards, modals, dropdowns, tabs)
    - Ensure accessibility (semantic HTML, ARIA labels)

- **File Structure Example:**
    ```
    5-Prototypes/
    ├── Campanhas/
    │   ├── PainelDeCampanhas.html
    │   └── DetalheCampanha.html
    ├── Seguranca2/
    │   ├── ListaUsuarios.html
    │   └── DetalheUsuario.html
    ├── index.html                   
    └── SejaBemVindo.html             
    ```

- **Example Usage:**
    - Input: View Name = "MarketPlace", Feature Name = "Marketplace"
    - Output: `5-Prototypes\Marketplace\MarketPlace.html` with:
        - Reference to  `../../Content/*.css*` and `../../Vendor/*.css*`
        - Page header and breadcrumb
        - Simplex UI Elements 
        - Filters and detail fields
        - JavaScript interactions
        - No header/sidebar/footer (uses index.html iframe)
    - Updated: `5-Prototypes\index.html` with new item in `menuStructure` pointing to `Marketplace/MarketPlace.html`