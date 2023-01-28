using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Selenium.Model.Exepciones
{
    public class CondicionException : Exception
    {
        
        public CondicionException(string mensajeError):base(mensajeError) {
            

        }
    }
}
