# Design System - UI Components

Este documento cataloga os componentes de interface do usuário (UIComponents) utilizados no sistema, baseado na análise dos arquivos `Simplex.md`, `Platform.md` e das implementações reais em `ListaUsuarios.cshtml` e `DetalheUsuario.cshtml`.

---

## 1. Layout Components

### 1.1 Header2

**Descrição:** Cabeçalho de segunda linha para painéis, contendo título, subtítulo e área de ações principais.

**Herança:** UIComponent

**HTML:**
```html
<div id="Header2" class="panel-body tooltip-demo" style="margin-left:0; padding-top:10px; padding-bottom:10px">
    <div class="col-lg-2 border-right">
        <!-- Additional Content or Logo -->
    </div>
    <div class="col-lg-4">
        <h4 class="font-extra-bold text-primary vertical-mid" style="margin-top:5px">Painel de Usuários</h4>
        <h6>
            <span id="NomeConta">Todos Usuários</span>
            <span id="Divisor"></span>
            <span id="NomeUnidade"></span>
        </h6>
    </div>
    <div class="col-lg-6 pull-right AreaPrincipal">
        <!-- Button to Open Modal -->
        <button id="but_NovoUsuario" class="btn btn-default pull-right" onclick="but_NovoUsuario_Click()" type="button" data-toggle="tooltip" data-placement="top" title="Adicionar Novo usuário">
            <i class="fa fa-plus"></i> Novo Usuário
        </button>
    </div>
</div>
```

**Variações:**
- Container pai (`.hpanel`) pode ter borda esquerda customizada: `border-left:solid; border-left-color:#62cb31; border-left-width:5px`
- Cores de borda podem variar conforme contexto (verde para sucesso, vermelho para alertas, etc.)

**Padrão de Nomenclatura:**
- ID: `Header2` (fixo)
- Elementos internos: `NomeConta`, `Divisor`, `NomeUnidade` (conforme necessidade)

**Bibliotecas:**
- Bootstrap (layout grid e classes utilitárias)
- Font Awesome (ícones)

---

### 1.2 Body1

**Descrição:** Container principal de conteúdo para views com layout padrão.

**Herança:** UIComponent

**HTML:**
```html
<div id="Body1" class="content">
    <div class="row">
        <div class="col-lg-12">
            <div class="hpanel">
                <div class="panel-heading">
                    <!-- Body1 content UIElements -->
                </div>
            </div>
        </div>
    </div>
</div>
```

**Variações:**
- Sem variações significativas encontradas

**Padrão de Nomenclatura:**
- ID: `Body1` (fixo)

**Bibliotecas:**
- Bootstrap (layout grid)

---

### 1.3 Body2

**Descrição:** Container de conteúdo alternativo com estilo mais leve.

**Herança:** UIComponent

**HTML:**
```html
<div class="content-lite">
    <!-- Body2 content UIElements -->
</div>
```

**Variações:**
- Sem variações significativas encontradas

**Padrão de Nomenclatura:**
- Classe: `content-lite` (fixo)

**Bibliotecas:**
- Bootstrap (classes base)

---

### 1.4 Row

**Descrição:** Container de linha para organização de colunas.

**Herança:** UIComponent

**HTML:**
```html
<div class="row tooltip-demo" style="padding-top:0;padding-bottom:0">
    <!-- Row content UIElements -->
</div>
```

**Variações:**
- Padding pode ser ajustado conforme necessidade

**Padrão de Nomenclatura:**
- Classe: `row` (fixo)

**Bibliotecas:**
- Bootstrap (grid system)

---

### 1.5 Col

**Descrição:** Container de coluna para organização dentro de rows.

**Herança:** UIComponent

**HTML:**
```html
<div class="col-lg-6 form-group">
    <!-- Col content UIElements -->
</div>
```

**Variações:**
- Tamanho da coluna varia: `col-lg-1` a `col-lg-12`
- Pode incluir `form-group` quando contém campos de formulário

**Padrão de Nomenclatura:**
- Classe: `col-lg-{n}` onde n = 1 a 12

**Bibliotecas:**
- Bootstrap (grid system)

---

## 2. Form Components

### 2.1 Text

**Descrição:** Campo de entrada de texto com label e validação.

**Herança:** UIComponent

**HTML:**
```html
<div class="form-group">
    <label for="txt_DsNome"><strong>Nome Completo:</strong></label>
    @Html.TextBoxFor(m => Model.modUsuario.ds_Nome, new { @class = "form-control input", @id = "txt_DsNome" })
    @Html.ValidationMessageFor(model => Model.modUsuario.ds_Nome, "", new { @class = "text-danger" })
</div>
```

**Variações:**
- Campos numéricos podem ter máscaras específicas (jQuery Mask)
- Campos desabilitados (para IDs): adicionar `disabled="disabled"` ou `readonly="readonly"`

**Padrão de Nomenclatura:**
- ID: `txt_{NomeCampo}` (ex: `txt_DsNome`, `txt_NmSalario`)
- Prefixo `txt_` é obrigatório

**Bibliotecas:**
- Bootstrap (estilos de formulário)
- jQuery Validation (validação client-side)
- jQuery Mask (máscaras de input)

---

### 2.2 Select

**Descrição:** Campo de seleção dropdown com suporte a Select2.

**Herança:** UIComponent

