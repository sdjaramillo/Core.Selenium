using BancaDigitalSelenium.Scripts.Shared;
using Core.LogicaScripts.Beneficiarios;
using Core.Models.Beneficiarios;
using Core.Models.Entidad;
using Core.Models.Entidad.Script;
using Core.Models.Interface;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaDigitalSelenium.Scripts.Beneficiarios
{
    internal class EditarBeneficiarioScript : ScriptBase, IScript
    {
        private EditarBeneficiarioBLL Logica { get; set; }

        public void SetConfig(IWebDriver driver, string json)
        {
            Logica = new EditarBeneficiarioBLL();
            Logica.SetConfig<EditarBeneficiarioParametro>(this, driver);
        }

        public void Execute()
        {
            List<EditarBeneficiarioParametro> ListaDatos = (List<EditarBeneficiarioParametro>)this.TestData;
            foreach (var data in ListaDatos)
            {
                this.Login(data.Usuario, data.Contrasena);
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
