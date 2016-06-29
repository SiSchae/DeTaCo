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
            for (int i = 0; i < rules.Count - 1; i++)
            {
                Compare(rules[i], rules[i + 1]);
            }
            
            Console.ReadLine();
        }

        public static void Compare(rule rule1, rule rule2)
        {
            for (int i = 0; i < rule1.actions.Count; i++)
            {
                if (rule1.actions[i] == rule2.actions[i] && rule1.actions[i] != "")
                {
                    Console.WriteLine(rule1.name + " Action: " + i + " = " + rule2.name + " Action: " + i);
                    int differenceCount = 0;
                    int difference = 0;
                    for (int j = 0; j < rule1.conditions.Count; j++)
                    {
                        if (!rule1.conditions[j].Equals(rule2.conditions[j]))
                        {
                            differenceCount++;
                            difference = j;
                        }
                        if (differenceCount == 1)
                        {
                            Console.WriteLine(differenceCount + " " + difference);
                        }   
                    }
                }
            }
        }
    }
}
