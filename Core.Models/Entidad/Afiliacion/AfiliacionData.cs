using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Entidad
{
    public class AfiliacionData : ScriptParametroBase
    {
        public string NumeroCedula { get; set; } =string.Empty; 
        public string Pin { get; set; } = string.Empty;
    }
}
