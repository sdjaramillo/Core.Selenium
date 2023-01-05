using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaDigitalSelenium.Class
{
    public class AutoReport
    {
        public ExtentHtmlReporter HtmlReporte;
        public ExtentReports Reporte;

        public string PathGuardado;

        public AutoReport(string nombrePrueba)
        {
            PathGuardado = nombrePrueba;
            HtmlReporte = new ExtentHtmlReporter(PathGuardado);
            Reporte = new ExtentReports();
            Reporte.AttachReporter(HtmlReporte);
            AgregarInformacionReprote("Fecha ejecución", DateTime.Now.ToShortDateString());
            AgregarInformacionReprote("Sistema", Environment.OSVersion.ToString());
            AgregarInformacionReprote("HostName", Environment.MachineName);
        }

        public void AgregarInformacionReprote(string nombre, string valor)
        {
            Reporte.AddSystemInfo(nombre, valor);
        }

        public void AgregarInformacionReprote(Dictionary<string, string> info)
        {
            foreach (var i in info)
            {
                AgregarInformacionReprote(i.Key, i.Value);
            }
        }

        public ExtentTest CrearTest(string descripcion)
        {
            ExtentTest test = Reporte.CreateTest(descripcion);
            return test;
        }

        public static ExtentTest CrearNodo(ExtentTest testPadre, string descripcion)
        {
            var nodo = testPadre.CreateNode(descripcion);
            return nodo;
        }

        public void GuardarReporte()
        {
            Reporte.Flush();
        }
    }
}
