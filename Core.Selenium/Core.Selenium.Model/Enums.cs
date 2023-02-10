using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Selenium.Model
{
    public static class Identificadores
    {
        public const string Id = "id";
        public const string Name = "name";
        public const string Css = "css";
        public const string ClassName = "className";
        public const string xpath = "xpath";
    }

    public static class TipoComando
    {
        public const string Comando = "comando";
        public const string Script = "script";
        public const string Condicion = "condicion";
    }

    public static class ResultadoCondicion
    {
        public const string Error = "error";
        public const string Ok = "ok";
    }

    public static class Acciones
    {
        public const string Type = "type";
        public const string Click = "click";
        public const string SendKeys = "sendkeys";
        public const string Select = "select";
        public const string Open = "open";
        public const string CheckAlert = "checkalert";
        public const string Screen = "screen";
        public const string GetText = "gettext";
        public const string GetValue = "getvalue";
        public const string Exists = "exists";
        public const string IsVisible = "isvisible";
        public const string GetValueLength = "getvaluelength";
        public const string GetTextLength = "gettextlength";
        public const string GetIsVisible = "getisvisible";
        public const string TerminarConError = "terminarconerror";
        public const string TerminarSinError = "terminarsinerror";
        public const string AssertAlert = "assertalert";
        public const string Wait = "wait";
    }

    public static class Condiciones
    {
        public const string Js = "js";
        public const string Clickable = "clickable";
        public const string IsVisible = "isvisible";
        public const string Exist = "exist";
        public const string IsEnabled = "isenabled";
        public const string TextIsPresent = "textispresent";
    }

    public static class AccionesJS
    {
        public const string SaveVar = "savevar";
    }
}
