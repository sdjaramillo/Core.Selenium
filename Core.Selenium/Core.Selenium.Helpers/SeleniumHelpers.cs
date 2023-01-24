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
            return new ChromeDriver();
        }
    }
}