**HTML:**
```html
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

**Variações:**
- Select com dados dinâmicos via Ajax
- Select com múltipla seleção: adicionar `multiple="multiple"`
- Select com busca (Select2 padrão)

**Padrão de Nomenclatura:**
- ID: `sel_{NomeCampo}` (ex: `sel_DsTipo`, `sel_IdIdiomaPreferido`)
- Classe: `js-source-states` para inicialização do Select2
- Prefixo `sel_` é obrigatório

**Bibliotecas:**
- Bootstrap (estilos de formulário)
- Select2 (dropdown avançado com busca)
- jQuery Validation (validação client-side)

---

### 2.3 Calendar

**Descrição:** Campo de seleção de data com datepicker.

**Herança:** UIComponent

**HTML:**
```html
<div class="form-group">
    <label for="cal_DtInclusao"><strong>Data de Inclusão:</strong></label>
    <div id="cal_DtInclusao" class="input-group date">
        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
        @Html.TextBoxFor(m => Model.modUsuario.dt_Inclusao, new { @class = "form-control input", id = "dt_Inclusao" })
        @Html.ValidationMessageFor(model => Model.modUsuario.dt_Inclusao, "", new { @class = "text-danger" })
    </div>
</div>
```

**Variações:**
- Formato de data pode variar: `dd/mm/yyyy`, `dd/mm/yyyy HH:mm`
- Pode ter restrições de data mínima/máxima

**Padrão de Nomenclatura:**
- ID do container: `cal_{NomeCampo}` (ex: `cal_DtInclusao`)
- ID do input: `dt_{NomeCampo}` (ex: `dt_Inclusao`)
- Prefixo `cal_` para container, `dt_` para input

**Bibliotecas:**
- Bootstrap (estilos de formulário)
- Bootstrap Datepicker (seleção de datas)
- Moment.js (manipulação de datas)
- Font Awesome (ícone de calendário)
- jQuery Validation (validação client-side)

---

### 2.4 Checkbox

**Descrição:** Campo de seleção booleana com visual customizado (iCheck).

**Herança:** UIComponent

**HTML:**
```html
<div class="checkbox i-checks no-padding">
    <input type="checkbox" id="chk_FlAtivo" class="input">
    &nbsp;Ativo
</div>
```

**Variações:**
- `no-padding`: remove espaçamento padrão
- Cores podem variar (ex: `icheckbox_square-green`)

**Padrão de Nomenclatura:**
- ID: `chk_{NomeCampo}` (ex: `chk_FlAtivo`)
- Classe: `i-checks` no container pai
- Prefixo `chk_` é obrigatório

**Bibliotecas:**
- iCheck (visual de checkbox/radio)
- jQuery

---

## 3. Button Components

### 3.1 NewButton

**Descrição:** Botão para criar novo registro.

**Herança:** Button → UIComponent

**HTML:**
```html
<button id="but_New" type="button" class="btn btn-primary">
    <i class="fa fa-plus"></i> Novo
</button>
```

**Variações:**
- Classe `btn-default` ao invés de `btn-primary` (encontrado em ListaUsuarios)
- Alinhamento `pull-right` para posicionar à direita
- Texto customizado (ex: "Novo Usuário" ao invés de "Novo")

**Padrão de Nomenclatura:**
- ID: `but_New` ou `but_Novo{Entidade}` (ex: `but_NovoUsuario`)
- Prefixo `but_` é obrigatório

**Bibliotecas:**
- Bootstrap (estilos de botão)
- Font Awesome (ícone fa-plus)

---

### 3.2 SaveButton

**Descrição:** Botão para salvar dados do formulário.

**Herança:** Button → UIComponent

**HTML:**
```html
<button id="but_Salvar" type="submit" class="btn btn-primary">
    <i class="fa fa-check"></i> Salvar
</button>
```

**Variações:**
- Sem variações significativas encontradas

**Padrão de Nomenclatura:**
- ID: `but_Salvar` (fixo)
- Prefixo `but_` é obrigatório

**Bibliotecas:**
- Bootstrap (estilos de botão)
- Font Awesome (ícone fa-check)

---

### 3.3 CloseButton

**Descrição:** Botão para fechar modal sem salvar.

**Herança:** Button → UIComponent

**HTML:**
```html
<button id="but_Fechar" type="button" class="btn btn-secondary" data-dismiss="modal">
    <i class="pe-7s-close"></i> Fechar
</button>
```

**Variações:**
- Sem variações significativas encontradas

**Padrão de Nomenclatura:**
- ID: `but_Fechar` (fixo)
- Prefixo `but_` é obrigatório

**Bibliotecas:**
- Bootstrap (estilos de botão e modal)
- Pe-icon-7-stroke (ícone pe-7s-close)

---

### 3.4 BackButton

**Descrição:** Botão para voltar à página anterior.

**Herança:** Button → UIComponent

**HTML:**
```html
<button id="but_Back" type="button" class="btn btn-default" onclick="window.history.back();">
    <i class="fa fa-chevron-left"></i> Voltar
</button>
```

**Variações:**
- Sem variações significativas encontradas

**Padrão de Nomenclatura:**
- ID: `but_Back` (fixo)
- Prefixo `but_` é obrigatório

**Bibliotecas:**
- Bootstrap (estilos de botão)
- Font Awesome (ícone fa-chevron-left)

---

### 3.5 EditButton

**Descrição:** Botão para editar registro (usado em grids).

**Herança:** Button → UIComponent

**HTML:**
```html
<button class="btn btn-info btn-xs" data-edit data-toggle="tooltip" title="Editar">
    <i class="fa fa-pencil"></i> Editar
