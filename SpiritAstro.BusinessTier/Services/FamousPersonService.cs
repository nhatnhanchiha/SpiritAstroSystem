using AutoMapper;
using AutoMapper.Configuration;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Requests.FamousPerson;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.FamousPerson;
using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface IFamousPersonService
    {
        Task<FamousPersonModel> GetFamousPersonById(long FamousPersonId);

        Task<long> CreateFamousPerson(CreateFamousPersonRequest createFamousPersonRequest);

        Task UpdateFamousPerson(long famouspersonId, UpdateFamousPersonRequest updateFamousPersonRequest);

       Task DeleteFamousPerson(long famouspersonId);
    }

    public partial class FamousPersonService
    {
        private readonly IConfigurationProvider _mapper;
        public FamousPersonService(IUnitOfWork unitOfWork, IFamousPersonRepository repository, IMapper mapper) : base(unitOfWork, repository) {
            _mapper = mapper.ConfigurationProvider;
        }

        public async Task<FamousPersonModel> GetFamousPersonById(long famouspersonId)
        {
            var FamousPersonModel = await Get().Where(fp => fp.Id == famouspersonId).ProjectTo<FamousPersonModel>(_mapper).FirstOrDefaultAsync();
            if (FamousPersonModel == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound, "Cannot find any famous person matches with id = ${famouspersonId}");
            }
            return FamousPersonModel;
        }

        public async Task<long> CreateFamousPerson(CreateFamousPersonRequest createFamousPersonRequest)
        {
            var mapper = _mapper.CreateMapper();
            var famousperson = mapper.Map<FamousPerson>(createFamousPersonRequest);
            await CreateAsyn(famousperson);
            return famousperson.Id;
        }

        public async Task UpdateFamousPerson(long famouspersonId, UpdateFamousPersonRequest updateFamousPersonRequest)
        {
           var famouspersonInDb = await Get().FirstOrDefaultAsync(fp => fp.Id == famouspersonId);
            if (famouspersonInDb == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound, "Cannot find any famous person matches with id = ${famouspersonId}");
            }

            var mapper = _mapper.CreateMapper();
            var famouspersonInRequest = mapper.Map<FamousPerson>(updateFamousPersonRequest);

            famouspersonInDb.Name = famouspersonInRequest.Name;
            famouspersonInDb.Description = famouspersonInRequest.Description;
            famouspersonInDb.Zodiac = famouspersonInRequest.Zodiac;

            await UpdateAsyn(famouspersonInDb);
        }

        public async Task DeleteFamousPerson(long famouspersonId)
        {
            var categoryInDb = await Get().FirstOrDefaultAsync(c => c.Id == famouspersonId);
            if (categoryInDb == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any category matches with id = {famouspersonId}");
            }

            await DeleteAsyn(categoryInDb);
        }
    }
}
