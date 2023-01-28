using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RazorEngine.Compilation.ImpromptuInterface.Dynamic;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Selenium.Helpers
{
    public static class ElementHelper
    {
        /// <summary>
        /// Metodo para obtener el innerText de un componente
        /// </summary>
        /// <param name="element"></param>
        /// <param name="trim"></param>
        /// <returns></returns>
        public static string GetInnerText(this IWebElement element, bool trim = true)
        {
            string innerText = element.GetAttribute("innerText");
            return trim ? innerText.Trim() : innerText;
        }

        /// <summary>
        /// Metodo para verificar si un elemento existe
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <returns></returns>
        public static bool CheckIfElementExists(this IWebDriver driver, By by)
        {
            var result = driver.FindElements(by);
            return (result.Count > 0);

        }

        /// <summary>
        /// Metodo para tomar un scrin
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static MediaEntityModelProvider TomarScreen(this IWebDriver driver)
        {
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            string screenshot = ss.AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build();
        }
        public static string TomarScreen64(this IWebDriver driver)
        {
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            return ss.AsBase64EncodedString;
        }

        public static IWebElement FluentWait(this IWebDriver driver, By by, int timeout = 30)
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(timeout);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(250);
            fluentWait.IgnoreExceptionTypes(typeof(OpenQA.Selenium.NoSuchElementException));

            IWebElement elemento = fluentWait.Until(x => x.FindElement(by));

            return elemento;
        }

        /// <summary>
        /// Metodo para esperar que un elemento sea visible o aparezca en la página.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static IWebElement WaitFindElement(this IWebDriver driver, By by, int timeout = 10)
        {
            WebDriverWait w = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            var element = w.Until(ExpectedConditions.ElementExists(by));
            return element;
        }

        /// <summary>
        /// Metodo para esperar hasta que un elemento pueda ser clickeado
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static IWebElement WaitElementTobeClicked(this IWebDriver driver, By by, int timeout = 10)
        {
            WebDriverWait w = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            w.Until(ExpectedConditions.ElementToBeClickable(by));

            return WaitFindElement(driver, by, timeout);
        }


        public static void checkAlert(this IWebDriver driver, int timeout = 1)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
                wait.Until(ExpectedConditions.AlertIsPresent());
                var alert = driver.SwitchTo().Alert();
                alert.Accept();
            }
            catch (Exception e)
            {
                //exception handling
            }
        }

        public static string AssertAlert(this IWebDriver driver, int timeout = 20)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                wait.Until(ExpectedConditions.AlertIsPresent());
                var alert = driver.SwitchTo().Alert();
                var textoAlerta = alert.Text;
                alert.Accept();
                return textoAlerta;
            }
            catch (Exception ex)
            {
                return $"No se encontro alerta :{ex.Message}";
            }
        }

        public static void ScrollIntoView(this IWebElement element, int wait = 1000)
        {
            try
            {
                var driver = ((IWrapsDriver)element).WrappedDriver;
                var js = (IJavaScriptExecutor)driver;
                js.ExecuteScript($"arguments[0].scrollIntoView(true);", element);
                Thread.Sleep(wait);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al mover al objeto");
            }
        }

        public static void SetValue(this IWebElement element, string value)
        {
            try
            {
                var driver = ((IWrapsDriver)element).WrappedDriver;
                var js = (IJavaScriptExecutor)driver;
                js.ExecuteScript($"arguments[0].value='{value}'", element);
                Thread.Sleep(200);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al mover al objeto");
            }
        }

        //public static void FindTable(this IWebDriver driver, By by, int? indexColumna = null, string valueToFind = "", string tagRow = "tr", string tagColumn = "td", ElementAction accion = ElementAction.Click, By byInsideColumn = null)
        //{
        //    var tabla = driver.FindElement(by);
        //    List<IWebElement> filas = tabla.FindElements(By.TagName(tagRow)).ToList();
        //    foreach (IWebElement row in filas)
        //    {
        //        List<IWebElement> columnas = row.FindElements(By.TagName(tagColumn)).ToList();

        //        if (indexColumna != null)
        //            columnas = new List<IWebElement> { columnas[(int)indexColumna] };

        //        foreach (IWebElement col in columnas)
        //        {
        //            IWebElement element = col;
        //            string valor = element.GetInnerText();

        //            if (byInsideColumn != null)
        //            {
        //                element = element.FindElement(byInsideColumn);
        //            }

        //        }
        //    }
        //}

    }
}
