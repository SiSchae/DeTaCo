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
            //*.csv spaltenweise einlesen
            string inputFile = args[0];
            string outputFile = args[1];

            using (StreamReader reader = new StreamReader(File.OpenRead(inputFile)))
            {
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
            }

            //Liste konsolidieren
            List<rule> consolidated = Consolidate(rules);

            //neues *.csv schreiben
            using (StreamWriter file = new StreamWriter(outputFile))
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
        }

        public static List<rule> Consolidate(List<rule> list)
        {
            //leere Ausgabeliste erzeugen
            List<rule> consolidatedRules = new List<rule>(); 
            
            for (int y = 0; y < list.Count; y++)                //
            {                                                   //Jede Regel mit jeder Regel
                for (int z = y + 1; z < list.Count ; z++)       //
                {
                    //Neue konsolidierte Ausgaberegel mit gleicher Anzahl an Bedingungen und Aktionen
                    rule consolidateRule = list[y];
                        
                    for (int i = 0; i < list[0].actions.Count; i++) //Alle Aktionen vergleichen
                    {
                        if (list[y].actions[i] == list[z].actions[i] && list[y].actions[i] != "") //überprüfen ob Aktionen gleich/nicht leer sind
                        {
                            int differenceCount = 0;
                            int difference = 0;
                            for (int j = 0; j < list[0].conditions.Count; j++) //alle Bedingungen auf Gleichheit überprüfen
                            {
                                if (!list[y].conditions[j].Equals(list[z].conditions[j]))
                                {
                                    differenceCount++;
                                    difference = j;
                                }
                                if (differenceCount == 1)
                                {

                                    for (int x = 0; x < list[y].conditions.Count; x++) //neue Bedingungsparameter (J/N/-) einfügen
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

                                if (!consolidatedRules.Contains(consolidateRule)) //Konsolidierte Regeln zur Ausgabeliste hinzufügen
                                    consolidatedRules.Add(consolidateRule);
                            }
                        }
                    }  
                }
                
            }
            
            return consolidatedRules; //Liste zurückgeben
        }
    }
}
