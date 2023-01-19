using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Entidad
{
    public class AfiliacionParametro : ScriptParametroBase
    {
        public string NumeroCedula { get; set; } = string.Empty;
        public string Pin { get; set; } = string.Empty;
        public string TipoIDentificacion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; }
    }
}
