using Core.Selenium.Model;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Selenium.Helpers
{
    public static class SeleniumKeysHelpers
    {
        public static string GetKeyString(Comando command)
        {
            if (command.Value == SeleniumKeysHelpers.KEY_ENTER)
                return Keys.Enter;

            if (command.Value == SeleniumKeysHelpers.KEY_BACKSPACE || command.Value == SeleniumKeysHelpers.KEY_BKSP)
                return Keys.Backspace;

            if (command.Value == SeleniumKeysHelpers.KEY_DEL || command.Value == SeleniumKeysHelpers.KEY_DELETE)
                return Keys.Delete;

            if (command.Value == SeleniumKeysHelpers.KEY_DOWN)
                return Keys.Down;

            if (command.Value == SeleniumKeysHelpers.KEY_LEFT)
                return Keys.Left;

            if (command.Value == SeleniumKeysHelpers.KEY_PAGE_DOWN || command.Value == SeleniumKeysHelpers.KEY_PGDN)
                return Keys.PageDown;

            if (command.Value == SeleniumKeysHelpers.KEY_PAGE_UP || command.Value == SeleniumKeysHelpers.KEY_PGUP)
                return Keys.PageUp;

            if (command.Value == SeleniumKeysHelpers.KEY_RIGHT)
                return Keys.Right;

            if (command.Value == SeleniumKeysHelpers.KEY_TAB)
                return Keys.Tab;

            if (command.Value == SeleniumKeysHelpers.KEY_UP)
                return Keys.Up;

            if (command.Value == SeleniumKeysHelpers.KEY_ESC)
                return Keys.Escape;

            return string.Empty;
        }
        public static string KEY_ENTER { get { return SetSeleniumFormat(GetMemberName(() => KEY_ENTER)); } }
        public static string KEY_LEFT { get { return SetSeleniumFormat(GetMemberName(() => KEY_LEFT)); } }
        public static string KEY_UP { get { return SetSeleniumFormat(GetMemberName(() => KEY_UP)); } }
        public static string KEY_RIGHT { get { return SetSeleniumFormat(GetMemberName(() => KEY_RIGHT)); } }
        public static string KEY_DOWN { get { return SetSeleniumFormat(GetMemberName(() => KEY_DOWN)); } }
        public static string KEY_PGUP { get { return SetSeleniumFormat(GetMemberName(() => KEY_PGUP)); } }
        public static string KEY_PAGE_UP { get { return SetSeleniumFormat(GetMemberName(() => KEY_PAGE_UP)); } }
        public static string KEY_PGDN { get { return SetSeleniumFormat(GetMemberName(() => KEY_PGDN)); } }
        public static string KEY_PAGE_DOWN { get { return SetSeleniumFormat(GetMemberName(() => KEY_PAGE_DOWN)); } }
        public static string KEY_BKSP { get { return SetSeleniumFormat(GetMemberName(() => KEY_BKSP)); } }
        public static string KEY_BACKSPACE { get { return SetSeleniumFormat(GetMemberName(() => KEY_BACKSPACE)); } }
        public static string KEY_DEL { get { return SetSeleniumFormat(GetMemberName(() => KEY_DEL)); } }
        public static string KEY_DELETE { get { return SetSeleniumFormat(GetMemberName(() => KEY_DELETE)); } }
        public static string KEY_TAB { get { return SetSeleniumFormat(GetMemberName(() => KEY_TAB)); } }
        public static string KEY_ESC { get { return SetSeleniumFormat(GetMemberName(() => KEY_ESC)); } }

        public static Dictionary<string, string> AllValues = new Dictionary<string, string>
            {
                {GetMemberName(() => KEY_ENTER), KEY_ENTER},
                {GetMemberName(() => KEY_LEFT), KEY_LEFT},
                {GetMemberName(() => KEY_UP), KEY_UP},
                {GetMemberName(() => KEY_RIGHT), KEY_RIGHT},
                {GetMemberName(() => KEY_DOWN), KEY_DOWN},
                {GetMemberName(() => KEY_PGUP), KEY_PGUP},
                {GetMemberName(() => KEY_PAGE_UP), KEY_PAGE_UP},
                {GetMemberName(() => KEY_PGDN), KEY_PGDN},
                {GetMemberName(() => KEY_PAGE_DOWN), KEY_PAGE_DOWN},
                {GetMemberName(() => KEY_BKSP), KEY_BKSP},
                {GetMemberName(() => KEY_BACKSPACE), KEY_BACKSPACE},
                {GetMemberName(() => KEY_DEL), KEY_DEL},
                {GetMemberName(() => KEY_DELETE), KEY_DELETE},
                {GetMemberName(() => KEY_TAB), KEY_TAB}
            };
        private static string GetMemberName<T>(Expression<Func<T>> memberExpression)
        {
            MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }

        private static string SetSeleniumFormat(string a)
        {
            return "${" + $"{a}" + "}";
        }
    }
}
