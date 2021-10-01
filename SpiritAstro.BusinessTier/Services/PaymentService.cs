using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SpiritAstro.BusinessTier.Commons.Utils;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Requests.Payment;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.Payment;
using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface IPaymentService
    {
        Task<long> CreatePayment(CreatePaymentRequest createPayment);
        Task UpdatePayment(long paymentId, UpdatePaymentRequest updatePayment);
        Task<PaymentModel> GetPaymentById(long PaymentId);
        Task<PageResult<PaymentModel>> GetListPayment(PaymentModel paymentFilter, int page, int limit, string[] fields, string sort);
        Task DeletePayment(long paymentId);
    }

    public partial class PaymentService
    {
        private readonly IConfigurationProvider _mapper;
        private const int DefaultPaging = 10;
        private const int LimitPaging = 50;

        public PaymentService(IUnitOfWork unitOfWork, IPaymentRepository repository, IMapper mapper) : base(
            unitOfWork, repository)
        {
            _mapper = mapper.ConfigurationProvider;
        }

        public async Task<long> CreatePayment(CreatePaymentRequest createPayment)
        {
            var mapper = _mapper.CreateMapper();
            var payment = mapper.Map<Payment>(createPayment);
            await CreateAsyn(payment);
            return payment.Id;
        }
        public async Task UpdatePayment(long paymentId, UpdatePaymentRequest updatePayment)
        {
            var paymentInDb = await Get().FirstOrDefaultAsync(p => p.Id == paymentId);
            if (paymentInDb == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any field matches with id = {paymentInDb}");
            }

            var mapper = _mapper.CreateMapper();
            var paymentInRequest = mapper.Map<Payment>(updatePayment);
            paymentInDb.BookingId = paymentInRequest.BookingId;

            await UpdateAsyn(paymentInDb);
        }

        public async Task<PaymentModel> GetPaymentById(long PaymentId)
        {
            var paymentModel = await Get().Where(p => p.Id == PaymentId).ProjectTo<PaymentModel>(_mapper).FirstOrDefaultAsync();
            if (paymentModel == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any payment matches with id = {PaymentId}");
            }

            return paymentModel;
        }

        public async Task<PageResult<PaymentModel>> GetListPayment(PaymentModel paymentFilter, int page, int limit, string[] fields, string sort)
        {
            var (total, queryable) = Get().ProjectTo<PaymentModel>(_mapper).DynamicFilter(paymentFilter).PagingIQueryable(page, limit, LimitPaging, DefaultPaging);
            if (sort != null)
            {
                queryable = queryable.OrderBy(sort);
            }
            if (fields.Length > 0)
            {
                queryable = queryable.Select<PaymentModel>(PaymentModel.Fields.Intersect(fields).ToArray()
                    .ToDynamicSelector<PaymentModel>());
            }
            return new PageResult<PaymentModel>
            {
                List = await queryable.ToListAsync(),
                Page = page,
                Limit = limit,
                Total = total,
            };
        }

        public async Task DeletePayment(long paymentId)
        {
            var paymentInDb = await Get().FirstOrDefaultAsync(p => p.Id == paymentId);
            if (paymentInDb == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any payment matches with id = {paymentId}");
            }

            await DeleteAsyn(paymentInDb);
        }
    }
}
