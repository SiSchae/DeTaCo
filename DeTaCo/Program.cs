using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeTaCo
{
    class Program
    {
        static void Main(string[] args)
        {
            //CSV-File Spaltenweise einlesen
            string file = @"C:\Users\simon\Documents\workspace\DeTaCo\DeTaCo\test.csv";

            StreamReader reader = new StreamReader(File.OpenRead(file));
            List<rule> rules = new List<rule>();
            while (!reader.EndOfStream)
            {

                var line = reader.ReadLine();
                string[] values = line.Split(',');
                if (rules.Count == 0)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        rules.Add(new rule());
                        rules[i].name = values[i];
                    }
                }
                if (values[0] == "J" || values[0] == "N")
                {
                    for (int i = 0; i < rules.Count; i++)
                    {
                        rules[i].conditions.Add(values[i]);
                    }
                }
                else if (!values[0].Contains("R"))
                {
                    for (int i = 0; i < rules.Count; i++)
                    {
                        rules[i].actions.Add(values[i]);
                    }
                }        
            }
            foreach (rule rule in rules)
            {
                rule.Output();
            }
            
            Console.ReadLine();
        }
    }
}
