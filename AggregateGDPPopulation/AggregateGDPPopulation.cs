using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AggregateGDPPopulation
{
    public class Class1
    {
        class ContinentInfo
        {
            public float GDP { get; set; }
            public float POPULATION { get; set; }
        }
        class RequiredInfo
        {
            public string Country { get; set; }
            public string Population { get; set; }
            public string GDP { get; set; }
        }

        public static void Method()
        {
            string filePathCSV = @"../../../../AggregateGDPPopulation/data/datafile.csv";
            List<string> contents = File.ReadAllLines(filePathCSV).ToList();
            string[] header = contents[0].Split(',');
            for(int i=0;i<header.Length;i++)
            {
                header[i] = header[i].Trim('\"');
            }
            int indexOfPopulation = Array.IndexOf(header, "Population (Millions) - 2012");
            int indexOfGDP = Array.IndexOf(header, "GDP Billions (US Dollar) - 2012");
            int indexOfCountry = Array.IndexOf(header, "Country Name");
            List<RequiredInfo> output = new List<RequiredInfo>();
            for (int i = 1; i < contents.Count()-1; i++)
            {
                string[] info = contents[i].Split(',');

                RequiredInfo inputOfInfo = new RequiredInfo
                {
                    Country = info[indexOfCountry].Trim('\"'),
                    Population = info[indexOfPopulation].Trim('\"'),
                    GDP = info[indexOfGDP].Trim('\"')
                };
                output.Add(inputOfInfo);
            }
            StreamReader readJSON = new StreamReader(@"../../../../AggregateGDPPopulation/data/country-continent-map.json");
            
            JObject data = JObject.Parse(readJSON.ReadToEnd());
            Dictionary<string,string> convertedData = JsonConvert.DeserializeObject< Dictionary<string, string>>(data.ToString());

            Dictionary<string, ContinentInfo> continentGDPPopulation = new Dictionary<string, ContinentInfo>();
            foreach(var item in output)
            {
                if (continentGDPPopulation.ContainsKey(convertedData[item.Country]))
                {
                    continentGDPPopulation[convertedData[item.Country]].GDP += float.Parse(item.GDP);
                    continentGDPPopulation[convertedData[item.Country]].POPULATION += float.Parse(item.Population); 
                }
                else
                {
                    ContinentInfo newContinent = new ContinentInfo { GDP = float.Parse(item.GDP), POPULATION = float.Parse(item.Population) };
                    continentGDPPopulation.Add(convertedData[item.Country], newContinent);
                }
                
            }

            var outputText = JsonConvert.SerializeObject(continentGDPPopulation);
            File.WriteAllText("../../../../Output/output.json", outputText);

        }

    }
}
