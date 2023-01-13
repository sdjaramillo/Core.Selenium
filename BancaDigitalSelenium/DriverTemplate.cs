using Core.Models.Interface;
using OpenQA.Selenium;
using System;

namespace BancaDigitalSelenium
{
    /// <summary>
    /// Plantilla principal para ejecución estandarizada de scripts
    /// </summary>
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
                script.Error(ex);                
            }
            finally
            {
                script.Finalizar();
            }
        }
    }
}
