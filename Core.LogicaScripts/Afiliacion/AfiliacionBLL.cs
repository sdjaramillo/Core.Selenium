using AventStack.ExtentReports.Model;
using Core.Helpers;
using Core.Models.Entidad;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.LogicaScripts.Afiliacion
{
    public class AfiliacionBLL
    {
        private IWebDriver _driver;
        public ScriptBase SetConfig<T>(ScriptBase script, IWebDriver driver)
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

            using (StreamReader sr = new StreamReader($"scripts/Afiliacion/{logicaInyectada}.json"))
            {
                JObject json = JObject.Parse(sr.ReadToEnd());
                script.TestData = json.SelectToken("data").ToObject<List<T>>();
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
    }
}
