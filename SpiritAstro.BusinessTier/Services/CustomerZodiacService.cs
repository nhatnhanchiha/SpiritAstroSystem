using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SpiritAstro.BusinessTier.Commons.Utils;
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
using SpiritAstro.BusinessTier.Services;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface ICustomerZodiacService
    {
        Task<PageResult<CustomerZodiacModel>> GetListCustomerZodiac(CustomerZodiacModel customerZodiacFilter, int page, int limit);
        Task<long> CreateCustomerZodiac(CustomerZodiacRequest customerZodiacRequest);
        Task<CustomerZodiacModel> GetCustomerZodiacById(long id);
    }
    public partial class CustomerZodiacService
    {
        private readonly IConfigurationProvider _mapper;
        private const int DefaultPaging = 10;
        private const int LimitPaging = 50;
        private readonly IAccountService _accountService;
        

        public CustomerZodiacService(IUnitOfWork unitOfWork, ICustomerZodiacRepository repository, IMapper mapper, IAccountService accountService) : base(
            unitOfWork, repository)
        {
            _accountService = accountService;
            _mapper = mapper.ConfigurationProvider;
        }

        public async Task<PageResult<CustomerZodiacModel>> GetListCustomerZodiac(CustomerZodiacModel customerZodiacFilter, int page, int limit)
        {
            var (total, queryable) = Get().ProjectTo<CustomerZodiacModel>(_mapper).DynamicFilter(customerZodiacFilter).PagingIQueryable(page, limit, LimitPaging, DefaultPaging);
            return new PageResult<CustomerZodiacModel>
            {
                List = await queryable.ToListAsync(),
                Page = page,
                Limit = limit,
                Total = total,
            };
        }

        public async Task<long> CreateCustomerZodiac(CustomerZodiacRequest customerZodiacRequest)
        {
            var customerId = _accountService.GetCustomerId();
            
            
            var mapper = _mapper.CreateMapper();
            var customerZodiac = mapper.Map<CustomerZodiac>(customerZodiacRequest);

            customerZodiac.CustomerId = customerId;
            
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
