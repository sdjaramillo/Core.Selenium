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
                this.Login(data.Usuario,data.Contrasena);


            }
        }

        public void Finalizar()
        {
            throw new NotImplementedException();
        }

    }
}
