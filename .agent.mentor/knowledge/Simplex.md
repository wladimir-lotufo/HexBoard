# SIMPLEX
Specification Prompt Guide

- SIMPLEX acronym: Simple Modeling Platform for Logic, Entities, and eXecution

## Overview
You are working with a high-level specification language called Simplex.

This prompt guide defines the complete Simplex notation format, including its hierarchy, syntax, 
validation rules, and detailed elements definitions. Use it to consistently define the elements of an application, 
from database structure to Services, APIs, and Views.

Simplex is a **minimal and domain-specific language** designed to describe software systems or applications
in a **clear and structured way**. 
It is intended to be used by Product Owners and Technical Leaders to bridge the communication gap between 
business and development teams, ensuring shared understanding of system requirements.

The simplex specifications are written in plain text within project files according Platform.md definitions.

## Embedding Simplex
Simplex specifications should be embedded within the project files as follows:
- **APIs / Controllers**: Inside `<summary>` tags with a ` ```simplex ` block.
- **Models / View Models**: At the top of the file within a `/* ... */` comment block.
- **Views**: At the top of the file within a `<!-- ... -->` HTML comment block.

**Your task is to**:
- Interpret, validate, or generate SIMPLEX specification files
- Follow the simplex syntax format and hierarchy strictly
- Ensure clarity, minimalism, and domain alignment
- Do not assume or inject implementation-level details unless explicitly defined

## Simplex ELEMENTS

### Base – (abstract)
Common ancestor for all elements in Simplex.

**Properties**:
- **Dataname**: A Dataname to allow reference this element.

**Validation Rules**:
- When "Validate" or "show inconsistencies" is requested, the system must check the Validation Rules of the requested element and all its sub-elements, recursively.
    - This verification must generate a list of Error or Warning messages according to the corresponding Validation Rule.
    - This list must include the following fields:
        - Counter: line counter for quick reference.
        - Warning/Error: "Warning" or "Error" depending on the type of message.
        - File/Line: name of the specification file and line number where the error occurred.
        - Description: description of the warning or error.

### App – (inherits Base)
The `App` (Application) class is the **principal element** of the SIMPLEX specification.
It inherits its properties and behaviors from `{Base}`.
It acts as the **root** connecting all elements of the application's specification.

**Simplex Syntax for App**:
```ebnf
<App> ::= {Dataname}":App" "{" "Description=" {Description} "}"
```

### Repository – (inherits Base)
The `Repository` class represents a **set of Tables** of a database and a **set of APIs** that allow interfacing with this database.
It inherits its properties and behaviors from `{Base}`.

**Simplex Syntax for Repository**:
```ebnf
<Repository> ::= {Dataname}":Repository" "{" "Description=" {Description} "}"
```

**Properties**:
- **Description**: A short description of the business purpose of this repository.
- **Tables**: A list of `{Table}` elements.
- **APIs**: A list of `{API}` elements.

**Example**:
```simplex
CadastroUsuario:Repository
{
    Description="This repository is responsible for the management functionalities and user data."
}
```

**Validation Rules**:
- Validate if the repository has a concise business-purpose description.
- Validate each table using `{Table.Validation Rules}`
- Validate each API using `{API.Validation Rules}`

### Controller – (inherits Base)
The `Controller` class represents a **set of Views** in an application specification.
It inherits its properties and behaviors from `{Base}`.

**Simplex Syntax for Controller**:
```ebnf
<Controller> ::= {Dataname} ":Controller" "{" "Description=" {Description} "}"
```

**Properties**:
- **Description**: A short description of the business purpose of this controller.
- **Views**: A list of `{View}` elements.

**Example**:
```simplex
CadastroUsuario:Controller
{
    Description="This controller is responsible for managing the views related to user registration and access."
}
```

**Validation Rules**:
- Validate if the controller has a concise description of its business objective.
- Validate each view using `{View.Validation Rules}` located in Platform.md

### Model – (inherits Base)
The `Model` class represents a **structure of Fields** (`{Field}` elements).
It inherits its properties and behaviors from `{Base}`.

**Simplex Syntax for Model**:
```ebnf
<Model> ::= {Dataname} ":Model" "{" {<Field.Syntax>} "}"
```
- The `:Model` suffix indicates that the file defines a Model structure.

**Properties**:
- **Description**: A short description of the content or entity that the Model represents.
- **Fields**: A list of `{Field}` elements.
  - Each field is described individually within the Model definition.

**Example**:
```simplex
Usuario:Model
{
    MA PK  UUID              id_Usuario      "Unique identifier of the User"
    MA     String(100)       nm_Nome         "Full name of the User"
    MA     String(255)       ds_Senha        "Encrypted password of the User"
}
```

**Validation Rules**:
- Validate if the Model includes a concise description of the entity or the type of data it stores.
- Check for duplicate datanames among the fields and list any duplicates found.
- Validate each field individually according to `{Field.Validation Rules}`.


### Table – (inherits Model)
The `Table` class represents the **field structure (record definition)** of a database table.
It inherits from the `Model` class.

**Simplex Syntax for Table**:
```ebnf
<Table> ::= {Dataname} ":Table" "{" "Description=" {Description} ";" {<Field.Syntax>} "}"
```
- The `:Table` suffix indicates that the file defines a table structure.

**Properties**:
- **Description**: A short description of the content or entity stored in the table.
- **Fields**: A list of `{Field}` elements.
  - Each field is described using the standard `Field` syntax.

**Example**:
```simplex
Usuario:Table
{
    MA PK  UUID                             id_Usuario      "Unique identifier of the User"
    MA     String(20)           ds_Nome             "Nome";         "Full name of the User"
    MA     String(255)                      ds_Senha        "Encrypted password of the User"
    MA     DateTime(dd/mm/yyyy)             dt_Inclusao     "User's inclusion date"
    MA     Enum                             ds_Tipo         "User type", A - Admin, U - User
    MA     Decimal(10,2)                    nm_Salario      "User's salary"
    MA     Fk(TipoUsuario.id_TipoUsuario)   id_TipoUsuario  "User type (Individual or Company)"
}
```

**Validation Rules**:
- Validate if the `Description` clearly defines the entity or content the table represents.
- Check for duplicate field `Datanames`, and emit an error for each duplicate.
- Validate each field using `{Field.Validation Rules}`.

**GenerateCode**:
- When user asks for Code Generation follow these steps
    - Save a Modal file for this Table, following simplex Modal definitions, at Modal specified Platform project folder for Model (APIs/{Table}/Mod{Table}.cs for new ones).
    - Save a API file for this Table, following simplex API definitions, at API specified Platform project folder for API.

### Field – (inherits Base)
The `Field` class represents a **business-content field type**.
It inherits its properties and behaviors from `{Base}`.

**Simplex Syntax for Field**:
```ebnf
<Field> ::= {Mandatory} {Pk} {Type} {Dataname} {GetProperties} ";";
<PkOrFk> ::= <Pk> ::= "PK" | "" | ε;
<GetProperties> ::= { propertyName "=" propertyValue "," }
```

**Properties**:
- **Dataname**: The identifier name for the field.
- **Mandatory**: Defines if the field is mandatory (`MA`) or optional (`OP`).
- **Type**: Defines the Domain or valid set of values for this field (e.g., `String`, `Decimal`, `DateTime`, `Enum`, `UUID`, `Pk`, `Fk`).
- **Description**: A concise explanation of the field's business meaning.
- **Label**: A short title to be used when displaying the field in Views (typically positioned above the field).

**Example**:
```simplex
Usuario:Table
{
    MA PK  UUID              id_Usuario      "Unique identifier of the User"
    MA     String(100)       nm_Nome         "Full name of the User"
    MA     String(255)       ds_Senha        "Encrypted password of the User"
    MA     DateTime(dd/mm/yyyy)  dt_Inclusao     "User's inclusion date"
    MA     Enum              ds_Tipo         "User type", A - Administrator, U - User
    MA     Decimal(10,2)     nm_Salario      "User's salary"
    MA     Fk(TipoUsuario.id_TipoUsuario)   id_TipoUsuario  "TipoUsuario.id_TipoUsuario"
}
```

**Validation Rules**:
- Validate if `Mandatory` is either `MA` or `OP`.
- Validate if `Description` provides a consistent explanation for the field.
- Validate if `Type` defined in one of elements descendant of Domain.
- Validate if `Label` is a short and consistent title for the field (abbreviations allowed).

### Domain – (inherits Base)
The `Domain` class defines **field types** from the perspective of **valid values** they can accept.
It inherits its properties and behaviors from `{Base}`.

**Abstract Class**:
- The `Domain` class is **abstract** and must **never be instantiated directly**.
- Only its **descendant classes** should be instantiated.

**Examples of Common Domain Classes**:
- **AlfaN**: Represents a text field with a defined maximum length.
- **Decimal**: Represents a decimal field.

**Purpose**:
- To standardize and control the value constraints for each type of field used across Models, Tables, and Views.

**Validation Rules**:
- Ensure that only descendant domain classes are instantiated.
- Validate that domain definitions correspond to the field requirements.

### Domains

- #### String – (inherits Domain)
    The `String` class defines an **alphanumeric field** with a **maximum character length**.
    It inherits from the abstract `Domain` class.

    **Purpose**:
    - Represents a text field constrained by a maximum number of characters.

    **Properties**:
    - **Size**: The maximum allowed number of characters.

    **Simplex Syntax for String**:
    ```ebnf
    <String> ::= "String(" {Size} ")"
    ```

    **Example**:
    ```simplex
    String(135)
    ```

    **Validation Rules**:
    - Validate if the field `Dataname` using `String` type follows the standard prefix: `ds_

