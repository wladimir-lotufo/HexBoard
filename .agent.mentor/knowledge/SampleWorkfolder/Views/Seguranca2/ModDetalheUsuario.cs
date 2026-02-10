/*
DetalheUsuario:Model
{
    OB     ModUsuario modUsuario;
    OB     List<ModIdioma> lstListIdiomas;
}
*/

using System.Collections.Generic;
using WarpSolutions.Models;

namespace WarpSolutions.Views.Seguranca2
{
    public class ModDetalheUsuario
    {
        public ModUsuario2 modUsuario { get; set; }
        public List<ModIdioma> lstListIdiomas { get; set; }
    }
}