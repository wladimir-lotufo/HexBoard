/*
Seguranca2:Controller
{
    void ListaUsuarios();
    void DetalheUsuario(int id_Usuario);
    List<ModUsuariosListar> ListarTodosUsuarios();
    ModUsuario2 butSalvar_Click(ModUsuario2 modUsuario);
    int butExcluir_Click(int id_Usuario);
}
*/

using System;
using System.Web.Mvc;
using System.Collections.Generic;
using Newtonsoft.Json;
using WarpSolutions.Views.Seguranca2;
using WarpSolutions.APIs.Usuario;
using WarpSolutions.Models;

namespace WarpSolutions.Controllers
{
    public class Seguranca2Controller : Controller
    {
        // Existing actions ...

        
        /// <summary>
        /// ```simplex
        /// void ListaUsuarios() {
        ///     List<ModUsuariosListar> lstUsuariosListar = dbUsuario.ListarTodosUsuarios();
        ///     return lstUsuariosListar;
        /// }
        /// ```
        /// </summary>
        public ActionResult ListaUsuarios()
        {
            List<ModUsuariosListar> lstUsuariosListar = new List<ModUsuariosListar>();
            DbUsuario dbUsuario = new DbUsuario();

            try
            {
                // Instancia nossa Conexao
                Conexao conexao = new Conexao(TipoConexao.Conexao.WebConfig);

                // Se existe erro na conexao move o erro para a classe de acesso 
                if (conexao.ExisteErro())
                {
                    setMensagemErro(conexao.mErro);
                    return View("ListaUsuarios", lstUsuariosListar);
                }

                // Fetch the list of users
                lstUsuariosListar = dbUsuario.ListarTodosUsuarios();

                return View("ListaUsuarios", lstUsuariosListar);
            }
            catch (Exception ex)
            {
                return View("ListaUsuarios", lstUsuariosListar).Mensagem(dbUsuario.MensagemErroFormatada(), ex.Message);
            }
        }

        /// <summary>
        /// ```simplex
        /// void DetalheUsuario(int id_Usuario) {
        ///     ModDetalheUsuario modDetalheUsuario;
        ///     if (id_Usuario != 0) {
        ///         modDetalheUsuario.modUsuario = dbUsuario.DalUsuarioConsultar(id_Usuario);
        ///     }
        ///     DbIdioma dbIdioma = new DbIdioma();
        ///     modDetalheUsuario.lstListIdiomas = dbIdioma.DalIdiomaListar();
        ///     return modDetalheUsuario;
        /// }
        /// ```
        /// </summary>
        public ActionResult DetalheUsuario(int id_Usuario)
        {
            ModDetalheUsuario modDetalheUsuario = new ModDetalheUsuario();
            DbUsuario dbUsuario = new DbUsuario();

            try
            {
                if (id_Usuario != 0)
                {
                    // Fetch the DetalheUsuario based on the ID
                    modDetalheUsuario.modUsuario = dbUsuario.DalUsuarioConsultar(id_Usuario);
                }

                // Fetch the IdiomasListar based 
                DbIdioma dbIdioma = new DbIdioma();
                modDetalheUsuario.lstListIdiomas = dbIdioma.DalIdiomaListar();

                return PartialView("DetalheUsuario", modDetalheUsuario);
            }
            catch (Exception ex)
            {
                return PartialView("DetalheUsuario", modDetalheUsuario).Mensagem(dbUsuario.MensagemErroFormatada(), ex.Message);
            }
        }

        /// <summary>
        /// ```simplex
        /// List<ModUsuariosListar> ListarTodosUsuarios() {
        ///     List<ModUsuariosListar> lstUsuariosListar;
        ///     try {
        ///         lstUsuariosListar = dbUsuario.ListarTodosUsuarios();
        ///         return lstUsuariosListar;
        ///     }
        ///     catch (Exception ex) {
        ///         return lstUsuariosListar.Mensagem(dbUsuario.MensagemErroFormatada(), ex.Message);
        ///     }
        /// }
        /// ```
        /// </summary>
        [HttpPost]
        public JsonResult ListarTodosUsuarios()
        {
            var resultado = "ERRO";
            var mensagens = new List<string>();
            var chaves = new List<string>();

            // Fetch the list of users
            DbUsuario dbUsuario = new DbUsuario();
            List<ModUsuariosListar> lstUsuariosListar = new List<ModUsuariosListar>();

            try
            {
                lstUsuariosListar = dbUsuario.ListarTodosUsuarios();
                resultado = "OK";
            }
            catch (Exception ex)
            {
                resultado = "ERRO";
                mensagens.Add("Não foi possível realizar a ação. Por favor tente novamente mais tarde ou comunique o administrador do sistema!");
                chaves.Add("1");
            }

            return new JsonNetResult { Data = new { Resultado = resultado, Chaves = chaves, Mensagens = mensagens, Retorno = lstUsuariosListar } };
        }

        /// <summary>
        /// ```simplex
        /// ModUsuario2 butSalvar_Click(ModUsuario2 modUsuario) {
        ///     if (modUsuario.id_Usuario == 0)
        ///     {
        ///         modUsuario = dbUsuario.DalUsuarioIncluir(modUsuario);
        ///     }
        ///     else
        ///     {
        ///         modUsuario = dbUsuario.DalUsuarioAlterar(modUsuario);
        ///     }
        ///     return modUsuario;
        /// }
        /// ```
        /// </summary>  
        [HttpPost]
        public JsonResult butSalvar_Click(ModUsuario2 modUsuario)
        {
            var resultado = "ERRO";
            var mensagens = new List<string>();
            var chaves = new List<string>();

            // Fetch the list of users
            DbUsuario dbUsuario = new DbUsuario();

            try
            {
                if (modUsuario.id_Usuario == 0)
                {
                    modUsuario = dbUsuario.DalUsuarioIncluir(modUsuario);
                }
                else
                {
                    modUsuario = dbUsuario.DalUsuarioAlterar(modUsuario);
                }

                resultado = "OK";
            }
            catch (Exception ex)
            {
                resultado = "ERRO";
                mensagens.Add(ex.Message);
                chaves.Add("1");
            }

            return new JsonNetResult { Data = new { Resultado = resultado, Chaves = chaves, Mensagens = mensagens, Retorno = modUsuario } };
        }

        /// <summary>
        /// ```simplex
        /// int butExcluir_Click(int id_Usuario) {
        ///     if (id_Usuario != 0)
        ///     {
        ///         dbUsuario.DalUsuarioExcluir(id_Usuario);
        ///     }
        ///     return id_Usuario;
        /// }
        /// ```
        /// </summary>  
        [HttpPost]
        public JsonResult butExcluir_Click(int id_Usuario)
        {
            var resultado = "ERRO";
            var mensagens = new List<string>();
            var chaves = new List<string>();

            // Fetch the list of users
            DbUsuario dbUsuario = new DbUsuario();

            try
            {
                if (id_Usuario != 0)
                {
                    dbUsuario.DalUsuarioExcluir(id_Usuario);
                }
                resultado = "OK";
            }
            catch (Exception ex)
            {
                resultado = "ERRO";
                mensagens.Add(ex.Message);
                chaves.Add("1");
            }

            return new JsonNetResult { Data = new { Resultado = resultado, Chaves = chaves, Mensagens = mensagens, Retorno = id_Usuario } };
        }

    }


}