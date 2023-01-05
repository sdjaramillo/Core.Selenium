using Core.Models.Entidad;
using Core.Models.Entidad.RecuperarContrasena;
using Core.Models.Interface;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using RazorEngine.Compilation.ImpromptuInterface;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core.LogicaScripts.RecuperarContrasena
{
    public class RecuperarContrasenaBLL
    {
        private IWebDriver _driver;
        public ScriptBase SetConfig<T>(ScriptBase script, IWebDriver driver)
        {
            _driver = driver;            
            var logicaInyectada = script.GetType().Name;
            script.URL = "https://bgrdigital-test.bgr.com.ec/Cuenta/Usuario";
            script.Reporte = new AutoReport(script.GetType().ToString().Split('.').Last());
            script.Driver = driver;
            script.Driver.Url = script.URL;
            script.Driver.Navigate();
            script.Variables = script.Variables ?? new Dictionary<string, string>();
            script.SeleniumJS = (IJavaScriptExecutor)driver;

            using (StreamReader sr = new StreamReader($"scripts/{logicaInyectada}/{logicaInyectada}.json"))
            {
                JObject json = JObject.Parse(sr.ReadToEnd());
                script.TestData = json.SelectToken("data").ToObject<List<T>>();
                //script.Variables = json.SelectToken("variables").ToObject<Dictionary<string, string>>();
                //script.Resultados = json.SelectToken("resultados").ToObject<Dictionary<string, string>>();
            }
            script.Test = script.Reporte.CrearTest(script.GetType().ToString()).AssignDevice(driver.GetType().ToString().Split('.').Last());
            return script;
        }

        public void SeleccionarTipoDocumento(string tipoDocumento)
        {
            const string botonCedula = "btnCedula";
            const string botonPasaporte = "btnPasaporte";

            tipoDocumento = tipoDocumento.Equals("cedula") ? botonCedula : botonPasaporte;
            var botonTipoDocumento = _driver.FindElement(By.Id(tipoDocumento));
            botonTipoDocumento.Click();
        }

        public void IngresarNumeroDocumento(string identificacion)
        {
            const string identificacionPersona = "IdentificacionPersona";
            var inputIdentificacon = _driver.FindElement(By.Name(identificacionPersona));
            inputIdentificacon.SendKeys(identificacion);
        }
    }
}
