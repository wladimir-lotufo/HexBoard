---
description: Save the View file "\Views\{Controller}\{View}.cshtml", generating the JavaScript code for Events defined at simplex.
---
description: Save the View file "\Views\{Controller}\{View}.cshtml", generating the JavaScript code for Events defined at simplex.
---

# Action: GenerateEventsView

- Objective: Save the View file "\Views\{Controller}\{View}.cshtml", generating the JavaScript code for Events defined at simplex.
- Steps:
    1- Read the simplex specification at start of file "\Views\{Controller}\{View}.cshtml" (embedded in HTML comments `<!-- ... -->`)
    2- Create a complete JavaScript code for each Event defined for this View simplex specification
    3- Check that each Event conforms the samples at `.agent/knowledge/SampleWorkfolder/Views/Seguranca2/ListaUsuarios.cshtml`
    4- Keep the simplex notation at first position in the file content, between comments.
- Examples: See examples at `.agent/knowledge/SampleWorkfolder/Views/Seguranca2/ListaUsuarios.cshtml`