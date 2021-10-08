using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SpiritAstro.BusinessTier.Commons.Utils;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Requests.Customer;
using SpiritAstro.BusinessTier.Requests.UserRole;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.Customer;
using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface ICustomerService
    {
        Task<PageResult<PublicCustomerModel>> GetAllCustomers(PublicCustomerModel filter, string[] fields,
            string sort, int page, int limit);

        Task<PublicCustomerModel> GetPublicCustomerById(long customerId);
        Task<CustomerModel> GetCustomerById(long customerId);

        Task RegisterACustomer(long userId, RegisterCustomerRequest registerCustomerRequest);
        Task UpdateCustomer(long customerId, UpdateCustomerRequest updateCustomerRequest);
        Task DeleteCustomer(long customerId);
    }

    public partial class CustomerService
    {
        private readonly IConfigurationProvider _mapper;
        private readonly IUserRoleService _userRoleService;
        private const int DefaultPaging = 20;
        private const int LimitPaging = 20;

        public CustomerService(IUnitOfWork unitOfWork, ICustomerRepository repository, IMapper mapper, IUserRoleService userRoleService) : base(
            unitOfWork, repository)
        {
            _userRoleService = userRoleService;
            _mapper = mapper.ConfigurationProvider;
        }


        public async Task<PageResult<PublicCustomerModel>> GetAllCustomers(PublicCustomerModel filter, string[] fields,
            string sort, int page, int limit)
        {
            var (total, queryable) = Get().Where(c => c.DeletedAt == null).ProjectTo<PublicCustomerModel>(_mapper)
                .DynamicFilter(filter)
                .PagingIQueryable(page, limit, LimitPaging, DefaultPaging);
            if (sort != null)
            {
                queryable = queryable.OrderBy(sort);
            }

            if (fields.Length > 0)
            {
                queryable = queryable.Select<PublicCustomerModel>(PublicCustomerModel.Fields.Intersect(fields).ToArray()
                    .ToDynamicSelector<PublicCustomerModel>());
            }

            return new PageResult<PublicCustomerModel>
            {
                List = await queryable.ToListAsync(),
                Page = page,
                Limit = limit,
                Total = total
            };
        }

        public async Task<PublicCustomerModel> GetPublicCustomerById(long customerId)
        {
            var publicCustomerModel = await Get().ProjectTo<PublicCustomerModel>(_mapper)
                .FirstOrDefaultAsync(c => c.Id == customerId && c.DeletedAt == null);
            if (publicCustomerModel == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any customer matches with id = {customerId}");
            }

            return publicCustomerModel;
        }

        public async Task<CustomerModel> GetCustomerById(long customerId)
        {
            var customerModel = await Get().ProjectTo<CustomerModel>(_mapper)
                .FirstOrDefaultAsync(c => c.Id == customerId && c.DeletedAt == null);
            if (customerModel == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any customer matches with id = {customerId}");
            }

            return customerModel;
        }

        public async Task RegisterACustomer(long userId, RegisterCustomerRequest registerCustomerRequest)
        {
            var customerInDb = await Get().FirstOrDefaultAsync(c => c.Id == userId);

            var mapper = _mapper.CreateMapper();
            var customer = mapper.Map<Customer>(registerCustomerRequest);
            if (customerInDb == null)
            {
                customer.Id = userId;
                customer.CreatedAt = DateTimeOffset.Now;
                customer.UpdatedAt = DateTimeOffset.Now;

                var transaction = await repository.BeginTransaction();
                try
                {
                    await CreateAsyn(customer);
                    await _userRoleService.CreateUserRole(new CreateUserRoleRequest
                    {
                        RoleId = "888",
                        UserId = customer.Id
                    });
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new ErrorResponse((int)HttpStatusCode.InternalServerError, "Error when registering a customer");
                }
                return;
            }

            if (customerInDb.DeletedAt == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.BadRequest, "This user has been a customer already!");
            }
        }

        public async Task UpdateCustomer(long customerId, UpdateCustomerRequest updateCustomerRequest)
        {
            var customerInDb = await Get().FirstOrDefaultAsync(c => c.Id == customerId && c.DeletedAt == null);
            if (customerInDb == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any customer matches with id = {customerId}");
            }

            var mapper = _mapper.CreateMapper();
            var customerInRequest = mapper.Map<Customer>(updateCustomerRequest);

            customerInDb.Name = customerInRequest.Name;
            customerInDb.PhoneNumber = customerInRequest.PhoneNumber;
            customerInDb.StatusPayment = customerInRequest.StatusPayment;
            customerInDb.Gender = customerInRequest.Gender;
            customerInDb.LatitudeOfBirth = customerInRequest.LatitudeOfBirth;
            customerInDb.LongitudeOfBirth = customerInRequest.LongitudeOfBirth;
            customerInDb.TimeOfBirth = customerInRequest.TimeOfBirth;
            
            await UpdateAsyn(customerInDb);
        }

        public async Task DeleteCustomer(long customerId)
        {
            var customer = await Get().FirstOrDefaultAsync(c => c.Id == customerId && c.DeletedAt == null);
            if (customer == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any customer matches with id = {customerId}");
            }
            
            customer.DeletedAt = DateTimeOffset.Now;

            var transaction = await repository.BeginTransaction();
            try
            {
                await UpdateAsyn(customer);
                await _userRoleService.DeleteAsyn(new UserRole { UserId = customer.Id, RoleId = "888" });
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw new ErrorResponse((int)HttpStatusCode.InternalServerError, "Error when deleting a customer");
            }
        }
    }
}