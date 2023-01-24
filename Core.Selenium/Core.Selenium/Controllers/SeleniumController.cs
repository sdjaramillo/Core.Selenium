using Core.Selenium.Helpers;
using Core.Selenium.Logic;
using Core.Selenium.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

            foreach (var data in script.ScriptData.DataTest)
            {
                try
                {
                    var logica = new LogicBase(driver);

                    var variablesSuite = data.SuiteVars;
                    var comandos = script.Comandos.Where(w => w.InicioSesion).OrderBy(ord => ord.Orden).ToList();

                    logica.EjecutarComandos(comandos);

                    foreach (var test in data.TestsVars)
                    {
                        try
                        {
                            var variables = test;
                            var comandosIterar = comandos.Where(w => w.Iterar).OrderBy(ord => ord.Orden);
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

            return Ok();
        }
    }
}
