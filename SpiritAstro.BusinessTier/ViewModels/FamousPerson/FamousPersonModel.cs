using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpiritAstro.BusinessTier.Commons.Attributes;

namespace SpiritAstro.BusinessTier.ViewModels.FamousPerson
{
    public class FamousPersonModel
    {
        public static string[] Fields =
        {
            "Id", "Name", "Description", "ZodiacId", "UrlImage", "Gender"
        };

        public long? Id { get; set; }
        [String]
        public string Name { get; set; }
        [String]
        public string Description { get; set; }
        public int? ZodiacId { get; set; }
        public string ZodiacName { get; set; }
        public string UrlImage { get; set; }
        public bool? Gender { get; set; }
    }
}
