using Core.Models.Entidad;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.LogicaScripts.Beneficiarios
{
    public class BeneficiariosBLL
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

            using (StreamReader sr = new StreamReader($"scripts/Beneficiarios/{logicaInyectada}.json"))
            {
                JObject json = JObject.Parse(sr.ReadToEnd());
                script.TestData = json.SelectToken("data").ToObject<List<T>>();
            }
            //script.Test = script.Reporte.CrearTest(script.GetType().ToString()).AssignDevice(driver.GetType().ToString().Split('.').Last());
            return script;
        }
    }
}
