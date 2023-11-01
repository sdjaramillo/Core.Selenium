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
    public static class LogicTemplate
    {
        /// <summary>
        /// Plantilla base para ejecución de script de automatización
        /// </summary>
        /// <param name="script">script con los comandos y datos de prueba</param>
        public static void EjecutarScript(ScriptBase script)
        {
            IWebDriver driver;
            LogicBase logica = null;
            
            foreach (var data in script.ScriptData.DataTest)
            {
                try
                {
                    driver = SeleniumHelpers.GetDriverInstance(script.ScriptData.Driver, script.ScriptData.ParametrosDriver);
                    driver.Url = string.IsNullOrEmpty(script.ScriptData.Url) ? driver.Url : script.ScriptData.Url;
                    string jsonCommands = Newtonsoft.Json.JsonConvert.SerializeObject(script.Comandos);
                    logica = new LogicBase(driver, data.NombrePrueba);

                    var listaComandos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Comando>>(jsonCommands);
                    var comandos = listaComandos.Where(w => w.InicioSesion).OrderBy(ord => ord.Orden).ToList();
                    InyectarVariables(comandos, data.SuiteVars);

                    if (comandos.Count > 0)
                    {                        
                        logica.EjecutarComandos(comandos, testName: data.NombrePrueba);                                             
                    }

                    foreach (var test in data.TestsVars)
                    {
                        try
                        {
                            logica.LimpiarVaraiblesEjecucion();
                            var variables = test;
                            var ListacomandosIterar = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Comando>>(jsonCommands);
                            var comandosIterar = ListacomandosIterar?.Where(w => w.Iterar).OrderBy(ord => ord.Orden).ToList();
                            InyectarVariables(comandosIterar, data.SuiteVars);
                            InyectarVariables(comandosIterar, test);

                            if (comandosIterar?.Count > 0) logica.EjecutarComandos(comandosIterar, testName: data.NombrePrueba);
                        }
                        catch (Exception exTest)
                        {
                            Console.WriteLine(exTest.Message);
                        }
                    }
                    var comandosFin = script.Comandos.Where(w => w.FinSesion).OrderBy(ord => ord.Orden).ToList();
                    if (comandosFin.Count > 0) logica.EjecutarComandos(comandosFin);
                                        
                }
                catch (Exception exPrueba)
                {
                    Console.WriteLine(exPrueba.Message);
                }
            }
            logica?.GuardarReporte();
        }

        /// <summary>
        /// Metodo que ayuda a inyectar varaibles de ejecución en los comandos
        /// </summary>
        /// <param name="comandos"></param>
        /// <param name="variables"></param>
        private static void InyectarVariables(List<Comando> comandos, Dictionary<string, string> variables)
        {
            comandos.ForEach(f =>
            {
                f.Value = f.Value.Inject(dictionary: variables);
                f.Target = f.Target.Inject(dictionary: variables);
                if (f.ComandosVerdadero?.Count > 0)
                {
                    InyectarVariables(f.ComandosVerdadero, variables);
                }

                if (f.ComandosFalso?.Count > 0)
                {
                    InyectarVariables(f.ComandosFalso, variables);
                }
            });
        }
    }
}