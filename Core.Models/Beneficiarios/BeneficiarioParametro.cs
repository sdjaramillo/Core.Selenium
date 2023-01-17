using Core.Models.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Beneficiarios
{
    public class BeneficiarioParametro: ScriptParametroBase
    {
        public string NumeroCuenta { get; set; }=string.Empty;
        public bool CuentaBgr { get; set; }        
    }
}
