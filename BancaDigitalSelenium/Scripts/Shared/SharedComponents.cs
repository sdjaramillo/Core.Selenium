using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BancaDigitalSelenium.Scripts.Shared
{
    public static class SharedComponents
    {
        /// <summary>
        /// Metodo para reutilizar login,
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="login">se encuentra con el id "NombreUsuario"</param>
        /// <param name="password">se encuentra con el id "Clave"</param>
        public static void Login(this IWebDriver driver, string nombreUsuario, string clave)
        {
            var campoNombreUsuario = driver.FindElement(By.Id("NombreUsuario"));
            campoNombreUsuario.SendKeys(nombreUsuario);

            var campoClave = driver.FindElement(By.Id("Clave"));
            campoClave.SendKeys(clave);

            var botonLogin = driver.FindElement(By.Id("IdBotonIngresarBanca"));
            botonLogin.Click();
        }       
    }
}
