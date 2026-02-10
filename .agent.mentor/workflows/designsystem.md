---
description: Create or update Design System component pages
---

# /designsystem Workflow

This workflow creates or updates a component page in `.agent/knowledge/DesignSystem/components/{category}/` based on a UIComponent input, while preserving recent layout adjustments.

## Component Categories

Components are organized into 7 category subfolders:

1. **formularios/** - Form structures and alerts (stdview, stdmodal, alert)
2. **estruturais/** - Layout and structural components (header2, row, col, content, contentfull, colorline, modal*)
3. **botoes/** - Button components (newbutton, savebutton, closebutton, backbutton, editbutton, uploadbutton, programbutton, reportsbutton)
4. **acoes/** - Action components (actionedit, actiondelete, actionview, actionmove, actionupload, actionmenu)
5. **dados/** - Data inputs and display (text, select, calendar, checkbox, grid, gridheader, badge, label)
6. **paineis/** - Panel components (filterpanel, tabpanel)
7. **graficos/** - Chart components (pizzachart)

## Input Parameters
- **UIComponentName**: The name of the component (e.g., "FilterPanel", "Button", "Grid")

## Steps

1. **Parse Component Name**
   - Convert the component name to lowercase for the filename (e.g., "FilterPanel" → "filterpanel.html")
   - Keep the original casing for display purposes

2. **Determine Component Category**
   - Map component name to appropriate category subfolder:
     - **formularios**: stdview, stdmodal, alert
     - **estruturais**: header2, row, col, content, contentfull, colorline, modal*, modalplaceholder, modalcontent, modalheader, modalfooter
     - **botoes**: *button (any component ending in 'button')
     - **acoes**: action* (any component starting with 'action')
     - **dados**: text, select, calendar, checkbox, grid, gridheader, badge, label
     - **paineis**: filterpanel, tabpanel
     - **graficos**: *chart (any component ending in 'chart')

3. **Check if Component File Exists**
   - Look for the file at `.agent/knowledge/DesignSystem/components/{category}/{componentname}.html`
   - If it exists, read the current file to preserve layout adjustments

4. **Extract Existing Layout Elements (if updating)**
   - Preserve the header structure (lines 114-123 in existing files)
   - Preserve custom styles in the `<style>` section
   - Preserve tab structure and navigation
   - Keep any custom JavaScript functions

5. **Generate or Update Component Content**
   - Create the HTML structure following the standard template:
     - DOCTYPE and HTML head with all CSS/JS references
     - Standard styles for `.ds-component-section`, `.ds-content-header`, `.ds-section`, `.ds-example`, `.ds-code`, etc.
     - Body with `viw_Component` container
     - Header section with `hpanel` and `panel-heading`
     - Tab navigation (Exemplo, HTML, Propriedades)
     - Tab content sections
   - For new components, search the codebase for usage examples
   - For existing components, merge new content with preserved layout

6. **Populate Component Tabs**
   - **Tab Exemplo**: Visual example of the component in use
     - For interactive components (buttons, alerts, badges, and others), add configuration section before example
     - Include checkboxes grouped by configuration type (variant, size, positioning, etc.)
   - **Tab HTML**: Code template with syntax highlighting and copy button
     - Should update dynamically based on configuration selections
   - **Tab Propriedades**: Naming conventions, variations, and required libraries

7. **Add Required Scripts**
   - Include jQuery and Bootstrap
   - Add `copyCode()` function for code copying
   - For interactive components, add JavaScript to:
     - Handle checkbox changes (radio-like behavior)
     - Update component example dynamically
     - Generate HTML code based on selections
   - Add any other component-specific JavaScript

8. **Save the Component File**
   - Write to `.agent/knowledge/DesignSystem/components/{category}/{componentname}.html`
   - Preserve file encoding (UTF-8 with BOM if original had it)

9. **Verify the Result**
   - Confirm the file was created/updated successfully
   - Show a summary of changes made
   - Provide the file path for review

## Template Structure

```html
<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>{ComponentName} - Design System</title>
    <!-- CSS References (5 levels to reach project root) -->
    <link rel="stylesheet" href="../../../../../Vendor/fontawesome/css/font-awesome.min.css">
    <link rel="stylesheet" href="../../../../../Vendor/bootstrap/dist/css/bootstrap.min.css">
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700" rel="stylesheet" type="text/css">
    <link rel="stylesheet" href="../../../../../Vendor/animate.css/animate.min.css">
    <link rel="stylesheet" href="../../../../../Icons/pe-icon-7-stroke/css/pe-icon-7-stroke.css">
    <link rel="stylesheet" href="../../../../../Content/custom.css">
    <link rel="stylesheet" href="../../../../../Content/spinner1.css">
    <link rel="stylesheet" href="../../../../../Vendor/toastr214/build/toastr.css">
    <link rel="stylesheet" href="../../../../../Content/style.css">
    <link rel="stylesheet" href="../../../../../Content/style-custom.css">
    <style>
        /* Standard Design System Styles */
    </style>
