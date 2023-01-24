using Core.Selenium.Helpers;
using Core.Selenium.Logic;
using Core.Selenium.Model;
using Core.Selenium.Report;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;

namespace Core.Selenium.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeleniumController : ControllerBase
    {
        [HttpPost]
        public IActionResult EjecutarScriptSesion([FromBody] ScriptBase script)
        {
            var driver = SeleniumHelpers.GetDriverInstance(script.ScriptData.Driver);
            driver.Url = script.ScriptData.Url;
            string jsonCommands = Newtonsoft.Json.JsonConvert.SerializeObject(script.Comandos);
            var logica = new LogicBase(driver, script.Nombre);

            foreach (var data in script.ScriptData.DataTest)
            {
                try
                {
                    var listaComandos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Comando>>(jsonCommands);
                    var comandos = listaComandos.Where(w => w.InicioSesion).OrderBy(ord => ord.Orden).ToList();
                    comandos.ForEach(f =>
                    {
                        f.Value = f.Value.Inject(dictionary: data.SuiteVars);
                        f.Target = f.Target.Inject(dictionary: data.SuiteVars);
                    });
                    logica.EjecutarComandos(comandos);

                    foreach (var test in data.TestsVars)
                    {
                        try
                        {
                            var variables = test;
                            var ListacomandosIterar = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Comando>>(jsonCommands);
                            var comandosIterar = ListacomandosIterar?.Where(w => w.Iterar).OrderBy(ord => ord.Orden).ToList();
                            comandosIterar?.ForEach(f =>
                            {
                                f.Value = f.Value.Inject(dictionary: test);
                                f.Target = f.Target.Inject(dictionary: test);
                            });

                            logica.EjecutarComandos(comandosIterar);
                        }
                        catch (Exception exTest)
                        {
                            Console.WriteLine(exTest.Message);
                        }
                    }
                    var comandosFin = script.Comandos.Where(w => w.FinSesion).OrderBy(ord => ord.Orden);
                }
                catch (Exception exPrueba)
                {
                    Console.WriteLine(exPrueba.Message);
                    driver = SeleniumHelpers.GetDriverInstance(script.ScriptData.Driver);
                }
            }

            logica.GuardarReporte();

            return Ok();
        }
    }
}
