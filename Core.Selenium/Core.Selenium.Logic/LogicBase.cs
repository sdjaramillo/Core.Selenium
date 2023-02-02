using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using Core.Selenium.Helpers;
using Core.Selenium.Model;
using Core.Selenium.Model.Exepciones;
using Core.Selenium.Report;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MongoDB.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V107.Debugger;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Core.Selenium.Logic
{
    public class LogicBase
    {
        private IWebDriver _driver { get; set; }
        private SeleniumReport _reporte { get; set; }
        private Dictionary<string, string> variablesEjecucion { get; set; }
        public LogicBase(IWebDriver driver, string testName)
        {
            _driver = driver;
            _reporte = new SeleniumReport(testName);
            variablesEjecucion = new Dictionary<string, string>();
        }

        public void EjecutarComandos(List<Comando> comandosEjecutar, ExtentTest test = null, bool screenIteration = true, string? testName = null)
        {
            test = (test == null) ? _reporte.CrearTest($"{testName ?? "test"}") : test.CreateNode(testName ?? "test");

            foreach (var cmd in comandosEjecutar)
            {
                try
                {
                    cmd.Target = cmd.Target.Inject(dictionary: variablesEjecucion);
                    cmd.Value = cmd.Value.Inject(dictionary: variablesEjecucion);
                    if (cmd.Tipo == null || cmd?.Tipo?.ToLower() == TipoComando.Comando.ToLower())
                    {
                        var splitTarget = SplitTarget.GetSplitTarget(cmd.Target);
                        By by = SeleniumHelpers.ObtenerIdentificador(splitTarget.Identificador, splitTarget.Valor);
                        PerformAction(cmd, by, test);
                    }

                    if (cmd?.Tipo?.ToLower() == TipoComando.Script.ToLower())
                    {
                        EvaluarScript(cmd);
                    }

                    if (cmd?.Tipo?.ToLower() == TipoComando.Condicion.ToLower())
                    {
                        EvaluarCondicion(cmd, test);
                    }

                    if (screenIteration)
                        test.Pass($"{cmd?.Orden} {cmd?.Tipo} {cmd?.Command} - {cmd?.Target}", _driver.TomarScreen());

                }
                catch (EjecucionTerminadaException ex)
                {
                    if (ex.TerminarConError)
                    {
                        test.Fail(ex.Message);
                    }
                    else
                    {
                        test.Pass(ex.Message);
                    }
                    throw ex;
                }
                catch (CondicionException ex)
                {
                    test.Fail(ex.Message, _driver.TomarScreen());
                    throw new CondicionException(ex.Message);
                }
                catch (Exception ex)
                {
                    test.Fail($"Error al ejecutar comando: {cmd.Orden}: {cmd.Command} {cmd.Target} - {ex.Message}", _driver.TomarScreen());
                    throw new Exception($"Error al ejecutar comando: {cmd.Orden}: {cmd.Command}");
                }
            }

            test.AgregarDatosEjecucion(variablesEjecucion, "Variables Recuperadas");
        }

        private void EvaluarScript(Comando cmd)
        {
            var js = (IJavaScriptExecutor)_driver;
            switch (cmd.Command)
            {
                case AccionesJS.SaveVar:

                    var resultado = js.ExecuteScript(cmd.Target);
                    string stringResult = resultado?.ToString() ?? "";
                    if (resultado is bool)
                    {
                        stringResult = resultado?.ToString()?.ToLower() ?? string.Empty;
                    }
                    AddVarEjecution(cmd.Value, stringResult);

                    break;

                default:
                    js.ExecuteScript(cmd.Target);
                    break;
            }
        }

        private void EvaluarCondicion(Comando cmd, ExtentTest test)
        {
            try
            {
                switch (cmd?.Command?.ToLower())
                {
                    case Condiciones.Js:
                        IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
                        var targetInject = cmd.Target.Inject(dictionary: variablesEjecucion);
                        bool resultadoScript = (bool)js.ExecuteScript(cmd.Target.Inject(dictionary: variablesEjecucion));
                        test.Info($"{targetInject}");
                        test.Info($"Resultado condicion {cmd.Comment}: {resultadoScript}");
                        EvaluarResultadoCondicion(resultadoScript, cmd, test);

                        break;
                    case Condiciones.Clickable:
                        var splitTargetClick = SplitTarget.GetSplitTarget(cmd.Target);
                        splitTargetClick.By = SeleniumHelpers.ObtenerIdentificador(splitTargetClick.Identificador, splitTargetClick.Valor);

                        var esclickeable = _driver.IsClickable(splitTargetClick.By);
                        EvaluarResultadoCondicion(esclickeable, cmd, test);

                        break;

                    case Condiciones.IsVisible:
                        var splitTarget = SplitTarget.GetSplitTarget(cmd.Target);
                        splitTarget.By = SeleniumHelpers.ObtenerIdentificador(splitTarget.Identificador, splitTarget.Valor);
                        int timeoutVisible;
                        if (int.TryParse(cmd.Value, out timeoutVisible))
                        {
                            var esVivisble = _driver.ElementIsVisible(splitTarget.By, timeoutVisible);
                            EvaluarResultadoCondicion(esVivisble, cmd, test);
                        }
                        else
                        {
                            var esVivisble = _driver.ElementIsVisible(splitTarget.By);
                            EvaluarResultadoCondicion(esVivisble, cmd, test);
                        }

                        break;

                    case Condiciones.Exist:
                        var splitTargetExist = SplitTarget.GetSplitTarget(cmd.Target);
                        splitTargetExist.By = SeleniumHelpers.ObtenerIdentificador(splitTargetExist.Identificador, splitTargetExist.Valor);
                        var existe = _driver.CheckElementExist(splitTargetExist.By);
                        EvaluarResultadoCondicion(existe, cmd, test);
                        break;
                }
            }
            catch (EjecucionTerminadaException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CondicionException(ex.Message);
            }
        }

        /// <summary>
        /// Metodo para evaluar y ejecutar comandos
        /// </summary>
        /// <param name="resultado"></param>
        /// <param name="accionCondicion"></param>
        private void EvaluarResultadoCondicion(bool resultado, Comando comando, ExtentTest test)
        {
            if (resultado && comando?.ComandosVerdadero?.Count > 0)
            {
                EjecutarComandos(comando.ComandosVerdadero, test: test, testName: $"Condición: {comando.Comment}");
            }

            if (!resultado && comando?.ComandosFalso?.Count > 0)
            {
                EjecutarComandos(comando.ComandosFalso, test: test, testName: $"Condición: {comando.Comment}");
            }
        }

        private void PerformAction(Comando comando, By by, ExtentTest test)
        {
            IWebElement element;
            switch (comando.Command.ToLower())
            {
                case "type":
                    element = _driver.WaitFindElement(by);
                    element.ScrollIntoView();
                    element.SendKeys(comando.Value);
                    break;

                case "click":
                    element = _driver.WaitFindElement(by);
                    element.ScrollIntoView();
                    _driver.WaitElementTobeClicked(by, 10);
                    element = _driver.WaitFindElement(by, 5);
                    element.Click();

                    break;
                case "sendkeys":
                case "send keys":
                    element = _driver.WaitFindElement(by);
                    element.ScrollIntoView();
                    var key = SeleniumKeysHelpers.GetKeyString(comando);
                    element.SendKeys(key);
                    break;

                case "select":
                    element = _driver.WaitFindElement(by);
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

                case "open":
                    _driver.Url = comando.Target;
                    break;

                case "screen":
                    test?.Pass(comando.Target, _driver.TomarScreen());
                    break;

                case "assertalert":

                    //Make sure the click above generates the alert
                    var textoAlerta = _driver.AssertAlert();
                    if (!textoAlerta.Contains(comando.Target))
                    {
                        throw new Exception($"alerta {comando.Target} no encontrada");
                    }
                    break;

                case Acciones.CheckAlert:

                    _driver.checkAlert(3);

                    break;

                case "gettext":
                    element = _driver.WaitFindElement(by);
                    var text = element.GetInnerText();
                    AddVarEjecution(comando.Value, text);
                    test.Pass($"{text} = {comando.Value}");
                    break;

                case Acciones.GetValue:
                    element = _driver.WaitFindElement(by);
                    var valorElemento = element.GetValue();
                    AddVarEjecution(comando.Value, valorElemento);
                    break;

                case Acciones.GetTextLength:
                    element = _driver.WaitFindElement(by);
                    var longitudText = element.GetInnerText().Length.ToString();
                    AddVarEjecution(comando.Value, longitudText);
                    test.Pass($"{comando.Value}:{longitudText}");
                    break;

                case Acciones.GetValueLength:
                    element = _driver.WaitFindElement(by);
                    var longitudValor = element.GetValue().Length.ToString();
                    AddVarEjecution(comando.Value, longitudValor);
                    test.Pass($"{comando.Value}:{longitudValor}");
                    break;

                case Acciones.GetIsVisible:

                    var isVisible = _driver.ElementIsVisible(by);
                    AddVarEjecution(comando.Value, isVisible.ToString().ToLower());

                    break;

                case "wait":
                    int tiempo;
                    if (int.TryParse(comando.Target, out tiempo))
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(tiempo));
                    }
                    break;



                case Acciones.TerminarSinError:
                    test.Pass($"{Acciones.TerminarSinError} : {comando.Target}",_driver.TomarScreen());
                    throw new EjecucionTerminadaException(comando.Target, false);

                case Acciones.TerminarConError:
                    test.Pass($"{Acciones.TerminarConError} : {comando.Target}", _driver.TomarScreen());
                    throw new EjecucionTerminadaException(comando.Target, true);


            }
        }

        public void GuardarReporte()
        {
            _reporte.GuardarReporte();
        }


        private void AddVarEjecution(string key, string value)
        {
            if (variablesEjecucion.ContainsKey(key))
            {
                variablesEjecucion[key] = value;
            }
            else
            {
                variablesEjecucion.Add(key, value);
            }
        }

        public void LimpiarVaraiblesEjecucion()
        {
            this.variablesEjecucion = new Dictionary<string, string>();
        }
    }
}
