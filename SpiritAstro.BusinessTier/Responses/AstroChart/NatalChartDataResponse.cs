using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SwissEphNet;

namespace SpiritAstro.BusinessTier.Responses.AstroChart
{
    public class NatalChartDataResponse
    {
        public List<Planet> Planets { get; set; }
        public List<Zodiac> Zodiacs { get; set; }

        public static NatalChartDataResponse FromString(string data)
        {
            var strings = data.Split("\n");
            var planets = new List<Planet>();
            for (var i = 9; i < 19; i++)
            {
                planets.Add(Planet.FromString(strings[i]));    
            }

            var zodiacs = new List<Zodiac>();
            for (var i = 29; i < 44; i++)
            {
                zodiacs.Add(Zodiac.FromString(strings[i]));
            }

            return new NatalChartDataResponse
            {
                Planets = planets,
                Zodiacs = zodiacs
            };
        }
    }

    public class Planet
    {
        public string Label;
        public string ElcLong { get; set; }
        public string ElcLat { get; set; }
        public string Dist { get; set; }
        public string Speed { get; set; }
        public string House { get; set; }

        public static Planet FromString(string data)
        {
            var strings = System.Text.RegularExpressions.Regex.Split( data, @"\s{2,}");
            return new Planet
            {
                Label = strings[0].Trim(),
                ElcLong = strings[1].Trim(),
                ElcLat = strings[2].Trim(),
                Dist = strings[3].Trim(),
                Speed = strings[4].Trim(),
                House = strings[5].Trim()
            };
        }
    }

    public class Zodiac
    {
        public string Label { get; set; }
        public string Value { get; set; }
        private static string[] NotHouseList = { "MC", "AC", "Vertex" };

        public static Zodiac FromString(string data)
        {
            string[] strings = data.Split(":");
            return new Zodiac
            {
                Label = Regex.Replace(strings[0], @"\s+", " ").Trim(),
                Value = Regex.Replace(strings[1], @"\s+", " ").Trim()
            };
        }
        
    }
}