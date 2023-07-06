using AventStack.ExtentReports;
using Core.Selenium.Helpers;
using Core.Selenium.Model;
using Core.Selenium.Model.Exepciones;
using Core.Selenium.Report;
using MongoDB.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V107.Debugger;
using OpenQA.Selenium.Support.UI;


namespace Core.Selenium.Logic
{
    /// <summary>
    /// Clase encargada de manejar
    /// </summary>
    public class LogicBase
    {
        /// <summary>
        /// Driver Chromium
        /// </summary>
        private IWebDriver _driver { get; set; }

        /// <summary>
        /// Clase para generar reporte de ejecución
        /// </summary>
        private SeleniumReport _reporte { get; set; }

        /// <summary>
        /// Diccionario para variables de ejecución
        /// </summary>
        private Dictionary<string, string> variablesEjecucion { get; set; }

        /// <summary>
        /// Variable para guardar información de tablas.
        /// </summary>
        private Dictionary<string, TablaBase> Tablas { get; set; } = new Dictionary<string, TablaBase>();

        /// <summary>
        /// Variable para escribir en el log del test
        /// </summary>
        private ExtentTest _test;

        /// <summary>
        /// Constructor que recibe instancia de driver
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="testName"></param>
        public LogicBase(IWebDriver driver, string testName)
        {
            _driver = driver;
            _reporte = new SeleniumReport(testName);
            variablesEjecucion = new Dictionary<string, string>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comandosEjecutar"></param>
        /// <param name="test"></param>
        /// <param name="screenIteration"></param>
        /// <param name="testName"></param>
        /// <exception cref="CondicionException"></exception>
        /// <exception cref="Exception"></exception>
        public void EjecutarComandos(List<Comando> comandosEjecutar, ExtentTest test = null, bool screenIteration = true, string? testName = null)
        {
            test = (test == null) ? _reporte.CrearTest($"{testName ?? "test"}") : test.CreateNode(testName ?? "test", "Detalle:");
            _test = test;
            var tiempoInicioPrueba = DateTime.Now;            
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
                        EvaluarScript(cmd);

                    if (cmd?.Tipo?.ToLower() == TipoComando.Condicion.ToLower())
                        EvaluarCondicion(cmd, test);

                    if (screenIteration)
                        test.Pass($"{cmd?.Orden}. {cmd?.Comment} > {cmd?.Tipo} > {cmd?.Command} : {cmd?.Target}", _driver.TomarScreen());
                    
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
                    AgregarVariablesEjecucion(test, variablesEjecucion);
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
            var tiempoFinPrueba = DateTime.Now;            
            test.Info($"Tiempo Prueba:{(tiempoFinPrueba - tiempoInicioPrueba).TotalSeconds}");
            AgregarVariablesEjecucion(test, variablesEjecucion);
        }

        /// <summary>
        /// Agrega variables de la ejecución
        /// </summary>
        /// <param name="test"></param>
        /// <param name="variablesEjecucion"></param>
        private void AgregarVariablesEjecucion(ExtentTest test, Dictionary<string, string> variablesEjecucion)
        {
            if (!test.Model.IsChild && variablesEjecucion.Count>0)
            {
                test.AgregarDatosEjecucion(variablesEjecucion, "Variables Recuperadas");
            }
        }

        /// <summary>
        /// Metodo encargado de manejar los comandos de tipo JS
        /// </summary>
        /// <param name="cmd"></param>
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

        /// <summary>
        /// Metodo para evaluar los comandos de tipo condición
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="test"></param>
        /// <exception cref="CondicionException"></exception>
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

                        int timeoutClickable = 0;
                        timeoutClickable = int.TryParse(cmd.Value, out timeoutClickable) ? timeoutClickable : 5;

                        var esclickeable = _driver.IsClickable(splitTargetClick.By);

                        test.Info($"Resultado condición \"{cmd.Comment}\": {esclickeable}");
                        EvaluarResultadoCondicion(esclickeable, cmd, test);

                        break;

                    case Condiciones.IsVisible:
                        var splitTarget = SplitTarget.GetSplitTarget(cmd.Target);
                        splitTarget.By = SeleniumHelpers.ObtenerIdentificador(splitTarget.Identificador, splitTarget.Valor);
                        int timeoutVisible;
                        timeoutVisible = int.TryParse(cmd.Value, out timeoutVisible) ? timeoutVisible : 10;

                        var esVivisble = _driver.ElementIsVisible(splitTarget.By, timeoutVisible);

                        test.Info($"Resultado condición \"{cmd.Comment}\": {esVivisble}");
                        EvaluarResultadoCondicion(esVivisble, cmd, test);
                        break;

                    case Condiciones.Exist:
                        var splitTargetExist = SplitTarget.GetSplitTarget(cmd.Target);
                        splitTargetExist.By = SeleniumHelpers.ObtenerIdentificador(splitTargetExist.Identificador, splitTargetExist.Valor);
                        var existe = _driver.CheckElementExist(splitTargetExist.By);
                        EvaluarResultadoCondicion(existe, cmd, test);
                        break;

                    case Condiciones.IsEnabled:
                        var splitTargetEnabled = SplitTarget.GetSplitTarget(cmd.Target); ;
                        splitTargetEnabled.By = SeleniumHelpers.ObtenerIdentificador(splitTargetEnabled.Identificador, splitTargetEnabled.Valor);
                        int timeOutEnabled;
                        timeOutEnabled = int.TryParse(cmd.Value, out timeOutEnabled) ? timeOutEnabled : 10;
                        bool esEnabled = _driver.ElementIsEnabled(splitTargetEnabled.By, timeOutEnabled);
                        test.Info($"Resultado Condición \"{cmd.Comment}\": {esEnabled}");
                        EvaluarResultadoCondicion(esEnabled, cmd, test);
                        break;

                    case Condiciones.TextIsPresent:
                        int timeOutText = 0;
                        timeOutText = int.TryParse(cmd.Value, out timeOutText) ? timeOutText : 10;
                        bool textoPresente = _driver.TextIsPresent(cmd.Target);
                        test.Info($"Resultado Condición \"{cmd.Comment}\" '{cmd.Target}': {textoPresente}");
                        EvaluarResultadoCondicion(textoPresente, cmd, test);
                        break;
                    case Condiciones.Table:

                        var tabla = Tablas[cmd.Target];
                        bool resultadoCondicionTabla = EvaluarCondicionTabla(tabla, cmd, test);
                        EvaluarResultadoCondicion(resultadoCondicionTabla, cmd, test);
                        break;

                    default:
                        throw new Exception($"Instrucción no reconocida: {cmd?.Command} - {cmd?.Tipo}");
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
        /// Metodo para evaluar los comandos de tipo tabla
        /// </summary>
        /// <param name="tabla"></param>
        /// <param name="comando"></param>
        /// <param name="test"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private bool EvaluarCondicionTabla(TablaBase tabla, Comando comando, ExtentTest test)
        {
            var parametros = comando.Value.Split(',');
            var comandoTabla = parametros[0];

            switch (comandoTabla)
            {
                case "rowCount":
                    var operador = parametros[1];
                    var valor = parametros[2];

                    if (operador.Equals("="))
                    {
                        return tabla.TotalFilas == int.Parse(valor);
                    }

                    if (operador.Equals(">"))
                    {
                        return tabla.TotalFilas > int.Parse(valor);
                    }

                    if (operador.Equals("<"))
                    {
                        return tabla.TotalFilas < int.Parse(valor);
                    }

                    break;

                case "containcolumns":
                    var columnasBuscadas = parametros.Skip(1);
                    foreach (var fila in tabla.Filas)
                    {
                        var keys = fila.Select(s => s.Key);
                        var num = columnasBuscadas.Except(keys).Count();
                        string rowInfo = string.Empty;
                        fila.ToList().ForEach(f =>
                        {
                            rowInfo += $" {f.Key}:{f.Value}";

                        });
                        if (num > 0)
                        {
                            return false;
                        }
                        test.Pass(rowInfo);
                    }
                    return true;
                    break;

            }

            throw new Exception("Comando condición tabla no especificado");
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
                EjecutarComandos(comando.ComandosVerdadero, test: test, testName: $"Condición: {comando.Orden}. {comando.Comment}: {resultado}");
            }

            if (!resultado && comando?.ComandosFalso?.Count > 0)
            {
                EjecutarComandos(comando.ComandosFalso, test: test, testName: $"Condición: {comando.Comment}: {resultado}");
            }
        }

