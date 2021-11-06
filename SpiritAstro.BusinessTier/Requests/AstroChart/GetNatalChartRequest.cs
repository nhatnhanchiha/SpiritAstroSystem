using System;

namespace SpiritAstro.BusinessTier.Requests.AstroChart
{
    public class GetNatalChartRequest
    {
        public DateTime TimeOfBirthInUtcTime { get; set; }
        public Coordinates Coordinates { get; set; }
    }

    public class Coordinates
    {
        public uint LonDeg { get; set; }
        public uint LonMin { get; set; }
        public uint LonSec { get; set; }
        public string LonEW { get; set; }
        public uint LatDeg { get; set; }
        public uint LatMin { get; set; }
        public uint LatSec { get; set; }
        public string LatNS { get; set; }

        public static Coordinates FromLatLong(double lon, double lat)
        {
            return new Coordinates
            {
                LonDeg = (uint)lon,
                LonMin = (uint)(lon * 100 % 100),
                LonSec = (uint)(lon * 10000 % 100),
                LonEW = lon > 0 ? "E" : "W",
                LatDeg = (uint)lat,
                LatMin = (uint)(lat * 100 % 100),
                LatSec = (uint)(lat * 10000 % 100),
                LatNS = lat > 0 ? "N" : "S"
            };
        }
    }
}