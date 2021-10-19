using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Canvas_SWD;
using SpiritAstro.BusinessTier.Responses.AstroChart;

namespace SpiritAstro.BusinessTier.Commons.Toolkit.Zodiac
{
    public class ZodiacPositionHouse
    {
        private readonly List<ZodiacPosition> _zodiacPositions;
        
        private static readonly (int, int) BlueSpecialScope1 = (55, 65);
        private static readonly (int, int) BlueSpecialScope2 = (111, 129);

        private static readonly (int, int) RedSpecialScope1 = (0, 9);
        private static readonly (int, int) RedSpecialScope2 = (81, 99);
        private static readonly (int, int) RedSpecialScope3 = (171, 180);

        private static readonly (int, int) GraySpecialScope1 = (43, 48);
        private static readonly (int, int) GraySpecialScope2 = (133, 137);
        
        private static readonly Pen BluePen = new(Color.Blue, 2);
        private static readonly Pen RedPen = new(Color.Red, 2);
        private static readonly Pen GrayPen = new(Color.Gray, 2);

        public ZodiacPositionHouse()
        {
            _zodiacPositions = new List<ZodiacPosition>();
        }

        public void Initialize(NatalChartDataResponse natalChartDataResponse)
        {
            foreach (var planetData in natalChartDataResponse.Planets.Select(planet => PlanetData.FromSource(planet.ElcLong)))
            {
                _zodiacPositions.Add(new ZodiacPosition(planetData.Zodiac, planetData.Degree));
            }
        }
        
        public void Draw(Graphics g)
        {
            for (int i = 0; i < _zodiacPositions.Count; i++)
            {
                for (int j = i + 1; j < _zodiacPositions.Count; j++)
                {
                    var degree = _zodiacPositions[i].GetAngleMeasure(_zodiacPositions[j]);
                    if ((degree >= BlueSpecialScope1.Item1 && degree <= BlueSpecialScope1.Item2)
                        || (degree >= BlueSpecialScope2.Item1 && degree <= BlueSpecialScope2.Item2))
                    {
                        ZodiacPosition.MyDrawLine(g, BluePen, _zodiacPositions[i], _zodiacPositions[j]);
                    }
                    else if ( (degree >= RedSpecialScope1.Item1 && degree <= RedSpecialScope1.Item2) ||
                              (degree >= RedSpecialScope2.Item1 && degree <= RedSpecialScope2.Item2)
                              || (degree >= RedSpecialScope3.Item1 && degree <= RedSpecialScope3.Item2))
                    {
                        ZodiacPosition.MyDrawLine(g, RedPen, _zodiacPositions[i], _zodiacPositions[j]);
                    }
                    else if ( (degree >= GraySpecialScope1.Item1 && degree <= GraySpecialScope1.Item2)
                              || (degree >= GraySpecialScope2.Item1 && degree <= GraySpecialScope2.Item2))
                    {
                        ZodiacPosition.MyDrawLine(g, GrayPen, _zodiacPositions[i], _zodiacPositions[j]);
                    }
                }
            }
        }

        private class PlanetData
        {
            public ZodiacEnum Zodiac { get; set; }
            public double Degree { get; set; }

            private static readonly Dictionary<string, ZodiacEnum> StringZodiacDict = new()
            {
                { "ar", ZodiacEnum.Aries },
                { "ta", ZodiacEnum.Taurus },
                { "ge", ZodiacEnum.Gemini },
                { "cn", ZodiacEnum.Cancer },
                { "le", ZodiacEnum.Leo },
                { "vi", ZodiacEnum.Virgo },
                { "li", ZodiacEnum.Libra },
                { "sc", ZodiacEnum.Scorpio },
                { "sa", ZodiacEnum.Sagittarius },
                { "cp", ZodiacEnum.Capricorn },
                { "aq", ZodiacEnum.Aquarius },
                { "pi", ZodiacEnum.Pisces }
            };
            public static PlanetData FromSource(string source)
            { 
                var strings = source.Replace("' ", "'").Split(" ");
                var degree = double.Parse(strings[0]);
                var zodiac = StringZodiacDict[strings[1]];
                
                if (strings.Count() > 2)
                {
                    var splitStrings = strings[2].Split("'");
                    degree += double.Parse(splitStrings[0]) / 60;
                    if (splitStrings.Count() > 1)
                    {
                        degree += double.Parse(splitStrings[1]) / 3600;
                    }
                }
                
                return new PlanetData
                {
                    Zodiac = zodiac,
                    Degree = degree
                };
            }
        }
    }
}