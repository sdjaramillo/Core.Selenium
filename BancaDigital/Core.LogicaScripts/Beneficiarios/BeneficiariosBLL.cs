using Core.Helpers;
using Core.Models.Entidad;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.LogicaScripts.Beneficiarios
{
    public class BeneficiariosBLL
    {
        private IWebDriver _driver;
        public ScriptBase SetConfig<T>(ScriptBase script, IWebDriver driver, string source)
        {
            _driver = driver;
            var logicaInyectada = script.GetType().Name;
            script.URL = "https://bgrdigital-test.bgr.com.ec";
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
                using (StreamReader sr = new StreamReader($"scripts/Beneficiarios/{logicaInyectada}.json"))
                {
                    JObject json = JObject.Parse(sr.ReadToEnd());
                    script.TestData = json.SelectToken("data").ToObject<List<T>>();
                }
            }
            //script.Test = script.Reporte.CrearTest(script.GetType().ToString()).AssignDevice(driver.GetType().ToString().Split('.').Last());
            return script;
        }

        public void AdministrarContactos()
        {
            var linkContactos = _driver.WaitFindElement(By.LinkText("Administrar mis contactos"));
            linkContactos.ScrollIntoView();
            linkContactos.Click();
        }

        public void NuevoContacto()
        {
            var linkNuevoBeneficiario = _driver.WaitFindElement(By.LinkText("Nuevo Beneficiario"));
            linkNuevoBeneficiario.ScrollIntoView();
            linkNuevoBeneficiario.Click();
        }

        public void SeleccionarBanco(bool cuentaBgr)
        {
            IWebElement radioBanco = (cuentaBgr) ? _driver.FindElement(By.Id("flexRadioDefault1")) : _driver.FindElement(By.Id("flexRadioDefault2"));
            radioBanco.ScrollIntoView();
            radioBanco.Click();
        }

        public void IngresarInformacionBeneficiarioPropio(string numeroCuenta)
        {
            var inputCuentaBgr = _driver.FindElement(By.Id("numeroCuenta"));
            inputCuentaBgr.ScrollIntoView();
            inputCuentaBgr.SendKeys(numeroCuenta);
        }

        public void ValidarCuentaBeneficiarioPropio()
        {
            var botonValidarCuenta = _driver.WaitElementTobeClicked(By.XPath("//button[.='Verificar']"), 10);
            botonValidarCuenta.ScrollIntoView();
            botonValidarCuenta.Click();
        }

        public Dictionary<string, string> ObtenerInformacionCuenta()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            WebDriverWait w = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            w.Until(ExpectedConditions.ElementIsVisible(By.ClassName("cards-informativo")));

            var informacionCuenta = _driver.WaitFindElement(By.ClassName("cards-informativo"), 20);
            if (informacionCuenta.CheckIfIsVisible())
            {
                var nombrBeneficiario = _driver.FindElement(By.Id("text-nombre")).GetValue<string>();
                result.Add("Nombre Beneficiario", nombrBeneficiario);

                var identificacionBeneficiario = _driver.FindElement(By.Id("text-identificacion-beneficiario")).GetValue<string>();
                result.Add("Identificacion Beneficiario", identificacionBeneficiario);

                var numeroCuenta = _driver.FindElement(By.Id("text-cuenta")).GetValue<string>();
                result.Add("Número Cuenta", numeroCuenta);

                var banco = _driver.FindElement(By.Id("text-banco")).GetValue<string>();
                result.Add("Banco", banco);

                return result;
            }
            throw new Exception("Información cuenta no encontrada");
        }

        public string ObtenerAlias(string alias, string nombre)
        {
            if (string.IsNullOrEmpty(alias))
            {
                string[] nombreSplit = nombre.Split(' ');
                alias = $"{nombreSplit[0][0]}{nombreSplit[1]}".ToLower();
            }
            return alias;
        }

        public void IngresarDatosNuevoContacto(string alias, string correo, string telefono)
        {
            var aliasInput = _driver.WaitFindElement(By.Id("txtAlias"));
            aliasInput.ScrollIntoView();
            aliasInput.SendKeys(alias);

            if (!string.IsNullOrEmpty(correo))
            {
                var correoInput = _driver.WaitFindElement(By.Id("emailBGR"));
                correoInput.ScrollIntoView();
            }
        }
    }
}