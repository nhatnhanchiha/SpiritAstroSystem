using AutoMapper;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Requests.Transaction;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.Transaction;
using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using AutoMapper.QueryableExtensions;
using SpiritAstro.BusinessTier.Commons.Utils;
using Microsoft.EntityFrameworkCore;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface ITransactionService
    {
        Task<long> CreateTransaction(CreateTransactionRequest createTransaction);
        Task<PageResult<TransactionModel>> GetListTransaction(TransactionModel transactionFilter, int page, int limit, string sort, string[] fields);
    }
    public partial class TransactionService
    {
        private readonly IConfigurationProvider _mapper;
        private const int DefaultPaging = 10;
        private const int LimitPaging = 50;

        public TransactionService(IUnitOfWork unitOfWork, ITransactionRepository repository, IMapper mapper) : base(
            unitOfWork, repository)
        {
            _mapper = mapper.ConfigurationProvider;
        }

        public async Task<long> CreateTransaction(CreateTransactionRequest createTransaction)
        {
            var mapper = _mapper.CreateMapper();
            var t = mapper.Map<Transaction>(createTransaction);
            await CreateAsyn(t);
            return t.PaymentId;
        }

        public async Task<PageResult<TransactionModel>> GetListTransaction(TransactionModel transactionFilter, int page, int limit, string sort, string[] fields)
        {
            var (total, queryable) = Get().ProjectTo<TransactionModel>(_mapper)
                 .DynamicFilter(transactionFilter).PagingIQueryable(page, limit, LimitPaging, DefaultPaging);
            if (sort != null)
            {
                queryable = queryable.OrderBy(sort);
            }
            if (fields.Length > 0)
            {
                queryable = queryable.Select<TransactionModel>(TransactionModel.Fields.Intersect(fields).ToArray()
                    .ToDynamicSelector<TransactionModel>());
            }
            return new PageResult<TransactionModel>
            {
                List = await queryable.ToListAsync(),
                Page = page,
                Limit = limit,
                Total = total,
            };
        }
    }
}
