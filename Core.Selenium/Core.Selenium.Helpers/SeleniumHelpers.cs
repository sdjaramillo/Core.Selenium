using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Selenium.Helpers
{
    public static class SeleniumHelpers
    {
        public static IWebDriver GetDriverInstance(string driver, string ruta = "Drivers")
        {            
            switch (driver.ToLower())
            {
                case "chrome":
                    return new ChromeDriver(ruta);

                case "edge":
                    return new EdgeDriver(ruta);
            }
            return new ChromeDriver("Drivers");
        }

        public static By ObtenerIdentificador(string identificador, string valor)
        {
            switch (identificador.ToLower())
            {
                case "id":

                    return By.Id(valor);

                case "name":
                    return By.Name(valor);

                case "css":
                    return By.CssSelector(valor);

                case "xpath":
                    return By.XPath(valor);

                case "classname":

                    return By.ClassName(valor);

                case "link":

                    return By.PartialLinkText(valor);
            }

            return null;
        }

        public static Func<IWebDriver, bool> PartialTextPresentInPage(string partialText)
        {
            return delegate (IWebDriver driver)
            {
                try
                {
                    driver.WaitFindElement(By.XPath($"//*[contains(text(),'{partialText}')]"));
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            };
        }
    }
}
