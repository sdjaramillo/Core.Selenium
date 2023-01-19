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
        public void SetConfig(IWebDriver driver)
        {
            Logica = new AfiliacionBLL();
            Logica.SetConfig<AfiliacionParametro>(this, driver);
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
                    //Logica.SeleccionarTipoDocumento(data.TipoIDentificacion);
                    var numeroCedula = Driver.FindElement(By.Name("CedulaRuc"));
                    numeroCedula.SendKeys(data.NumeroCedula);

                    Logica.IngresarPinTarjetaDebito(data.Pin);

                    ///Verificar terminos y condiciones
                    var temrinosCondiciones = Driver.FindElement(By.ClassName("terminos-condiciones"));
                    temrinosCondiciones.Click();

                    var aceptarTerminosCondiciones = Driver.FindElement(By.Id("cbox2"));
                    aceptarTerminosCondiciones.Click();

                    Test.Pass("Datos de validación", Driver.TomarScreen());
                    var BotonContinuar = Driver.FindElement(By.Id("btn_validar"));
                    BotonContinuar.Click();

                    Thread.Sleep(1000);

                    Logica.ValidarMensajeErrorPin();
                    ////////// FIN PRIMERA PARTE


                    var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
                    var telefono = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("telefono")));
                    telefono.SendKeys(data.Telefono);

                    var correo = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("correo")));
                    correo.SendKeys(data.Correo);

                    Test.Pass("Datos Personales", Driver.TomarScreen());
                    var botonCotinuar = Driver.FindElement(By.Id("valida_datos"));
                    botonCotinuar.Click();

                    var usuario = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("txtUsuario")));
                    usuario.SendKeys(data.Usuario);

                    var contrasena = Driver.FindElement(By.Id("contrasena"));
                    contrasena.SendKeys(data.Contrasena);

                    var contrasenaConfirmacion = Driver.FindElement(By.Id("confirmacion"));
                    contrasenaConfirmacion.SendKeys(data.Contrasena);

                    Test.Pass("Usuario y contrasena", Driver.TomarScreen());
                    Thread.Sleep(1000);
                    var botonContinuar = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btn_valida_usuario")));
                    botonContinuar = Driver.FindElement(By.Id("btn_valida_usuario"));
                    botonContinuar.Click();

                    wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("submit")));
                    Thread.Sleep(1500);
                    Driver.FindElement(By.Name("submit")).Click();


                    //INGRESAR CODIGO TEMPORAL
                    var codigo = Driver.FindElement(By.Id("txtCodigo1"));
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
                    Test.Pass("Afiliación", Driver.TomarScreen());
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
