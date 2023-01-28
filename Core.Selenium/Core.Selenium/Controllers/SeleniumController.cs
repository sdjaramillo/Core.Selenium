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
            LogicTemplate.EjecutarScript(script);
            return Ok();
        }
    }
}
