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
    }
}