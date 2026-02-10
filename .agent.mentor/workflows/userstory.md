---
description: Create or Revise a User Story based on updated inputs.
---

1- **Inputs:**
    1. **Feature Name:** The feature/module this view belongs to (e.g., `Configurar Campanha`, `Filtrar Solicitações`, `Visualizar Ofertas`)

2- **Verify Changes:**
    1. The new User History should be based on the changes found.
    2. If changes are not found, ask for changes to be perfomed.

3- **Identify Context:**
    - Ask for the **Functionality/Goal** of the user story.
    - Ask for the **User Story ID** (e.g., US003) if not provided.
    - Ask for unexpected conditions or edge cases, clarifying then in Acceptance Criteria.

4- **Analyze Requirements**:
    - Based on the functionality, infer the **Persona** (defaults to "Gestor de Partnership" or similar if evident, otherwise ask).
    - Check related Business Entities at `3-BusinessEntities/` (to verify fields and relationships to be used, or to be created). 
    - Outline the **Acceptance Criteria**.
    - Identify relevant **Technical Notes** (Screen, Entities, APIs).

5- Group changes by affected View begerating a User Story for each View changes.

6- **Generate Content**:
    - Create the file content using the template below.

    ```markdown
    # História de Usuário: {Title}

    ## Como
    {Persona}

    ## Quero
    {Goal}

    ## Para
    {Benefit}

    ## Critérios de Aceitação

    - [ ] {Criteria 1}
    - [ ] {Criteria 2}
    - [ ] ...

    ## Definição de Pronto (DoD)

    - [ ] Lean Inception Visão, Personas e Jornadas revisados de acordo com objetivo da User Story (1-LeanInception/Lean Inception.md)
    - [ ] Funcionalidades revisadas de acordo com objetivo da User Story (2-ViewsAndFeatures/{ViewName}.md)
    - [ ] Business Entities revisadas de acordo com objetivo da User Story (3-BusinessEntities/{EntityName}.md)
    - [ ] Database revisada de acordo com objetivo da User Story (4-Database/Scripts.sql)
    - [ ] Prototype revisado de acordo com objetivo da User Story (5-Prototypes/{FeatureName}/{ViewName}.html)
    - [ ] Código das APIs, Models, Controllers e Views revisado de acordo com objetivo da User Story

    ## Notas Técnicas

    - **View:** {Screen Name}
    - **Business Entities:** {Entities} (List of entities/fields affected by the user story)
        - {Entity Name}.{Field Name} {Type} {Description}
    - **API:** {API Name}

    ## Estimativa

    **Story Points:** {Points}

    ## Dependências

    - {Dependency 1}
    - {Dependency 2}
    ```

    6.  **Save File**:
        - Construct the file path: `0-User Stories/Gestor de Partnership/{Journey}/{Sub-step}/{ID}-{Title_Slug}.md`.
    - Create the file.
