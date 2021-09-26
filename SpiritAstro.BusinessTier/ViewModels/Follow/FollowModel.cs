using SpiritAstro.BusinessTier.ViewModels.Astrologer;
using SpiritAstro.BusinessTier.ViewModels.Customer;
using SpiritAstro.BusinessTier.ViewModels.Users;

namespace SpiritAstro.BusinessTier.ViewModels.Follow
{
    public class FollowModel
    {
        public long AstrologerId { get; set; }
        public long CustomerId { get; set; }
    }

    public class FollowWithAstrologer
    {
        public long AstrologerId { get; set; }
        public long CustomerId { get; set; }
        public PublicAstrologerModel Astrologer { get; set; }
    }

    public class FollowWithCustomer
    {
        public long AstrologerId { get; set; }
        public long CustomerId { get; set; }
        public PublicCustomerModel Customer { get; set; }
    }
}