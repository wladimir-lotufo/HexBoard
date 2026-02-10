/*
ModUsuarioConsultar:Model
{
    OB PK  UUID                 id_Usuario      "Código identificador do Usuário"
    OB     String(100)          ds_Nome         "Nome Completo do Usuário"
    OB     String(255)          ds_Senha        "Senha Criptografada do Usuário"
    OB     DateTime(dd/MM/yyyy) dt_Inclusao     "Data de Inclusão do Usuário"
    OB     String(1)            ds_Tipo         "Tipo do Usuário"
    OP     Decimal(10,2)        nm_Salario      "Salário do Usuário"
    OB     Int                  id_TipoUsuario  "Código identificador do Tipo Usuário"
}
*/
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace WarpSolutions.APIs.Usuario
{
    public class ModUsuarioConsultar
    {
        [Key]
        [Display(Name = "Código identificador do Usuário")]
        public int id_Usuario { get; set; }

        [Display(Name = "Nome Completo do Usuário")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome pode ter no máximo 100 caracteres.")]
        public string ds_Nome { get; set; }

        [Display(Name = "Senha Criptografada do Usuário")]
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MaxLength(255, ErrorMessage = "A senha pode ter no máximo 255 caracteres.")]
        public string ds_Senha { get; set; }

        [Display(Name = "Data de Inclusão do Usuário")]
        [Required(ErrorMessage = "A data de inclusão é obrigatória.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dt_Inclusao { get; set; }

        [Display(Name = "Tipo do Usuário")]
        [Required(ErrorMessage = "O tipo do usuário é obrigatório.")]
        [StringLength(1, ErrorMessage = "O tipo deve ter apenas 1 caractere.")]
        public string ds_Tipo { get; set; }

        [Display(Name = "Salário do Usuário")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        [Range(0, 1000000, ErrorMessage = "O salário deve estar entre 0 e 1.000.000,00.")]
        public Decimal? nm_Salario { get; set; }

        [Display(Name = "Código identificador do Tipo Usuário")]
        [Required(ErrorMessage = "O Tipo de Usuário é obrigatório.")]
        public int id_TipoUsuario { get; set; }

        public static ModUsuarioConsultar Read(SqlDataReader reader)
        {
            ModUsuarioConsultar modUsuarioConsultar = new ModUsuarioConsultar();

            modUsuarioConsultar.id_Usuario = ConverteReader.ConverteInt(reader["id_Usuario"]);
            modUsuarioConsultar.ds_Nome = reader["ds_Nome"].ToString() ?? "";
            modUsuarioConsultar.ds_Senha = reader["ds_Senha"].ToString() ?? "";
            modUsuarioConsultar.dt_Inclusao = ConverteReader.ConverteDateTime(reader["dt_Inclusao"]) ?? DateTime.MinValue;
            modUsuarioConsultar.ds_Tipo = reader["ds_Tipo"].ToString() ?? "";
            modUsuarioConsultar.nm_Salario = ConverteReader.ConverteDecimal(reader["nm_Salario"]);
            modUsuarioConsultar.id_TipoUsuario = ConverteReader.ConverteInt(reader["id_TipoUsuario"]);

            return modUsuarioConsultar;
        }

        public void Validar()
        {
            if (id_Usuario == 0)
            {
                throw new Exception("Campo id_Usuario não pode estar vazio.");
            }

            if (string.IsNullOrEmpty(ds_Nome) || ds_Nome.Length > 100)
            {
                throw new Exception("Nome do usuário não pode estar vazio e deve ter no máximo 100 caracteres.");
            }

            if (string.IsNullOrEmpty(ds_Senha) || ds_Senha.Length > 255)
            {
                throw new Exception("Senha não pode estar vazia e deve ter no máximo 255 caracteres.");
            }

            if (dt_Inclusao == default)
            {
                throw new Exception("Data de inclusão é obrigatória.");
            }

            if (string.IsNullOrEmpty(ds_Tipo) || ds_Tipo.Length != 1)
            {
                throw new Exception("Tipo do usuário é obrigatório e deve ter 1 caractere.");
            }

            if (nm_Salario < 0 || nm_Salario > 1000000)
            {
                throw new Exception("Salário deve estar entre 0 e 1.000.000,00.");
            }

            if (id_TipoUsuario == 0)
            {
                throw new Exception("Campo id_TipoUsuario não pode estar vazio.");
            }
        }
    }
}