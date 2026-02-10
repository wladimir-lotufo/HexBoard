using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WarpSolutions.Models;

namespace WarpSolutions.APIs.PrtShipCampanha
{
    public partial class DbPrtShipCampanha : Erros
    {
        /// <summary>
        /// ```simplex
        /// Int DalPrtShipCampanhaIncluir (ds_Campanha, ds_ObservacaoGestao, ds_Cenario, nm_QtdCotas, nm_PercentualDiluir, fl_Status, id_UsuarioEfetivacao, dt_Efetivacao, id_UltimaAtualizacao, dt_UltimaAtualizacao, fl_Inativo, id_TipoCampanha, id_Organizacao, fl_ShowMarketPlace, id_AporteCota, nm_ValorUnitario) {
        ///     INSERT INTO PrtShipCampanha (id_Campanha, ds_Campanha, ds_ObservacaoGestao, ds_Cenario, nm_QtdCotas, nm_PercentualDiluir, fl_Status, id_UsuarioEfetivacao, dt_Efetivacao, id_UltimaAtualizacao, dt_UltimaAtualizacao, fl_Inativo, id_TipoCampanha, id_Organizacao, fl_ShowMarketPlace, id_AporteCota, nm_ValorUnitario) VALUES (@id_Campanha, @ds_Campanha, @ds_ObservacaoGestao, @ds_Cenario, @nm_QtdCotas, @nm_PercentualDiluir, @fl_Status, @id_UsuarioEfetivacao, @dt_Efetivacao, @id_UltimaAtualizacao, @dt_UltimaAtualizacao, @fl_Inativo, @id_TipoCampanha, @id_Organizacao, @fl_ShowMarketPlace, @id_AporteCota, @nm_ValorUnitario);
        ///     return id_Campanha;
        /// }
        /// ```
        /// </summary>
        public int DalPrtShipCampanhaIncluir(string ds_Campanha, string ds_ObservacaoGestao, string ds_Cenario, int nm_QtdCotas, int nm_PercentualDiluir, int fl_Status, int id_UsuarioEfetivacao, DateTime dt_Efetivacao, int id_UltimaAtualizacao, DateTime dt_UltimaAtualizacao, int fl_Inativo, int id_TipoCampanha, int id_Organizacao, int fl_ShowMarketPlace, int id_AporteCota, decimal nm_ValorUnitario, Conexao conexao = null, SqlTransaction tran = null)
        {
            int id_Campanha = 0;
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
                string query = @"INSERT INTO PrtShipCampanha (id_Campanha, ds_Campanha, ds_ObservacaoGestao, ds_Cenario, nm_QtdCotas, nm_PercentualDiluir, fl_Status, id_UsuarioEfetivacao, dt_Efetivacao, id_UltimaAtualizacao, dt_UltimaAtualizacao, fl_Inativo, id_TipoCampanha, id_Organizacao, fl_ShowMarketPlace, id_AporteCota, nm_ValorUnitario) 
                                VALUES (@id_Campanha, @ds_Campanha, @ds_ObservacaoGestao, @ds_Cenario, @nm_QtdCotas, @nm_PercentualDiluir, @fl_Status, @id_UsuarioEfetivacao, @dt_Efetivacao, @id_UltimaAtualizacao, @dt_UltimaAtualizacao, @fl_Inativo, @id_TipoCampanha, @id_Organizacao, @fl_ShowMarketPlace, @id_AporteCota, @nm_ValorUnitario);";

                SqlCommand cmd = new SqlCommand(query, conexao.conn);
                if (tran != null) cmd.Transaction = tran;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id_Campanha", id_Campanha);
                cmd.Parameters.AddWithValue("@ds_Campanha", ds_Campanha ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ds_ObservacaoGestao", ds_ObservacaoGestao ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ds_Cenario", ds_Cenario ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@nm_QtdCotas", nm_QtdCotas);
                cmd.Parameters.AddWithValue("@nm_PercentualDiluir", nm_PercentualDiluir);
                cmd.Parameters.AddWithValue("@fl_Status", fl_Status);
                cmd.Parameters.AddWithValue("@id_UsuarioEfetivacao", id_UsuarioEfetivacao);
                cmd.Parameters.AddWithValue("@dt_Efetivacao", dt_Efetivacao);
                cmd.Parameters.AddWithValue("@id_UltimaAtualizacao", id_UltimaAtualizacao);
                cmd.Parameters.AddWithValue("@dt_UltimaAtualizacao", dt_UltimaAtualizacao);
                cmd.Parameters.AddWithValue("@fl_Inativo", fl_Inativo);
                cmd.Parameters.AddWithValue("@id_TipoCampanha", id_TipoCampanha);
                cmd.Parameters.AddWithValue("@id_Organizacao", id_Organizacao);
                cmd.Parameters.AddWithValue("@fl_ShowMarketPlace", fl_ShowMarketPlace);
                cmd.Parameters.AddWithValue("@nm_ValorUnitario", nm_ValorUnitario);
                cmd.Parameters.AddWithValue("@id_AporteCota", id_AporteCota);

                cmd.ExecuteNonQuery();

                if ((ConexaoExterna == false) && (tran != null))
                {
                    tran.Commit();
                }
                
                return id_Campanha;
            }
            catch (Exception e)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }

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
        /// ModPrtShipCampanha DalPrtShipCampanhaConsultar (id_Campanha) {
        ///     ModPrtShipCampanha modPrtShipCampanha = SELECT * FROM PrtShipCampanha WHERE id_Campanha = @id_Campanha;
        ///     return modPrtShipCampanha;
        /// }
        /// ```
        /// </summary>
        public ModPrtShipCampanha DalPrtShipCampanhaConsultar(int id_Campanha, Conexao conexao = null, SqlTransaction tran = null)
        {
            ModPrtShipCampanha modPrtShipCampanha = new ModPrtShipCampanha();
            bool ConexaoExterna = (conexao == null ? false : true);

            if (ConexaoExterna == false)
            {
                conexao = new Conexao(TipoConexao.Conexao.WebConfig);
                if (conexao.ExisteErro()) return modPrtShipCampanha;
                if (!conexao.OpenConexao())
                {
                    setMensagemErro(conexao.mErro);
                    return modPrtShipCampanha;
                }

                if (tran == null)
                {
                    tran = conexao.conn.BeginTransaction();
                }
            }

            try
            {
                SqlDataReader reader;
                string query = "SELECT * FROM PrtShipCampanha WHERE id_Campanha = @id_Campanha;";

                SqlCommand cmd = new SqlCommand(query, conexao.conn);
                if (tran != null) cmd.Transaction = tran;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id_Campanha", id_Campanha);

                using (reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        modPrtShipCampanha = ModPrtShipCampanha.Read(reader);
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

                setMensagemErro(e.Message.ToString());
            }

            if (ConexaoExterna == false)
            {
                conexao.CloseConexao();
            }

            return modPrtShipCampanha;
        }

        /// <summary>
        /// ```simplex
        /// void DalPrtShipCampanhaAlterar (id_Campanha, ds_Campanha, ds_ObservacaoGestao, ds_Cenario, nm_QtdCotas, nm_PercentualDiluir, fl_Status, id_UsuarioEfetivacao, dt_Efetivacao, id_UltimaAtualizacao, dt_UltimaAtualizacao, fl_Inativo, id_TipoCampanha, id_Organizacao, fl_ShowMarketPlace, id_AporteCota, nm_ValorUnitario) {
        ///     UPDATE PrtShipCampanha SET ds_Campanha = @ds_Campanha, ds_ObservacaoGestao = @ds_ObservacaoGestao, ds_Cenario = @ds_Cenario, nm_QtdCotas = @nm_QtdCotas, nm_PercentualDiluir = @nm_PercentualDiluir, fl_Status = @fl_Status, id_UsuarioEfetivacao = @id_UsuarioEfetivacao, dt_Efetivacao = @dt_Efetivacao, id_UltimaAtualizacao = @id_UltimaAtualizacao, dt_UltimaAtualizacao = @dt_UltimaAtualizacao, fl_Inativo = @fl_Inativo, id_TipoCampanha = @id_TipoCampanha, id_Organizacao = @id_Organizacao, fl_ShowMarketPlace = @fl_ShowMarketPlace, id_AporteCota = @id_AporteCota, nm_ValorUnitario = @nm_ValorUnitario WHERE id_Campanha = @id_Campanha;
        /// }
        /// ```
        /// </summary>
        public void DalPrtShipCampanhaAlterar(int id_Campanha, string ds_Campanha, string ds_ObservacaoGestao, string ds_Cenario, int nm_QtdCotas, int nm_PercentualDiluir, int fl_Status, int id_UsuarioEfetivacao, DateTime dt_Efetivacao, int id_UltimaAtualizacao, DateTime dt_UltimaAtualizacao, int fl_Inativo, int id_TipoCampanha, int id_Organizacao, int fl_ShowMarketPlace, int id_AporteCota, decimal nm_ValorUnitario, Conexao conexao = null, SqlTransaction tran = null)
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
                string query = @"UPDATE PrtShipCampanha SET 
                                ds_Campanha = @ds_Campanha, 
                                ds_ObservacaoGestao = @ds_ObservacaoGestao, 
                                ds_Cenario = @ds_Cenario, 
                                nm_QtdCotas = @nm_QtdCotas, 
                                nm_PercentualDiluir = @nm_PercentualDiluir, 
                                fl_Status = @fl_Status, 
                                id_UsuarioEfetivacao = @id_UsuarioEfetivacao, 
                                dt_Efetivacao = @dt_Efetivacao, 
                                id_UltimaAtualizacao = @id_UltimaAtualizacao, 
                                dt_UltimaAtualizacao = @dt_UltimaAtualizacao, 
                                fl_Inativo = @fl_Inativo, 
                                id_TipoCampanha = @id_TipoCampanha, 
                                id_Organizacao = @id_Organizacao, 
                                fl_ShowMarketPlace = @fl_ShowMarketPlace, 
                                id_AporteCota = @id_AporteCota, 
                                nm_ValorUnitario = @nm_ValorUnitario 
                                WHERE id_Campanha = @id_Campanha;";

                SqlCommand cmd = new SqlCommand(query, conexao.conn);
                if (tran != null) cmd.Transaction = tran;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id_Campanha", id_Campanha);
                cmd.Parameters.AddWithValue("@ds_Campanha", ds_Campanha ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ds_ObservacaoGestao", ds_ObservacaoGestao ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ds_Cenario", ds_Cenario ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@nm_QtdCotas", nm_QtdCotas);
                cmd.Parameters.AddWithValue("@nm_PercentualDiluir", nm_PercentualDiluir);
                cmd.Parameters.AddWithValue("@fl_Status", fl_Status);
                cmd.Parameters.AddWithValue("@id_UsuarioEfetivacao", id_UsuarioEfetivacao);
                cmd.Parameters.AddWithValue("@dt_Efetivacao", dt_Efetivacao);
                cmd.Parameters.AddWithValue("@id_UltimaAtualizacao", id_UltimaAtualizacao);
                cmd.Parameters.AddWithValue("@dt_UltimaAtualizacao", dt_UltimaAtualizacao);
                cmd.Parameters.AddWithValue("@fl_Inativo", fl_Inativo);
                cmd.Parameters.AddWithValue("@id_TipoCampanha", id_TipoCampanha);
                cmd.Parameters.AddWithValue("@id_Organizacao", id_Organizacao);
                cmd.Parameters.AddWithValue("@fl_ShowMarketPlace", fl_ShowMarketPlace);
                cmd.Parameters.AddWithValue("@id_AporteCota", id_AporteCota);
                cmd.Parameters.AddWithValue("@nm_ValorUnitario", nm_ValorUnitario);

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

                setMensagemErro(e.Message.ToString());
            }

            if (ConexaoExterna == false)
            {
                conexao.CloseConexao();
            }
        }

