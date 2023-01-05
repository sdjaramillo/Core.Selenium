using BancaDigitalSelenium.Interface;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaDigitalSelenium
{
    public class DriverTemplate
    {
        public static void EjecutarScript(IScript script, IWebDriver driver)
        {
            try
            {
                script.SetConfig(driver);
                script.Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