</head>
<body>
    <div id="viw_Component" class="content-full">
        <div class="row">
            <div class="col-lg-12">
                <div class="hpanel">
                    <!-- Header Section -->
                    <div id="PanelHeader" class="panel-heading">
                        <div class="row">
                            <div class="col-lg-12">
                                <h4 class="font-extra-bold text-primary">{ComponentName}</h4>
                                <small class="text-muted">{Component Description}</small>
                            </div>
                        </div>
                    </div>
                    
                    <!-- Content Section -->
                    <div id="PanelBody" class="panel-body tooltip-demo">
                        <!-- TabPanel -->
                        <ul id="pnl_{ComponentName}Tabs" class="nav nav-tabs">
                            <li class="active">
                                <a data-toggle="tab" href="#tab_Exemplo">
                                    <i class="fa fa-eye text-primary fa-lg"></i> Exemplo
                                </a>
                            </li>
                            <li>
                                <a data-toggle="tab" href="#tab_HTML">
                                    <i class="fa fa-code text-primary fa-lg"></i> HTML
                                </a>
                            </li>
                            <li>
                                <a data-toggle="tab" href="#tab_Propriedades">
                                    <i class="fa fa-cog text-primary fa-lg"></i> Propriedades
                                </a>
                            </li>
                        </ul>
                        
                        <div class="tab-content">
                            <br />
                            <!-- Tabs content here -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <!-- JS References (5 levels to reach project root) -->
    <script src="../../../../../Scripts/jquery-3.6.0.min.js"></script>
    <script src="../../../../../Vendor/bootstrap/dist/js/bootstrap.min.js"></script>
    <script>
        function copyCode(btn) {
            var codeBlock = btn.parentElement.nextElementSibling;
            var code = codeBlock.innerText;
            var tempInput = document.createElement("textarea");
            tempInput.value = code;
            document.body.appendChild(tempInput);
            tempInput.select();
            document.execCommand("copy");
            document.body.removeChild(tempInput);
            var originalText = btn.innerText;
            btn.innerText = "Copiado!";
            setTimeout(function () { btn.innerText = originalText; }, 2000);
        }
    </script>
