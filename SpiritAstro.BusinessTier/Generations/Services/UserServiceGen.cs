/////////////////////////////////////////////////////////////////
//
//              AUTO-GENERATED
//
/////////////////////////////////////////////////////////////////

using Microsoft.Extensions.Configuration;
using SpiritAstro.DataTier.Models;
using SpiritAstro.BusinessTier.Generations.Repositories;
using Test.DataTier.BaseConnect;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    
    public partial interface IUserService:IBaseService<User>
    {
    }
    public partial class UserService:BaseService<User>,IUserService
    {
        public UserService(IUnitOfWork unitOfWork,IUserRepository repository):base(unitOfWork,repository)
        {
        }
    }
}
