using AventStack.ExtentReports.Model;
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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.LogicaScripts.Afiliacion
{
    public class AfiliacionBLL
    {
        private IWebDriver _driver;
        public ScriptBase SetConfig<T>(ScriptBase script, IWebDriver driver, string source="")
        {
            _driver = driver;
            var logicaInyectada = script.GetType().Name;
            script.URL = "https://bgrdigital-test.bgr.com.ec/Cuenta/Afiliate";
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
                using (StreamReader sr = new StreamReader($"scripts/Afiliacion/{logicaInyectada}.json"))
                {
                    JObject json = JObject.Parse(sr.ReadToEnd());
                    script.TestData = json.SelectToken("data").ToObject<List<T>>();
                }

            }           
            return script;
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

        public void ValidarMensajeErrorPin()
        {
            var errorPin = _driver.FindElement(By.Id("error-pin"));
            if (errorPin.Enabled && errorPin.Displayed)
            {
                throw new Exception(errorPin.GetInnerText());
            }
        }

        public void IngresarNumeroCedula(string numeroCedula)
        {
            const string CampoCedula = "CedulaRuc";
            var numeroCedulaInput = _driver.FindElement(By.Name(CampoCedula));
            numeroCedulaInput.ScrollIntoView();
            numeroCedulaInput.SendKeys(numeroCedula);
        }

        public void LinkTerminosyCondiciones()
        {
            var temrinosCondiciones = _driver.FindElement(By.ClassName("terminos-condiciones"));
            temrinosCondiciones.Click();
        }

        public void AceptarTerminosyCondiciones()
        {
            var aceptarTerminosCondiciones = _driver.FindElement(By.Id("cbox2"));
            aceptarTerminosCondiciones.Click();
        }

        public void BotonContinuar()
        {
            var BotonContinuar = _driver.FindElement(By.Id("btn_validar"));
            BotonContinuar.Click();
        }

        public void IngresarTelefono(string telefono)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
            var telefonoInput = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("telefono")));
            telefonoInput.SendKeys(telefono);
        }

        public void IngresarCorreo(string correo)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
            var correoInput = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("correo")));
            correoInput.SendKeys(correo);
        }

        public void BotonValidarDatosPersonales()
        {
            var botonCotinuar = _driver.FindElement(By.Id("valida_datos"));
            botonCotinuar.ScrollIntoView();
            botonCotinuar.Click();
        }

        public void IngresarUsuario(string usuario)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
            var usuarioInput = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("txtUsuario")));
            usuarioInput.ScrollIntoView();
            usuarioInput.SendKeys(usuario);
        }

        public void IngresarContraseñaYConfirmacion(string contrasena1, string contrasena2)
        {
            var contrasena = _driver.FindElement(By.Id("contrasena"));
            contrasena.ScrollIntoView();
            contrasena.SendKeys(contrasena1);

            var contrasenaConfirmacion = _driver.FindElement(By.Id("confirmacion"));
            contrasenaConfirmacion.ScrollIntoView();
            contrasenaConfirmacion.SendKeys(contrasena2);
        }

        public void BotonValidarUsuarioContrasena()
        {
            const string botonValidarUsuario = "btn_valida_usuario";
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(botonValidarUsuario)));
            IWebElement botonContinuar = _driver.FindElement(By.Id(botonValidarUsuario));
            botonContinuar.ScrollIntoView();
            botonContinuar.Click();
        }
    }
}
