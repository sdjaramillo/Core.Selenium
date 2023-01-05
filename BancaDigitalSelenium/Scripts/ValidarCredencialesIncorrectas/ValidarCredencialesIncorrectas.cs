using BancaDigitalSelenium.Helpers;
using BancaDigitalSelenium.Interface;
using BancaDigitalSelenium.Scripts.Shared;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaDigitalSelenium.Scripts
{
    internal class ValidarCredencialesIncorrectas : ScriptBase, IScript
    {
        private List<ValidarCredencialesIncorrectasDATA> TestData { get; set; }


        /// <summary>
        /// Metodo para ejecutar el script
        /// </summary>
        public void Execute()
        {
            foreach (var data in TestData)
            {
                List<string> resultadosPrueba = new List<string>();
                this.SeleniumDriver.Url = this.URL;
                SeleniumDriver.Navigate();
                SeleniumDriver.Navigate().Refresh();
                SeleniumDriver.Login(data.Usuario, data.Clave);

                if (ValidarMensajeCuentaDeshabilitada())
                {
                    resultadosPrueba.Add(ObtenerMensajeCuentaDeshabilitada());
                }

                if (ValidarMensajeUsuarioBloqueado())
                {
                    resultadosPrueba.Add(ObtenerMensajeUsuarioBloqueado());
                }

                if (ValidarMensajesCredencialesIncorrectas())
                {
                    resultadosPrueba.Add(ObtenerMensajesValidacionLogin());
                }
                ValidarResultadoPrueba(resultadosPrueba);
            }
        }

        private void ValidarResultadoPrueba(List<string> resultadosPrueba)
        {
            var resultados = resultadosPrueba.Where(w => Resultados.Any(a => a.Value == w));
            string resultado = resultados.Count() > 0 ? "Prueba exitosa" : "Prueba Fallida";

            Console.WriteLine(resultado);
        }

        /// <summary>
        /// Metodo donde se personaliza la configuración del script
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="vars"></param>
        public void SetConfig(IWebDriver driver)
        {
            this.URL = "https://bgrdigital-test.bgr.com.ec/Cuenta/Login";
            this.SeleniumDriver = driver;
            this.SeleniumDriver.Url = URL;
            this.SeleniumDriver.Navigate();

            this.Variables = Variables ?? new Dictionary<string, string>();
            this.SeleniumJS = (IJavaScriptExecutor)driver;

            using (StreamReader sr = new StreamReader("scripts/ValidarCredencialesIncorrectas/ValidarCredencialesIncorrectas.json"))
            {
                JObject json = JObject.Parse(sr.ReadToEnd());
                TestData = json.SelectToken("data").ToObject<List<ValidarCredencialesIncorrectasDATA>>();
                Variables = json.SelectToken("variables").ToObject<Dictionary<string, string>>();
                Resultados = json.SelectToken("resultados").ToObject<Dictionary<string, string>>();
            }
        }

        /// <summary>
        /// Valida si se muestra el mensaje de error
        /// </summary>
        /// <returns></returns>
        public bool ValidarMensajesCredencialesIncorrectas()
        {
            const string credencialesNoCoinciden = "Tus-credenciales-no-coinciden-Por-favor-vuelve-a-intentar-nuevamente";

            if (SeleniumDriver.CheckIfElementExists(By.ClassName(credencialesNoCoinciden)))
            {
                var mensajeError = SeleniumDriver.FindElements(By.ClassName(credencialesNoCoinciden));
                return mensajeError.Where(w => w.Displayed && w.Enabled).Count() > 0;
            }
            return false;
        }

        private bool ValidarMensajeCuentaDeshabilitada()
        {
            const string cuentaBloqueada = "Tu-cuenta-ha-sido-temporalmente-deshabilitada-por-favor-cambia-tu-contrasea-para-poder-desbloquear";
            if (SeleniumDriver.CheckIfElementExists(By.ClassName(cuentaBloqueada)))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Valida si se muestra el mensaje de que la contraseña sera bloqueada en el siguiente intento
        /// </summary>
        /// <returns></returns>
        private bool ValidarMensajeUsuarioBloqueado()
        {
            const string usuarioBloqueado = "Tu-usuario-ser-bloqueado-al-siguiente-intento-fallido-para-proteger-tu-cuenta";

            if (SeleniumDriver.CheckIfElementExists(By.ClassName(usuarioBloqueado)))
            {
                var mensajeBloqueo = SeleniumDriver.FindElements(By.ClassName(usuarioBloqueado));
                return mensajeBloqueo.Where(w => w.Displayed && w.Enabled).Count() > 0;
            }
            return false;
        }

        /// <summary>
        /// ObtieneMensaje en caso de que se muestre
        /// </summary>
        /// <returns></returns>
        public string ObtenerMensajesValidacionLogin()
        {
            var item = SeleniumDriver.FindElement(By.Id("IdFallido1")).GetInnerText();
            return item;
        }

        private string ObtenerMensajeUsuarioBloqueado()
        {
            const string credencialesNoCoinciden = "Tu-usuario-ser-bloqueado-al-siguiente-intento-fallido-para-proteger-tu-cuenta";
            var item = SeleniumDriver.FindElement(By.ClassName(credencialesNoCoinciden)).GetInnerText();
            return item;
        }

        private string ObtenerMensajeCuentaDeshabilitada()
        {
            const string usuarioBloqueado = "Tu-cuenta-ha-sido-temporalmente-deshabilitada-por-favor-cambia-tu-contrasea-para-poder-desbloquear";
            var item = SeleniumDriver.FindElement(By.ClassName(usuarioBloqueado)).GetInnerText();
            return item;
        }
    }

    internal class ValidarCredencialesIncorrectasDATA
    {
        public string Usuario { get; set; }
        public string Clave { get; set; }
    }
}
