using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeTaCo
{
    class RuleComparer : IComparer<rule>
    {
        public int Compare(rule x, rule y)
        {
            rule rule1 = x;
            rule rule2 = y;
            string[] name1 = rule1.name.Split('R');
            string[] name2 = rule2.name.Split('R');

            name1 = name1[1].Split('/');
            name2 = name2[1].Split('/');

            int nameint1 = int.Parse(name1[0]);
            int nameint2 = int.Parse(name2[0]);
            return nameint1.CompareTo(nameint2);
        }
    }
}
