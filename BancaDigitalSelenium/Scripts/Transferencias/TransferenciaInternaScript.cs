using BancaDigitalSelenium.Scripts.Shared;
using Core.Helpers;
using Core.LogicaScripts.RecuperarContrasena;
using Core.LogicaScripts.TransferenciaInterna;
using Core.Models.Entidad;
using Core.Models.Entidad.RecuperarContrasena;
using Core.Models.Entidad.Transferencias;
using Core.Models.Interface;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BancaDigitalSelenium.Scripts.Transferencias
{
    public class TransferenciaInternaScript : ScriptBase, IScript
    {
        private TransferenciaInternaBLL Logica { get; set; }

        public void SetConfig(IWebDriver driver)
        {
            Logica = new TransferenciaInternaBLL();
            Logica.SetConfig<TransferenciaInternaParametro>(this, driver);
            driver.Manage().Window.Maximize();

        }

        public void Execute()
        {
            List<TransferenciaInternaParametro> ListaDatos = (List<TransferenciaInternaParametro>)TestData;

            foreach (var data in ListaDatos)
            {

                this.Login(data.Usuario, data.Contrasena);
                this.SeleccionarOpcionMenu(By.Id("menu-transferencias"));

                foreach (var transferencia in data.Transferencias)
                {
                    try
                    {
                        Test = Reporte.CrearTest($"Transferencia: {transferencia.CuentaOrigen} > {transferencia.NumeroCuenta}");
                        var info = Test.CreateNode("Información Prueba");
                        info.Info($"Cuenta origen: {transferencia.CuentaOrigen}");
                        info.Info($"Monto: {transferencia.Monto}");
                        info.Info($"Motivo: {transferencia.Motivo}");
                        info.Info($"Cuenta Destino: {transferencia.NumeroCuenta}");

                        //BOTON NUEVA TRANSFERENCIA
                        Logica.BotonNuevaTransferencia();

                        //SELECCIONAR CUENTA
                        Logica.SeleccionoarCuentaDestino(transferencia.NumeroCuenta);
                        Test.Pass("Selección Cuenta Destino", Driver.TomarScreen());

                        //BOTON CONTINUAR
                        Thread.Sleep(1000);
                        Logica.BotonContinuar();


                        //CAMPO MONTO
                        Thread.Sleep(500);
                        Driver.WaitFindElement(By.Id("Monto")).Click();
                        SeleniumJS.ExecuteScript("document.getElementById('Monto').value= ''");
                        Driver.WaitFindElement(By.Id("Monto")).SendKeys(transferencia.Monto);
                        Driver.WaitFindElement(By.Id("Monto")).SendKeys(Keys.Tab);
                        Driver.WaitFindElement(By.Id("Motivo")).SendKeys(transferencia.Motivo);
                        Driver.WaitFindElement(By.Id("btnMonto")).Click();
                        Test.Pass("Detalle Transferencia", Driver.TomarScreen());

                        //3
                        Thread.Sleep(500);
                        SelectElement comboCuentaOrigen = new SelectElement(Driver.FindElement(By.Id("InformacionOrigen_NumeroCuenta")));
                        comboCuentaOrigen.SelectByText(transferencia.CuentaOrigen, true);
                        Test.Pass("Cuenta Destino", Driver.TomarScreen());

                        var botonCuenta = Driver.FindElement(By.Id("btnCuenta"));
                        botonCuenta.Click();

                        var botonConfirmarTransferencia = Driver.FindElement(By.Id("button-inactive"));
                        botonConfirmarTransferencia.Click();
                        //Driver.Navigate().Refresh();
                        if (data.UrlFallido == Driver.Url)
                        {
                            Test.Fail(data.UrlFallido, Driver.TomarScreen());
                        }
                        if (data.UrlCorrecto == Driver.Url)
                        {
                            Test.Pass("Transferencia Exitosa", Driver.TomarScreen());
                        }
                    }
                    catch (Exception ex)
                    {
                        Test.Fail(ex.Message);
                    }

                    this.SeleccionarOpcionMenu(By.Id("menu-transferencias"));
                }
            }
        }
        public void Error(Exception ex)
        {
        }

        public void Finalizar()
        {
            Reporte.GuardarReporte();
        }


    }
}
