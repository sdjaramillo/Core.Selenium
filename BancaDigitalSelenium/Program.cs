using BancaDigitalSelenium.Scripts;
using BancaDigitalSelenium.Scripts.Afiliacion;
using BancaDigitalSelenium.Scripts.Beneficiarios;
using BancaDigitalSelenium.Scripts.RecuperarContrasena;
using BancaDigitalSelenium.Scripts.Transferencias;
using Core.Helpers;
using Core.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;


namespace BancaDigitalSelenium
{
    internal class Program
    {
        static void Main(string[] args)
        {


            Console.Write($"1.Chrome\n2.Edge\n3.Firefox");
            //SELECCIONAR CUALQUIER NAVEGADOR
            var driveropt = Console.ReadLine();
            IWebDriver driver = null;
            switch (driveropt)
            {
                case "1":
                    driver = new ChromeDriver("drivers");
                    break;

                case "2":
                    driver = new EdgeDriver("drivers");
                    break;
                case "3":
                    driver = new FirefoxDriver("drivers");
                    break;
            }




            //DriverTemplate.EjecutarScript(new AgregarBeneficiarioScript(),driver,"");
            //DriverTemplate.EjecutarScript(new AfiliacionScript(), driver,"");
            //DriverTemplate.EjecutarScript(new RecuperarUsuarioScript(), driver,"");
            //DriverTemplate.EjecutarScript(new RecuperarContrasenaScript(), driver,"");
            //DriverTemplate.EjecutarScript(new ValidarCredencialesIncorrectas(), chrome,"");
            DriverTemplate.EjecutarScript(new TransferenciaInternaScript(), driver,"");
        }
    }
}

