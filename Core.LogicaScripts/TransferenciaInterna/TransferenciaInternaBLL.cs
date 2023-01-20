using AventStack.ExtentReports.Model;
using Core.Helpers;
using Core.Models.Entidad;
using Core.Models.Entidad.Transferencias;
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

namespace Core.LogicaScripts.TransferenciaInterna
{
    public class TransferenciaInternaBLL
    {
        private IWebDriver _driver;
        public ScriptBase SetConfig<T>(ScriptBase script, IWebDriver driver)
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

            using (StreamReader sr = new StreamReader($"scripts/Transferencias/{logicaInyectada}.json"))
            {
                JObject json = JObject.Parse(sr.ReadToEnd());
                script.TestData = json.SelectToken("data").ToObject<List<T>>();
            }
            //script.Test = script.Reporte.CrearTest(script.GetType().ToString()).AssignDevice(driver.GetType().ToString().Split('.').Last());
            return script;
        }

        public void BotonNuevaTransferencia()
        {
            const string botonNuevaTransferenciaID = "button-active-transferencia";
            var nuevaTransferencia = _driver.FindElement(By.ClassName(botonNuevaTransferenciaID));
            nuevaTransferencia.ScrollIntoView();
            nuevaTransferencia.Click();
        }

        public void SeleccionoarCuentaDestino(string numeroCuenta)
        {
            var cuentaDestinoElement = _driver.FindElement(By.Id(numeroCuenta));
            cuentaDestinoElement.ScrollIntoView();
            _driver.FindElement(By.Id(numeroCuenta)).Click();
        }

        public void BotonContinuar()
        {
            const string botonBeneficiarioId = "btnBeneficiario";
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(botonBeneficiarioId)));
            var element = _driver.FindElement(By.Id(botonBeneficiarioId));
            element.ScrollIntoView();            
            _driver.FindElement(By.Id(botonBeneficiarioId)).Click();
        }
    }
}
