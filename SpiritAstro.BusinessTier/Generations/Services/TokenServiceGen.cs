/////////////////////////////////////////////////////////////////
//
//              AUTO-GENERATED
//
/////////////////////////////////////////////////////////////////

using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;
using SpiritAstro.BusinessTier.Generations.Repositories;
namespace SpiritAstro.BusinessTier.Generations.Services
{
    
    public partial interface ITokenService:IBaseService<Token>
    {
    }
    public partial class TokenService:BaseService<Token>,ITokenService
    {
        public TokenService(IUnitOfWork unitOfWork,ITokenRepository repository):base(unitOfWork,repository){}
    }
}
