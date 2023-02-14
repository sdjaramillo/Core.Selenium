using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using static System.Net.Mime.MediaTypeNames;

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
        public static List<IWebElement> WaitFindElements(this IWebDriver driver, By by, int timeout = 10)
        {
            WebDriverWait w = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            var elements = w.Until(ExpectedConditions.ElementExists(by));
            return driver.FindElements(by).ToList();
        }
        public static bool ElementIsVisible(this IWebDriver driver, By by, int timeout = 10)
        {
            try
            {
                WebDriverWait w = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                var element = w.Until(ExpectedConditions.ElementIsVisible(by));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ElementIsEnabled(this IWebDriver driver, By by, int timeout = 10)
        {
            try
            {
                return WaitFindElement(driver, by, timeout).Enabled;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool TextIsPresent(this IWebDriver driver, string text, int timeout = 10)
        {
            try
            {
                var exist = WaitFindElement(driver, By.XPath($"//*[contains(text(),'{text}')]"));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool CheckElementExist(this IWebDriver driver, By by, int timeout = 5)
        {
            try
            {
                WebDriverWait w = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                w.Until(ExpectedConditions.ElementExists(by));
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public static IWebElement FindInsideElement(this IWebDriver driver, string targets, int timeout = 10)
        {
            WebDriverWait w = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));

            IWebElement element = null;
            string[] comandos = targets.Split(';');
            foreach (var cmd in comandos)
            {
                string[] comandoSplit = cmd.Split('=');
                string target = comandoSplit[0];
                string valor = string.Join("=", comandoSplit.Skip(1));

                By by = SeleniumHelpers.ObtenerIdentificador(target, valor);
                element = element == null ? driver.WaitFindElement(by) : element.FindElement(by);
            }
            return element;
        }

        public static IWebElement WaitFindElement(this IWebElement element, By by, int timeout = 5)
        {
            var driver = ((IWrapsDriver)element).WrappedDriver;

            return element.FindElement(by);
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

        public static bool IsClickable(this IWebDriver driver, By by, int timeout = 5)
        {
            try
            {
                WaitElementTobeClicked(driver, by, timeout);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
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

        public static string GetValue(this IWebElement element, bool trim = true)
        {
            try
            {
                var driver = ((IWrapsDriver)element).WrappedDriver;
                var js = (IJavaScriptExecutor)driver;
                var value = js.ExecuteScript($"return arguments[0].value", element);
                Thread.Sleep(200);
                return value?.ToString()?.Trim() ?? "";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener valor {ex.Message}");
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
