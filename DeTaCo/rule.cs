using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeTaCo
{
    class rule
    {
        public string name;
        public List<string> conditions;
        public List<string> actions;
        
        public rule()
        {
            name = "";
            conditions = new List<string>();
            actions = new List<string>();
        }

        //reine Test-Funktion zur Ausgabe der Werte
        public void Output()
        {
            Console.WriteLine(this.name);
            foreach (string condition in conditions)
            {
                Console.WriteLine(condition);
            }
            foreach (string action in actions)
            {
                Console.WriteLine(action);
            }
            Console.WriteLine();
        }
            
    }
}
