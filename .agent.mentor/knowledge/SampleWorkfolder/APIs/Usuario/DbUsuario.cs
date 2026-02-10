using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WarpSolutions.Models;
using WarpSolutions.Views.Seguranca2;

namespace WarpSolutions.APIs.Usuario
{
    public partial class DbUsuario : Erros
    {
        /// <summary>
        /// ```simplex
        /// ModUsuario2 DalUsuarioIncluir (ds_Nome, ds_Senha, dt_Inclusao, ds_Tipo, nm_Salario, id_IdiomaPreferido) {
        ///     INSERT INTO Usuario2 (ds_Nome, ds_Senha, dt_Inclusao, ds_Tipo, nm_Salario, id_IdiomaPreferido) 
        ///         VALUES (@ds_Nome, @ds_Senha, @dt_Inclusao, @ds_Tipo, @nm_Salario, @id_IdiomaPreferido);
        ///     ModUsuario2 modUsuario2 = SELECT * FROM Usuario2 WHERE id_Usuario = @@IDENTITY;
        ///     return modUsuario2;
        /// }
        /// ```
        /// </summary>
        /// <param name="ds_Nome"></param>
        /// <param name="ds_Senha"></param>
        /// <param name="dt_Inclusao"></param>
        /// <param name="ds_Tipo"></param>
        /// <param name="nm_Salario"></param>
        /// <param name="id_IdiomaPreferido"></param>
        /// <param name="conexao"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public ModUsuario2 DalUsuarioIncluir(string ds_Nome, string ds_Senha, DateTime dt_Inclusao, string ds_Tipo, decimal nm_Salario, int id_IdiomaPreferido, Conexao conexao = null, SqlTransaction tran = null)
        {
            ModUsuario2 _return = new ModUsuario2();

            bool ConexaoExterna = (conexao == null ? false : true);

            if (ConexaoExterna == false)
            {
                conexao = new Conexao(TipoConexao.Conexao.WebConfig);
                if (conexao.ExisteErro()) return _return;
                if (!conexao.OpenConexao())
                {
                    setMensagemErro(conexao.mErro);
                    return _return;
                }

                if (tran == null)
                {
                    tran = conexao.conn.BeginTransaction();
                }
            }

            try
            {
                SqlDataReader reader;
                string query = @"INSERT INTO Usuario2 (ds_Nome, ds_Senha, dt_Inclusao, ds_Tipo, nm_Salario, id_IdiomaPreferido) 
                                VALUES (@ds_Nome, @ds_Senha, @dt_Inclusao, @ds_Tipo, @nm_Salario, @id_IdiomaPreferido); 
                                SELECT * FROM Usuario2 WHERE id_Usuario = @@IDENTITY;";

                SqlCommand cmd = new SqlCommand(query, conexao.conn);
                if (tran != null) cmd.Transaction = tran;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ds_Nome", ds_Nome);
                cmd.Parameters.AddWithValue("@ds_Senha", ds_Senha);
                cmd.Parameters.AddWithValue("@dt_Inclusao", dt_Inclusao);
                cmd.Parameters.AddWithValue("@ds_Tipo", ds_Tipo);
                cmd.Parameters.AddWithValue("@nm_Salario", nm_Salario);
                cmd.Parameters.AddWithValue("@id_IdiomaPreferido", id_IdiomaPreferido);

                using (reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        _return = ModUsuario2.Read(reader);
                    }
                }

                if ((ConexaoExterna == false) && (tran != null))
                {
                    tran.Commit();
                }
            }
            catch (Exception e)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }

