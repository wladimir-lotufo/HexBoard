# Platform
The `Platform` class defines the **technology stack** used in the application and how source code should be generated.

**Main Definitions**:
- **Database**
    - Sql Server
- **Backend**
    - C#
    - .NET Framework 4.8
- **FrontEnd**
    - AspX
    - Razor
    - JavaScript
    - Ajax
- **Architecture**: 
    - Application architecture uses MVC.

**Code Generation**
- 'Project' structure folder
    - "\APIs\" (root folder for APIs)
        - "\APIs\{API}\Db{Api}.cs" (files for API code (e.g. DbUsuario.cs)
        - "\APIs\{API}\Mod{Table}.cs" (files for NEW Table models)
        - "\APIs\{API}\Mod{Service}.cs" (files for Models related to Service return
    - "\Controllers\{Controller}Controller.cs" (files for Controller file)
    - "\Views\{Controller}\"
        - {View}.cshtml" (files for Views files)
        - "Mod{View}.cs" (files for custom Model related to View input modal)
- Always follow the code generation templates samples at ## SAMPLES

## Coding General Rules
    - Always expand all code to be generated
    - Never use "..." to abbreviate parts of code.

## View General Rules
    - Init the file with the code of StdView or StdModal
    - All HTML elements with an "id" property should have this property at first position
    - Use the UIComponent **Html Template** at the desired location of the View
    - You should always replace the inner '{Render}' tags in this **Html Template** code, with the referred element **Html Template**

## Platform – UIComponents (HTML Templates)
This section defines the **HTML implementation** for the **UIComponents** defined in `Simplex.md`. Use these templates when converting Simplex Views to Razor/HTML.

## Mapping
- Use this mapping for default when creating UIComponents related to Domain fields
    - UIComponent x Domain
        - Pk: Use UIComponent Label or Text (Readonly). It represents an IDENTITY field (integer auto-sequential) and is never editable.
        - UUID: Use UIComponent Label or Text (Readonly). It represents an IDENTITY field (auto-sequential) and is never editable.
        - String: Use UIComponent Text
        - DateTime: Use UIComponent Calendar
        - FK: Use UIComponent Select, with an id and description of FK table
        - Int: Use UIComponent Text with defined integer numeric format
        - Decimal: Use UIComponent Text with defined decimal numeric format
        - Enum: 
            - For two-state (e.g. on-off, ativo-inativo) use UIComponent Switch
            - For three or more states use UIComponent Select
        - Workflow: Use UIComponent Select

## UIComponents List

### Structural UIComponents

#### StdView – (inherits View)
    - When requested to create a new screen (or View, or page) create .cshtml file as below
    **Mockup Html Example**
    ```cshtml
    @{
        ViewBag.Title = "Painel de Campanhas";
        Layout = "~/Views/Shared/_Layout2.cshtml";
    }

    <!-- Content or ContentFull-->
    ```

#### StdModal – (inherits View)
    - StdModal should be used to create a modal window, that opens when a button is clicked
    - The View that calls this StdModal should have a ModalPlaceholder element
    - The ModalPlaceholder element should be used to render the StdModal content

    **Mockup Html Example**
    ```cshtml
    @model WarpSolutions.Views.Seguranca2.ModDetalheUsuario
    @using Newtonsoft.Json
    @{
        ViewBag.Title = "Detalhes do Usuário";

        var jsonModel = JsonConvert.SerializeObject(Model, new JsonSerializerSettings {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind
        });
    }

    <!-- ModalContent -->

    <div id="mod_DetalheUsuario" class="modal-content">
        <div class="row">
            <div class="col-lg-12">
                <div class="color-line"></div>

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    </button>
                    <h4 class="modal-title">Detalhe do Usuário</h4>
                </div>
    
                <div class="modal-body">
                    <-- Modal content UIElements --> 
                </div>

                <div class="modal-footer">
                    <button id="but_Fechar" class="btn btn-default btn-sm" type="button" data-dismiss="modal"><i class="fa fa-sign-out"></i> Fechar</button>
                    <button id="but_Salvar" class="btn btn-primary btn-sm" type="submit"><i class="fa fa-check"></i> Salvar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- End StdModal -->
    ```


#### Content – (inherits UiComponent)
    - Should be used to wrap content of a StdView
    **Html Template Example**
    ```cshtml
    <div class="content">
        <row>
            <col>
                <!-- UIComponents -->
            </col>
        </row>
    </div>
    ```

#### ContentFull – (inherits UiComponent)
    - Should be used to wrap content of a StdView
    **Html Template Example**
    ```cshtml
    <div class="content-full">
        <row>
            <col>
                <!-- UIComponents -->
            </col>
        </row>
    </div>
    ```

#### ModalContent – (inherits UiComponent)
    - Should be used to wrap content of a StdModal
    **Html Template Example**
    ```cshtml
    <div class="modal-content">
        <row>
            <col>
                <!-- UIComponents -->
            </col>
        </row>
    </div>
    ```

#### ModalHeader – (inherits UiComponent)
    - Should be used to wrap header of a Modal
    **Html Template Example**
    ```cshtml
    <div class="modal-header">
        <row>
            <col>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                </button>
                <h4 class="modal-title">Detalhe do Usuário</h4>
            </col>
        </row>
    </div>
    ```



#### ModalFooter – (inherits UiComponent)
    - Should be used to wrap footer of a Modal
    **Html Template Example**
    ```cshtml
    <div class="modal-footer">
        <button id="but_Fechar" class="btn btn-default btn-sm" type="button" data-dismiss="modal"><i class="fa fa-sign-out"></i> Fechar</button>
        <button id="but_Salvar" class="btn btn-primary btn-sm" type="submit"><i class="fa fa-check"></i> Salvar</button>
    </div>
    ```

### ColorLine – (inherits UiComponent)
    - Should be used to wrap color line of a Modal
    **Html Template Example**
    ```cshtml
    <div class="color-line"></div>
    ```





#### ViewHeader - (Inherits PanelHeading)
    - Should be used to wrap heading of a View
    **Html Template Example**
    ```cshtml
    <div class="row">
        <div class="col-lg-4">
            <h4 class="font-extra-bold text-primary">Painel de Usuários</h4>
            <h6>
                <span id="NomeConta">Todos Usuários</span>
                <span id="Divisor"></span>
                <span id="NomeUnidade"></span>
            </h6>
        </div>

        <div class="col-lg-8 pull-right">
            <!-- Optional -->
            <button id="but_NovoUsuario" class="btn btn-default pull-right" onclick="but_NovoUsuario_Click()" type="button" data-toggle="tooltip" data-placement="top" title="Adicionar Novo usuário" data-original-title="Adicionar Novo Usuário">
                <i class="fa fa-plus"></i> Novo Usuário
            </button>
        </div>
    </div>
    ```





#### Row – (inherits UiComponent)
    - Should be used to wrap rows of a Panel
    - col-lg-xx where xx is the desired width for this column
    **Html Template Example**
    ```cshtml
    <div class="row">
        <div class="col-lg-12">
            <!-- UIComponents -->
        </div>
    </div>
    ```

#### Col – (inherits UiComponent)
    - Should be used to wrap columns of a Row
    - col-lg-xx where xx is the desired width for this column
    **Html Template Example**
    ```cshtml
    <div class="col-lg-12">
        <!-- UIComponents -->
    </div>
    ```

### Form UIComponents

#### Text – (inherits UIComponent)
    **Html Template Example**
    ```cshtml
    <div class="form-group">
        <label for="txt_DsNome"><strong>Nome Completo:</strong></label>
        @Html.TextBoxFor(m => Model.modUsuario.ds_Nome, new { @class = "form-control input", @id = "txt_DsNome" })
        @Html.ValidationMessageFor(model => Model.modUsuario.ds_Nome, "", new { @class = "text-danger" })
    </div>
    ```

#### Label – (inherits UIComponent)
    - Label that identifies a field in a form
    **Html Template Example**
    ```cshtml
    <label for="txt_DsNome"><strong>Nome Completo:</strong></label>
    ```

#### Select – (inherits UIComponent)
    **Html Template Example**
    ```cshtml
    <div class="form-group">
        <label for="sel_DsTipo"><strong>Tipo:</strong></label>
        @Html.DropDownListFor(
            m => m.modUsuario.ds_Tipo,
            new List<SelectListItem> {
                new SelectListItem { Value = "A", Text = "Administrador" },
                new SelectListItem { Value = "U", Text = "Usuário" }
            },
            "-- selecione --",
            new { @class = "form-control js-source-states", @id = "sel_DsTipo" }
        )
        @Html.ValidationMessageFor(model => Model.modUsuario.ds_Tipo, "", new { @class = "text-danger" })
    </div>
    ```

#### Calendar – (inherits UIComponent)
    **Html Template Example**
    ```cshtml
    <div class="form-group">
        <label for="cal_DtInclusao"><strong>Data de Inclusão:</strong></label>
        <div id="cal_DtInclusao" class="input-group date" >
            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
            @Html.TextBoxFor(m => Model.modUsuario.dt_Inclusao, new { @class = "form-control input", id = "dt_Inclusao" })
            @Html.ValidationMessageFor(model => Model.modUsuario.dt_Inclusao, "", new { @class = "text-danger" })
        </div>
    </div>
    ```

#### Switch – (inherits UIComponent)
    **Html Template Example**
    ```cshtml
    <input id="swi_FlAtivo" type="checkbox" class="js-switch form-control input"/>
    ```

#### Checkbox – (inherits UIComponent)
    - Standard checkbox with i-checks style
    **Html Template Example**
    ```cshtml
    <div class="checkbox i-checks no-padding">
        <input type="checkbox" id="chk_FlAtivo" class="input">
        &nbsp;Ativo
    </div>
    <script>
        $(document).ready(function () {
             $('.i-checks').iCheck({
                 checkboxClass: 'icheckbox_square-green',
                 radioClass: 'iradio_square-green',
             });
        });
    </script>
    ```

#### Alert – (inherits UIComponent)
    - Modal alerts and confirmations using SweetAlert
    **JavaScript Template Example**
    ```javascript
    swal({
        title: "Atenção!",
        text: "Esta é uma mensagem de alerta.",
        type: "warning",
        confirmButtonText: "OK"
    });
    ```

#### Badge – (inherits UIComponent)
    **Html Template Example**
    ```cshtml
    <span id="bad_BadgeId" class="label label-success">Ativo</span>
    ```

#### PizzaChart – (inherits UIComponent)
    **Html Template Example**
    ```cshtml
    <div id="piz_ChartId">
    @{

    }
    </div>
    ```



#### GridHeader – (inherits UIComponent)
    - This Header is used to wrap the grid header of a page
    **Html Template Example**
    ```cshtml
    <!-- GridHeader -->
    <div class="row">
        <div class="col-sm-4">
            <div class="dataTables_length" id="datatablecustom_length">
                <label>Exibir 
                    <select name="datatablecustom_length" aria-controls="datatablecustom" class="form-control input-sm">
                        <option value="10">10</option>
                        <option value="25">25</option>
                        <option value="50">50</option>
                        <option value="-1">All</option>
                    </select> registros por página</label>
            </div>
        </div>
                                
        <div class="col-sm-4 text-center">
            <div class="dt-buttons btn-group">          
                <button class="btn btn-default buttons-copy buttons-html5 btn-sm" tabindex="0" aria-controls="datatablecustom" type="button">
                    <span>Copy</span>
                </button>
                <button class="btn btn-default buttons-csv buttons-html5 btn-sm" tabindex="0" aria-controls="datatablecustom" type="button">
                    <span>CSV</span>
                </button>
                <button class="btn btn-default buttons-pdf buttons-html5 btn-sm" tabindex="0" aria-controls="datatablecustom" type="button"><span>PDF</span></button>
                <button class="btn btn-default buttons-excel buttons-html5 btn-sm" tabindex="0" aria-controls="datatablecustom" type="button"><span>Excel</span></button>
                <button class="btn btn-default buttons-print btn-sm" tabindex="0" aria-controls="datatablecustom" type="button"><span>Print</span></button>
            </div>
        </div>
        <div class="col-sm-4">
            <div id="datatablecustom_filter" class="dataTables_filter">
                <label>Pesquisar: <input type="search" class="form-control input-sm" placeholder="" aria-controls="datatablecustom"></label>
            </div>
        </div>
    </div>
    <!-- End GridHeader -->
    ```

- #### GridBody – (inherits UIComponent)
    **Mockup Html Example**
    ```cshtml
    <!-- GridBody of User List -->
    <div class="table-responsive">
        <table id="tbl_Usuarios" class="table table-striped table-responsive table-hover dataTable">
            <thead>
                <tr>
                    <th class="text-center">ID</th>
                    <th class="text-center">Nome</th>
                    <th class="text-center">Tipo</th>
                    <th class="text-center">Salário</th>
                    <th class="text-center">Data de Inclusão</th>
                    <th class="text-center">Ações</th>
                </tr>
            </thead>
            <tbody id="tbl_UsuariosBody">
                <tr id="usuarioTemplate" display:none>
                    <td class="text-center" data-id_Usuario></td>
                    <td class="text-center" data-ds_Nome></td>
                    <td class="text-center" data-ds_Tipo></td>
                    <td class="text-center" data-nm_Salario></td>
                    <td class="text-center" data-dt_Inclusao></td>
                    <td class="text-center">
                        <button class="btn btn-info btn-xs" data-edit data-toggle="tooltip" title="Editar"><i class="fa fa-pencil"></i></button>
                        <button class="btn btn-danger btn-xs" data-delete data-toggle="tooltip" title="Excluir"><i class="fa fa-trash"></i></button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <!-- End User GridBody -->
    ```

- ### Buttons
    - This is an abstract Button element
    - The action of this button should be implemented on the onclick event calling a javascript function.
    - If this button is calling a Model View, a ModalPlaceholder should be inserted at the main View

- #### SaveButton – (inherits Button)
    **Mockup Html Example**
    ```cshtml
    <button id="but_Salvar" type="submit" class="btn btn-primary">
        <i class="fa fa-check"></i> Salvar
    </button>
    ```

- #### CloseButton – (inherits Button)
    **Mockup Html Example**
    ```cshtml
    <button id="but_Fechar" type="button" class="btn btn-secondary" data-dismiss="modal">
        <i class="pe-7s-close"></i> Fechar
    </button>
    ```

- #### BackButton – (inherits Button)
    **Mockup Html Example**
    ```cshtml
    <button id="but_Back" type="button" class="btn btn-secondary" onclick="window.history.back();">
        <i class="fa fa-chevron-left"></i> Voltar
    </button>
    ```

- #### ProgramButton – (inherits Button)
    **Mockup Html Example**
    ```cshtml
    <button id="but_Program" class="btn btn-default pull-left" type="button" data-toggle="collapse" data-target="#collapseExample" aria-expanded="false" aria-controls="collapseExample">
        <i class="fa fa-calendar"></i> Programação  
    </button>
    ```

- #### NewButton – (inherits Button)
    **Mockup Html Example**
    ```cshtml
    <button id="but_New" type="button" class="btn btn-primary">
        <i class="fa fa-plus"></i> Novo
    </button>
    ```

- #### EditButton – (inherits Button)
    **Mockup Html Example**
    ```cshtml
    <button class="btn btn-info btn-xs" data-edit data-toggle="tooltip" title="Editar">
        <i class="fa fa-pencil"></i> Editar
    </button>
    ```

- #### UploadButton – (inherits Button)
    **Mockup Html Example**
    ```cshtml
    <button id="but_Upload" type="button" class="btn btn-info">
        <i class="pe-7s-upload"></i> Anexar Arquivo
    </button>
    ```

- #### ActionMenu – (inherits UIComponent)
    **Mockup Html Example**
    ```cshtml
    <div id="mnu_MenuId" class="btn-group dropdown-example-1">
        <a href="#" class="dropdown-toggle hover" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
            <i class="fa fa-cog fa-lg"></i>
        </a>
        <ul class="dropdown-menu dropdown-menu-right" style="min-width:@vTamanho">
            <li class="text-center" style="width:100">
        
                <-- ActionMenu content Actions buttons -->

            </li>
        </ul>
    </div>
    ```

- #### ActionEdit – (inherits Button)
    **Mockup Html Example**
    ```cshtml
    <button id="but_Editar" class="btn btn-info btn-xs" data-edit data-toggle="tooltip" title="Editar">
        <i class="fa fa-pencil"></i>
    </button>
    ```
    
    **Alternative (Legacy - Link Style)**
    ```cshtml
    <a onclick="" class="tooltip-demo" data-toggle="tooltip" data-placement="top" title="Alterar">
        <span class="text-center">
            <i class="fa fa-edit fa-lg text-success"></i>
        </span>
    </a>
    ```

- #### ActionView – (inherits Button)
    **Mockup Html Example**
    ```cshtml
    <a id="act_View" onclick="" class="tooltip-demo" data-toggle="tooltip" data-placement="top" title="Consultar">
        <span class="text-center">
            <i class="fa fa-eye fa-lg text-success"></i>
        </span>
    </a>
    ```

- #### ActionDelete – (inherits Button)
    **Mockup Html Example**
    ```cshtml
    <button id="but_Excluir" class="btn btn-danger btn-xs" data-delete data-toggle="tooltip" title="Excluir">
        <i class="fa fa-trash"></i>
    </button>
    ```
    
    **Alternative (Legacy - Link Style)**
    ```cshtml
    <a id="act_Delete" onclick="" class="tooltip-demo" data-toggle="tooltip" data-placement="top" title="Excluir">
        <span class="hred">
            <i class="fa fa-trash-o fa-lg text-danger"></i>
        </span>
    </a>
    ```

- #### ActionMove – (inherits Button)
    **Mockup Html Example**
    ```cshtml
    <a id="act_Move" onclick="" class="tooltip-demo" data-toggle="tooltip" data-placement="top" title="Mover">
        <span class="hred">
            <i class="fa fa-external-link fa-lg text-warning"></i>
        </span>
    </a>
    ```

- #### ActionUpload – (inherits Button)
    **Mockup Html Example**
    ```cshtml
    <a id="act_Upload" onclick="" class="tooltip-demo" data-toggle="tooltip" data-placement="top" title="Anexos">
        <span class="hred">
            <i class="fa fa-paperclip fa-lg text-primary"></i>
        </span>
    </a>
    ```

- #### ReportsButton – (inherits Button)
    **Mockup Html Example**
    ```cshtml
    <button id="act_Reports" type="button" class="btn btn-info">
        <i class="pe-7s-graph1"></i> Relatórios
    </button>
    ```

- #### FilterPanel – (inherits UIComponent)
    **Mockup Html Example**
    ```cshtml
    <!-- pnlFilter -->
    <div id="pnl_Filter" class="col-lg-2" id="pnlFiltros">
        <div class="hpanel hyellow" style="padding-bottom:0">
            <div class="panel-heading" style="padding-bottom:0">
                <h5 class="text-warning">Filtros</h5>
            </div>
            <div class="panel-body tooltip-demo no-padding" style="border-left:none;border-right:none">

            <!-- pnlFilter content UIElements -->

            </div>
        </div>
    </div>
    <!-- End pnlFilter -->
    ```

- #### TabPanel – (inherits UIComponent)
    **Mockup Html Example**
    ```cshtml
    <ul id="pnl_TabId" class="nav nav-tabs">
        <li class="active"><a data-toggle="tab" href="#tabLancamento"><i class="fa fa-pencil text-primary fa-lg"></i> Lançamento</a></li>
        <li class=""><a id="abaRateio" data-toggle="tab" href="#tabRateio" onclick=""><i class="fa fa-sitemap text-primary fa-rotate-270 fa-lg"></i> Rateio</a></li>
    </ul>
    <div class="tab-content">
    <br />
    <div id="tab_Lancamento" class="tab-pane active">

        <-- tab-pane content UIElements -->

    </div>
    <div id="tab_Rateio" class="tab-pane">

        <-- tab-pane content UIElements -->

    </div>
    ```

- #### ModalPlaceholder – (inherits UIComponent)
    - Modal Placeholder is used to open a Model View inside the main View.
    - The placeholder is a HTML code to be inserted at main window, and the modal View will be opened inside it.
    - The javascript code bellow should be inserted at the scripts section of the View
    - When user defines an action to open a modal window use de javascript code onclick="loadViewModal(url)"

    **Mockup Html Example**
    ```cshtml
    <!-- Modal for DetalheUsuario -->
    <div id="mod_DetalheUsuario" class="modal fade" role="dialog" tabindex="-1" aria-hidden="true" style="overflow:auto;" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" >
            <div class="modal-content">
                <div id="mod_DetalheUsuarioBody" class="modal-body tooltip-demo" style="padding-bottom:0" >

                </div>
            </div>
        </div>
    </div>
    ```

    ```javascript
    function loadViewModal(url) {
        $("#mod_DetalheUsuario").load(url, function () {
            $('#mod_DetalheUsuarioBody').modal('show');
        })
    }
    ```


## SAMPLES
- You can find sample project structures inside the following files in `.agent\knowledge\SampleWorkfolder`.
- Those samples link a specification sample and related code generated.
    - \Views\Seguranca2\ListaUsuarios.cshtml
    - \Views\Seguranca2\DetalheUsuario.cshtml   
    - \Views\Seguranca2\ModDetalheUsuario.cs   
    - \Controllers\Seguranca2Controller.cs
    - \APIs\Idioma\*.cs
    - \APIs\Usuario\*.cs
    - \APIs\Usuario\ModUsuarioConsultar.cs
    - \APIs\Usuario\ModUsuariosListar.cs
