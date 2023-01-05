using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Interface
{
    public interface IScript
    {
        /// <summary>
        /// Metodo para configuración de scripts
        /// </summary>
        /// <param name="driver"></param>
        void SetConfig(IWebDriver driver);

        /// <summary>
        /// Metodo para ejecutar script
        /// </summary>
        void Execute();

        /// <summary>
        /// Metodo cuando finaliza script
        /// </summary>
        void Finalizar();

        /// <summary>
        /// Metodo cuando ocurre un error
        /// </summary>
        void Error(Exception ex);
    }
}
