namespace SpiritAstro.BusinessTier.ViewModels.FieldDetail
{
    public class FieldDetailModel
    {
        public static readonly string[] Fields = { "AstrologerId", "FieldId", "Exp" };
        public long AstrologerId { get; set; }
        public long FieldId { get; set; }
        public long Exp { get; set; }
    }
}