namespace SpiritAstro.BusinessTier.ViewModels.Planet
{
    public class PlanetModel
    {
        public static readonly string[] Fields = { "Id", "Name", "Description" };
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}