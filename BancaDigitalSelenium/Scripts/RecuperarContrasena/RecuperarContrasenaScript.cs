using AventStack.ExtentReports.Model;
using Core.LogicaScripts.RecuperarContrasena;
using Core.Models.Entidad.RecuperarContrasena;
using Core.Models.Entidad;
using Core.Models.Interface;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using BancaDigitalSelenium.Scripts.Shared;
using Core.Helpers;

namespace BancaDigitalSelenium.Scripts.RecuperarContrasena
{
    public class RecuperarContrasenaScript : ScriptBase, IScript
    {
        private RecuperarContrasenaBLL Logica { get; set; }
        public void SetConfig(IWebDriver driver)
        {
            Logica = new RecuperarContrasenaBLL();
            Logica.SetConfig<RecuperarContrasenaParametro>(this, driver);
        }

        public void Execute()
        {
            List<RecuperarContrasenaParametro> ListaDatos = (List<RecuperarContrasenaParametro>)TestData;
            foreach (var data in ListaDatos)
            {
                try
                {
                    var nombreTest = $"{this.GetType().ToString().Split('.').Last()} {ListaDatos.IndexOf(data) + 1}";
                    var navegador = Driver.GetType().ToString().Split('.').Last();
                    Test = Reporte.CrearTest(nombreTest).AssignDevice(navegador);
                    Test.CreateNode("Datos Ejecución").AgregarInformacionParametro<RecuperarContrasenaParametro>(data);

                    Driver.Url = URL;
                    Logica.SeleccionarTipoDocumento(data.TipoIdentificacion);
                    Logica.IngresarNumeroDocumento(data.NumeroDocumento, data.TipoIdentificacion);
                    Logica.ValidarMensajeErrorDatosValidacion();
                    Test.Info("Ingreso número documento correcto", Driver.TomarScreen());
                    Logica.ContinuarValidarIdentificacion();
                    Logica.IngresarPinTarjetaDebito(data.Pin);
                    Logica.ValidaMensajeErrorTarjetaDebito();
                    Test.Info("Ingreso Pin", Driver.TomarScreen());
                    Logica.ValidarPin();
                    Logica.EsperarMensajeValidando();
                    Logica.ValidaMensajeErrorTarjetaDebito();

                    Logica.IngresarContrasenaNueva(data.Contrasena, data.Contrasena);

                    Logica.ValidarClave();

                    Logica.RestaurarContrasena();

                    this.IngresarCodigoTemporal(data.CodigoTemporal);
                    //Test.Info("Ingreso códig temporal", Driver.TomarScreen());
                    //string usuarioRecuperado = Logica.ObtenerUsuarioRecuperado();
                    //Test.Pass($"Usuario Recupetado {usuarioRecuperado}", Driver.TomarScreen());
                    var x = Driver.WaitFindElement(By.ClassName("fecha-clave-cambiada"));
                    Test.Pass($"Fecha recuperación: {x.GetInnerText()}");
                    if (Driver.Url == "https://bgrdigital-test.bgr.com.ec/Cuenta/RecuperarClaveExito")
                    {
                        Test.Pass("Clave Recuperada", Driver.TomarScreen());
                    }
                }              
                catch (Exception ex)
                {
                    Test.Fail(ex.Message, Driver.TomarScreen());
                }
            }
        }

        public void Error(Exception ex)
        {
            Test.Fail(ex.Message, Driver.TomarScreen());
        }

        public void Finalizar()
        {
            Reporte.GuardarReporte();
        }

    }
}
