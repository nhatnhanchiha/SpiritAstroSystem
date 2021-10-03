using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SpiritAstro.BusinessTier.Commons.Utils;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Requests.FieldDetail;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.FieldDetail;
using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface IFieldDetailService
    {
        Task<PageResult<FieldDetailModel>> GetAllFieldDetail(FieldDetailModel filter, string[] fields,
            string sort, int page, int limit);

        Task CreateFieldDetail(CreateFieldDetailRequest createFieldDetailRequest);
        Task UpdateFieldDetail(UpdateFieldDetailRequest updateFieldDetailRequest);
        Task DeleteFieldDetail(DeleteFieldDetailRequest deleteFieldDetailRequest);
    }

    public partial class FieldDetailService
    {
        private readonly IConfigurationProvider _mapper;
        private const int DefaultPaging = 20;
        private const int LimitPaging = 20;

        public FieldDetailService(IUnitOfWork unitOfWork, IFieldDetailRepository repository, IMapper mapper) : base(
            unitOfWork, repository)
        {
            _mapper = mapper.ConfigurationProvider;
        }

        public async Task<PageResult<FieldDetailModel>> GetAllFieldDetail(FieldDetailModel filter, string[] fields,
            string sort, int page, int limit)
        {
            var (total, queryable) = Get().ProjectTo<FieldDetailModel>(_mapper).DynamicFilter(filter).PagingIQueryable(page, limit, LimitPaging, DefaultPaging);
            
            if (sort != null)
            {
                queryable = queryable.OrderBy(sort);
            }

            if (fields.Length > 0)
            {
                queryable = queryable.Select<FieldDetailModel>(FieldDetailModel.Fields.Intersect(fields).ToArray()
                    .ToDynamicSelector<FieldDetailModel>());
            }

            return new PageResult<FieldDetailModel>
            {
                List = await queryable.ToListAsync(),
                Page = page,
                Limit = limit,
                Total = total
            };
        }

        public async Task CreateFieldDetail(CreateFieldDetailRequest createFieldDetailRequest)
        {
            var fieldDetailInDb = await Get().FirstOrDefaultAsync(fd => fd.AstrologerId == createFieldDetailRequest.AstrologerId && fd.FieldId == createFieldDetailRequest.FieldId);
            if (fieldDetailInDb != null)
            {
                throw new ErrorResponse((int)HttpStatusCode.BadRequest, "This entry has been in system");
            }

            var mapper = _mapper.CreateMapper();
            var fieldDetail = mapper.Map<FieldDetail>(createFieldDetailRequest);
            await CreateAsyn(fieldDetail);
        }

        public async Task UpdateFieldDetail(UpdateFieldDetailRequest updateFieldDetailRequest)
        {
            var fieldDetailInDb = await Get().FirstOrDefaultAsync(fd => fd.AstrologerId == updateFieldDetailRequest.AstrologerId && fd.FieldId == updateFieldDetailRequest.FieldId);
            if (fieldDetailInDb == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.BadRequest, "Cannot find this entry in system");
            }

            var mapper = _mapper.CreateMapper();
            var fieldDetail = mapper.Map<FieldDetail>(updateFieldDetailRequest);

            await CreateAsyn(fieldDetail);
        }

        public async Task DeleteFieldDetail(DeleteFieldDetailRequest deleteFieldDetailRequest)
        {
            var fieldDetail = await Get().FirstOrDefaultAsync(fd => fd.AstrologerId == deleteFieldDetailRequest.AstrologerId && fd.FieldId == deleteFieldDetailRequest.FieldId);
            if (fieldDetail == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.BadRequest, "Cannot find this entry in system");
            }

            await DeleteAsyn(fieldDetail);
        }
    }
}