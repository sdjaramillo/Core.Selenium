using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Selenium.Model
{
    public class ScriptBase
    {
        /// <summary>
        /// Nombre de la prueba 
        /// </summary>
        public string Nombre { get; set; }         
        
        /// <summary>
        /// Comandos de la prueba
        /// </summary>
        public List<Comando> Comandos { get; set; }

        /// <summary>
        /// Datos de ejecución del script
        /// </summary>
        public ParametroScript ScriptData { get; set; }        
    }
}
