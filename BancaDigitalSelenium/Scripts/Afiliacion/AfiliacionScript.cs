using Core.Helpers;
using Core.LogicaScripts.Afiliacion;
using Core.LogicaScripts.RecuperarContrasena;
using Core.Models.Entidad;
using Core.Models.Entidad.RecuperarContrasena;
using Core.Models.Interface;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BancaDigitalSelenium.Scripts.Afiliacion
{
    /// <summary>
    /// Script para automatizar flujo de afiliación
    /// Recibe como dato parametros Afiliacion parámetro:
    /// NumeroCedula
    /// Pin 
    /// TipoIDentificacion
    /// Telefono
    /// Correo
    /// </summary>
    public class AfiliacionScript : ScriptBase, IScript
    {
        /// <summary>
        /// Logica del script
        /// </summary>
        private AfiliacionBLL Logica { get; set; }
        public void SetConfig(IWebDriver driver, string jsonOrigen)
        {
            Logica = new AfiliacionBLL();
            Logica.SetConfig<AfiliacionParametro>(this, driver,jsonOrigen);
        }
        public void Execute()
        {
            List<AfiliacionParametro> ListaDatos = (List<AfiliacionParametro>)TestData;
            //Ingreso´cédula
            foreach (var data in ListaDatos)
            {
                try
                {
                    Driver.Url = URL;
                    Test = Reporte.CrearTest($"Afiliación {data.NumeroCedula}");
                    var datosPrueba = Test.CreateNode("Datos Ejecución");
                    datosPrueba.AgregarInformacionParametro<AfiliacionParametro>(data);


                    Logica.IngresarNumeroCedula(data.NumeroCedula);
                    Logica.IngresarPinTarjetaDebito(data.Pin);
                    Logica.LinkTerminosyCondiciones();
                    Logica.AceptarTerminosyCondiciones();
                    Test.Pass("Datos de validación", Driver.TomarScreen());
                    Logica.BotonContinuar();

                    Thread.Sleep(1000);

                    Logica.ValidarMensajeErrorPin();
                    Logica.IngresarTelefono(data.Telefono);
                    Logica.IngresarCorreo(data.Correo);
                    Test.Pass("Datos Personales", Driver.TomarScreen());
                    Logica.BotonValidarDatosPersonales();

                    Thread.Sleep(1000);

                    Logica.IngresarUsuario(data.Usuario);
                    Logica.IngresarContraseñaYConfirmacion(data.Contrasena, data.Contrasena);
                    Test.Pass("Usuario y contrasena", Driver.TomarScreen());
                    Logica.BotonValidarUsuarioContrasena();


                    var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("submit")));
                    Thread.Sleep(1500);
                    Driver.FindElement(By.Name("submit")).ScrollIntoView();
                    Driver.FindElement(By.Name("submit")).Click();

                    //INGRESAR CODIGO TEMPORAL
                    var codigo = Driver.FindElement(By.Id("txtCodigo1"));
                    codigo.ScrollIntoView();
                    codigo.SendKeys(data.CodigoTemporal[0].ToString());

                    codigo = Driver.FindElement(By.Id("txtCodigo2"));
                    codigo.SendKeys(data.CodigoTemporal[1].ToString());

                    codigo = Driver.FindElement(By.Id("txtCodigo3"));
                    codigo.SendKeys(data.CodigoTemporal[2].ToString());

                    codigo = Driver.FindElement(By.Id("txtCodigo4"));
                    codigo.SendKeys(data.CodigoTemporal[3].ToString());

                    codigo = Driver.FindElement(By.Id("txtCodigo5"));
                    codigo.SendKeys(data.CodigoTemporal[4].ToString());

                    codigo = Driver.FindElement(By.Id("txtCodigo6"));
                    codigo.SendKeys(data.CodigoTemporal[5].ToString());

                    wait.Until(ExpectedConditions.ElementExists(By.ClassName("Gracias-por-confiar-en-nosotros")));
                    Test.Pass("Afiliación Completada", Driver.TomarScreen());
                }
                catch (Exception ex)
                {
                    Test.Fail(ex.Message, Driver.TomarScreen());
                }
            }
        }
        public void Error(Exception ex)
        {

        }

        public void Finalizar()
        {
            Reporte.GuardarReporte();
            Driver.Quit();
            Driver.Dispose();
        }


    }
}
