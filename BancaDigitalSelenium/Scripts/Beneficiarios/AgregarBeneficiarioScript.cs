using BancaDigitalSelenium.Scripts.Shared;
using Core.Helpers;
using Core.LogicaScripts.Beneficiarios;
using Core.Models.Beneficiarios;
using Core.Models.Entidad;
using Core.Models.Interface;
using OpenQA.Selenium;
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
        public void SetConfig(IWebDriver driver)
        {
            Logica = new BeneficiariosBLL();
            Logica.SetConfig<BeneficiarioParametro>(this, driver);
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
                Test = Reporte.CrearTest($"Agregar Beneficiario: {data.Usuario}");
                this.Login(data.Usuario, data.Contrasena);

                foreach (var ben in data.Beneficiarios)
                {
                    try
                    {
                        Test = Test.CreateNode($"Beneficiario: {ben.CuentaBeneficiario}");
                        Test.CreateNode($"Datos Ejecución").AgregarInformacionParametro<BeneficiarioParametro>(ben);

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

                            var guardado = Driver.WaitFindElement(By.XPath(".//div[text()='¡Beneficiario guardado!']"),20);
                            if (Driver.Url == "https://bgrdigital-test.bgr.com.ec/Contactos/ContactoGuardarExito")
                            {
                                Test.Pass("Contacto Guardado", Driver.TomarScreen());
                            }

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
