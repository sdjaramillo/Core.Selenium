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
        public List<Beneficiario> Beneficiarios { get; set; }
    }
}