</button>
```

**Variações:**
- Sem texto, apenas ícone (encontrado em ListaUsuarios)
- ID dinâmico baseado no registro: `but_Editar_{id}`

**Padrão de Nomenclatura:**
- ID: `but_Editar` ou `but_Editar_{id}` para grids
- Atributo: `data-edit` para seleção JavaScript
- Prefixo `but_` é obrigatório

**Bibliotecas:**
- Bootstrap (estilos de botão e tooltip)
- Font Awesome (ícone fa-pencil)

---

### 3.6 UploadButton

**Descrição:** Botão para upload de arquivos.

**Herança:** Button → UIComponent

**HTML:**
```html
<button id="but_Upload" type="button" class="btn btn-info">
    <i class="pe-7s-upload"></i> Anexar Arquivo
</button>
```

**Variações:**
- Sem variações significativas encontradas

**Padrão de Nomenclatura:**
- ID: `but_Upload` (fixo)
- Prefixo `but_` é obrigatório

**Bibliotecas:**
- Bootstrap (estilos de botão)
- Pe-icon-7-stroke (ícone pe-7s-upload)

---

### 3.7 ProgramButton

**Descrição:** Botão para abrir painel de programação/agendamento.

**Herança:** Button → UIComponent

**HTML:**
```html
<button id="but_Program" class="btn btn-default pull-left" type="button" data-toggle="collapse" data-target="#collapseExample" aria-expanded="false" aria-controls="collapseExample">
    <i class="fa fa-calendar"></i> Programação
</button>
```

**Variações:**
- Sem variações significativas encontradas

**Padrão de Nomenclatura:**
- ID: `but_Program` (fixo)
- Prefixo `but_` é obrigatório

**Bibliotecas:**
- Bootstrap (estilos de botão e collapse)
- Font Awesome (ícone fa-calendar)

---

### 3.8 ReportsButton

**Descrição:** Botão para acessar relatórios.

**Herança:** Button → UIComponent

**HTML:**
```html
<button id="act_Reports" type="button" class="btn btn-info">
    <i class="pe-7s-graph1"></i> Relatórios
</button>
```

**Variações:**
- Sem variações significativas encontradas

**Padrão de Nomenclatura:**
- ID: `act_Reports` (fixo)

**Bibliotecas:**
- Bootstrap (estilos de botão)
- Pe-icon-7-stroke (ícone pe-7s-graph1)

---

## 4. Action Components

### 4.1 ActionEdit

**Descrição:** Ação de edição em grids ou listas.

**Herança:** Button → UIComponent

**HTML:**
```html
<button id="but_Editar" class="btn btn-info btn-xs" data-edit data-toggle="tooltip" title="Editar">
    <i class="fa fa-pencil"></i>
</button>
```

**HTML Alternativo (Legacy - Link Style):**
```html
<a onclick="" class="tooltip-demo" data-toggle="tooltip" data-placement="top" title="Alterar">
    <span class="text-center">
        <i class="fa fa-edit fa-lg text-success"></i>
    </span>
</a>
```

**Variações:**
- Implementação moderna usa `<button>` (recomendado)
- Implementação legacy usa `<a>` (manter compatibilidade)
- ID dinâmico em grids: `but_Editar_{id}`

**Padrão de Nomenclatura:**
- ID: `but_Editar` ou `but_Editar_{id}` para grids
- Atributo: `data-edit` para seleção JavaScript
- Prefixo `but_` é obrigatório

**Bibliotecas:**
- Bootstrap (estilos de botão e tooltip)
- Font Awesome (ícones fa-pencil ou fa-edit)

---

### 4.2 ActionDelete

**Descrição:** Ação de exclusão em grids ou listas.

**Herança:** Button → UIComponent

**HTML:**
```html
<button id="but_Excluir" class="btn btn-danger btn-xs" data-delete data-toggle="tooltip" title="Excluir">
    <i class="fa fa-trash"></i>
</button>
```

**HTML Alternativo (Legacy - Link Style):**
```html
<a id="act_Delete" onclick="" class="tooltip-demo" data-toggle="tooltip" data-placement="top" title="Excluir">
    <span class="hred">
        <i class="fa fa-trash-o fa-lg text-danger"></i>
    </span>
</a>
```

**Variações:**
- Implementação moderna usa `<button>` (recomendado)
- Implementação legacy usa `<a>` (manter compatibilidade)
- ID dinâmico em grids: `but_Excluir_{id}` ou `but_Delete_{id}`

**Padrão de Nomenclatura:**
- ID: `but_Excluir` ou `but_Excluir_{id}` para grids
- Atributo: `data-delete` para seleção JavaScript
- Prefixo `but_` é obrigatório

**Bibliotecas:**
- Bootstrap (estilos de botão e tooltip)
- Font Awesome (ícones fa-trash ou fa-trash-o)
- SweetAlert (confirmação de exclusão)

---

### 4.3 ActionView

**Descrição:** Ação de visualização/consulta em grids ou listas.

**Herança:** Button → UIComponent

**HTML:**
```html
<a id="act_View" onclick="" class="tooltip-demo" data-toggle="tooltip" data-placement="top" title="Consultar">
    <span class="text-center">
        <i class="fa fa-eye fa-lg text-success"></i>
    </span>
