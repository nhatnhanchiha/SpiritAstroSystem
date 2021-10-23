using System.Threading.Tasks;
using AutoMapper;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Requests.Token;
using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface ITokenService
    {
        Task RegisterAToken(long userId, CreateTokenRequest createTokenRequest);
    }
    public partial class TokenService
    {
        private readonly IConfigurationProvider _mapper;

        public TokenService(IUnitOfWork unitOfWork, ITokenRepository repository, IMapper mapper) : base(unitOfWork,
            repository)
        {
            _mapper = mapper.ConfigurationProvider;
        }
        public async Task RegisterAToken(long userId, CreateTokenRequest createTokenRequest)
        {
            var mapper = _mapper.CreateMapper();
            var token = mapper.Map<Token>(createTokenRequest);
            token.UserId = userId;
            await CreateAsyn(token);
        }
    }
}