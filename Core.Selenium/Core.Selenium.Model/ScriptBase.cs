using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Selenium.Model
{
    public class ScriptBase
    {
        //Nombre de la prueba
        public string Nombre { get; set; }                
        public List<Comando> Comandos { get; set; }
        public ParametroScript ScriptData { get; set; }        
    }
}