- #### Pk – (inherits Domain)
    The `Pk` class defines a **Primary Key field**.
    It inherits from the abstract `Domain` class.

    **Purpose**:
    - Represents an **auto-incrementing integer** (IDENTITY) field used as a **Primary Key**.

    **Simplex Syntax for Pk**:
    ```ebnf
    <Pk> ::= "Pk"
    ```

    **Example**:
    ```simplex
    Pk
    ```

    **Validation Rules**:
    - Ensure it is used as the Primary Key of the table.

- #### UUID – (inherits Domain)
    The `UUID` class defines a **long integer counter field** that **automatically increments** with each new record inserted into the table.
    It inherits from the abstract `Domain` class.

    **Purpose**:
    - Typically used as a **Primary Key (PK)** for tables or data structures.

    **Simplex Syntax for UUID**:
    ```ebnf
    <UUID> ::= "UUID"
    ```

    **Example**:
    ```simplex
    UUID
    ```

    **Validation Rules**:
    - Ensure it is appropriately used in contexts that require an auto-incrementing identifier (usually primary keys).

- #### Token – (inherits Domain)
    The `Token` class defines an **auto-incrementing identifier field** used as a unique identifier.
    It inherits from the abstract `Domain` class.

    **Purpose**:
    - Represents an auto-incrementing integer or GUID used as a **Primary Key (PK)** for tables.
    - Maps to SQL type `uniqueidentifier`.

    **Simplex Syntax for Token**:
    ```ebnf
    <Token> ::= "Token"
    ```

    **Example**:
    ```simplex
    Token
    ```

    **Validation Rules**:
    - Ensure it is appropriately used in contexts that require an auto-incrementing identifier (usually primary keys).
    - Validate if the field `Dataname` using `Token` type follows the standard prefix: `tk_`