</body>
</html>
```

## Important Path Notes

> [!IMPORTANT]
> Component files in category subfolders require **5 levels** (`../../../../../`) to reach project root, not 4.
> 
> Path structure:
> - `components/{category}/{file}.html` → `../../../../../` → project root
> - Example: `components/layout/header2.html` needs 5 levels to reach `Scripts/`, `Vendor/`, etc.

> [!WARNING]
> Do NOT use escaped slashes (`../../`) - use normal forward slashes (`../../../../../`)

> [!IMPORTANT]
> **Select2 Library Path**: When using Select2, the correct path is `Vendor/select2-4.1.0/dist/`, NOT `Vendor/select2/dist/`.
> - CSS: `../../../../../Vendor/select2-4.1.0/dist/css/select2.min.css`
> - JS: `../../../../../Vendor/select2-4.1.0/dist/js/select2.min.js`

## Interactive Configuration Pattern

For components that benefit from visual configuration (buttons, actions, alerts, badges, labels), follow the **4-column configuration pattern** established by NewButton.

**Reference Components**:
- [newbutton.html](file:///c:/Users/wladi/source/repos/PartnerShip/.agent/knowledge/DesignSystem/components/botoes/newbutton.html) - **PRIMARY REFERENCE** - Complete 4-column configuration template
- [actionedit.html](file:///c:/Users/wladi/source/repos/PartnerShip/.agent/knowledge/DesignSystem/components/acoes/actionedit.html) - Action button with 4-column configuration
- [alert.html](file:///c:/Users/wladi/source/repos/PartnerShip/.agent/knowledge/DesignSystem/components/formularios/alert.html) - Alert with multiple configuration options

### Configuration Section Structure (4 Columns)

Use `col-md-3` for each configuration category to create a 4-column layout:

1. **Variante** - Component variants (primary, default, success, danger, warning, info, white, secondary)
2. **Tamanho** - Size options (lg, normal, sm, xs)
3. **Posicionamento** - Positioning (default, pull-right, pull-left)
4. **Estilo** - Style modifiers (normal, btn-block, btn-outline)

```html
<!-- Configuration Section -->
<div class="ds-section">
    <h2 class="ds-section-title">Configuração</h2>
    <div style="background-color: #fff; border: 1px solid #e7eaec; border-radius: 4px; padding: 20px; margin: 20px 0;">
        <div class="row">
            <!-- Column 1: Variante -->
            <div class="col-md-3">
                <h4 style="font-size: 14px; font-weight: 600; margin-bottom: 10px;">Variante</h4>
                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="primary"> Primary</label></div>
                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="default" checked> Default</label></div>
                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="success"> Success</label></div>
                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="danger"> Danger</label></div>
                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="warning"> Warning</label></div>
                <div class="checkbox"><label><input type="checkbox" class="config-variant" value="info"> Info</label></div>
            </div>
            
            <!-- Column 2: Tamanho -->
            <div class="col-md-3">
                <h4 style="font-size: 14px; font-weight: 600; margin-bottom: 10px;">Tamanho</h4>
                <div class="checkbox"><label><input type="checkbox" class="config-size" value="lg"> Large (lg)</label></div>
                <div class="checkbox"><label><input type="checkbox" class="config-size" value=""> Normal</label></div>
                <div class="checkbox"><label><input type="checkbox" class="config-size" value="sm" checked> Small (sm)</label></div>
                <div class="checkbox"><label><input type="checkbox" class="config-size" value="xs"> Extra Small (xs)</label></div>
            </div>
            
            <!-- Column 3: Posicionamento -->
            <div class="col-md-3">
                <h4 style="font-size: 14px; font-weight: 600; margin-bottom: 10px;">Posicionamento</h4>
                <div class="checkbox"><label><input type="checkbox" class="config-position" value="" checked> Default</label></div>
                <div class="checkbox"><label><input type="checkbox" class="config-position" value="pull-right"> Pull-right</label></div>
                <div class="checkbox"><label><input type="checkbox" class="config-position" value="pull-left"> Pull-left</label></div>
            </div>
            
            <!-- Column 4: Estilo -->
            <div class="col-md-3">
                <h4 style="font-size: 14px; font-weight: 600; margin-bottom: 10px;">Estilo</h4>
                <div class="checkbox"><label><input type="checkbox" class="config-style" value="" checked> Normal</label></div>
                <div class="checkbox"><label><input type="checkbox" class="config-style" value="btn-block"> Block</label></div>
                <div class="checkbox"><label><input type="checkbox" class="config-style" value="btn-outline"> Outline</label></div>
            </div>
        </div>
    </div>
</div>
```

### Context Selector Pattern (for Input Components)

For input components (Text, Select, Calendar, Checkbox, etc.), add a **Context Selector** dropdown above the configuration options to provide preset configurations for common use cases.

**Reference Components**:
- [text.html](file:///c:/Users/wladi/source/repos/PartnerShip/.agent/knowledge/DesignSystem/components/dados/text.html) - Text input with context selector
- [select.html](file:///c:/Users/wladi/source/repos/PartnerShip/.agent/knowledge/DesignSystem/components/dados/select.html) - Select dropdown with context selector

#### Context Selector Structure

Add this section **before** the 4-column configuration:

```html
<!-- Context Selector -->
<div class="row" style="margin-bottom: 20px; padding-bottom: 20px; border-bottom: 2px solid #e7eaec;">
    <div class="col-md-12">
        <h4 style="font-size: 14px; font-weight: 600; margin-bottom: 10px;">
            <i class="fa fa-lightbulb-o text-warning"></i> Contextos Comuns
        </h4>
        <select id="{component}-context-selector" class="form-control" style="max-width: 400px;">
            <option value="">-- Selecione um contexto --</option>
            <option value="context-1">Context Description 1</option>
            <option value="context-2">Context Description 2</option>
            <!-- Add more contexts based on usage analysis -->
        </select>
        <small class="text-muted">Selecione um contexto para aplicar a configuração automaticamente</small>
    </div>
