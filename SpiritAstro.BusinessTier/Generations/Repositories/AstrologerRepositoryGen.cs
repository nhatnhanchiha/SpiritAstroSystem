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
    public partial interface IAstrologerRepository :IBaseRepository<Astrologer>
    {
    }
    public partial class AstrologerRepository :BaseRepository<Astrologer>, IAstrologerRepository
    {
         public AstrologerRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

