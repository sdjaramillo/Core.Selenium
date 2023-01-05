using Core.Models.Entidad;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Interface
{
    public interface IBL
    {
        void SetConfig<T>(ScriptBase script, IWebDriver driver);
    }
}
