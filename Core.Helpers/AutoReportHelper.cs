using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    /// <summary>
    /// Clase utilitaria para los objetos de tipo reporte.
    /// </summary>
    public static class AutoReportHelper
    {
        /// <summary>
        /// Agrega como información los datos que sean enviados
        /// </summary>
        /// <param name="test"></param>
        /// <param name="infoEjecucion"></param>
        /// <param name="label"></param>
        public static void AgregarDatosEjecucion(this ExtentTest test, Dictionary<string, string> infoEjecucion, string label = "")
        {
            foreach (var info in infoEjecucion)
            {
                test.Info($"{info.Key} {info.Value}");
            }
        }

        public static void AgregarInformacionParametro<T>(this ExtentTest test, object data)
        {

            var propiedades = data.GetType().GetProperties();

            foreach (var pro in propiedades)
                test.Info($"{pro.Name}: {pro.GetValue(data)}");
        }
    }
}
