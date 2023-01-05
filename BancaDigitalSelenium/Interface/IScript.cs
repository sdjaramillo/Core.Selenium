using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancaDigitalSelenium.Interface
{
    public interface IScript
    {
        void SetConfig(IWebDriver driver);
        void Execute();
    }
}
