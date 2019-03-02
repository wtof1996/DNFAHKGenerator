using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNFAHKGenerator.Parser
{
    public class HotKeyConverter
    {
        public static string ParseInput(string input)
        {
            var ret = input;

            ret = ret.Replace("上", "↑");
            ret = ret.Replace("下", "↓");
            ret = ret.Replace("左", "←");
            ret = ret.Replace("右", "→");
            ret = ret.Replace("空格", "Space");

            return ret;
        }
    }
}
