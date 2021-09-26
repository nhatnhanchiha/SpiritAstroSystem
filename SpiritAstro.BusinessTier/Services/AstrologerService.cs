using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.DataTier.BaseConnect;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface IAstrologerService
    {
        Task IsAstrologer(long astrologerId);
    }
    public partial class AstrologerService
    {
        private readonly IConfigurationProvider _mapper;

        public AstrologerService(IUnitOfWork unitOfWork, IAstrologerRepository repository, IMapper mapper) : base(
            unitOfWork, repository)
        {
            _mapper = mapper.ConfigurationProvider;
        }

        public async Task IsAstrologer(long astrologerId)
        {
            var astrologer = await Get().FirstOrDefaultAsync(a => a.Id == astrologerId && a.DeletedAt == null);
            if (astrologer == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any astrologer has id = {astrologerId}");
            }
        }
    }
}