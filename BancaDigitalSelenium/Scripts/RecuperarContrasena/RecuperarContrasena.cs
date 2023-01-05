using Core.LogicaScripts.RecuperarContrasena;
using Core.Models.Entidad;
using Core.Models.Entidad.RecuperarContrasena;
using Core.Models.Interface;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

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
            foreach (var data in (List<RecuperarContrasenaData>)TestData)
            {
                Logica.SeleccionarTipoDocumento(data.TipoIdentificacion);
                Logica.IngresarNumeroDocumento(data.NumeroDocumento);
            }
        }

        public void Error(Exception ex)
        {

        }

        public void Finalizar()
        {

        }
    }
}
