namespace SpiritAstro.BusinessTier.Requests.FieldDetail
{
    public class CreateFieldDetailRequest
    {
        public long AstrologerId { get; set; }
        public long FieldId { get; set; }
        public long Exp { get; set; }
    }
}