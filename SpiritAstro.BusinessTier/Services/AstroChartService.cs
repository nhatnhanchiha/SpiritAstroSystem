using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpiritAstro.BusinessTier.Commons.Toolkit.Zodiac;
using SpiritAstro.BusinessTier.Entities.AstroChart;
using SpiritAstro.BusinessTier.Requests.AstroChart;
using SpiritAstro.BusinessTier.Responses.AstroChart;

namespace SpiritAstro.BusinessTier.Services
{
    public interface IAstroChartService
    {
        Task<string> Execute(GetNatalChartRequest getNatalChartRequest);
    }
    
    public class AstroChartService : IAstroChartService
    {
        public async Task<string> Execute(GetNatalChartRequest getNatalChartRequest)
        {
            var mySwissEphNet = new MySwissEphNet(getNatalChartRequest);
            var bf = new StringBuilder();
            mySwissEphNet.Swisseph(bf);
            
            var natalChartDataResponse = NatalChartDataResponse.FromString(bf.ToString());
            
            var zodiacPositionHouse = new ZodiacPositionHouse();
            zodiacPositionHouse.Initialize(natalChartDataResponse);
            
            var image = Image.FromFile(@"Resources/background.jpg");
            var g = Graphics.FromImage(image);
            zodiacPositionHouse.Draw(g);
            g.Flush();
            
            var memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Png);
            
            var firebaseStorageService = new FirebaseStorageService();
            var uploadFileGolang = await firebaseStorageService.UploadFileGolang(memoryStream.ToArray());
            
            var result = JsonConvert.DeserializeObject<List<string>>(uploadFileGolang);
            if (result == null)
            {
                return "";
            }

            return result.First();
        }
    }
}