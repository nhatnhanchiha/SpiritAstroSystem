/////////////////////////////////////////////////////////////////
//
//              AUTO-GENERATED
//
/////////////////////////////////////////////////////////////////

using Microsoft.EntityFrameworkCore;
using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;
namespace SpiritAstro.BusinessTier.Generations.Repositories
{
    public partial interface ICustomerZodiacRepository :IBaseRepository<CustomerZodiac>
    {
    }
    public partial class CustomerZodiacRepository :BaseRepository<CustomerZodiac>, ICustomerZodiacRepository
    {
         public CustomerZodiacRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

