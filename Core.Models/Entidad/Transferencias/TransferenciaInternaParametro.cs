using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Entidad.Transferencias
{
    public class TransferenciaInternaParametro : ScriptParametroBase
    {
        public List<Transferencia> Transferencias { get; set; }
        public string UrlFallido { get; set; }
    }
}