- #### DateTime – (inherits Domain)
    The `DateTime` class defines a **date field** in the system, including **day, month, and year** components.
    It inherits from the abstract `Domain` class.

    **Purpose**:
    - Used to represent calendar dates with a defined mask for formatting.

    **Properties**:
    - **Mask**: Defines the visual format for displaying the date, e.g., `dd/mm/yyyy` or `dd/mm`.
    - **Properties**: Same as `Mask`, used in syntax definition.

    **Simplex Syntax for DateTime**:
    ```ebnf
    <DateTime> ::= "DateTime(" {Mask} ")"
    <GetProperties> ::= {Mask}
    ```

    **Example**:
    ```simplex
    DateTime(dd/mm/yyyy)
    ```

    **Validation Rules**:
    - `Mask` is required.
    - Validate if the `Mask` matches common standard date formats used in the market (e.g., `dd/mm/yyyy`, `yyyy-mm-dd`).

- #### Fk – (inherits UUID)
    The `Fk` class defines a **foreign key field** that references an identifier field from another table.
    It inherits from the `UUID` domain.

    **Purpose**:
    - Used to create **relationships between tables**.
    - These relationships, along with the tables, define the **Entity-Relationship Model** of the application.
    - In self-referencing cases, the `Fk` field may point to a field within the same table.

    **Properties**:
    - **FkTable**: The name of the referenced table.
    - **FkField**: The name of the referenced field within that table.

    **Simplex Syntax for Fk**:
    ```ebnf
    <Fk> ::= "Fk(" {FkTable}"."{FkField} ")"
    <GetProperties> ::= {FkTable.FkField.GetProperties}
    ```

    **Example**:
    ```simplex
    Fk(Usuario.id_Usuario)
    ```

    **Validation Rules**:
    - Validate if the referenced table and field `{FkTable}.{FkField}` exist in the application's table specifications.
    - Allow for self-referencing relationships within the same table.

