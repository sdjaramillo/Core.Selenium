using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Selenium.Model
{
    public class SplitTarget
    {
        public string Identificador { get; set; }
        public string Valor { get; set; }
        public By By { get; set; }

        public static SplitTarget GetSplitTarget(string targetSplit, char charSplit = '=')
        {
            string[] targetSplitList = targetSplit.Split(charSplit);
            string identificador = targetSplitList[0];
            string valor = string.Join(charSplit, targetSplitList.Skip(1));

            return new SplitTarget
            {
                Identificador = identificador,
                Valor = valor
            };
        }
    }
}
