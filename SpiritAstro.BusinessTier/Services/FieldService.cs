using AutoMapper;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Requests.Field;
using SpiritAstro.BusinessTier.ViewModels.Field;
using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface IFieldService
    {
        Task<long> CreateField(CreateFieldRequest createFieldRequest);
    }
    public partial class FieldService
    {
        private readonly IConfigurationProvider _mapper;

        public FieldService(IUnitOfWork unitOfWork, IFieldRepository repository, IMapper mapper) : base(
            unitOfWork, repository)
        {
            _mapper = mapper.ConfigurationProvider;
        }

        public async Task<long> CreateField(CreateFieldRequest createFieldRequest)
        {
            var mapper = _mapper.CreateMapper();
            var field = mapper.Map<Field>(createFieldRequest);
            await CreateAsyn(field);
            return field.Id;
        }
    }
}
