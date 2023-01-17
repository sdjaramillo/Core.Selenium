using BancaDigitalSelenium.Scripts.Shared;
using Core.Helpers;
using Core.LogicaScripts.RecuperarContrasena;
using Core.LogicaScripts.TransferenciaInterna;
using Core.Models.Entidad;
using Core.Models.Entidad.RecuperarContrasena;
using Core.Models.Entidad.Transferencias;
using Core.Models.Interface;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
                    Test = Reporte.CrearTest($"Transferencia: {transferencia.CuentaOrigen} > {transferencia.NumeroCuenta}");
                    var info = Test.CreateNode("Información Prueba");
                    info.Info($"Cuenta origen: {transferencia.CuentaOrigen}");
                    info.Info($"Monto: {transferencia.Monto}");
                    info.Info($"Motivo: {transferencia.Motivo}");
                    info.Info($"Cuenta Destino: {transferencia.NumeroCuenta}");

                    //BOTON NUEVA TRANSFERENCIA
                    var nuevaTransferencia = Driver.FindElement(By.ClassName("button-active-transferencia"));
                    nuevaTransferencia.Click();

                    //SELECCIONAR CUENTA
                    Driver.FindElement(By.Id(transferencia.NumeroCuenta)).Click();
                    Test.Pass("Cuenta Destino", Driver.TomarScreen());
                    //BOTON CONTINUAR
                    Driver.FindElement(By.Id("btnBeneficiario")).Click();


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
