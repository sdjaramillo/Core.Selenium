using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Selenium.Model.Exepciones
{
    /// <summary>
    /// Excepción personalizada para manejar condicones fallidas
    /// </summary>
    public class CondicionException : Exception
    {
        public CondicionException(string mensajeError) : base(mensajeError)
        {
        }
    }

    /// <summary>
    /// Excepción personalizada para terminar ejecución ya sea con error o no.
    /// </summary>
    public class EjecucionTerminadaException : Exception
    {
        public bool TerminarConError { get; private set; }
        public EjecucionTerminadaException(string mensajeError, bool terminarConError) : base(mensajeError)
        {
            TerminarConError = terminarConError;
        }
    }

}
