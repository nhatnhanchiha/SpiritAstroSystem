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
    public partial interface IPlanetRepository :IBaseRepository<Planet>
    {
    }
    public partial class PlanetRepository :BaseRepository<Planet>, IPlanetRepository
    {
         public PlanetRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

