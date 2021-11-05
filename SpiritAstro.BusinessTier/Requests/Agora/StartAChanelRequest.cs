using System.ComponentModel.DataAnnotations;

namespace SpiritAstro.BusinessTier.Requests.Agora
{
    public class StartAChanelRequest
    {
        [Range(1, 12)]
        public int TimeInHour { get; set; }
    }
}