using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Beneficiarios
{
    public class Beneficiario
    {
        public string CuentaBeneficiario { get; set; } = string.Empty;
        public bool CuentaBgr { get; set; }
        public string Alias { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string TipoDocumento { get; set; }
        public string Banco { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string TipoCuenta { get; set; }
    }
}