- #### Int – (inherits Domain)
    The `Int` class defines a **numeric field** with a configurable number of **integer** places.
    It inherits from the abstract `Domain` class.

    **Purpose**:
    - Represents an **integer** field.
    - Input values are validated against `MinValue` and `MaxValue` constraints.

    **Properties**:
    - **Integer**: Number of integer digits.
    - **MinValue**: Minimum valid value.
    - **MaxValue**: Maximum valid value.

    **Simplex Syntax for Int**:
    ```ebnf
    <Int> ::= "Int(" {Integer} ")"
    <GetProperties> ::= "MinValue=" {MinValue} ", MaxValue=" {MaxValue}
    ```

    **Example**:
    ```simplex
    Int(10) {Dataname} MinValue=0, MaxValue=1000000
    ```

    **Validation Rules**:
    - Validate if the defined precision (Integer part) is within the supported limit (e.g., <= 10).
    - Validate if the defined range (MinValue and MaxValue) is within the supported limit (e.g., <= 10).

- #### Decimal – (inherits Domain)
    The `Decimal` class defines a **numeric field** with a configurable number of **integer** and **decimal** digits.
    It inherits from the abstract `Domain` class.

    **Purpose**:
    - Represents an **decimal** field.
    - Input values are validated against `MinValue` and `MaxValue` constraints.

    **Properties**:
    - **Integer**: Number of integer digits.
    - **Decimals**: Number of decimal digits.
    - **MinValue**: Minimum valid value.
    - **MaxValue**: Maximum valid value.

    **Simplex Syntax for Int**:
    ```ebnf
    <Decimal> ::= "Decimal(" {Integer}"," {Decimal}")"
    <GetProperties> ::= "MinValue=" {MinValue} ", MaxValue=" {MaxValue}
    ```

    **Example**:
    ```simplex
    Decimal(10,2) {Dataname} MinValue=0,00, MaxValue=1000000,00
    ```

    **Validation Rules**:
    - Validate if Integer is less than or equal to 10.
    - Validate if Decimals is less than or equal to 2.
    - Validate if the defined range (MinValue and MaxValue) is within the supported limit (e.g., <= 10).

- #### Enum – (inherits Domain)
    The `Enum` class defines a **code-description pair list** field.
    It inherits from the abstract `Domain` class.

    **Purpose**:
    - Represents a predefined list of code-description pairs.
    - The **code** is stored in the database (can be integer or alphanumeric).
    - The **description** is displayed to the user in Views.

    **Properties**:
    - **List**: A list containing the code-description pairs.

    **Simplex Syntax for Enum**:
    ```ebnf
    <Enum> ::= "Enum()"
    <GetProperties> ::= { <code> "-" <description> "," }
    <code> ::= Code of the item in the list.
    <description> ::= Description of the item.
    ```

    **Examples**:
    ```simplex
    Enum() {Dataname} 1-Pessoa Juridica, 2-Pessoa Fisica
    ```
    ```simplex
    Enum() {Dataname} "A"-Active, "I"-Inactive, "C"-Canceled
    ```
    - If the values list is not yet defined, annotate as: `(no values undefined)`

    **Validation Rules**:
    - Validate that the `List` contains at least **two** items.

