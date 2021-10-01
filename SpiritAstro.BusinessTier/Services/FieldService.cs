using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SpiritAstro.BusinessTier.Commons.Utils;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Requests.Field;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.Field;
using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface IFieldService
    {
        Task<long> CreateField(CreateFieldRequest createFieldRequest);
        Task<FieldModel> GetFieldById(long fieldId);
        Task UpdateField(long fieldId, UpdateFieldRequest updateFieldRequest);
        Task DeleteField(long fieldId);
        Task<PageResult<FieldModel>> GetListField(FieldModel fieldFilter, int page, int limit, string[] fields, string sort);
    }
    public partial class FieldService
    {
        private readonly IConfigurationProvider _mapper;
        private const int DefaultPaging = 10;
        private const int LimitPaging = 50;

        public FieldService(IUnitOfWork unitOfWork, IFieldRepository repository, IMapper mapper) : base(
            unitOfWork, repository)
        {
            _mapper = mapper.ConfigurationProvider;
        }

        public async Task<PageResult<FieldModel>> GetListField(FieldModel fieldFilter, int page, int limit, string[] fields, string sort)
        {
            var (total, queryable) = Get().ProjectTo<FieldModel>(_mapper).DynamicFilter(fieldFilter).PagingIQueryable(page, limit, LimitPaging, DefaultPaging);
            if (sort != null)
            {
                queryable = queryable.OrderBy(sort);
            }
            if (fields.Length > 0)
            {
                queryable = queryable.Select<FieldModel>(FieldModel.Fields.Intersect(fields).ToArray()
                    .ToDynamicSelector<FieldModel>());
            }
            return new PageResult<FieldModel>
            {
                List = await queryable.ToListAsync(),
                Page = page,
                Limit = limit,
                Total = total,
            };
        }

        public async Task<long> CreateField(CreateFieldRequest createFieldRequest)
        {
            var mapper = _mapper.CreateMapper();
            var field = mapper.Map<Field>(createFieldRequest);
            field.CreatedAt = DateTimeOffset.Now;
            await CreateAsyn(field);
            return field.Id;
        }

        public async Task<FieldModel> GetFieldById(long fieldId)
        {
            var fieldModel = await Get().Where(f => f.Id == fieldId).ProjectTo<FieldModel>(_mapper).FirstOrDefaultAsync();
            if (fieldModel == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any field matches with id = {fieldId}");
            }

            return fieldModel;
        }

        public async Task UpdateField(long fieldId, UpdateFieldRequest updateFieldRequest)
        {
            var fieldInDb = await Get().FirstOrDefaultAsync(f => f.Id == fieldId);
            if (fieldInDb == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any field matches with id = {fieldId}");
            }

            var mapper = _mapper.CreateMapper();
            var fieldInRequest = mapper.Map<Field>(updateFieldRequest);

            fieldInDb.Name = fieldInRequest.Name;
            fieldInDb.Exp = fieldInRequest.Exp;
            fieldInDb.PriceTableId = fieldInRequest.PriceTableId;
            fieldInDb.UpdatedAt = DateTimeOffset.Now;

            await UpdateAsyn(fieldInDb);
        }

        public async Task DeleteField(long fieldId)
        {
            var fieldInDb = await Get().FirstOrDefaultAsync(f => f.Id == fieldId);
            if (fieldInDb == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any field matches with id = {fieldId}");
            }

            fieldInDb.DeletedAt = DateTimeOffset.Now;

            await UpdateAsyn(fieldInDb);
        }
    }
}
