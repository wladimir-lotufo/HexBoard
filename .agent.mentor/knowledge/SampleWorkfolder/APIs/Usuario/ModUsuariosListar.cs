/*
ModUsuariosListar:Model
{
    OB PK  UUID                 id_Usuario          "Identificador único do Usuário";
    OB     String(20)           ds_Nome             "Nome completo do Usuário" Min:4, Max:20;
    OB     String(255)          ds_Senha            "Senha criptografada do Usuário";
    OB     DateTime(dd/MM/yyyy) dt_Inclusao         "Data de inclusão do Usuário" Min:"01/01/2000", Max:"31/12/2100";
    OB     Enum                 ds_Tipo             "Tipo do Usuário", List: "A"-Administrador, "U"-Usuário;
    OP     Decimal(10,2)        nm_Salario          "Salário do Usuário" Min:0, Max:1.000.000;
    OB     Fk                   id_IdiomaPreferido  "IdiomaPreferido.id_IdiomaPreferido";
    OB     String               ds_Idioma           "Nome do Idioma Preferido";
}
*/

using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace WarpSolutions.APIs.Usuario
{
    public class ModUsuariosListar
    {
        public int id_Usuario { get; set; }

        public string ds_Nome { get; set; }

        [Display(Name = "Senha do Usuário")]
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MaxLength(255, ErrorMessage = "A senha pode ter no máximo 255 caracteres.")]
        public string ds_Senha { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dt_Inclusao { get; set; }

        [Display(Name = "Tipo do Usuário")]
        [Required(ErrorMessage = "O tipo do usuário é obrigatório.")]
        public string ds_Tipo { get; set; }

        [Display(Name = "Salário do Usuário")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        [Range(0, 1000000, ErrorMessage = "O salário deve estar entre 0 e 1.000.000,00.")]
        public decimal? nm_Salario { get; set; }

        public int id_IdiomaPreferido { get; set; }

        public string ds_Idioma { get; set; }

        public static ModUsuariosListar Read(SqlDataReader reader)
        {
            ModUsuariosListar modUsuariosListar = new ModUsuariosListar();

            modUsuariosListar.id_Usuario = ConverteReader.ConverteInt(reader["id_Usuario"]);
            modUsuariosListar.ds_Nome = reader["ds_Nome"].ToString() ?? string.Empty;
            modUsuariosListar.ds_Senha = reader["ds_Senha"].ToString() ?? string.Empty;
            modUsuariosListar.dt_Inclusao = ConverteReader.ConverteDateTime(reader["dt_Inclusao"]) ?? DateTime.MinValue;
            modUsuariosListar.ds_Tipo = reader["ds_Tipo"].ToString() ?? string.Empty;
            modUsuariosListar.nm_Salario = reader["nm_Salario"] != DBNull.Value ? Convert.ToDecimal(reader["nm_Salario"]) : (decimal?)null;
            modUsuariosListar.id_IdiomaPreferido = reader["id_IdiomaPreferido"] != DBNull.Value ? Convert.ToInt32(reader["id_IdiomaPreferido"]) : 0;
            modUsuariosListar.ds_Idioma = reader["ds_Idioma"].ToString() ?? string.Empty;

            return modUsuariosListar;
        }

    }
}