- #### Workflow – (inherits Enum)
    The `Workflow` class defines a **state field** with defined transition rules.
    It inherits from the `Enum` class.

    **Purpose**:
    - Represents a status field where transitions between values are governed by specific business rules.
    - The states and transition rules are defined in the **Workflows** section of the Lean Inception document.

    **Simplex Syntax for Workflow**:
    ```ebnf
    <Workflow> ::= "Workflow()"
    <GetProperties> ::= { <code> "-" <description> "," }
    ```

    **Example**:
    ```simplex
    Workflow() {Dataname} 1-Draft, 2-Active, 3-Closed
    ```

    **Validation Rules**:
    - Validate that the corresponding workflow definition exists in the Lean Inception document.
    - Inherits validation rules from `Enum`.

### API – (inherits Base)
The `API` class defines a **set of Services** (methods or functions) related to a specific business functionality of the application.
It inherits from `{Base}`.

**Simplex Syntax for Api**:
```ebnf
<API> ::= {Dataname} ":API" "{" {<Service>} "}"
```

**Properties**:
- **Description**: Describes the business purpose of the services provided by this API.
- **Services**: A list of `{Service}` elements defined using the Simplex syntax.

**Example**:
```simplex
Usuario:API
{
    UUID DalUsuarioIncluir(nm_Nome, ds_Senha, dt_Inclusao, ds_Tipo, nm_Salario, id_TipoUsuario)
    {
        UUID id_Usuario =
            INSERT INTO Usuario(nm_Nome, ds_Senha, dt_Inclusao, ds_Tipo, nm_Salario, id_TipoUsuario)
            VALUES(@nm_Nome, @ds_Senha, @dt_Inclusao, @ds_Tipo, @nm_Salario, @id_TipoUsuario);
            SELECT @@IDENTITY as NewId;
        return id_Usuario;
    }

    ModUsuarioConsultar DalUsuarioConsultar(id_Usuario)
    {
        ModUsuarioConsultar retorno = SELECT
                USU.*,
                TPU.ds_TipoUsuario
            FROM Usuario AS USU
            LEFT JOIN TipoUsuario AS TPU ON USU.id_TipoUsuario = TPU.id_TipoUsuario
            WHERE USU.id_Usuario = @id_Usuario;
        return retorno;
    }

    void DalUsuarioAlterar(id_Usuario, nm_Nome, ds_Senha, ds_Tipo, nm_Salario, id_TipoUsuario)
    {
        UPDATE Usuario
        SET nm_Nome = @nm_Nome,
            ds_Senha = @ds_Senha,
            ds_Tipo = @ds_Tipo,
            nm_Salario = @nm_Salario,
            id_TipoUsuario = @id_TipoUsuario
        WHERE id_Usuario = @id_Usuario;
    }

    List<ModUsuarioListar> DalUsuarioListar()
    {
        List<ModUsuarioListar> retorno = SELECT
                USU.ds_Senha,
                USU.ds_Tipo,
                USU.nm_Salario,
                USU.id_TipoUsuario,
                TPU.ds_TipoUsuario
            FROM Usuario as USU, TipoUsuario as TPU
            WHERE Usuario.id_TipoUsuario = TipoUsuario.id_TipoUsuario;
        return retorno;
    }

    void DalUsuarioExcluir(id_Usuario)
    {
        DELETE FROM Usuario WHERE id_Usuario = @id_Usuario;
    }

}
```

**Validation Rules**:
- Validate if the API includes a meaningful and concise business-purpose `Description`.
- Validate the syntax and structure of each defined `Service` element within the API block.

### Service – (inherits Base)
The `Service` class defines a **business service**, **function**, or **method**.
It inherits from `{Base}`.

**Purpose**:
- Executes a sequential set of **commands** (business logic).
- Can receive input parameters and return a result.

**Properties**:
- **Description**: Describes the essence of the service.
- **Input**: list of Fields.
- **Output**: a Domain, Model, or FR
- **Commands**: list of '{Command}'. Sequence of operations to be executed within the service.

