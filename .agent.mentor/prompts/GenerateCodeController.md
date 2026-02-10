---
description: Save the Controller file named "\Controllers\{Controller}Controller.cs",  based on a View simplex input.
---

# Action: GenerateCodeController

- Objective: Save the Controller file named "\Controllers\{Controller}Controller.cs",  based on a View simplex input.
- Steps:
    1- Verify de folder where it should be created
    2- Create an void constructor with View dataname. See example for View "Lote.cshtml". This constructor corresponds to Load() event.
        ```cshtml
        // GET: Lote
        public ActionResult Lote()
        {
            return View();
        }

        public ActionResult ModalLote()
        {
            ModPrtShipLote modLote = new ModPrtShipLote();
            return PartialView(modLote);
        }

        public JsonResult SaveLoteModifications(ModPrtShipLote modLote)
        {
            DbPrtShipLote dbPrtShipLote = new DbPrtShipLote();
            if (modLote.IdLote == 0)
            {
                // Create a new Lote
                dbPrtShipLote.DalPrtShipLoteIncluir(modLote);
            }
            else
            {
                // Update the existing Lote
                dbPrtShipLote.DalPrtShipLoteAlterar(modLote);
            }
---
description: Save the Controller file named "\Controllers\{Controller}Controller.cs",  based on a View simplex input.
---

# Action: GenerateCodeController

- Objective: Save the Controller file named "\Controllers\{Controller}Controller.cs",  based on a View simplex input.
- Steps:
    1- Verify de folder where it should be created
    2- Create an void constructor with View dataname. See example for View "Lote.cshtml". This constructor corresponds to Load() event.
        ```cshtml
        // GET: Lote
        public ActionResult Lote()
        {
            return View();
        }

        public ActionResult ModalLote()
        {
            ModPrtShipLote modLote = new ModPrtShipLote();
            return PartialView(modLote);
        }

        public JsonResult SaveLoteModifications(ModPrtShipLote modLote)
        {
            DbPrtShipLote dbPrtShipLote = new DbPrtShipLote();
            if (modLote.IdLote == 0)
            {
                // Create a new Lote
                dbPrtShipLote.DalPrtShipLoteIncluir(modLote);
            }
            else
            {
                // Update the existing Lote
                dbPrtShipLote.DalPrtShipLoteAlterar(modLote);
            }

            return Json(new { success = true, message = "Lote saved successfully." });
        }
        ```    
    3- Create an Controller Method for each View event thas needs acess to APis Services.
    4- Generate the method logic according to the correpondent simplex Event defined at View.
    5- View Examples: See examples at `.agent/knowledge/SampleWorkfolder/Views/Seguranca2/ListaUsuarios.cshtml`
    6- Code Examples: See examples at `.agent/knowledge/SampleWorkfolder/Controllers/Seguranca2Controller.cs`