using Core.Helpers;
using Core.Models.Entidad;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.LogicaScripts.RecuperarContrasena
{
    public class RecuperarContrasenaBLL
    {
        private IWebDriver _driver;
        public ScriptBase SetConfig<T>(ScriptBase script, IWebDriver driver, string source = "")
        {
            _driver = driver;
            var logicaInyectada = script.GetType().Name;
            script.URL = "https://bgrdigital-test.bgr.com.ec/Cuenta/Restaurarclave";
            script.Reporte = new AutoReport(script.GetType().ToString().Split('.').Last(), script.PathGuardado);
            script.Driver = driver;
            script.Driver.Url = script.URL;
            script.Driver.Navigate();
            script.Variables = script.Variables ?? new Dictionary<string, string>();
            script.SeleniumJS = (IJavaScriptExecutor)driver;


            if (!string.IsNullOrEmpty(source))
            {
                JObject json = JObject.Parse(source);
                script.TestData = json.SelectToken("data").ToObject<List<T>>();
            }
            else
            {
                using (StreamReader sr = new StreamReader($"scripts/RecuperarContrasena/{logicaInyectada}.json"))
                {
                    JObject json = JObject.Parse(sr.ReadToEnd());
                    script.TestData = json.SelectToken("data").ToObject<List<T>>();
                }
            }
            //script.Test = script.Reporte.CrearTest(script.GetType().ToString()).AssignDevice(driver.GetType().ToString().Split('.').Last());
            return script;
        }

        public void SeleccionarTipoDocumento(string tipoDocumento)
        {
            const string botonCedula = "btnCedula";
            const string botonPasaporte = "btnPasaporte";

            tipoDocumento = tipoDocumento.Equals("Cedula") ? botonCedula : botonPasaporte;
            var botonTipoDocumento = _driver.FindElement(By.Id(tipoDocumento));
            botonTipoDocumento.ScrollIntoView();
            botonTipoDocumento.Click();
        }

        public void IngresarNumeroDocumento(string numeroIdentificacion, string tipoDocumento)
        {
            const string identificacion = "IdentificacionPersona";
            const string identificacionPersona = "IdentificacionPersona";

            var elemento = tipoDocumento.Equals("Cedula") ? identificacion : identificacionPersona;

            var inputIdentificacon = _driver.FindElement(By.Name(elemento));
            inputIdentificacon.ScrollIntoView();
            inputIdentificacon.SendKeys(numeroIdentificacion);
            inputIdentificacon.SendKeys(Keys.Tab);
        }

        public void ValidarMensajeErrorDatosValidacion()
        {
            const string spanErrorId = "error-t-1";

            var spanError = _driver.FindElement(By.Id(spanErrorId));
            if (spanError.Enabled && spanError.Displayed)
            {
                var mensaje = spanError.GetInnerText();
                throw new Exception(mensaje);
            }
        }

        public void ContinuarValidarIdentificacion()
        {
            const string botonContinuarId = "btnValidarIdentificacion";

            var botonContinuar = _driver.FindElement(By.Id(botonContinuarId));
            botonContinuar.ScrollIntoView();
            botonContinuar.Click();
        }

        public void IngresarPinTarjetaDebito(string pin)
        {
            const string pin1 = "Pin1";
            const string pin2 = "Pin2";
            const string pin3 = "Pin3";
            const string pin4 = "Pin4";

            var elemento = _driver.FindElement(By.Id(pin1));
            elemento.ScrollIntoView();
            elemento.SendKeys(pin[0].ToString());

            elemento = _driver.FindElement(By.Id(pin2));
            elemento.SendKeys(pin[1].ToString());

            elemento = _driver.FindElement(By.Id(pin3));
            elemento.SendKeys(pin[2].ToString());

            elemento = _driver.FindElement(By.Id(pin4));
            elemento.SendKeys(pin[3].ToString());

            elemento.SendKeys(Keys.Tab);
        }

        public void ValidaMensajeErrorTarjetaDebito()
        {
            const string spanErrorId = "error-t-2";
            var spanError = _driver.FindElement(By.Id(spanErrorId));
            if (spanError.Enabled && spanError.Displayed)
            {
                var mensaje = spanError.GetInnerText();
                throw new Exception(mensaje);
            }
        }

        public void ValidarPin()
        {
            const string botonValidarPinId = "btnValidarPin";

            var botonValidarPin = _driver.FindElement(By.Id(botonValidarPinId));
            botonValidarPin.ScrollIntoView();
            botonValidarPin.Click();
        }

        public void EsperarMensajeValidando(int timeout = 30)
        {
            const string botonValidarPinId = "btnValidarPin";

            var botonValidarPin = _driver.FindElement(By.Id(botonValidarPinId));
            int contador = 0;
            while (!botonValidarPin.Enabled && botonValidarPin.GetInnerText().Equals("Validando..."))
            {
                contador++;
                if (contador >= timeout)
                {
                    throw new Exception("Timeout");
                }
                Thread.Sleep(1000);
            }
        }

        public void VerificarErrorValidarPin()
        {
            const string errorId = "error-2";
            var divError = _driver.FindElement(By.Id(errorId));
        }

        public void ValidarClave()
        {
            const string botonRestaurarUsuario = "btnValidaClave";

            var botonValidarClave = _driver.FindElement(By.Id(botonRestaurarUsuario));
            botonValidarClave.ScrollIntoView();
            botonValidarClave.Click();
        }

        public string ObtenerUsuarioRecuperado()
        {
            const string divUsuario = "usuario-recuperado";

            var recuperarUsuario = _driver.WaitFindElement(By.Id(divUsuario));
            return recuperarUsuario.GetInnerText();
        }

        public void IngresarContrasenaNueva(string contrasena1, string contrasena2)
        {
            var inputContrasena = _driver.WaitFindElement(By.Id("contrasena"));
            inputContrasena.ScrollIntoView();
            inputContrasena.SendKeys(contrasena1);
            inputContrasena.SendKeys(Keys.Tab);

            var inputConfirmacion = _driver.WaitFindElement(By.Id("confirmacion"));
            inputConfirmacion.ScrollIntoView();
            inputConfirmacion.SendKeys(contrasena2);
            inputConfirmacion.SendKeys(Keys.Tab);
        }

        public void RestaurarContrasena()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("submit")));
            Thread.Sleep(1500);
            _driver.FindElement(By.Name("submit")).ScrollIntoView();
            _driver.FindElement(By.Name("submit")).Click();
        }
    }
}
