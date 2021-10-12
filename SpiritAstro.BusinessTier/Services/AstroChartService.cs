using System.Text;
using SpiritAstro.BusinessTier.Entities.AstroChart;
using SpiritAstro.BusinessTier.Requests.AstroChart;
using SpiritAstro.BusinessTier.Responses.AstroChart;

namespace SpiritAstro.BusinessTier.Services
{
    public interface IAstroChartService
    {
        NatalChartDataResponse Execute(GetNatalChartRequest getNatalChartRequest);
    }
    
    public class AstroChartService : IAstroChartService
    {
        public NatalChartDataResponse Execute(GetNatalChartRequest getNatalChartRequest)
        {
            var mySwissEphNet = new MySwissEphNet(getNatalChartRequest);
            var bf = new StringBuilder();
            mySwissEphNet.Swisseph(bf);
            return NatalChartDataResponse.FromString(bf.ToString());
        }    
    }
}