using System;
using System.Data.SqlClient;

namespace WarpSolutions.Models
{
    public class ModPrtShipCampanha
    {
        public int id_Campanha { get; set; }
        public DateTime dt_Criacao { get; set; }
        public string ds_Campanha { get; set; }
        public string ds_ObservacaoGestao { get; set; }
        public string ds_Cenario { get; set; }
        public int nm_QtdCotas { get; set; }
        public int nm_PercentualDiluir { get; set; }
        public int fl_Status { get; set; }
        public int id_UsuarioEfetivacao { get; set; }
        public DateTime dt_Efetivacao { get; set; }
        public int id_UltimaAtualizacao { get; set; }
        public DateTime dt_UltimaAtualizacao { get; set; }
        public int fl_Inativo { get; set; }
        public int id_TipoCampanha { get; set; }
        public int id_Organizacao { get; set; }
        public int fl_ShowMarketPlace { get; set; }
        public int id_AporteCota { get; set; }
        public decimal nm_ValorUnitario { get; set; }

        public static ModPrtShipCampanha Read(SqlDataReader reader)
        {
            ModPrtShipCampanha model = new ModPrtShipCampanha();
            if (reader["id_Campanha"] != DBNull.Value) model.id_Campanha = Convert.ToInt32(reader["id_Campanha"]);
            if (reader["dt_Criacao"] != DBNull.Value) model.dt_Criacao = Convert.ToDateTime(reader["dt_Criacao"]);
            if (reader["ds_Campanha"] != DBNull.Value) model.ds_Campanha = reader["ds_Campanha"].ToString();
            if (reader["ds_ObservacaoGestao"] != DBNull.Value) model.ds_ObservacaoGestao = reader["ds_ObservacaoGestao"].ToString();
            if (reader["ds_Cenario"] != DBNull.Value) model.ds_Cenario = reader["ds_Cenario"].ToString();
            if (reader["nm_QtdCotas"] != DBNull.Value) model.nm_QtdCotas = Convert.ToInt32(reader["nm_QtdCotas"]);
            if (reader["nm_PercentualDiluir"] != DBNull.Value) model.nm_PercentualDiluir = Convert.ToInt32(reader["nm_PercentualDiluir"]);
            if (reader["fl_Status"] != DBNull.Value) model.fl_Status = Convert.ToInt32(reader["fl_Status"]);
            if (reader["id_UsuarioEfetivacao"] != DBNull.Value) model.id_UsuarioEfetivacao = Convert.ToInt32(reader["id_UsuarioEfetivacao"]);
            if (reader["dt_Efetivacao"] != DBNull.Value) model.dt_Efetivacao = Convert.ToDateTime(reader["dt_Efetivacao"]);
            if (reader["id_UltimaAtualizacao"] != DBNull.Value) model.id_UltimaAtualizacao = Convert.ToInt32(reader["id_UltimaAtualizacao"]);
            if (reader["dt_UltimaAtualizacao"] != DBNull.Value) model.dt_UltimaAtualizacao = Convert.ToDateTime(reader["dt_UltimaAtualizacao"]);
            if (reader["fl_Inativo"] != DBNull.Value) model.fl_Inativo = Convert.ToInt32(reader["fl_Inativo"]);
            if (reader["id_TipoCampanha"] != DBNull.Value) model.id_TipoCampanha = Convert.ToInt32(reader["id_TipoCampanha"]);
            if (reader["id_Organizacao"] != DBNull.Value) model.id_Organizacao = Convert.ToInt32(reader["id_Organizacao"]);
            if (reader["fl_ShowMarketPlace"] != DBNull.Value) model.fl_ShowMarketPlace = Convert.ToInt32(reader["fl_ShowMarketPlace"]);
            if (reader["id_AporteCota"] != DBNull.Value) model.id_AporteCota = Convert.ToInt32(reader["id_AporteCota"]);
            if (reader["nm_ValorUnitario"] != DBNull.Value) model.nm_ValorUnitario = Convert.ToDecimal(reader["nm_ValorUnitario"]);
            return model;
        }
    }
}
