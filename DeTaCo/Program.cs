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
            string inputFile = args[0];
            string outputFile = args[1];

            StreamReader reader = new StreamReader(File.OpenRead(inputFile));
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
            List<rule> consolidated = Consolidate(rules);

            /*foreach (rule rule in consolidated)
            {
                rule.Output();
            }*/

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(outputFile))
            {
                for (int i = 0; i < consolidated.Count; i++)
                {
                    file.Write(consolidated[i].name + ",");
                }
                file.Write(Environment.NewLine);
                for (int i = 0; i < consolidated[0].conditions.Count; i++)
                {
                    for (int j = 0; j < consolidated.Count; j++)
                    {
                        file.Write(consolidated[j].conditions[i] + ",");
                    }
                    file.Write(Environment.NewLine);
                }
                for (int i = 0; i < consolidated[0].actions.Count; i++)
                {
                    for (int j = 0; j < consolidated.Count; j++)
                    {
                        file.Write(consolidated[j].actions[i] + ",");
                    }
                    file.Write(Environment.NewLine);
                }
            }


            Console.ReadLine();
        }

        public static List<rule> Consolidate(List<rule> list)
        {
            List<rule> consolidatedRules = new List<rule>(); 
            
            for (int y = 0; y < list.Count; y++)
            {
                for (int z = y + 1; z < list.Count ; z++)
                {
                    if(list[y] != list[z])
                    {
                        rule consolidateRule = list[y];
                        
                        for (int i = 0; i < list[0].actions.Count; i++)
                        {
                            if (list[y].actions[i] == list[z].actions[i] && list[y].actions[i] != "")
                            {
                                int differenceCount = 0;
                                int difference = 0;
                                for (int j = 0; j < list[0].conditions.Count; j++)
                                {
                                    if (!list[y].conditions[j].Equals(list[z].conditions[j]))
                                    {
                                        differenceCount++;
                                        difference = j;
                                    }
                                    if (differenceCount == 1)
                                    {

                                        for (int x = 0; x < list[y].conditions.Count; x++)
                                        {
                                            if (x == difference)
                                            {
                                                consolidateRule.conditions[difference] = "-";
                                            }
                                            else
                                            {
                                                consolidateRule.conditions[x] = list[y].conditions[x];
                                            }
                                        }
                                    }

                                    if (!consolidatedRules.Contains(consolidateRule))
                                        consolidatedRules.Add(consolidateRule);
                                }
                            }
                        }
                    }  
                }
                
            }
            
            return consolidatedRules;
        }
    }
}
                    
            /*for (int i = 0; i < rule1.actions.Count; i++)
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
                         
                    }
                    if (differenceCount == 1)
                    {
                        rule return_rule = new rule();
                        for (int i = 0; i < rule1.conditions.Count; i++)
                        {
                            if (i == difference)
                            {
                                return_rule.conditions[difference] = "-";
                            }
                            else
                            {
                                return_rule.conditions[i] = rule1.conditions[i];
                            }
                        }
                        return_rule.actions = rule1.actions;
                        return return_rule;
                    }
                    else
                        return rule1;
                }
                else
                        return rule1;
            }*/
