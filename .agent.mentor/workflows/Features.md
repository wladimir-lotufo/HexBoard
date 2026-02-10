---
description: Define or update Features in 2-ViewsAndFeatures folder
---

# Workflow: Features

- **Objective**: Define or update a **Feature** in `2-ViewsAndFeatures\{FeatureName}.md`.

- **Inputs**:
    1. **Views Folder**: `2-ViewsAndFeatures`.
    2. **Feature Details**: Name, Description, Functionalities, Users.

- **Outputs**:
    1. **Updated Feature File**: `2-ViewsAndFeatures\{FeatureName}.md` with new or updated details.

- **Steps**:
    1. **Check Existence**: Check if `2-ViewsAndFeatures\{FeatureName}.md` exists.
    3. **Update/Create Feature**:
        - Add a new feature or update an existing one.
        - Format:
            ```markdown
            ## {View: NomeDaView}
            **Objetivo**: {Descrição}

            ### Feature: {Nome da Funcionalidade}
            **Descrição**: {Descrição detalhada}

            #### User Inputs
            1. **{Nome do Input}**: {Descrição}

            #### System
            1. **{Nome do Output}**: {Descrição}

            #### Actions
            1. **{Nome da Ação}**
                - {Detalhe da ação}
            ```
    4. **Trigger Prototype Workflow**: After defining a Feature, it is recommended to run the **Prototype** workflow to create the HTML visualization.

- **Example Usage**:
    - **Input**: Define feature "RelatorioAdesao".
    - **Output**: `2-ViewsAndFeatures\RelatorioAdesao.md` created with feature details.
