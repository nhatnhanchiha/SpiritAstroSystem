using SpiritAstro.BusinessTier.Commons.Attributes;

namespace SpiritAstro.BusinessTier.ViewModels.Category
{
    public class CategoryModel
    {
        public long? Id { get; set; }
        [String]
        public string Name { get; set; }
    }
}