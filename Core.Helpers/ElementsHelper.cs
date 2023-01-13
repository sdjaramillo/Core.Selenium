using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace Core.Helpers
{
    /// <summary>
    /// Clase utilitaria para componentes de selenium
    /// </summary>
    public static class ElementsHelper
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

        public static IWebElement WaitFindElement(this IWebDriver driver, By by, int timeout=10)
        {
            WebDriverWait w = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            var element = w.Until(ExpectedConditions.ElementExists(by));
            return element;
        }
    }
}