</a>
```

**Variações:**
- Pode ser implementado como `<button>` seguindo padrão moderno

**Padrão de Nomenclatura:**
- ID: `act_View` ou `act_View_{id}` para grids

**Bibliotecas:**
- Bootstrap (tooltip)
- Font Awesome (ícone fa-eye)

---

### 4.4 ActionMove

**Descrição:** Ação de mover/transferir registro.

**Herança:** Button → UIComponent

**HTML:**
```html
<a id="act_Move" onclick="" class="tooltip-demo" data-toggle="tooltip" data-placement="top" title="Mover">
    <span class="hred">
        <i class="fa fa-external-link fa-lg text-warning"></i>
    </span>
</a>
```

**Variações:**
- Pode ser implementado como `<button>` seguindo padrão moderno

**Padrão de Nomenclatura:**
- ID: `act_Move` ou `act_Move_{id}` para grids

**Bibliotecas:**
- Bootstrap (tooltip)
- Font Awesome (ícone fa-external-link)

---

### 4.5 ActionUpload

**Descrição:** Ação de upload/anexo em grids ou listas.

**Herança:** Button → UIComponent

**HTML:**
```html
<a id="act_Upload" onclick="" class="tooltip-demo" data-toggle="tooltip" data-placement="top" title="Anexos">
    <span class="hred">
        <i class="fa fa-paperclip fa-lg text-primary"></i>
    </span>
</a>
```

**Variações:**
- Pode ser implementado como `<button>` seguindo padrão moderno

**Padrão de Nomenclatura:**
- ID: `act_Upload` ou `act_Upload_{id}` para grids

**Bibliotecas:**
- Bootstrap (tooltip)
- Font Awesome (ícone fa-paperclip)

---

### 4.6 ActionMenu

**Descrição:** Menu dropdown de ações para um registro.

**Herança:** UIComponent

**HTML:**
```html
<div id="mnu_MenuId" class="btn-group dropdown-example-1">
    <a href="#" class="dropdown-toggle hover" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
        <i class="fa fa-cog fa-lg"></i>
    </a>
    <ul class="dropdown-menu dropdown-menu-right" style="min-width:@vTamanho">
        <li class="text-center" style="width:100">
            <!-- ActionMenu content Actions buttons -->
        </li>
    </ul>
</div>
```

**Variações:**
- Largura do menu pode variar
- Conteúdo interno varia conforme ações disponíveis

**Padrão de Nomenclatura:**
- ID: `mnu_{NomeMenu}` (ex: `mnu_AcoesUsuario`)
- Prefixo `mnu_` é obrigatório

**Bibliotecas:**
- Bootstrap (dropdown)
- Font Awesome (ícone fa-cog)

---

## 5. Data Display Components

### 5.1 Grid

**Descrição:** Tabela de dados com funcionalidades de ordenação, filtro e paginação.

**Herança:** UIComponent

**HTML:**
```html
<div class="table-responsive">
    <table id="datatablecustom" class="table table-striped table-responsive table-hover dataTable">
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
        <tbody id="lstViewListaUsuariosBody">
            <tr id="usuarioTemplate" style="display:none">
                <td class="text-center" data-id_Usuario></td>
                <td class="text-center" data-ds_Nome></td>
                <td class="text-center" data-ds_Tipo></td>
                <td class="text-center" data-nm_Salario></td>
                <td class="text-center" data-dt_Inclusao></td>
                <td class="text-center">
                    <button class="btn btn-info btn-xs" data-edit data-toggle="tooltip" title="Editar">
                        <i class="fa fa-pencil"></i>
                    </button>
                    <button class="btn btn-danger btn-xs" data-delete data-toggle="tooltip" title="Excluir">
                        <i class="fa fa-trash"></i>
                    </button>
                </td>
            </tr>
        </tbody>
    </table>
</div>
```

**Variações:**
- Colunas variam conforme entidade
- Template de linha oculto (`display:none`) é clonado via JavaScript
- Botões de ação variam conforme necessidade

**Padrão de Nomenclatura:**
- ID da tabela: `datatablecustom` (padrão) ou `tbl_{NomeEntidade}`
- ID do tbody: `lstView{NomeEntidade}Body` (ex: `lstViewListaUsuariosBody`)
- ID do template: `{entidade}Template` (ex: `usuarioTemplate`)
- Atributos data: `data-{nomeCampo}` para binding de dados

**Bibliotecas:**
- Bootstrap (estilos de tabela)
- DataTables (funcionalidades de grid)
- DataTables Bootstrap (integração)
- DataTables Plugins (botões de exportação)
- jQuery

---

### 5.2 GridHeader

**Descrição:** Cabeçalho de controle do grid com opções de exibição, exportação e busca.

**Herança:** UIComponent

**HTML:**
```html
<div class="row">
    <div class="col-sm-4">
        <div class="dataTables_length" id="datatablecustom_length">
            <label>Exibir 
                <select name="datatablecustom_length" aria-controls="datatablecustom" class="form-control input-sm">
                    <option value="10">10</option>
                    <option value="25">25</option>
                    <option value="50">50</option>
                    <option value="-1">All</option>
                </select> registros por página
            </label>
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
            <button class="btn btn-default buttons-pdf buttons-html5 btn-sm" tabindex="0" aria-controls="datatablecustom" type="button">
                <span>PDF</span>
            </button>
            <button class="btn btn-default buttons-excel buttons-html5 btn-sm" tabindex="0" aria-controls="datatablecustom" type="button">
                <span>Excel</span>
            </button>
            <button class="btn btn-default buttons-print btn-sm" tabindex="0" aria-controls="datatablecustom" type="button">
                <span>Print</span>
            </button>
        </div>
    </div>
    <div class="col-sm-4">
        <div id="datatablecustom_filter" class="dataTables_filter">
            <label>Pesquisar: <input type="search" class="form-control input-sm" placeholder="" aria-controls="datatablecustom"></label>
        </div>
    </div>
