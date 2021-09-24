using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Requests.CustomerZodiac;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.CustomerZodiac;
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
    public partial interface ICustomerZodiacService
    {
        Task<long> CreateCustomerZodiac(CustomerZodiacRequest customerZodiacRequest);
        Task<CustomerZodiacModel> GetCustomerZodiacById(long id);
    }
    public partial class CustomerZodiacService
    {
        private readonly IConfigurationProvider _mapper;

        public CustomerZodiacService(IUnitOfWork unitOfWork, ICustomerZodiacRepository repository, IMapper mapper) : base(
            unitOfWork, repository)
        {
            _mapper = mapper.ConfigurationProvider;
        }

        public async Task<long> CreateCustomerZodiac(CustomerZodiacRequest customerZodiacRequest)
        {
            var mapper = _mapper.CreateMapper();
            var customerZodiac = mapper.Map<CustomerZodiac>(customerZodiacRequest);
            await CreateAsyn(customerZodiac);
            return customerZodiac.Id;
        }

        public async Task<CustomerZodiacModel> GetCustomerZodiacById(long id)
        {
            var czModel = await Get().Where(cz => cz.Id == id).ProjectTo<CustomerZodiacModel>(_mapper).FirstOrDefaultAsync();
            if (czModel == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any category matches with id = {id}");
            }

            return czModel;
        }

    }
}
