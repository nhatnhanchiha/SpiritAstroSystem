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
        public PublicUserModel Astrologer { get; set; }
    }

    public class FollowWithCustomer
    {
        public long AstrologerId { get; set; }
        public long CustomerId { get; set; }
        public PublicUserModel Customer { get; set; }
    }
}