using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace back.Classe
{
    public static class Protection
    {
        public static string XSS(string _text)
        {
            Regex regHtml = new Regex("<[^>]*>");
            return regHtml.Replace(_text, "");
        }
    }
}
