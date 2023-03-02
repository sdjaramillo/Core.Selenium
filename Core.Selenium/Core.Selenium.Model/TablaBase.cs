using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Selenium.Model
{
    public class TablaBase
    {
        public string Identificador { get; set; } = string.Empty;
        public List<string> Columnas { get; set; } = new List<string>();
        public List<Dictionary<string, string>> Filas { get; set; } = new List<Dictionary<string, string>>();
        public int TotalColumnas
        {
            get
            {
                return Columnas.Count;
            }
        }

        public int TotalFilas
        {
            get
            {
                return Filas.Count;
            }
        }
    }
}