                // Trata os erros de nossa conex�o
                setMensagemErro(e.Message.ToString());
            }

            if (ConexaoExterna == false)
            {
                conexao.CloseConexao();
            }

            return _return;
        }

        /// <summary>   
        /// ```simplex
        /// ModUsuario2 DalUsuarioIncluir (modUsuario) {
        ///     INSERT INTO Usuario2 (ds_Nome, ds_Senha, dt_Inclusao, ds_Tipo, nm_Salario, id_IdiomaPreferido) 
        ///         VALUES (@modUsuario.ds_Nome, @modUsuario.ds_Senha, @modUsuario.dt_Inclusao, @modUsuario.ds_Tipo, @modUsuario.nm_Salario, @modUsuario.id_IdiomaPreferido);
        ///     ModUsuario2 modUsuario2 = SELECT * FROM Usuario2 WHERE id_Usuario = @@IDENTITY;
        ///     return modUsuario2;
        /// }
        /// ```
        /// </summary>
        /// <param name="modUsuario"></param>
        /// <param name="conexao"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public ModUsuario2 DalUsuarioIncluir(ModUsuario2 modUsuario, Conexao conexao = null, SqlTransaction tran = null)
        {
            bool ConexaoExterna = (conexao == null ? false : true);

            if (ConexaoExterna == false)
            {
                conexao = new Conexao(TipoConexao.Conexao.WebConfig);
                if (conexao.ExisteErro()) return modUsuario;
                if (!conexao.OpenConexao())
                {
                    setMensagemErro(conexao.mErro);
                    return modUsuario;
                }

                if (tran == null)
                {
                    tran = conexao.conn.BeginTransaction();
                }
            }

            try
            {
                SqlDataReader reader;
                string query = @"INSERT INTO Usuario2 (ds_Nome, ds_Senha, dt_Inclusao, ds_Tipo, nm_Salario, id_IdiomaPreferido) 
                                VALUES (@ds_Nome, @ds_Senha, @dt_Inclusao, @ds_Tipo, @nm_Salario, @id_IdiomaPreferido); 
                                SELECT * FROM Usuario2 WHERE id_Usuario = @@IDENTITY;";

                SqlCommand cmd = new SqlCommand(query, conexao.conn);
                if (tran != null) cmd.Transaction = tran;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ds_Nome", modUsuario.ds_Nome);
                cmd.Parameters.AddWithValue("@ds_Senha", modUsuario.ds_Senha);
                cmd.Parameters.AddWithValue("@dt_Inclusao", modUsuario.dt_Inclusao);
                cmd.Parameters.AddWithValue("@ds_Tipo", modUsuario.ds_Tipo);
                cmd.Parameters.AddWithValue("@nm_Salario", modUsuario.nm_Salario);
                cmd.Parameters.AddWithValue("@id_IdiomaPreferido", modUsuario.id_IdiomaPreferido);

                using (reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        modUsuario = ModUsuario2.Read(reader);
                    }
                }

                if ((ConexaoExterna == false) && (tran != null))
                {
                    tran.Commit();
                }
            }
            catch (Exception e)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }

                // Trata os erros de nossa conex�o
                setMensagemErro(e.Message.ToString());
            }

            if (ConexaoExterna == false)
            {
                conexao.CloseConexao();
            }

            return modUsuario;
        }

        /// <summary>
        /// ```simplex
        /// ModUsuario2 DalUsuarioConsultar (id_Usuario) {
        ///     ModUsuario2 modUsuario2 = SELECT * FROM Usuario2 WHERE id_Usuario = @id_Usuario;
        ///     return modUsuario2;
        /// }
        /// ```
        /// </summary>
        /// <param name="id_Usuario"></param>
        /// <param name="conexao"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public ModUsuario2 DalUsuarioConsultar(int id_Usuario, Conexao conexao = null, SqlTransaction tran = null)
        {
            ModUsuario2 _return = new ModUsuario2();

            bool ConexaoExterna = (conexao == null ? false : true);

            if (ConexaoExterna == false)
            {
                conexao = new Conexao(TipoConexao.Conexao.WebConfig);
                if (conexao.ExisteErro()) return _return;
                if (!conexao.OpenConexao())
                {
                    setMensagemErro(conexao.mErro);
                    return _return;
                }

                if (tran == null)
                {
                    tran = conexao.conn.BeginTransaction();
                }
            }

            try
            {
                SqlDataReader reader;
                string query = "SELECT * FROM Usuario2 WHERE id_Usuario = @id_Usuario;";

                SqlCommand cmd = new SqlCommand(query, conexao.conn);
                if (tran != null) cmd.Transaction = tran;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id_Usuario", id_Usuario);

                using (reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        _return = ModUsuario2.Read(reader);
                    }
                }

                if ((ConexaoExterna == false) && (tran != null))
                {
                    tran.Commit();
                }
            }
            catch (Exception e)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }

                // Trata os erros de nossa conex�o
                setMensagemErro(e.Message.ToString());
            }

            if (ConexaoExterna == false)
            {
                conexao.CloseConexao();
            }

            return _return;
        }

        /// <summary>
        /// ```simplex
        /// ModUsuario2 DalUsuarioAlterar (id_Usuario, ds_Nome, ds_Senha, dt_Inclusao, ds_Tipo, nm_Salario, id_IdiomaPreferido) {
        ///     ModUsuario2 modUsuario2 = SELECT * FROM Usuario2 WHERE id_Usuario = @id_Usuario;
        ///     return modUsuario2;
        /// }
        /// ```
        /// </summary>
        /// <param name="id_Usuario"></param>
        /// <param name="ds_Nome"></param>
        /// <param name="ds_Senha"></param>
        /// <param name="dt_Inclusao"></param>
        /// <param name="ds_Tipo"></param>
        /// <param name="nm_Salario"></param>
        /// <param name="id_IdiomaPreferido"></param>
        /// <param name="conexao"></param>
        /// <param name="tran"></param>
        /// <returns></returns> 
        public ModUsuario2 DalUsuarioAlterar(int id_Usuario, string ds_Nome, string ds_Senha, DateTime dt_Inclusao, string ds_Tipo, decimal nm_Salario, int id_IdiomaPreferido, Conexao conexao = null, SqlTransaction tran = null)
        {
            ModUsuario2 _return = new ModUsuario2();

            bool ConexaoExterna = (conexao == null ? false : true);

            if (ConexaoExterna == false)
            {
                conexao = new Conexao(TipoConexao.Conexao.WebConfig);
                if (conexao.ExisteErro()) return _return;
                if (!conexao.OpenConexao())
                {
                    setMensagemErro(conexao.mErro);
                    return _return;
                }

                if (tran == null)
                {
                    tran = conexao.conn.BeginTransaction();
                }
            }

            try
            {
                string query = "UPDATE Usuario2 SET ds_Nome = @ds_Nome, ds_Senha = @ds_Senha, dt_Inclusao = @dt_Inclusao, ds_Tipo = @ds_Tipo, nm_Salario = @nm_Salario, id_IdiomaPreferido = @id_IdiomaPreferido WHERE id_Usuario = @id_Usuario;";

                SqlCommand cmd = new SqlCommand(query, conexao.conn);
                if (tran != null) cmd.Transaction = tran;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id_Usuario", id_Usuario);
                cmd.Parameters.AddWithValue("@ds_Nome", ds_Nome);
                cmd.Parameters.AddWithValue("@ds_Senha", ds_Senha);
                cmd.Parameters.AddWithValue("@dt_Inclusao", dt_Inclusao);
                cmd.Parameters.AddWithValue("@ds_Tipo", ds_Tipo);
                cmd.Parameters.AddWithValue("@nm_Salario", nm_Salario);
                cmd.Parameters.AddWithValue("@id_IdiomaPreferido", id_IdiomaPreferido);

                cmd.ExecuteNonQuery();

                if ((ConexaoExterna == false) && (tran != null))
                {
                    tran.Commit();
                }

                _return.id_Usuario = id_Usuario;
                _return.ds_Nome = ds_Nome;
                _return.ds_Senha = ds_Senha;
                _return.dt_Inclusao = dt_Inclusao;
                _return.ds_Tipo = ds_Tipo;
                _return.nm_Salario = nm_Salario;
                _return.id_IdiomaPreferido = id_IdiomaPreferido;

            }
            catch (Exception e)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }

                // Trata os erros de nossa conex�o
                setMensagemErro(e.Message.ToString());
            }

            if (ConexaoExterna == false)
            {
                conexao.CloseConexao();
            }

            return _return;
        }

        /// <summary>
        /// ```simplex
        /// ModUsuario2 DalUsuarioAlterar (modUsuario) {
        ///     ModUsuario2 modUsuario2 = SELECT * FROM Usuario2 WHERE id_Usuario = @modUsuario.id_Usuario;
        ///     return modUsuario2;
        /// }
        /// ```
        /// </summary>
        /// <param name="modUsuario"></param>
        /// <param name="conexao"></param>
        /// <param name="tran"></param>
        /// <returns></returns> 
        public ModUsuario2 DalUsuarioAlterar(ModUsuario2 modUsuario, Conexao conexao = null, SqlTransaction tran = null)
        {
            bool ConexaoExterna = (conexao == null ? false : true);

            if (ConexaoExterna == false)
            {
                conexao = new Conexao(TipoConexao.Conexao.WebConfig);
                if (conexao.ExisteErro()) return modUsuario;
                if (!conexao.OpenConexao())
                {
                    setMensagemErro(conexao.mErro);
                    return modUsuario;
                }

                if (tran == null)
                {
                    tran = conexao.conn.BeginTransaction();
                }
            }

            try
            {
                string query = "UPDATE Usuario2 SET ds_Nome = @ds_Nome, ds_Senha = @ds_Senha, dt_Inclusao = @dt_Inclusao, ds_Tipo = @ds_Tipo, nm_Salario = @nm_Salario, id_IdiomaPreferido = @id_IdiomaPreferido WHERE id_Usuario = @id_Usuario;";

                SqlCommand cmd = new SqlCommand(query, conexao.conn);
                if (tran != null) cmd.Transaction = tran;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id_Usuario", modUsuario.id_Usuario);
                cmd.Parameters.AddWithValue("@ds_Nome", modUsuario.ds_Nome);
                cmd.Parameters.AddWithValue("@ds_Senha", modUsuario.ds_Senha);
                cmd.Parameters.AddWithValue("@dt_Inclusao", modUsuario.dt_Inclusao);
                cmd.Parameters.AddWithValue("@ds_Tipo", modUsuario.ds_Tipo);
                cmd.Parameters.AddWithValue("@nm_Salario", modUsuario.nm_Salario);
                cmd.Parameters.AddWithValue("@id_IdiomaPreferido", modUsuario.id_IdiomaPreferido);

                cmd.ExecuteNonQuery();

                if ((ConexaoExterna == false) && (tran != null))
                {
                    tran.Commit();
                }

            }
            catch (Exception e)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }

                // Trata os erros de nossa conex�o
                setMensagemErro(e.Message.ToString());
            }

            if (ConexaoExterna == false)
            {
                conexao.CloseConexao();
            }

            return modUsuario;
        }

        /// <summary>
        /// ```simplex
        /// void DalUsuarioExcluir (id_Usuario) {
        ///     DELETE FROM Usuario2 WHERE id_Usuario = @id_Usuario;
        /// }
        /// ```
        /// </summary>
        /// <param name="id_Usuario"></param>
        /// <param name="conexao"></param>
        /// <param name="tran"></param> 
        public void DalUsuarioExcluir(int id_Usuario, Conexao conexao = null, SqlTransaction tran = null)
        {
            bool ConexaoExterna = (conexao == null ? false : true);

            if (ConexaoExterna == false)
            {
                conexao = new Conexao(TipoConexao.Conexao.WebConfig);
                if (conexao.ExisteErro()) return;
                if (!conexao.OpenConexao())
                {
                    setMensagemErro(conexao.mErro);
                    return;
                }

                if (tran == null)
                {
                    tran = conexao.conn.BeginTransaction();
                }
            }

            try
            {
                string query = "DELETE FROM Usuario2 WHERE id_Usuario = @id_Usuario;";

                SqlCommand cmd = new SqlCommand(query, conexao.conn);
                if (tran != null) cmd.Transaction = tran;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id_Usuario", id_Usuario);

                cmd.ExecuteNonQuery();

                if ((ConexaoExterna == false) && (tran != null))
                {
                    tran.Commit();
                }
            }
            catch (Exception e)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }

                // Trata os erros de nossa conex�o
                setMensagemErro(e.Message.ToString());
            }

            if (ConexaoExterna == false)
            {
                conexao.CloseConexao();
            }
        }

        /// <summary>
        /// ```simplex
        /// List<ModUsuariosListar> ListarTodosUsuarios () {
        ///     List<ModUsuariosListar> lstUsuariosListar = SELECT * FROM Usuario2;
        ///     return lstUsuariosListar;
        /// }
        /// ```
        /// </summary>
        /// <param name="conexao"></param>
        /// <param name="tran"></param>
        /// <returns></returns> 
        public List<ModUsuariosListar> ListarTodosUsuarios (Conexao conexao = null, SqlTransaction tran = null)
        {
            List<ModUsuariosListar> _return = new List<ModUsuariosListar>();

            bool ConexaoExterna = (conexao == null ? false : true);

            if (ConexaoExterna == false)
            {
                conexao = new Conexao(TipoConexao.Conexao.WebConfig);
                if (conexao.ExisteErro()) return _return;
                if (!conexao.OpenConexao())
                {
                    setMensagemErro(conexao.mErro);
                    return _return;
                }

                if (tran == null)
                {
                    tran = conexao.conn.BeginTransaction();
                }
            }

            try
            {
                SqlDataReader reader;
                string query =
                    @"SELECT 
                            US2.id_Usuario, 
                            US2.ds_Nome, 
                            US2.ds_Senha, 
                            US2.dt_Inclusao, 
                            US2.ds_Tipo, 
                            US2.nm_Salario, 
                            US2.id_IdiomaPreferido, 
                            IDI.ds_Idioma
                        FROM Usuario2 US2 
                        INNER JOIN Idioma IDI on US2.id_IdiomaPreferido = IDI.id_Idioma
                     ";

                SqlCommand cmd = new SqlCommand(query, conexao.conn);
                if (tran != null) cmd.Transaction = tran;
                cmd.CommandType = System.Data.CommandType.Text;

                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _return.Add(ModUsuariosListar.Read(reader));
                    }
                    reader.Close();
                }

                if ((ConexaoExterna == false) && (tran != null))
                {
                    tran.Commit();
                }
            }
            catch (Exception e)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }

                // Trata os erros de nossa conex�o
                setMensagemErro(e.Message.ToString());
            }

            if (ConexaoExterna == false)
            {
                conexao.CloseConexao();
            }

            return _return;
        }

    }
}