</div>
```

> [!IMPORTANT]
> Use a **unique ID** for the context selector (e.g., `text-context-selector`, `select-context-selector`) to avoid conflicts with the component's example element.

#### Context Presets JavaScript

Define preset configurations based on actual usage patterns from the codebase:

```javascript
// Context presets (based on usage analysis)
var contexts = {
    'context-default': {
        option1: 'value1',
        option2: 'value2',
        option3: false
    },
    'context-special': {
        option1: 'different-value',
        option2: 'value2',
        option3: true
    }
    // Add more contexts
};

// Context selector handler
$('#component-context-selector').on('change', function () {
    var contextKey = $(this).val();
    if (contextKey && contexts[contextKey]) {
        applyContext(contexts[contextKey]);
    }
});

function applyContext(context) {
    // Apply each configuration option
    $('.config-option1').prop('checked', false);
    $('.config-option1[value="' + context.option1 + '"]').prop('checked', true);
    
    $('.config-option2').prop('checked', false);
    $('.config-option2[value="' + context.option2 + '"]').prop('checked', true);
    
    $('.config-option3').prop('checked', context.option3);
    
    // Update the component
    updateComponent();
}
```

#### Example: Text Input Contexts

Based on usage analysis showing:
- 95% use default alignment
- 3% use text-right for numeric fields
- Various states (disabled/readonly)

```javascript
var contexts = {
    'text-default': {
        alignment: '',
        state: '',
        type: 'text',
        placeholder: 'Digite aqui...',
        required: false
    },
    'text-numeric': {
        alignment: 'text-right',
        state: '',
        type: 'number',
        placeholder: '0,00',
        required: false
    },
    'text-email': {
        alignment: '',
        state: '',
        type: 'email',
        placeholder: 'exemplo@email.com',
        required: true
    },
    'text-readonly': {
        alignment: '',
        state: 'readonly',
        type: 'text',
        placeholder: '',
        required: false
    }
};
```

#### Example: Select Contexts

Based on usage analysis showing:
- 90% use Select2 with placeholder
- Some use multiple selection
- Some are disabled

```javascript
var contexts = {
    'select-default': {
        type: 'select2',
        state: '',
        multiple: '',
        required: false,
        placeholder: true
    },
    'select-simple': {
        type: 'simple',
        state: '',
        multiple: '',
        required: false,
        placeholder: true
    },
    'select-multiple': {
        type: 'select2',
        state: '',
        multiple: 'multiple',
        required: false,
        placeholder: true
    }
};
```

### Configuration Columns for Input Components

For input components, adapt the 4-column pattern to match the component's specific needs:

**Text Input** (4 columns):
1. **Alinhamento** - Default, text-right, text-center
2. **Estado** - Normal, Disabled, Readonly
3. **Tipo** - Text, Email, Number
4. **Opções** - Placeholder (input), Required (checkbox)

**Select** (4 columns):
1. **Tipo** - Com Select2, Simples
2. **Estado** - Normal, Disabled
3. **Seleção** - Única, Múltipla
4. **Opções** - Required, Com Placeholder

**Checkbox** (adapt as needed):
1. **Estilo** - iCheck styles
2. **Estado** - Normal, Disabled, Checked
3. **Tipo** - Checkbox, Radio
4. **Opções** - Required, Inline

> [!TIP]
> **Before creating contexts**: Analyze the codebase to find actual usage patterns. Use `grep_search` to find how the component is used in Views, then create contexts that match the most common patterns (e.g., 90%, 5%, 3%, 1% usage).



### JavaScript Pattern (Radio-like Behavior)

> [!CRITICAL]
> **HTML String Generation**: Always use **single-line strings** with `\n` escape sequences for newlines. 
> **NEVER** use multi-line string concatenation with literal line breaks - this causes JavaScript syntax errors.

```javascript
$(document).ready(function() {
    // Radio-like behavior for each configuration category
    $('.config-variant').on('change', function() {
        if (this.checked) {
            $('.config-variant').not(this).prop('checked', false);
        }
        updateComponent();
    });
    
    $('.config-size').on('change', function() {
        if (this.checked) {
            $('.config-size').not(this).prop('checked', false);
        }
        updateComponent();
    });
    
    $('.config-position').on('change', function() {
        if (this.checked) {
            $('.config-position').not(this).prop('checked', false);
        }
        updateComponent();
    });
    
    $('.config-style').on('change', function() {
        if (this.checked) {
            $('.config-style').not(this).prop('checked', false);
        }
        updateComponent();
    });
    
    function updateComponent() {
        var $component = $('#component-example');
        
        // Get selected values
        var variant = $('.config-variant:checked').val() || 'default';
        var size = $('.config-size:checked').val() || '';
        var position = $('.config-position:checked').val() || '';
        var style = $('.config-style:checked').val() || '';
        
        // Build class string
        var classes = ['btn', 'btn-' + variant];
        if (size) classes.push('btn-' + size);
        if (position) classes.push(position);
        if (style) classes.push(style);
        
        // Update component
        $component.attr('class', classes.join(' '));
        
        // Update HTML code
        updateHTMLCode(classes.join(' '));
    }
    
    function updateHTMLCode(classString) {
        // CORRECT: Single-line string with \n escape sequences
        var htmlCode = '&lt;button class="' + classString + '"&gt;\n    Button Text\n&lt;/button&gt;';
        $('#component-html-code').html(htmlCode);
        
        // WRONG: Multi-line concatenation (causes syntax errors)
        // var htmlCode = '&lt;button class="' + classString + '"&gt;\r\n' +
        //     '    Button Text\r\n' +
        //     '&lt;/button&gt;';
    }
});
```

### Critical Rules for HTML Code Generation

> [!WARNING]
> **Common Mistake**: Breaking HTML strings across multiple lines causes "Unterminated string literal" errors.

**✅ CORRECT Pattern**:
```javascript
var htmlCode = '&lt;a href="#" class="' + classString + '" data-toggle="tooltip" title="Edit"&gt;\n    &lt;i class="fa fa-pencil"&gt;&lt;/i&gt;\n&lt;/a&gt;';
```

**❌ WRONG Pattern** (causes errors):
```javascript
var htmlCode = '&lt;a href="#" class="' + classString + '" title="Edit"&gt;\r\n    ' +\r\n    '    &lt;i class="fa fa-pencil"&gt;&lt;/i&gt;\r\n    ' +\r\n    '&lt;/a&gt;';
```

### Class Order Convention

Always maintain this CSS class order:
1. `btn` (base class)
2. `btn-{variant}` (variant class)
3. `btn-{size}` (size class, if specified)
4. `{position}` (positioning class, if specified)
5. `{style}` (style modifier, if specified)
6. Component-specific classes (e.g., `dropdown-toggle` for menus)

**Example**: `btn btn-primary btn-sm pull-right btn-outline`

## Navigation Structure

The Design System navigation is organized into 7 main categories:

1. **Formulários** - Form structures (StdView, StdModal) and feedback (Alert)
2. **Estruturais** - Layout components and modal parts
3. **Botões** - All button components
4. **Ações** - Grid and list actions
5. **Dados** - Data inputs and display components
6. **Paineis** - Panel components
7. **Gráficos** - Chart components

## Notes
- Always preserve existing layout customizations when updating
- Search the codebase for real usage examples of the component
- **Components in subfolders need 5 levels (`../../../../../`) to reach project root**
- Use proper HTML encoding in code examples (&lt; &gt; &amp;)
- For interactive components, use SaveButton or Alert as reference templates
- Include component-specific JavaScript if needed (e.g., showhide for FilterPanel)
- Ensure paths use forward slashes, not escaped slashes
