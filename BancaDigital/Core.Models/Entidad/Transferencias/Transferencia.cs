using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Entidad.Transferencias
{
    public class Transferencia
    {
        public string NumeroCuenta { get; set; }
        public string Monto { get; set; }
        public string Motivo { get; set; }
        public string CuentaOrigen { get; set; }
    }
}