</div>
```

**Variações:**
- Botões de exportação podem ser customizados
- Opções de quantidade de registros podem variar

**Padrão de Nomenclatura:**
- IDs gerados automaticamente pelo DataTables

**Bibliotecas:**
- Bootstrap (layout e estilos)
- DataTables (funcionalidades)
- DataTables Buttons (exportação)

---

### 5.3 Badge

**Descrição:** Etiqueta de status ou categoria.

**Herança:** UIComponent

**HTML:**
```html
<span id="bad_BadgeId" class="label label-success">Ativo</span>
```

**Variações:**
- Classes de cor: `label-success`, `label-danger`, `label-warning`, `label-info`, `label-primary`, `label-default`
- Texto varia conforme status

**Padrão de Nomenclatura:**
- ID: `bad_{NomeBadge}` (ex: `bad_Status`)
- Prefixo `bad_` é obrigatório

**Bibliotecas:**
- Bootstrap (estilos de label)

---

### 5.4 Label

**Descrição:** Rótulo de texto para campos ou informações.

**Herança:** UIComponent

**HTML:**
```html
<label for="txt_DsNome"><strong>Nome Completo:</strong></label>
```

**Variações:**
- Pode incluir ícones
- Pode ter classes de cor ou destaque

**Padrão de Nomenclatura:**
- Atributo `for` deve referenciar o ID do campo associado

**Bibliotecas:**
- Bootstrap (estilos base)

---

## 6. Panel Components

### 6.1 FilterPanel

**Descrição:** Painel lateral de filtros.

**Herança:** UIComponent

**HTML:**
```html
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
```

**Variações:**
- Largura pode variar: `col-lg-2`, `col-lg-3`, etc.
- Cor pode variar: `hyellow`, `hblue`, etc.

**Padrão de Nomenclatura:**
- ID: `pnl_Filter` (fixo)
- Prefixo `pnl_` é obrigatório

**Bibliotecas:**
- Bootstrap (layout e estilos)

---

### 6.2 TabPanel

**Descrição:** Painel com abas para organização de conteúdo.

**Herança:** UIComponent

**HTML:**
```html
<ul id="pnl_TabId" class="nav nav-tabs">
    <li class="active">
        <a data-toggle="tab" href="#tabLancamento">
            <i class="fa fa-pencil text-primary fa-lg"></i> Lançamento
        </a>
    </li>
    <li class="">
        <a id="abaRateio" data-toggle="tab" href="#tabRateio" onclick="">
            <i class="fa fa-sitemap text-primary fa-rotate-270 fa-lg"></i> Rateio
        </a>
    </li>
</ul>
<div class="tab-content">
    <br />
    <div id="tab_Lancamento" class="tab-pane active">
        <!-- tab-pane content UIElements -->
    </div>
    <div id="tab_Rateio" class="tab-pane">
        <!-- tab-pane content UIElements -->
    </div>
</div>
```

**Variações:**
- Número de abas varia
- Ícones e textos customizados

**Padrão de Nomenclatura:**
- ID do container: `pnl_{NomeTab}` (ex: `pnl_TabUsuario`)
- ID das abas: `tab_{NomeAba}` (ex: `tab_Lancamento`)
- Prefixo `pnl_` para container, `tab_` para conteúdo

**Bibliotecas:**
- Bootstrap (tabs)
- Font Awesome (ícones)

---

## 7. Modal Components

### 7.1 ModalPlaceholder

**Descrição:** Container para injeção de conteúdo modal via Ajax.

**Herança:** UIComponent

**HTML:**
```html
<div id="mod_DetalheUsuario" class="modal fade" role="dialog" tabindex="-1" aria-hidden="true" style="overflow:auto;" data-backdrop="static" data-keyboard="false">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div id="mod_DetalheUsuarioBody" class="modal-body tooltip-demo" style="padding-bottom:0">
                <!-- Modal content loaded via Ajax -->
            </div>
        </div>
    </div>
</div>
```

**Variações:**
- Tamanho: `modal-sm`, `modal-lg`, `modal-xl`
- Comportamento: `data-backdrop="static"` (não fecha ao clicar fora)

**Padrão de Nomenclatura:**
- ID do modal: `mod_{NomeModal}` (ex: `mod_DetalheUsuario`)
- ID do body: `mod_{NomeModal}Body` (ex: `mod_DetalheUsuarioBody`)
- Prefixo `mod_` é obrigatório

**Bibliotecas:**
- Bootstrap (modal)
- jQuery (Ajax loading)

**JavaScript Associado:**
```javascript
function loadViewModal(url) {
    $("#mod_DetalheUsuario").load(url, function () {
        $('#mod_DetalheUsuarioBody').modal('show');
    })
}
```

---

### 7.2 Modal2

**Descrição:** Estrutura padrão de modal (Modal2) utilizada para formulários e detalhes.

**Herança:** View (Modal2 inherits View in Simplex)

**HTML:**
```html
<div id="mod_DetalheUsuario" class="modal-content">
    <div class="row">
        <div class="col-lg-12">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"></button>
                <h4 class="modal-title">Detalhe do Usuário</h4>
            </div>
            <div class="modal-body">
                <!-- Form content -->
                <form>
                    <!-- Form fields -->
                    
                    <div class="modal-footer">
                        <button id="but_Fechar" type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                        <button id="but_Salvar" type="submit" class="btn btn-primary">Salvar</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</div>