        /// <summary>
        /// ```simplex
        /// void DalPrtShipCampanhaExcluir (id_Campanha) {
        ///     DELETE FROM PrtShipCampanha WHERE id_Campanha = @id_Campanha;
        /// }
        /// ```
        /// </summary>
        public void DalPrtShipCampanhaExcluir(int id_Campanha, Conexao conexao = null, SqlTransaction tran = null)
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
                string query = "DELETE FROM PrtShipCampanha WHERE id_Campanha = @id_Campanha;";

                SqlCommand cmd = new SqlCommand(query, conexao.conn);
                if (tran != null) cmd.Transaction = tran;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id_Campanha", id_Campanha);

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

                setMensagemErro(e.Message.ToString());
            }

            if (ConexaoExterna == false)
            {
                conexao.CloseConexao();
            }
        }

        /// <summary>
        /// ```simplex
        /// List<ModPrtShipCampanha> DalPrtShipCampanhaListar () {
        ///     List<ModPrtShipCampanha> modPrtShipCampanhaList = SELECT * FROM PrtShipCampanha;
        ///     return modPrtShipCampanhaList;
        /// }
        /// ```
        /// </summary>
        public List<ModPrtShipCampanha> DalPrtShipCampanhaListar(Conexao conexao = null, SqlTransaction tran = null)
        {
            List<ModPrtShipCampanha> modPrtShipCampanhaList = new List<ModPrtShipCampanha>();
            bool ConexaoExterna = (conexao == null ? false : true);

            if (ConexaoExterna == false)
            {
                conexao = new Conexao(TipoConexao.Conexao.WebConfig);
                if (conexao.ExisteErro()) return modPrtShipCampanhaList;
                if (!conexao.OpenConexao())
                {
                    setMensagemErro(conexao.mErro);
                    return modPrtShipCampanhaList;
                }

                if (tran == null)
                {
                    tran = conexao.conn.BeginTransaction();
                }
            }

            try
            {
                SqlDataReader reader;
                string query = "SELECT * FROM PrtShipCampanha;";

                SqlCommand cmd = new SqlCommand(query, conexao.conn);
                if (tran != null) cmd.Transaction = tran;
                cmd.CommandType = System.Data.CommandType.Text;

                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        modPrtShipCampanhaList.Add(ModPrtShipCampanha.Read(reader));
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

                setMensagemErro(e.Message.ToString());
            }

            if (ConexaoExterna == false)
            {
                conexao.CloseConexao();
            }

            return modPrtShipCampanhaList;
        }
    }
}
