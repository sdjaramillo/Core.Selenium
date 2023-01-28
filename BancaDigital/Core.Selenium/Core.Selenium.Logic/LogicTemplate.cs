﻿using Core.Selenium.Helpers;
using Core.Selenium.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Selenium.Logic
{
    public class LogicTemplate
    {
        public static void EjecutarScript(ScriptBase script)
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
                    if (comandos.Count > 0) logica.EjecutarComandos(comandos);

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
                    var comandosFin = script.Comandos.Where(w => w.FinSesion).OrderBy(ord => ord.Orden).ToList();
                    if (comandosFin.Count > 0) logica.EjecutarComandos(comandosFin);

                }
                catch (Exception exPrueba)
                {
                    Console.WriteLine(exPrueba.Message);
                    driver = SeleniumHelpers.GetDriverInstance(script.ScriptData.Driver);
                }
            }

            logica.GuardarReporte();
        }
    }
}