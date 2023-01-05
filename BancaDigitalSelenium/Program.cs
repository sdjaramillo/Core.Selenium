using BancaDigitalSelenium.Scripts;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BancaDigitalSelenium
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //SELECCIONAR CUALQUIER NAVEGADOR
            var driver = new ChromeDriver("drivers");

            DriverTemplate.EjecutarScript(new RecuperarContrasena(), driver);
            //DriverTemplate.EjecutarScript(new ValidarCredencialesIncorrectas(), chrome);
        }
    }
}

