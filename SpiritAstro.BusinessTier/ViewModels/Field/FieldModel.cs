using SpiritAstro.BusinessTier.Commons.Attributes;

namespace SpiritAstro.BusinessTier.ViewModels.Field
{
    public class FieldModel
    {
        public static string[] Fields =
        {
            "Id", "Name", "Exp"
        };
        public long? Id { get; set; }
        [String]
        public string Name { get; set; }
        public long? Exp { get; set; }
        public long? PriceTableId { get; set; }
    }
}
