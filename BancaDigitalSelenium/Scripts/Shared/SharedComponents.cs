using Core.Models.Entidad;
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
        public static void Login(this ScriptBase script, string nombreUsuario, string clave)
        {
            var campoNombreUsuario = script.Driver.FindElement(By.Id("NombreUsuario"));
            campoNombreUsuario.SendKeys(nombreUsuario);

            var campoClave = script.Driver.FindElement(By.Id("Clave"));
            campoClave.SendKeys(clave);

            var botonLogin = script.Driver.FindElement(By.Id("IdBotonIngresarBanca"));
            botonLogin.Click();
        }

        public static void IngresarCodigoTemporal(this ScriptBase script, string codigo)
        {
            List<string> pin = new List<string> { "Pin1", "Pin2", "Pin3", "Pin4", "Pin5", "Pin6" };
            foreach (var p in pin)
            {
                var pinInput = script.Driver.FindElement(By.Id(p));
                pinInput.SendKeys(codigo[pin.IndexOf(p)].ToString());
            }

        }

        public static void SeleccionarOpcionMenu(this ScriptBase script,By by)
        {
            var opcMenu = script.Driver.FindElement(by);
            opcMenu.Click();
        }
    }
}
