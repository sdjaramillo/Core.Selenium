using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Entidad.RecuperarContrasena
{
    public class RecuperarContrasenaData : ScriptParametroBase
    {
        public string TipoIdentificacion { get; set; }
        public string NumeroDocumento { get; set; }
        public string Pin { get; set; }
    }
}
