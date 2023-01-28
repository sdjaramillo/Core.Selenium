using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorEngine.Compilation.ImpromptuInterface.Dynamic;

namespace Core.Models.Entidad
{
    /// <summary>
    /// Clase para menejo de reporteria
    /// </summary>
    public class AutoReport
    {
        public ExtentHtmlReporter HtmlReporte;
        public ExtentReports Reporte;

        /// <summary>
        /// Ruta donde se guardara el reporte.
        /// </summary>
        public string PathGuardado;

        public AutoReport(string nombrePrueba, string pathGuardado)
        {
            PathGuardado = !string.IsNullOrEmpty(pathGuardado) ? $@"{PathGuardado}\" : pathGuardado;
            HtmlReporte = new ExtentHtmlReporter(PathGuardado);
            Reporte = new ExtentReports();
            Reporte.AttachReporter(HtmlReporte);
            AgregarInformacionReporte("Fecha ejecución", DateTime.Now.ToShortDateString());
            AgregarInformacionReporte("Sistema", Environment.OSVersion.ToString());
            AgregarInformacionReporte("HostName", Environment.MachineName);
        }

        /// <summary>
        /// Agrega Información al reporte
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="valor"></param>
        public void AgregarInformacionReporte(string nombre, string valor)
        {
            Reporte.AddSystemInfo(nombre, valor);
        }

        public void AgregarInformacionReporte(Dictionary<string, string> info)
        {
            foreach (var i in info)
            {
                AgregarInformacionReporte(i.Key, i.Value);
            }
        }

        /// <summary>
        /// Genera un test sobre la prueba actual
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Metodo para guardar el reporte en la ruta especificada
        /// </summary>
        public void GuardarReporte()
        {
            Reporte.Flush();
        }
    }
}
