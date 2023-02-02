﻿using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Selenium.Report
{
    public class SeleniumReport
    {
        public ExtentHtmlReporter HtmlReporte;
        public ExtentReports Reporte;

        /// <summary>
        /// Ruta donde se guardara el reporte.
        /// </summary>
        public string PathGuardado;

        /// <summary>
        /// Constructor para reporte de seleniu, recibe la ruta de guardado como parámetro.
        /// </summary>
        /// <param name="pathGuardado"></param>
        public SeleniumReport(string pathGuardado)
        {
            PathGuardado = !string.IsNullOrEmpty(pathGuardado) ? $@"{pathGuardado}\" : PathGuardado;
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

        /// <summary>
        /// Agregar información de un diccionario a los datros de prueba
        /// </summary>
        /// <param name="info"></param>
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

        /// <summary>
        /// Metodo para generar un nodo o subseccion de un test
        /// </summary>
        /// <param name="testPadre"></param>
        /// <param name="descripcion"></param>
        /// <returns></returns>
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
