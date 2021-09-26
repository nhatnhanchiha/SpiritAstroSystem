namespace SpiritAstro.BusinessTier.Services
{
    public interface IAccountService
    {
        long GetCustomerId();
        long GetAstrologerId();
    }
    
    public class AccountService : IAccountService
    {
        public long GetCustomerId()
        {
            return 1;
        }

        public long GetAstrologerId()
        {
            return 1;
        }
    }
}