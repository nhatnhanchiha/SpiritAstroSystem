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
    public partial interface IPostPlanetRepository :IBaseRepository<PostPlanet>
    {
    }
    public partial class PostPlanetRepository :BaseRepository<PostPlanet>, IPostPlanetRepository
    {
         public PostPlanetRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

