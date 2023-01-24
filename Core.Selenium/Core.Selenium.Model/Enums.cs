using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Selenium.Model
{
    public enum Identificadores
    {
        id,
        name,
        css,
        className,
        xpath,
    }

    public enum TipoComando
    {
        comando,
        script
    }
}
