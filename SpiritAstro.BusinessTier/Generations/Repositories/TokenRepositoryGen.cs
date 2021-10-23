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
    public partial interface ITokenRepository :IBaseRepository<Token>
    {
    }
    public partial class TokenRepository :BaseRepository<Token>, ITokenRepository
    {
         public TokenRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

