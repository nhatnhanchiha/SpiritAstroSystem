using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SpiritAstro.BusinessTier.Commons.Utils;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Requests.PriceTable;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.PriceTable;
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
    public partial interface IPriceTableService
    {
        Task<long> CreatePriceTable(CreatePriceTableRequest createPriceTable);
        Task UpdatePriceTable(long priceTableId, UpdatePriceTableRequest updatePriceTable);
        Task<PriceTableModel> GetPriceTableById(long priceTableId);
        Task<PageResult<PriceTableModel>> GetListPriceTable(PriceTableModel priceTableFilter, int page, int limit, string[] fields, string sort);
    }

    public partial class PriceTableService
    {
        private readonly IConfigurationProvider _mapper;
        private const int DefaultPaging = 10;
        private const int LimitPaging = 50;

        public PriceTableService(IUnitOfWork unitOfWork, IPriceTableRepository repository, IMapper mapper) : base(
            unitOfWork, repository)
        {
            _mapper = mapper.ConfigurationProvider;
        }

        public async Task<long> CreatePriceTable(CreatePriceTableRequest createPriceTable)
        {
            var mapper = _mapper.CreateMapper();
            var pt = mapper.Map<PriceTable>(createPriceTable);
            await CreateAsyn(pt);
            return pt.Id;
        }

        public async Task UpdatePriceTable(long priceTableId, UpdatePriceTableRequest updatePriceTable)
        {
            var priceTableInDb = await Get().FirstOrDefaultAsync(pt => pt.Id == priceTableId);
            if (priceTableInDb == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any price_table matches with id = {priceTableId}");
            }

            var mapper = _mapper.CreateMapper();
            var priceTableInRequest = mapper.Map<PriceTable>(updatePriceTable);

            priceTableInDb.Duration1 = priceTableInRequest.Duration1;
            priceTableInDb.Duration2 = priceTableInRequest.Duration2;
            priceTableInDb.Duration3 = priceTableInRequest.Duration3;
            priceTableInDb.Price1 = priceTableInRequest.Price1;
            priceTableInDb.Price2 = priceTableInRequest.Price2;
            priceTableInDb.Price3 = priceTableInRequest.Price3;

            await UpdateAsyn(priceTableInDb);
        }

        public async Task<PriceTableModel> GetPriceTableById(long priceTableId)
        {
            var priceTableModel = await Get().Where(pt => pt.Id == priceTableId).ProjectTo<PriceTableModel>(_mapper).FirstOrDefaultAsync();
            if (priceTableModel == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any price_table matches with id = {priceTableId}");
            }

            return priceTableModel;
        }

        public async Task<PageResult<PriceTableModel>> GetListPriceTable(PriceTableModel priceTableFilter, int page, int limit, string[] fields, string sort)
        {
            var (total, queryable) = Get().ProjectTo<PriceTableModel>(_mapper).DynamicFilter(priceTableFilter).PagingIQueryable(page, limit, LimitPaging, DefaultPaging);
            if (sort != null)
            {
                queryable = queryable.OrderBy(sort);
            }
            if (fields.Length > 0)
            {
                queryable = queryable.Select<PriceTableModel>(PriceTableModel.Fields.Intersect(fields).ToArray()
                    .ToDynamicSelector<PriceTableModel>());
            }
            return new PageResult<PriceTableModel>
            {
                List = await queryable.ToListAsync(),
                Page = page,
                Limit = limit,
                Total = total,
            };
        }
    }

    
}
