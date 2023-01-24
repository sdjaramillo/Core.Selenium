using BancaDigitalSelenium.Scripts.Shared;
using Core.Helpers;
using Core.LogicaScripts.Beneficiarios;
using Core.Models.Beneficiarios;
using Core.Models.Entidad;
using Core.Models.Interface;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BancaDigitalSelenium.Scripts.Beneficiarios
{
    public class AgregarBeneficiarioScript : ScriptBase, IScript
    {
        private BeneficiariosBLL Logica;
        public void SetConfig(IWebDriver driver,string jsoOrigen)
        {
            Logica = new BeneficiariosBLL();
            Logica.SetConfig<BeneficiarioParametro>(this, driver, jsoOrigen);
        }
        public void Error(Exception ex)
        {
            Test.Pass(ex.Message, Driver.TomarScreen());
        }

        public void Execute()
        {
            List<BeneficiarioParametro> ListaDatos = (List<BeneficiarioParametro>)this.TestData;
            foreach (var data in ListaDatos)
            {
                this.Login(data.Usuario, data.Contrasena);
                foreach (var ben in data.Beneficiarios)
                {
                    try
                    {
                        Test = Reporte.CrearTest($"Agregar Beneficiario: {ben.CuentaBeneficiario}");
                        Test.AgregarInformacionParametro<BeneficiarioParametro>(ben);

                        this.SeleccionarOpcionMenu(By.Id("Transferencias"));
                        Logica.AdministrarContactos();
                        Test.Pass("Administrar Contactos", Driver.TomarScreen());

                        Logica.NuevoContacto();
                        Test.Pass("Nuevo Beneficiario", Driver.TomarScreen());

                        Logica.SeleccionarBanco(ben.CuentaBgr);
                        Test.Pass("Seleccion Banco", Driver.TomarScreen()); ;

                        if (ben.CuentaBgr)
                        {
                            Logica.IngresarInformacionBeneficiarioPropio(ben.CuentaBeneficiario);
                            Test.Pass("Ingreso información beneficiario", Driver.TomarScreen());
                            Logica.ValidarCuentaBeneficiarioPropio();
                            Test.Pass("Verificar Beneficiario", Driver.TomarScreen());
                            Thread.Sleep(500);
                            var informacionCuenta = Logica.ObtenerInformacionCuenta();
                            Test.Pass("Información Cuenta", Driver.TomarScreen());
                            Test.AgregarDatosEjecucion(informacionCuenta);

                            //Informacion Beneficiario
                            string alias = Logica.ObtenerAlias(ben.Alias, informacionCuenta["Nombre Beneficiario"]);
                            Logica.IngresarDatosNuevoContacto(alias, ben.Correo, ben.Telefono);
                            Test.Pass($"Alias beneficiario: {alias}");

                            //Boton Continuar
                            var botonContinuar = Driver.WaitElementTobeClicked(By.Id("continuarValidacion"));
                            botonContinuar.ScrollIntoView();
                            botonContinuar.Click();

                            var botonGuardarContacto = Driver.WaitElementTobeClicked(By.Id("btnSeguir"));
                            botonGuardarContacto.ScrollIntoView();
                            botonGuardarContacto.Click();

                            this.IngresarCodigoTemporal();

                            var botonContinuarOTP = Driver.WaitElementTobeClicked(By.Id("continuarValidacionOtp"));
                            botonContinuarOTP.ScrollIntoView();
                            botonContinuarOTP.Click();

                            var guardado = Driver.WaitFindElement(By.XPath(".//div[text()='¡Beneficiario guardado!']"), 20);
                            if (Driver.Url == "https://bgrdigital-test.bgr.com.ec/Contactos/ContactoGuardarExito")
                            {
                                Test.Pass("Contacto Guardado", Driver.TomarScreen());
                            }
                        }
                        else
                        {
                            var comboBancos = Driver.WaitFindElement(By.Id("idBanco"));
                            SelectElement selectBancos = new SelectElement(comboBancos);
                            selectBancos.SelectByText(ben.Banco, true);

                            var numeroCuentaInput = Driver.FindElement(By.Id("numeroCuentaOtros"));
                            numeroCuentaInput.ScrollIntoView();
                            numeroCuentaInput.SendKeys(ben.CuentaBeneficiario);

                            var tipoCuenta = (ben.TipoCuenta == "CA") ? Driver.FindElement(By.Id("radioCA")) : Driver.FindElement(By.Id("radioCC"));
                            tipoCuenta.ScrollIntoView();
                            tipoCuenta.Click();

                            Test.Pass("Información Cuenta", Driver.TomarScreen());

                            var nombreBeneficiario = Driver.FindElement(By.Id("nombreOtros"));
                            nombreBeneficiario.ScrollIntoView();
                            nombreBeneficiario.SendKeys("nombre beneficiario otro banco");

                            var aliasBeneficiario = Driver.FindElement(By.Id("txtAliasOtros"));
                            aliasBeneficiario.ScrollIntoView();
                            aliasBeneficiario.SendKeys(ben.Alias);

                            var correoBeneficiario = Driver.FindElement(By.Id("idCorreoOtros"));
                            correoBeneficiario.ScrollIntoView();
                            correoBeneficiario.SendKeys(ben.Correo);

                            var numeroBeneficiario = Driver.FindElement(By.Id("idCelular"));
                            numeroBeneficiario.ScrollIntoView();
                            numeroBeneficiario.SendKeys(ben.Telefono);


                            var tipoDocumento = Driver.FindElement(By.XPath($"//div[contains(@class, 'btn-group mt-3')]/input[contains(@value,'{ben.TipoDocumento}')]"));
                            tipoDocumento.ScrollIntoView();
                            tipoDocumento.FireClick();

                            IWebElement numeroIdentificacion = null;
                            switch (ben.TipoDocumento)
                            {
                                case "Cedula":
                                    numeroIdentificacion = Driver.FindElement(By.Id("numerocedula"));
                                    break;
                                case "RUC":
                                    numeroIdentificacion = Driver.FindElement(By.Id("RucOtros"));
                                    break;
                                case "Pasaporte":
                                    numeroIdentificacion = Driver.FindElement(By.Id("PasaporteOtros"));
                                    break;
                            }
                            numeroIdentificacion.ScrollIntoView();
                            numeroIdentificacion.SendKeys(ben.NumeroIdentificacion);

                            Test.Pass("Información Beneficiario", Driver.TomarScreen());

                            var botonContinuarValidacion = Driver.WaitElementTobeClicked(By.Id("continuarValidacion"));
                            botonContinuarValidacion.ScrollIntoView();
                            botonContinuarValidacion.Click();

                            Test.Pass("Información Beneficiario", Driver.TomarScreen());


                            Driver.WaitFindElement(By.Id("btnSeguir"));
                            var botonGuardarContacto = Driver.WaitElementTobeClicked(By.Id("btnSeguir"));
                            botonGuardarContacto.ScrollIntoView();
                            botonGuardarContacto.Click();

                            this.IngresarCodigoTemporal();

                            var botonValidarOtp = Driver.WaitElementTobeClicked(By.Id("continuarValidacionOtp"));
                            botonValidarOtp.ScrollIntoView();
                            botonValidarOtp.Click();
                            var guardado = Driver.WaitFindElement(By.XPath(".//div[text()='¡Beneficiario guardado!']"), 20);

                            Test.Pass("Información Beneficiario", Driver.TomarScreen());
                        }
                    }
                    catch (Exception ex)
                    {
                        Test.Fail(ex.Message, Driver.TomarScreen());
                    }
                }
                this.Logout();
            }
        }

        public void Finalizar()
        {
            Reporte.GuardarReporte();
        }

    }
}