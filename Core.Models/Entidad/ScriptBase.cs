using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Models.Entidad
{
    public class ScriptBase
    {
        /// <summary>
        /// Driver para selenium
        /// </summary>
        public IWebDriver Driver { get; set; }

        /// <summary>
        /// driver para ejecutar JavaScript
        /// </summary>
        public IJavaScriptExecutor SeleniumJS { get; set; }

        /// <summary>
        /// Driver para espera 
        /// </summary>
        public WebDriverWait SeleniumWait { get; set; }

        /// <summary>
        /// Diccionario para gaurdar variables
        /// </summary>
        public Dictionary<string, string> Variables = new Dictionary<string, string>();

        /// <summary>
        /// Driver para guardar resultados de comparación
        /// </summary>
        public Dictionary<string, string> Resultados = new Dictionary<string, string>();

        /// <summary>
        /// Driver para guardar ventanas
        /// </summary>
        public IDictionary<string, object> Ventanas = new Dictionary<string, object>();

        /// <summary>
        /// URL del sitio
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Objeto para generar reporteria
        /// </summary>
        public AutoReport Reporte { get; set; }

        /// <summary>
        /// Variable para guardar el test principal del script
        /// </summary>
        public ExtentTest Test { get; set; }

        /// <summary>
        /// Objeto para tipo de dato variables
        /// </summary>
        public object TestData { get; set; }

        public ScriptBase _script { get; set; }

        public string PathGuardado { get; set; }

    }
}
