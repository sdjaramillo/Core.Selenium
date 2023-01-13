using BancaDigitalSelenium.Scripts.Shared;
using Core.Helpers;
using Core.LogicaScripts.RecuperarContrasena;
using Core.Models.Entidad;
using Core.Models.Entidad.RecuperarContrasena;
using Core.Models.Interface;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BancaDigitalSelenium.Scripts
{

    public class RecuperarContrasena : ScriptBase, IScript
    {
        private RecuperarContrasenaBLL Logica { get; set; }
        public void SetConfig(IWebDriver driver)
        {
            Logica = new RecuperarContrasenaBLL();
            Logica.SetConfig<RecuperarContrasenaData>(this, driver);
        }

        public void Execute()
        {
            List<RecuperarContrasenaData> ListaDatos = (List<RecuperarContrasenaData>)TestData;
            foreach (var data in ListaDatos)
            {
                try
                {
                    var nombreTest = $"{this.GetType().ToString().Split('.').Last()} {ListaDatos.IndexOf(data) + 1}";
                    var navegador = Driver.GetType().ToString().Split('.').Last();
                    Test = Reporte.CrearTest(nombreTest).AssignDevice(navegador);

                    Driver.Url = URL;
                    Logica.SeleccionarTipoDocumento(data.TipoIdentificacion);
                    Logica.IngresarNumeroDocumento(data.NumeroDocumento, data.TipoIdentificacion);
                    Logica.ValidarMensajeErrorDatosValidacion();
                    Test.Info("Ingreso número documento correcto", Driver.TomarScreen());
                    Logica.ContinuarValidarIdentificacion();
                    Logica.IngresarPinTarjetaDebito(data.Pin);
                    Logica.ValidaMensajeErrorTarjetaDebito();
                    Logica.ValidarPin();
                    Logica.EsperarMensajeValidando();
                    Logica.ValidaMensajeErrorTarjetaDebito();
                    Logica.RestaurarUsuario();
                    this.IngresarCodigoTemporal(data.CodigoTemporal);
                    string usuarioRecuperado = Logica.ObtenerUsuarioRecuperado();
                    Test.Pass($"Usuario Recupetado {usuarioRecuperado}", Driver.TomarScreen());
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
