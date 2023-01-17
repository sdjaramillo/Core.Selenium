using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Entidad
{
    public class ScriptParametroBase
    {
        List<string> ResultadosEsperados { get; set; } = new List<string>();
        public string CodigoTemporal { get; set; } = "123456";
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
    }
}