        /// <summary>
        /// Ejecuta comandos
        /// </summary>
        /// <param name="comando"></param>
        /// <param name="by"></param>
        /// <param name="test"></param>
        /// <exception cref="Exception"></exception>
        /// <exception cref="EjecucionTerminadaException"></exception>
        private void PerformAction(Comando comando, By by, ExtentTest test)
        {
            IWebElement element;
            switch (comando.Command.ToLower())
            {
                case Acciones.Maximizar:
                    _driver.Manage().Window.Maximize();
                    break;

                case Acciones.Type:
                    element = _driver.WaitFindElement(by);
                    element.ScrollIntoView();
                    element.SendKeys(comando.Value);
                    break;

                case Acciones.Click:
                    element = _driver.WaitFindElement(by);
                    element.ScrollIntoView();
                    _driver.WaitElementTobeClicked(by, 10);
                    element = _driver.WaitFindElement(by, 5);
                    element.Click();

                    break;
                case Acciones.SendKeys:
                case "send keys":
                    element = _driver.WaitFindElement(by);
                    element.ScrollIntoView();
                    var key = SeleniumKeysHelpers.GetKeyString(comando);
                    element.SendKeys(key);
                    break;

                case Acciones.Select:
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

                case Acciones.Open:
                    _driver.Url = comando.Target;
                    break;

                case Acciones.Screen:
                    test?.Info(comando.Target, _driver.TomarScreen());
                    break;

                case Acciones.AssertAlert:
                    
                    var textoAlerta = _driver.AssertAlert();
                    if (!textoAlerta.Contains(comando.Target))
                    {
                        throw new Exception($"alerta {comando.Target} no encontrada");
                    }
                    break;

                case Acciones.CheckAlert:

                    _driver.checkAlert(3);

                    break;

                case Acciones.GetText:
                    element = _driver.WaitFindElement(by);
                    var text = element.GetInnerText();
                    AddVarEjecution(comando.Value, text);
                    test.Info($"{comando.Orden}. Variable recuperada: {comando.Value} = {text}");
                    break;

                case Acciones.GetValue:
                    element = _driver.WaitFindElement(by);
                    var valorElemento = element.GetValue();
                    AddVarEjecution(comando.Value, valorElemento);
                    test.Info($"{comando.Orden}. Variable recuperada: {comando.Value} = {valorElemento}");
                    break;

                case Acciones.GetTextLength:
                    element = _driver.WaitFindElement(by);
                    var longitudText = element.GetInnerText().Length.ToString();
                    AddVarEjecution(comando.Value, longitudText);
                    test.Info($"{comando.Orden}. Variable recuperada: {comando.Value} = {longitudText}");
                    break;

                case Acciones.GetValueLength:
                    element = _driver.WaitFindElement(by);
                    var longitudValor = element.GetValue().Length.ToString();
                    AddVarEjecution(comando.Value, longitudValor);
                    test.Info($"{comando.Orden}. Variable recuperada: {comando.Value} = {longitudValor}");
                    break;

                case Acciones.GetIsVisible:

                    var isVisible = _driver.ElementIsVisible(by);
                    AddVarEjecution(comando.Value, isVisible.ToString().ToLower());
                    test.Info($"{comando.Orden}. Variable recuperada: {comando.Value} = {isVisible.ToString().ToLower()}");

                    break;

                case Acciones.Wait:
                    int tiempo;
                    if (int.TryParse(comando.Target, out tiempo))
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(tiempo));
                    }
                    break;

