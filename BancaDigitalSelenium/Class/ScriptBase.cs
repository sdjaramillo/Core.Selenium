using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using BancaDigitalSelenium.Interface;

namespace BancaDigitalSelenium
{
    public class ScriptBase
    {
        public IWebDriver SeleniumDriver { get; set; }
        public IJavaScriptExecutor SeleniumJS { get; set; }
        public WebDriverWait SeleniumWait { get; set; }
        public Dictionary<string, string> Variables = new Dictionary<string, string>();
        public Dictionary<string, string> Resultados = new Dictionary<string, string>();
        public IDictionary<string, object> Ventanas = new Dictionary<string, object>();
        protected string URL { get; set; }        
    }
}