**Simplex Syntax for Service**:
```ebnf
<Service> ::= returnType name "(" parameters ")" body ;
returnType ::= [ "void" | Output ] ;
name ::= Dataname ;
parameters ::= [ parameter { "," parameter } ] ;
parameter ::= Input.fieldType Input.fieldIdentifier ;
body ::= "{" { command } "}" ;
command ::= Command.Text ";" ;
```

**Command Examples**:
- Conditional logic with `if/else` blocks.
- Local variable declaration and assignment:
  - `UUID id_Usuario = 0;`
  - `String nm_Produto = "Product Name";`
  - `DateTime dt_Payment;`
  - `ModProduto mod_Produto = SELECT * FROM Produto WHERE id_Produto = @id_Produto;`
- Service or method calls:
  - `ModProduto mod_Produto = Produto.DalConsultar(id_Produto);`
- Iteration with `foreach`:
  ```csharp
  List<ModProduto> lst_Produtos = SELECT * FROM Produto;
  foreach (ModProduto modProduto in lst_Produtos)
  {
      // further commands
  }
  ```

**Model References**:
- If Output does not reference a Table model specification, it must reference a local Model specification inside the API's folder with the name '{Service.Dataname}.mod'.
- If a return Model is not a Table, it must be defined locally as a .mod file with the same name as the service.

**Examples**:
```simplex
void DalUsuarioAlterar(id_Usuario, nm_Nome, ds_Senha, ds_Tipo, nm_Salario, id_TipoUsuario)
{
    UPDATE Usuario
    SET nm_Nome = @nm_Nome,
        ds_Senha = @ds_Senha,
        ds_Tipo = @ds_Tipo,
        nm_Salario = @nm_Salario,
        id_TipoUsuario = @id_TipoUsuario
    WHERE id_Usuario = @id_Usuario;
}
```

```simplex
UUID DalUsuarioIncluir(nm_Nome, ds_Senha, dt_Inclusao, ds_Tipo, nm_Salario, id_TipoUsuario)
{
    UUID id_Usuario =
        INSERT INTO Usuario(nm_Nome, ds_Senha, dt_Inclusao, ds_Tipo, nm_Salario, id_TipoUsuario)
        VALUES(@nm_Nome, @ds_Senha, @dt_Inclusao, @ds_Tipo, @nm_Salario, @id_TipoUsuario);
        SELECT @@IDENTITY as id_Usuario;
    return id_Usuario;
}
```

**Validation Rules**:
- Ensure parameters and return models are correctly typed.
- Validate the proper structure and sequencing of `commands`.
- Confirm that logic blocks, variable declarations, service calls, and iteration commands are syntactically correct.

### Command – (inherits Base)
The `Command` class defines a **single operation or step** within the logic of a Service.
It inherits from `{Base}`.

**Purpose**:
- Represents one line or unit of execution in the service's command sequence.
- The entire business logic of a `Service` is built by sequentially executing its `Command` elements.

**Properties**:
- **Text**: The raw content of the command.
  - May be written in natural language.
  - May also follow the syntax rules of a specific subclass of `Command`.

**Simplex Syntax for Command**:
```ebnf
<Command> ::= {User Text} ";"
```

**Example**:
```simplex
Execute the SQL command:
    UPDATE Usuario
    SET nm_Nome = @nm_Nome,
        ds_Senha = @ds_Senha,
        ds_Tipo = @ds_Tipo,
        nm_Salario = @nm_Salario,
        id_TipoUsuario = @id_TipoUsuario
    WHERE id_Usuario = @id_Usuario;
```

**Validation Rules**:
- Ensure the `Text` is syntactically valid (either natural language or conforming to subclass-specific format).
- If the command represents a SQL or function call, validate its structure and referenced fields.

### View – (inherits Base)
The `View` class defines a **user interface** or **window**
It inherits from `{Base}`.

**Purpose**:
- Opens an aaplication window where the user can interact with data to perform a bussiness function.
- Can receive Input parameters and return an Output.

**Properties**:
- **Description**: Describes the essence of the service.
- **Input**: list of Fields.
- **Output**: a Domain, Model, or FR
- **UIComponents**: list of '{UIComponent}'
- **Events**: list of '{Event}'

