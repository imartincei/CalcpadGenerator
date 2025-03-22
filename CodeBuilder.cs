using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcpadGenerator
{
    internal class CodeBuilder
    {
        internal static string LookupDropdown(List<List<object>> data)
        {
            StringBuilder sb = new();
            foreach (var row in data)
            {
                foreach (var value in row)
                {
                    sb.Append(value);
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
