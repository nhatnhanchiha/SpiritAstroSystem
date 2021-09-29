using SpiritAstro.BusinessTier.Commons.Attributes;

namespace SpiritAstro.BusinessTier.ViewModels.Category
{
    public class CategoryModel
    {
        public static string[] Fields = { "Id", "Name" };
        public long? Id { get; set; }
        [String]
        public string Name { get; set; }
    }
}