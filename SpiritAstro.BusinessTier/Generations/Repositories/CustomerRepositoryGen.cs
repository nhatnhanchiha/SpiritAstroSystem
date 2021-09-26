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
    public partial interface ICustomerRepository :IBaseRepository<Customer>
    {
    }
    public partial class CustomerRepository :BaseRepository<Customer>, ICustomerRepository
    {
         public CustomerRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

