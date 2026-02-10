using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WarpSolutions.Models;

namespace WarpSolutions.APIs.Usuario
{
    public partial class DbIdioma : Erros
    {
        /// <summary>
        /// ```simplex
        /// int DalIdiomaIncluir (ds_Idioma) {
        ///     INSERT INTO Idioma (ds_Idioma) VALUES (@ds_Idioma); 
        ///     id_Idioma = SELECT @@IDENTITY as NewId;
        ///     return id_Idioma;
        /// }
        /// ```
        /// </summary>
        /// <param name="ds_Idioma"></param>
        /// <param name="conexao"></param>
        /// <param name="tran"></param>
        /// <returns></returns> 
        public int DalIdiomaIncluir(string ds_Idioma, Conexao conexao = null, SqlTransaction tran = null)
        {
            bool ConexaoExterna = (conexao == null ? false : true);

            if (ConexaoExterna == false)
            {
                conexao = new Conexao(TipoConexao.Conexao.WebConfig);
                if (conexao.ExisteErro()) return 0;
                if (!conexao.OpenConexao())
                {
                    setMensagemErro(conexao.mErro);
                    return 0;
                }

                if (tran == null)
                {
                    tran = conexao.conn.BeginTransaction();
                }
            }

            try
            {
                SqlDataReader reader;
                string query = @"INSERT INTO Idioma (ds_Idioma) 
                                VALUES (@ds_Idioma); 
                                SELECT @@IDENTITY as NewId;";

                SqlCommand cmd = new SqlCommand(query, conexao.conn);
                if (tran != null) cmd.Transaction = tran;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ds_Idioma", ds_Idioma);

                using (reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Convert.ToInt32(reader["NewId"]);
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

            return 0;
        }

        /// <summary>
        /// ```simplex
        /// ModIdioma DalIdiomaConsultar (id_Idioma) {
        ///     ModIdioma modIdioma = SELECT * FROM Idioma WHERE id_Idioma = @id_Idioma;
        ///     return modIdioma;
        /// }
        /// ```
        /// </summary>
        /// <param name="id_Idioma"></param>
        /// <param name="conexao"></param>
        /// <param name="tran"></param>
        /// <returns></returns> 
        public ModIdioma DalIdiomaConsultar(int id_Idioma, Conexao conexao = null, SqlTransaction tran = null)
        {
            ModIdioma modIdioma = new ModIdioma();

            bool ConexaoExterna = (conexao == null ? false : true);

            if (ConexaoExterna == false)
            {
                conexao = new Conexao(TipoConexao.Conexao.WebConfig);
                if (conexao.ExisteErro()) return modIdioma;
                if (!conexao.OpenConexao())
                {
                    setMensagemErro(conexao.mErro);
                    return modIdioma;
                }

                if (tran == null)
                {
                    tran = conexao.conn.BeginTransaction();
                }
            }

            try
            {
                SqlDataReader reader;
                string query = "SELECT * FROM Idioma WHERE id_Idioma = @id_Idioma;";

                SqlCommand cmd = new SqlCommand(query, conexao.conn);
                if (tran != null) cmd.Transaction = tran;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id_Idioma", id_Idioma);

                using (reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        modIdioma = ModIdioma.Read(reader);
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

            return modIdioma;
        }

        /// <summary>
        /// ```simplex
        /// void DalIdiomaAlterar (id_Idioma, ds_Idioma) {
        ///     UPDATE Idioma SET ds_Idioma = @ds_Idioma WHERE id_Idioma = @id_Idioma;
        /// }
        /// ```
        /// </summary>
        /// <param name="id_Idioma"></param>
        /// <param name="ds_Idioma"></param>
        /// <param name="conexao"></param>
        /// <param name="tran"></param> 
        public void DalIdiomaAlterar(int id_Idioma, string ds_Idioma, Conexao conexao = null, SqlTransaction tran = null)
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
                string query = "UPDATE Idioma SET ds_Idioma = @ds_Idioma WHERE id_Idioma = @id_Idioma;";

                SqlCommand cmd = new SqlCommand(query, conexao.conn);
                if (tran != null) cmd.Transaction = tran;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id_Idioma", id_Idioma);
                cmd.Parameters.AddWithValue("@ds_Idioma", ds_Idioma);

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
        /// void DalIdiomaExcluir (id_Idioma) {
        ///     DELETE FROM Idioma WHERE id_Idioma = @id_Idioma;
        /// }
        /// ```
        /// </summary>
        /// <param name="id_Idioma"></param>
        /// <param name="conexao"></param>
        /// <param name="tran"></param>
        public void DalIdiomaExcluir(int id_Idioma, Conexao conexao = null, SqlTransaction tran = null)
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
                string query = "DELETE FROM Idioma WHERE id_Idioma = @id_Idioma;";

                SqlCommand cmd = new SqlCommand(query, conexao.conn);
                if (tran != null) cmd.Transaction = tran;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id_Idioma", id_Idioma);

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
        /// List<ModIdioma> DalIdiomaListar () {
        ///     List<ModIdioma> modIdiomaList = SELECT * FROM Idioma;
        ///     return modIdiomaList;
        /// }
        /// ```
        /// </summary>
        /// <param name="conexao"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public List<ModIdioma> DalIdiomaListar(Conexao conexao = null, SqlTransaction tran = null)
        {
            List<ModIdioma> modIdiomaList = new List<ModIdioma>();

            bool ConexaoExterna = (conexao == null ? false : true);

            if (ConexaoExterna == false)
            {
                conexao = new Conexao(TipoConexao.Conexao.WebConfig);
                if (conexao.ExisteErro()) return modIdiomaList;
                if (!conexao.OpenConexao())
                {
                    setMensagemErro(conexao.mErro);
                    return modIdiomaList;
                }

                if (tran == null)
                {
                    tran = conexao.conn.BeginTransaction();
                }
            }

            try
            {
                SqlDataReader reader;
                string query = "SELECT * FROM Idioma;";

                SqlCommand cmd = new SqlCommand(query, conexao.conn);
                if (tran != null) cmd.Transaction = tran;
                cmd.CommandType = System.Data.CommandType.Text;

                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        modIdiomaList.Add(ModIdioma.Read(reader));
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

                // Trata os erros de nossa conexão
                setMensagemErro(e.Message.ToString());
            }

            if (ConexaoExterna == false)
            {
                conexao.CloseConexao();
            }

            return modIdiomaList;
        }


    }
}