```

**Variações:**
- Footer pode ter diferentes botões conforme ação
- Header pode não ter botão de fechar

**Padrão de Nomenclatura:**
- Classes fixas do Bootstrap

**Bibliotecas:**
- Bootstrap (modal)

---

## 8. Chart Components

### 8.1 PizzaChart

**Descrição:** Gráfico de pizza para visualização de dados.

**Herança:** UIComponent

**HTML:**
```html
<div id="piz_ChartId">
    @{
        // Chart rendering code
    }
</div>
```

**Variações:**
- Bibliotecas de chart podem variar (Chart.js, Highcharts, etc.)

**Padrão de Nomenclatura:**
- ID: `piz_{NomeChart}` (ex: `piz_VendasPorRegiao`)
- Prefixo `piz_` é obrigatório

**Bibliotecas:**
- Chart.js ou biblioteca de gráficos escolhida

---

## 9. Padrões Gerais

### 9.1 Nomenclatura de IDs

| Tipo de Componente | Prefixo | Exemplo |
|---|---|---|
| Text Input | `txt_` | `txt_DsNome` |
| Select/Dropdown | `sel_` | `sel_DsTipo` |
| Calendar | `cal_` | `cal_DtInclusao` |
| Switch/Checkbox | `swi_` | `swi_FlAtivo` |
| Button | `but_` | `but_Salvar` |
| Modal | `mod_` | `mod_DetalheUsuario` |
| Panel | `pnl_` | `pnl_Filter` |
| Tab | `tab_` | `tab_Lancamento` |
| Grid/List | `lstView` | `lstViewListaUsuarios` |
| Badge | `bad_` | `bad_Status` |
| Menu | `mnu_` | `mnu_Acoes` |
| Chart | `piz_` | `piz_Vendas` |

### 9.2 Nomenclatura de Funções JavaScript

| Tipo de Evento | Padrão | Exemplo |
|---|---|---|
| Load | `{NomeView}_Load()` | `DetalheUsuario_Load()` |
| Click | `but_{NomeBotao}_Click()` | `but_Salvar_Click()` |
| Return (Modal) | `mod_{NomeModal}_Return()` | `mod_DetalheUsuario_Return()` |
| Refresh | `Refresh{NomeComponente}()` | `RefreshGridUsuarios()` |
| Initialize | `Initialize{Componente}()` | `InitializeComponents()` |

### 9.3 Classes CSS Comuns

**Layout:**
- `row`, `col-lg-{n}`, `col-md-{n}`, `col-sm-{n}`, `col-xs-{n}`
- `pull-right`, `pull-left`, `text-center`, `text-right`, `text-left`

**Formulários:**
- `form-group`, `form-control`, `input`, `input-group`

**Botões:**
- `btn`, `btn-primary`, `btn-secondary`, `btn-default`, `btn-info`, `btn-danger`, `btn-warning`, `btn-success`
- `btn-xs`, `btn-sm`, `btn-lg`

**Tabelas:**
- `table`, `table-striped`, `table-responsive`, `table-hover`, `dataTable`

**Modais:**
- `modal`, `modal-fade`, `modal-dialog`, `modal-lg`, `modal-sm`, `modal-content`, `modal-header`, `modal-body`, `modal-footer`

**Painéis:**
- `hpanel`, `panel-heading`, `panel-body`, `panel-footer`

### 9.4 Bibliotecas JavaScript Globais

| Biblioteca | Propósito | Bundle |
|---|---|---|
| jQuery | Manipulação DOM e Ajax | `~/bundles/jquery` |
| jQuery Validation | Validação de formulários | `~/bundles/jqueryval` |
| jQuery Mask | Máscaras de input | Manual |
| Bootstrap | Framework CSS/JS | Incluído no layout |
| Select2 | Dropdowns avançados | `~/bundles/select2/js` |
| DataTables | Grids de dados | `~/bundles/datatables/js` |
| Bootstrap Datepicker | Seleção de datas | `~/bundles/datepicker/js` |
| Moment.js | Manipulação de datas | Manual |
| SweetAlert | Alertas e confirmações | `~/bundles/sweetAlert/js` |
| Switchery | Switches visuais | `~/bundles/switchery/js` |
| iCheck | Checkboxes customizados | `~/bundles/iCheck/js` |
| Ladda | Botões com loading | `~/bundles/ladda/js` |

### 9.5 Padrão de Comunicação Ajax

```javascript
$.ajax({
    type: "POST",
    url: "/{Controller}/{Action}",
    data: JSON.stringify({ parametro: valor }),
    contentType: 'application/json; charset=utf-8',
    dataType: 'json',
    success: function (response) {
        if (response.Resultado != "OK") {
            var msg = '';
            for (var i = 0; i < response.Mensagens.length; i++) {
                msg = msg + response.Mensagens[i] + '\n';
            }
            swal({
                title: 'Atenção!',
                text: msg,
                type: 'error',
            });
        } else {
            // Sucesso
        }
    },
    error: function (response) {
        swal({
            title: 'Erro!',
            text: "Falha na comunicação com o servidor.",
            type: 'error',
        });
    }
});
```

### 9.6 Funções Utilitárias Comuns

**Formatação de Data:**
```javascript
function ddMMyyyy(date) {
    const dd = String(date.getDate()).padStart(2,'0');
    const mm = String(date.getMonth()+1).padStart(2,'0');
    const yyyy = date.getFullYear();
    return `${dd}/${mm}/${yyyy}`;
}

