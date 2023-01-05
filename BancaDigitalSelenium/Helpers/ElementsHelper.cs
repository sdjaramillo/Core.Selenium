using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaDigitalSelenium.Helpers
{
    public static class ElementsHelper
    {
        public static string GetInnerText(this IWebElement element, bool trim = true)
        {
            string innerText = element.GetAttribute("innerText");
            return trim ? innerText.Trim() : innerText;
        }

        public static bool CheckIfElementExists(this IWebDriver driver, By by)
        {
            var result = driver.FindElements(by);
            return (result.Count > 0);

        }
    }
}
