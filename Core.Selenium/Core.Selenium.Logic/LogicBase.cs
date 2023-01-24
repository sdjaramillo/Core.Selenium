using Core.Selenium.Helpers;
using Core.Selenium.Model;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Selenium.Logic
{
    public class LogicBase
    {
        private IWebDriver _driver { get; set; }

        public LogicBase(IWebDriver driver)
        {
            _driver = driver;
        }

        public void EjecutarComandos(List<Comando> comandosEjecutar)
        {
            foreach (var cmd in comandosEjecutar)
            {
                try
                {
                    if (cmd.Tipo == null || cmd?.Tipo?.ToLower() == TipoComando.comando.ToString().ToLower())
                    {
                        IWebElement elemento = null;

                        string[] targetSplit = cmd.Target.Split('=');
                        string identificador = targetSplit[0];
                        string valor = string.Join("=", targetSplit.Skip(1));

                        By by = ObtenerIdentificador(identificador, valor);
                        elemento = _driver.WaitFindElement(by);
                    }

                    if (cmd?.Tipo?.ToLower() == TipoComando.script.ToString().ToLower())
                    {
                        IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
                        var resultadoScript = js.ExecuteScript(cmd.Target);
                    }
                }
                catch (Exception ex)
                {

                    throw new Exception($"Error al ejecutar comando: {cmd.Orden}: {cmd.Command}");
                }
            }
        }

        private By ObtenerIdentificador(string identificador, string valor)
        {
            switch (identificador.ToLower())
            {
                case "id":

                    return By.Id(valor);

                case "name":
                    return By.Name(valor);

                case "css":
                    return By.CssSelector(valor);

                case "xpath":
                    return By.XPath(valor);

                case "classname":

                    return By.ClassName(valor);
            }

            throw new Exception($"no definido: {identificador}:{valor}");
        }

    }
}