                case Acciones.FindClick:

                    var elementoClick = _driver.FindElements(by).FirstOrDefault(w => w.GetInnerText().Contains(comando.Value));
                    elementoClick.ScrollIntoView();
                    elementoClick.Click();
                    break;

                case Acciones.TerminarSinError:
                    test.Pass($"{Acciones.TerminarSinError} : {comando.Target}", _driver.TomarScreen());
                    throw new EjecucionTerminadaException(comando.Target, false);

                case Acciones.TerminarConError:
                    test.Pass($"{Acciones.TerminarConError} : {comando.Target}", _driver.TomarScreen());
                    throw new EjecucionTerminadaException(comando.Target, true);

                case Acciones.InformacionTabla:

                    var tabla = new TablaBase();

                    var tablaElement = _driver.WaitFindElement(by);
                    var columnas = tablaElement.WaitFindElement(By.TagName("thead")).FindElements(By.TagName("th"));
                    var filas = tablaElement.WaitFindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));

                    foreach (var col in columnas)
                    {
                        if (!string.IsNullOrEmpty(col.GetInnerText()))
                            tabla.Columnas.Add(col.GetInnerText());
                    }

                    foreach (var row in filas)
                    {
                        var rows = new Dictionary<string, string>();
                        var celdas = row.FindElements(By.TagName("td"));
                        for (int i = 0; i < tabla.TotalColumnas; i++)
                        {
                            string nombreColumna = tabla.Columnas[i];
                            string valorCelda = celdas[i].GetInnerText();

                            rows.Add(nombreColumna, valorCelda);
                        }
                        tabla.Filas.Add(rows);
                    }

                    if (Tablas.ContainsKey(comando.Value))                    
                        Tablas[comando.Value] = tabla;                    
                    else                    
                        Tablas.Add(comando.Value, tabla);                    

                    break;

                default:
                    throw new Exception($"Instrucción no reconozida: {comando.Command} - {comando.Tipo}");
            }
        }

        /// <summary>
        /// Guarda reporte de ejecución de pruebas
        /// </summary>
        public void GuardarReporte()
        {
            _reporte.GuardarReporte();
        }

        /// <summary>
        /// Agrega varaibles de ejecución
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
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

        /// <summary>
        /// Limpia las varaibles de ejecución
        /// </summary>
        public void LimpiarVaraiblesEjecucion()
        {
            this.variablesEjecucion = new Dictionary<string, string>();
        }

        public void EscribirLog(Status status, string mensaje)
        {
            _test.Log(status, mensaje);
        }
    }
}
