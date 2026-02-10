---
description: Define or update User Journeys in Lean Inception.md
---

# Workflow: Jornadas

- **Objective**: Define or update the **Jornadas de Usuário** section in `1-LeanInception\Lean Inception.md`.

- **Inputs**:
    1. **Lean Inception File**: `1-LeanInception\Lean Inception.md`.
    2. **Journey Details**: Persona, Journey Name, Objective, Entities involved, Step-by-step flow.

- **Outputs**:
    1. **Updated Lean Inception File**: `LeanInception\Lean Inception.md` with new or updated Journeys.

- **Steps**:
    1. **Read Lean Inception**: Read `LeanInception\Lean Inception.md` and locate the `# Jornadas de Usuário` section.
    2. **Locate Persona**: Find the subsection for the relevant Persona (e.g., `## 👩‍🏫 Gestor de Partnership`).
    3. **Update/Create Journey**:
        - Add a new journey or update an existing one.
        - Format:
            ```markdown
            ### Jornada X: {Nome da Jornada}
            *   **Objetivo:** {Descrição do objetivo}
            *   **Entidades:**
                *   `Entidade1` (Atributos)
            *   **Passo a Passo:**
                1.  Passo 1. {Tela: NomeDaTela}
                2.  Passo 2. {Tela: NomeDaTela}
            ```
    4. **Verify Views**: Ensure that every step referencing a `{Tela: ...}` corresponds to a View defined in `2-ViewsAndFeatures/` (or triggers the **Views** workflow to create it).

- **Example Usage**:
    - **Input**: Add "Jornada 3: Cancelar Campanha" for "Gestor".
    - **Output**: `Lean Inception.md` updated with the new journey steps.
