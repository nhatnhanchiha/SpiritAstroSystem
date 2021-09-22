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
    public partial interface IFamousPersonRepository :IBaseRepository<FamousPerson>
    {
    }
    public partial class FamousPersonRepository :BaseRepository<FamousPerson>, IFamousPersonRepository
    {
         public FamousPersonRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

