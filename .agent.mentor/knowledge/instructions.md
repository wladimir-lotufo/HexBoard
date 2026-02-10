---
description: 
---

# Padrões de Codificação e Arquitetura 

Este documento descreve os padrões de codificação, nomenclatura e arquitetura identificados no módulo `Seguranca2` e que devem ser seguidos para novos desenvolvimentos.

> [!NOTE]
> Para definições detalhadas de **Simplex** e **Platform** (Estrutura de Diretórios, Geraçao de Código, UIComponents), consulte os arquivos `.agent/knowledge/Simplex.md` e `.agent/knowledge/Platform.md`.

## 1. Regras de Interação (Áudio)
> [!IMPORTANT]
> **Transição de Áudio para Texto**: Para qualquer entrada de áudio recebida, **ANTES** de executar qualquer comando:
> 1.  **Transcreva** o áudio para texto.
> 2.  **Apresente** o texto transcrito de forma estruturada.
> 3.  **Solicite confirmação** do usuário para prosseguir com a execução.

## 2. Padrões de Nomenclatura

### 2.1. Elementos de Interface (HTML/Razor)
Utilize prefixos de 3 letras seguidos de underscore para IDs e nomes de variáveis de elementos UI:

*   **Botões**: `but_[Acao]` (ex: `but_Salvar`, `but_NovoUsuario`, `but_Editar`)
*   **Campos de Texto**: `txt_[NomeCampo]` (ex: `txt_DsNome`, `txt_NmSalario`)
*   **Selects/Dropdowns**: `sel_[NomeCampo]` (ex: `sel_DsTipo`, `sel_IdIdiomaPreferido`)
*   **Calendários/Datas**: `cal_[NomeCampo]` ou `dt_[NomeCampo]` no input group (ex: `cal_DtInclusao`)
*   **Grids/Tabelas**: `lst_[NomeLista]` ou `datatablecustom`
*   **Badges/Labels**: `bad_[Nome]` (ex: `bad_Status`)
*   **Paineis/Tabs**: `pnl_[Nome]` ou `tab_[Nome]`

### 2.2. Banco de Dados e Models
*   **Tabelas**: `[Entidade]` (ex: `Usuario`)
*   **Colunas**:
    *   Chave Primária: `id_[Entidade]` (ex: `id_Usuario`)
    *   Strings/Descrições: `ds_[Nome]` (ex: `ds_Nome`, `ds_Senha`, `ds_Tipo`)
    *   Valores Numéricos/Monetários: `nm_[Nome]` (ex: `nm_Salario`)
    *   Datas: `dt_[Nome]` (ex: `dt_Inclusao`)
    *   Chaves Estrangeiras (Fk): `id_[EntidadeRelacionada]` (ex: `id_IdiomaPreferido`)

### 2.3. Métodos C# (API/DAL)
*   **Incluir**: `Dal[Entidade]Incluir`
*   **Alterar**: `Dal[Entidade]Alterar`
*   **Consultar**: `Dal[Entidade]Consultar`
*   **Excluir**: `Dal[Entidade]Excluir`
*   **Listar**: `Listar[Entidade]s` ou `Dal[Entidade]Listar`

## 3. Padrões de Implementação (Resumo)

### 3.1. Frontend (Razor + jQuery)
*   **Bibliotecas**: jQuery, Bootstrap, DataTables, SweetAlert (`swal`), Select2.
*   **Inicialização**: Use `$(document).ready()` e chamadas AJAX padronizadas.
*   **Serialização**: `JsonConvert.SerializeObject` para passar Models para JS.

### 3.2. Backend (Controller)
*   **Actions**: `ActionResult` (View), `PartialView` (Modal), `JsonResult` (CRUD).
*   **Retorno**: Use `JsonNetResult`.

### 3.3. Acesso a Dados (ADO.NET)
*   **Classe Conexao**: Gerenciamento manual de transação (`SqlTransaction`).
*   **Mapeamento**: `SqlCommand` com parâmetros explícitos.