**Simplex Syntax for View**:
```ebnf
<View> ::= "void" ViewName "(" Parameters ")" ":View" "{" 
              UIComponents 
              Events 
          "}"
ViewName       ::= identifier
Parameters     ::= [ Parameter { "," Parameter } ]
Parameter      ::= Type identifier
Type           ::= DomainType | ModelType
UIComponents ::= "/// Declaração de Componentes da View" { UIComponent }
Events         ::= "/// Declaração dos Eventos" { Event }
```

### UIComponent – (inherits Base)
The `UIComponent` class defines a **user component interface**
It inherits from `{Base}`.

**Purpose**:
- It´s an abstract class used as base class for all kind of Elements used in Views.
- Examples: Text, Select, Label, Button, Grid, Icon, Link, etc.
- Can receive Input parameters and return an Output.

### Event – (inherits Service)
This class inherits from Service.
The only difference is that Event are methods that run when the user interacts with UIComponents.

### UIComponents
    - UI Components are used to build Views.
    - When building a MockUp, utilize the definitions **Mockup Html Example** described in Platform.md.
    - When generating a Mockup screen, show it using `updateFileContent`, named `ViewName.cshtml`.

- #### Text – (inherits UIComponent)
    The `Text` class defines a TextBox field for user input.
    **Syntax**: `Text({Field}) {Dataname};`
    **Example**: `Text(Usuario.nm_Usuario) txt_NmUsuario;`

- #### Label – (inherits UIComponent)
    The `Label` class defines a static text label.
    **Syntax**: `Label({Text}) {Dataname};`
    **Example**: `Label(Usuario.nm_Usuario) lbl_NmUsuario;`

- #### Select – (inherits UIComponent)
    The `Select` class defines a DropDownList or ComboBox.
    **Syntax**: `Select({Service}) {Dataname};`
    **Example**: `Select(DalListarTipos) sel_DsTipo;`

- #### Calendar – (inherits UIComponent)
    The `Calendar` class defines a DatePicker input.
    **Syntax**: `Calendar({Field}) {Dataname};`
    **Example**: `Calendar(Usuario.dt_Inclusao) cal_DtInclusao;`

- #### Switch – (inherits UIComponent)
    The `Switch` class defines a toggle switch (boolean).
    **Syntax**: `Switch({Field}) {Dataname};`
    **Example**: `Switch(Usuario.fl_Ativo) swi_FlAtivo;`

- #### Checkbox – (inherits UIComponent)
    The `Checkbox` class defines a checkbox input.
    **Syntax**: `Checkbox({Field}) {Dataname};`
    **Example**: `Checkbox(Usuario.fl_Ativo) chk_FlAtivo;`

- #### Grid – (inherits UIComponent)
    The `Grid` class defines a data table.
    **Syntax**: `Grid({Service}) {Dataname};`
    **Example**: `Grid(DalListarUsuarios) grd_Usuarios;`

- #### Button – (inherits UIComponent)
    Base class for buttons. Specific types:
    - `SaveButton`: Submit/Save action.
    - `CloseButton`: Close modal/window.
    - `BackButton`: Navigate back.
    - `NewButton`: Create new item.
    - `EditButton`: Edit item.
    - `UploadButton`: Upload file.
    - `ReportsButton`: View reports.
    - `ProgramButton`: Scheduling/Programming.
    **Syntax**: `{ButtonType}() {Dataname};`
    **Example**: `SaveButton() but_Salvar;`

- #### Action – (inherits UIComponent)
    Base class for grid row actions. Specific types:
    - `ActionEdit`: Edit row.
    - `ActionDelete`: Delete row.
    - `ActionView`: View details.
    - `ActionMove`: Move item.
    - `ActionUpload`: Upload attachment.
    **Syntax**: `{ActionType}() {Dataname};`
    **Example**: `ActionEdit() act_Editar;`

- #### Panel – (inherits UIComponent)
    Base class for containers. Specific types:
    - `FilterPanel`: Container for search filters.
    - `TabPanel`: Container for tabs cases.
    **Syntax**: `{PanelType}() {Dataname};`

- #### Chart – (inherits UIComponent)
    Base class for charts. Specific types:
    - `PizzaChart`: Pie chart.
    **Syntax**: `{ChartType}() {Dataname};`






