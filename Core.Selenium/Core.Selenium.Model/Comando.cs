using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Core.Selenium.Model
{
    public class Comando
    {
        public string Id { get; set; }
        public int Orden { get; set; }
        public string Command { get; set; }
        public string Comment { get; set; }
        public string Target { get; set; }
        public string Value { get; set; }
        //public List<Listring> Targets { get; set; }
        public string? Tipo { get; set; }
        public bool InicioSesion { get; set; }
        public bool FinSesion { get; set; }
        public bool Iterar { get; set; } = true;
    }
}
