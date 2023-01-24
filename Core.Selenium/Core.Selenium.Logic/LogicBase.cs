using AventStack.ExtentReports;
using Core.Selenium.Helpers;
using Core.Selenium.Model;
using Core.Selenium.Report;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Core.Selenium.Logic
{
    public class LogicBase
    {
        private IWebDriver _driver { get; set; }
        private SeleniumReport _reporte { get; set; }
        public LogicBase(IWebDriver driver, string testName)
        {
            _driver = driver;
            _reporte = new SeleniumReport(testName);
        }

        public void EjecutarComandos(List<Comando> comandosEjecutar, bool screenIteration = true, string? testName = null)
        {
            var test = _reporte.CrearTest($"{testName ?? "test"} {_reporte?.Reporte.Stats.ChildCount}");

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
                        PerformAction(elemento, cmd, by);
                    }

                    if (cmd?.Tipo?.ToLower() == TipoComando.script.ToString().ToLower())
                    {
                        IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
                        var resultadoScript = js.ExecuteScript(cmd.Target);
                    }

                    if (cmd?.Tipo?.ToLower() == TipoComando.condicion.ToString().ToLower())
                    {
                        try
                        {
                            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
                            bool resultadoScript = (bool)js.ExecuteScript(cmd.Target);

                            if (resultadoScript && cmd?.Command.ToLower() == ResultadoCondicion.error.ToString().ToLower())
                            {
                                throw new Exception($"Cumple condición : {cmd.Target}");
                            }
                            else if (!resultadoScript && cmd?.Command.ToLower() == ResultadoCondicion.error.ToString().ToLower())
                            {
                                test.Pass($"Condicion: {cmd.Target}", _driver.TomarScreen());
                            }

                            if (resultadoScript && cmd?.Command.ToLower() == ResultadoCondicion.ok.ToString().ToLower())
                            {
                                test.Pass($"Condicion: {cmd.Target}", _driver.TomarScreen());
                            }
                            else if (!resultadoScript && cmd?.Command.ToLower() == ResultadoCondicion.ok.ToString().ToLower())
                            {
                                throw new Exception($"No cumple condición {cmd.Target}");
                            }

                        }
                        catch (Exception ex)
                        {
                            test.Fail(ex.Message, _driver.TomarScreen());
                        }
                    }

                    if (screenIteration)
                        test.Pass($"{cmd?.Command} - {cmd?.Target}", _driver.TomarScreen());

                }
                catch (Exception ex)
                {
                    test.Fail($"Error al ejecutar comando: {cmd.Orden}: {cmd.Command} - {ex.Message}", _driver.TomarScreen());
                    throw new Exception($"Error al ejecutar comando: {cmd.Orden}: {cmd.Command}");
                }
            }
        }

        private void PerformAction(IWebElement element, Comando comando, By by)
        {
            switch (comando.Command.ToLower())
            {
                case "type":
                    element.ScrollIntoView();
                    element.SendKeys(comando.Value);
                    break;

                case "click":
                    element.ScrollIntoView();
                    _driver.WaitElementTobeClicked(by, 10);
                    element = _driver.WaitFindElement(by, 5);
                    element.Click();

                    break;
                case "sendkeys":
                case "send keys":
                    element.ScrollIntoView();
                    var key = GetKeyString(comando);
                    element.SendKeys(key);
                    break;

                case "select":
                    element.ScrollIntoView();
                    SelectElement comboCuentaOrigen = new SelectElement(element);
                    var valueImput = comando.Value.Split('=');
                    var tag = valueImput[0];
                    var valor = string.Join("=", valueImput.Skip(1));

                    if (tag == "label")
                        comboCuentaOrigen.SelectByText(valor, true);
                    if (tag == "value")
                        comboCuentaOrigen.SelectByValue(valor);
                    if (tag == "index")
                        comboCuentaOrigen.SelectByIndex(Convert.ToInt32(valor));
                    break;
            }
        }

        public static string GetKeyString(Comando command)
        {
            if (command.Value == SeleniumKeysHelpers.KEY_ENTER)
                return Keys.Enter;

            if (command.Value == SeleniumKeysHelpers.KEY_BACKSPACE || command.Value == SeleniumKeysHelpers.KEY_BKSP)
                return Keys.Backspace;

            if (command.Value == SeleniumKeysHelpers.KEY_DEL || command.Value == SeleniumKeysHelpers.KEY_DELETE)
                return Keys.Delete;

            if (command.Value == SeleniumKeysHelpers.KEY_DOWN)
                return Keys.Down;

            if (command.Value == SeleniumKeysHelpers.KEY_LEFT)
                return Keys.Left;

            if (command.Value == SeleniumKeysHelpers.KEY_PAGE_DOWN || command.Value == SeleniumKeysHelpers.KEY_PGDN)
                return Keys.PageDown;

            if (command.Value == SeleniumKeysHelpers.KEY_PAGE_UP || command.Value == SeleniumKeysHelpers.KEY_PGUP)
                return Keys.PageUp;

            if (command.Value == SeleniumKeysHelpers.KEY_RIGHT)
                return Keys.Right;

            if (command.Value == SeleniumKeysHelpers.KEY_TAB)
                return Keys.Tab;

            if (command.Value == SeleniumKeysHelpers.KEY_UP)
                return Keys.Up;

            if (command.Value == SeleniumKeysHelpers.KEY_ESC)
                return Keys.Escape;

            return string.Empty;
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

        public void GuardarReporte()
        {
            _reporte.GuardarReporte();
        }

    }
}