function ddMMyyyyUTC(d) {
    if (!(d instanceof Date) || isNaN(d)) return '';
    const dd = String(d.getUTCDate()).padStart(2,'0');
    const mm = String(d.getUTCMonth()+1).padStart(2,'0');
    const yyyy = d.getUTCFullYear();
    return `${dd}/${mm}/${yyyy}`;
}
```

**Formatação de Moeda:**
```javascript
function formatMoneyBR(valor) {
    if (valor == null || valor === '') return '';
    const n = typeof valor === 'string'
        ? Number(valor.replace(/\./g, '').replace(',', '.'))
        : Number(valor);
    return Number.isFinite(n)
        ? n.toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 })
        : '';
}
```

---

## 10. Estilos Visuais e Inicialização JavaScript

### 10.1 Estilos CSS Personalizados

**Cores Primárias:**
```css
/* Verde principal */
.text-primary, .btn-primary { color: #1ab394; }
.bg-primary { background-color: #1ab394; }

/* Verde destaque */
.text-success { color: #62cb31; }
.bg-success { background-color: #62cb31; }

/* Vermelho perigo */
.text-danger { color: #ed5565; }
.bg-danger { background-color: #ed5565; }

/* Amarelo aviso */
.text-warning { color: #f8ac59; }
.bg-warning { background-color: #f8ac59; }

/* Azul informação */
.text-info { color: #23c6c8; }
.bg-info { background-color: #23c6c8; }

/* Cinza padrão */
.text-muted { color: #676a6c; }
.bg-default { background-color: #f3f3f4; }
```

**Painéis Customizados:**
```css
.hpanel {
    background-color: #fff;
    border: 1px solid #e7eaec;
    border-radius: 2px;
}

.hyellow {
    border-color: #f8ac59;
}

.hblue {
    border-color: #1ab394;
}

.hred {
    border-color: #ed5565;
}
```

**Bordas e Separadores:**
```css
.border-right {
    border-right: 1px solid #e7eaec;
}

.border-left {
    border-left: 1px solid #e7eaec;
}
```

### 10.2 Inicialização de Componentes JavaScript

**Select2 (Dropdowns Avançados):**
```javascript
// Inicialização básica
$('.js-source-states').select2({
    placeholder: '-- selecione --',
    allowClear: true
});

// Com Ajax para dados dinâmicos
$('#sel_IdCliente').select2({
    placeholder: 'Digite para buscar...',
    minimumInputLength: 2,
    ajax: {
        url: '/Controller/SearchClientes',
        dataType: 'json',
        delay: 250,
        data: function (params) {
            return {
                q: params.term,
                page: params.page
            };
        },
        processResults: function (data) {
            return {
                results: data.items
            };
        }
    }
});
```

**Bootstrap Datepicker (Calendário):**
```javascript
// Inicialização básica
$('.input-group.date').datepicker({
    format: 'dd/mm/yyyy',
    language: 'pt-BR',
    autoclose: true,
    todayHighlight: true
});

// Com restrições de data
$('#cal_DtInicio').datepicker({
    format: 'dd/mm/yyyy',
    language: 'pt-BR',
    autoclose: true,
    startDate: new Date(),
    endDate: '+1y'
});

// Evento de mudança
$('#cal_DtInclusao').on('changeDate', function(e) {
    console.log('Data selecionada:', e.date);
});
```

**Switchery (Switches):**
```javascript
// Inicialização básica
var elem = document.querySelector('.js-switch');
var switchery = new Switchery(elem, {
    color: '#1ab394',
    secondaryColor: '#dfdfdf',
    jackColor: '#fff'
});

// Inicialização múltipla
var elems = Array.prototype.slice.call(document.querySelectorAll('.js-switch'));
elems.forEach(function(elem) {
    new Switchery(elem, {
        color: '#1ab394',
        size: 'small'
    });
});

// Obter valor
elem.addEventListener('change', function() {
    console.log('Switch state:', elem.checked);
});
```

**DataTables (Grids):**
```javascript
// Inicialização completa
$('#datatablecustom').DataTable({
    pageLength: 25,
    responsive: true,
    language: {
        url: '//cdn.datatables.net/plug-ins/1.10.24/i18n/Portuguese-Brasil.json'
    },
    dom: '<"html5buttons"B>lTfgitp',
    buttons: [
        { extend: 'copy' },
        { extend: 'csv' },
        { extend: 'excel', title: 'ExcelFile' },
        { extend: 'pdf', title: 'PDFFile' },
        {
            extend: 'print',
            customize: function (win) {
                $(win.document.body).addClass('white-bg');
                $(win.document.body).css('font-size', '10px');
                $(win.document.body).find('table')
                    .addClass('compact')
                    .css('font-size', 'inherit');
            }
        }
    ]
});
```

**SweetAlert (Confirmações):**
```javascript
// Alerta simples
swal({
    title: 'Sucesso!',
    text: 'Operação realizada com sucesso.',
    type: 'success'
});

// Confirmação
swal({
    title: 'Tem certeza?',
    text: 'Esta ação não poderá ser desfeita!',
    type: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#1ab394',
    cancelButtonColor: '#ed5565',
    confirmButtonText: 'Sim, excluir!',
    cancelButtonText: 'Cancelar'
}).then(function(result) {
    if (result.value) {
        // Executar ação
        deleteRecord();
    }
});
```

**jQuery Mask (Máscaras de Input):**
```javascript
// Máscara de CPF
$('#txt_NrCpf').mask('000.000.000-00');

// Máscara de CNPJ
$('#txt_NrCnpj').mask('00.000.000/0000-00');

// Máscara de Telefone
var SPMaskBehavior = function (val) {
    return val.replace(/\D/g, '').length === 11 ? '(00) 00000-0000' : '(00) 0000-00009';
};
var spOptions = {
    onKeyPress: function(val, e, field, options) {
        field.mask(SPMaskBehavior.apply({}, arguments), options);
    }
};
$('#txt_NrTelefone').mask(SPMaskBehavior, spOptions);

// Máscara de Moeda
$('#txt_NmValor').mask('#.##0,00', { reverse: true });
```

### 10.3 Padrão de Inicialização em Views

**Estrutura Padrão:**
```javascript
$(document).ready(function() {
    // 1. Inicializar componentes visuais
    InitializeComponents();
    
    // 2. Carregar dados iniciais
    LoadInitialData();
    
    // 3. Configurar eventos
    SetupEventHandlers();
});

function InitializeComponents() {
    // Select2
    $('.js-source-states').select2({
        placeholder: '-- selecione --',
        allowClear: true
    });
    
    // Datepicker
    $('.input-group.date').datepicker({
        format: 'dd/mm/yyyy',
        language: 'pt-BR',
        autoclose: true
    });
    
    // Switchery
    var elems = Array.prototype.slice.call(document.querySelectorAll('.js-switch'));
    elems.forEach(function(elem) {
        new Switchery(elem, { color: '#1ab394' });
    });
    
    // Máscaras
    $('#txt_NrCpf').mask('000.000.000-00');
    $('#txt_NmSalario').mask('#.##0,00', { reverse: true });
    
    // Tooltips
    $('[data-toggle="tooltip"]').tooltip();
}

function LoadInitialData() {
    // Carregar dados via Ajax se necessário
}

function SetupEventHandlers() {
    // Configurar eventos de botões, etc.
}
```

### 10.4 Referências de Bibliotecas Necessárias

**CSS (no `<head>`):**
```html
<!-- Bootstrap -->
<link rel="stylesheet" href="~/Vendor/bootstrap/dist/css/bootstrap.min.css">

<!-- Font Awesome -->
<link rel="stylesheet" href="~/Vendor/fontawesome/css/font-awesome.min.css">

<!-- Pe-icon-7-stroke -->
<link rel="stylesheet" href="~/Icons/pe-icon-7-stroke/css/pe-icon-7-stroke.css">

<!-- Select2 -->
<link rel="stylesheet" href="~/Vendor/select2/dist/css/select2.min.css">

<!-- Bootstrap Datepicker -->
<link rel="stylesheet" href="~/Vendor/bootstrap-datepicker/dist/css/bootstrap-datepicker3.min.css">

<!-- Switchery -->
<link rel="stylesheet" href="~/Vendor/switchery/dist/switchery.min.css">

<!-- DataTables -->
<link rel="stylesheet" href="~/Vendor/datatables/media/css/dataTables.bootstrap.min.css">

<!-- SweetAlert -->
<link rel="stylesheet" href="~/Vendor/sweetalert/dist/sweetalert.css">

<!-- Estilos customizados -->
<link rel="stylesheet" href="~/Content/style.css">
<link rel="stylesheet" href="~/Content/custom.css">
```

**JavaScript (antes do `</body>`):**
```html
<!-- jQuery -->
<script src="~/Scripts/jquery-3.6.0.min.js"></script>

<!-- Bootstrap -->
<script src="~/Vendor/bootstrap/dist/js/bootstrap.min.js"></script>

<!-- Select2 -->
<script src="~/Vendor/select2/dist/js/select2.min.js"></script>

<!-- Bootstrap Datepicker -->
<script src="~/Vendor/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
<script src="~/Vendor/bootstrap-datepicker/dist/locales/bootstrap-datepicker.pt-BR.min.js"></script>

<!-- Moment.js -->
<script src="~/Vendor/moment/min/moment.min.js"></script>

<!-- Switchery -->
<script src="~/Vendor/switchery/dist/switchery.min.js"></script>

<!-- jQuery Mask -->
<script src="~/Vendor/jquery-mask/dist/jquery.mask.min.js"></script>

<!-- DataTables -->
<script src="~/Vendor/datatables/media/js/jquery.dataTables.min.js"></script>
<script src="~/Vendor/datatables/media/js/dataTables.bootstrap.min.js"></script>
<script src="~/Vendor/datatables-buttons/js/dataTables.buttons.min.js"></script>

<!-- SweetAlert -->
<script src="~/Vendor/sweetalert/dist/sweetalert.min.js"></script>

<!-- jQuery Validation -->
<script src="~/Scripts/jquery.validate.min.js"></script>
<script src="~/Scripts/jquery.validate.unobtrusive.min.js"></script>
```
