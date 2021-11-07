using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SpiritAstro.BusinessTier.Commons.Utils;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.Astrologer;
using System.Linq.Dynamic.Core;
using SpiritAstro.BusinessTier.Requests.AstroChart;
using SpiritAstro.BusinessTier.Requests.Astrologer;
using SpiritAstro.BusinessTier.Requests.UserRole;
using SpiritAstro.BusinessTier.Services;
using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface IAstrologerService
    {
        Task IsAstrologer(long astrologerId);

        Task<PageResult<PublicAstrologerModel>> GetAllAstrologers(PublicAstrologerModel filter, string[] fields,
            string sort, int page, int limit);

        Task<PageResult<PublicAstrologerModelForAdmin>> GetAllAstrologersForAdmin(PublicAstrologerModelForAdmin filter,
            string[] fields,
            string sort, int page, int limit);

        Task<PublicAstrologerModel> GetPublicAstrologerById(long astrologerId);
        Task<AstrologerModel> GetAstrologerById(long astrologerId);
        Task RegisterAnAstrologer(RegisterAstrologerRequest registerAstrologerRequest);
        Task UpdateAnAstrologer(long astrologerId, UpdateAstrologerRequest updateAstrologerRequest);

        Task DeleteAnAstrologer(long astrologerId);
    }

    public partial class AstrologerService
    {
        private readonly IConfigurationProvider _mapper;
        private readonly IUserRoleService _userRoleService;
        private const int DefaultPaging = 20;
        private const int LimitPaging = 20;
        private readonly IAstroChartService _astroChartService;

        public AstrologerService(IUnitOfWork unitOfWork, IAstrologerRepository repository, IMapper mapper,
            IUserRoleService userRoleService, IAstroChartService astroChartService) : base(
            unitOfWork, repository)
        {
            _userRoleService = userRoleService;
            _astroChartService = astroChartService;
            _mapper = mapper.ConfigurationProvider;
        }

        public async Task IsAstrologer(long astrologerId)
        {
            var astrologer = await Get().FirstOrDefaultAsync(a => a.Id == astrologerId && a.DeletedAt == null);
            if (astrologer == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any astrologer has id = {astrologerId}");
            }
        }

        public async Task<PageResult<PublicAstrologerModel>> GetAllAstrologers(PublicAstrologerModel filter,
            string[] fields, string sort, int page, int limit)
        {
            var (total, queryable) = Get().Where(a => a.DeletedAt == null).ProjectTo<PublicAstrologerModel>(_mapper)
                .DynamicFilter(filter)
                .PagingIQueryable(page, limit, LimitPaging, DefaultPaging);
            if (sort != null)
            {
                queryable = queryable.OrderBy(sort);
            }

            if (fields.Length > 0)
            {
                queryable = queryable.Select<PublicAstrologerModel>(PublicAstrologerModel.Fields.Intersect(fields)
                    .ToArray()
                    .ToDynamicSelector<PublicAstrologerModel>());
            }

            return new PageResult<PublicAstrologerModel>
            {
                List = await queryable.ToListAsync(),
                Page = page,
                Limit = limit,
                Total = total
            };
        }

        public async Task<PageResult<PublicAstrologerModelForAdmin>> GetAllAstrologersForAdmin(
            PublicAstrologerModelForAdmin filter, string[] fields, string sort, int page, int limit)
        {
            var (total, queryable) = Get()
                .Where(a => filter.IsDeleted == null || ((bool)filter.IsDeleted
                    ? a.DeletedAt != null
                    : a.DeletedAt == null)).ProjectTo<PublicAstrologerModelForAdmin>(_mapper).DynamicFilter(filter)
                .PagingIQueryable(page, limit, LimitPaging, DefaultPaging);

            if (sort != null)
            {
                queryable = queryable.OrderBy(sort);
            }


            if (fields.Length > 0)
            {
                queryable = queryable.Select<PublicAstrologerModelForAdmin>(PublicAstrologerModelForAdmin.Fields
                    .Intersect(fields).ToArray()
                    .ToDynamicSelector<PublicAstrologerModelForAdmin>());
            }

            return new PageResult<PublicAstrologerModelForAdmin>
            {
                List = await queryable.ToListAsync(),
                Page = page,
                Limit = limit,
                Total = total
            };
        }

        public async Task<PublicAstrologerModel> GetPublicAstrologerById(long astrologerId)
        {
            var astrologer = await Get().ProjectTo<PublicAstrologerModel>(_mapper)
                .FirstOrDefaultAsync(a => a.Id == astrologerId && a.DeletedAt == null);
            if (astrologer == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any astrologer matches id = {astrologerId}");
            }

            return astrologer;
        }

        public async Task<AstrologerModel> GetAstrologerById(long astrologerId)
        {
            var astrologer = await Get().ProjectTo<AstrologerModel>(_mapper)
                .FirstOrDefaultAsync(a => a.Id == astrologerId && a.DeletedAt == null);
            if (astrologer == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any astrologer matches with id = {astrologerId}");
            }

            return astrologer;
        }

        public async Task RegisterAnAstrologer(RegisterAstrologerRequest registerAstrologerRequest)
        {
            var mapper = _mapper.CreateMapper();
            var astrologer = mapper.Map<Astrologer>(registerAstrologerRequest);
            var astrologerInDb = await Get().FirstOrDefaultAsync(a => a.Id == astrologer.Id);

            if (astrologerInDb == null)
            {
                astrologer.CreatedAt = DateTimeOffset.Now;
                astrologer.UpdatedAt = DateTimeOffset.Now;
                var transaction = await repository.BeginTransaction();
                try
                {
                    await CreateAsyn(astrologer);
                    await _userRoleService.CreateUserRole(new CreateUserRoleRequest
                    {
                        UserId = astrologer.Id,
                        RoleId = "8888"
                    });
                    
                    var getNatalChartRequest = new GetNatalChartRequest
                    {
                        Coordinates = Coordinates.FromLatLong(astrologer.LongitudeOfBirth, astrologer.LatitudeOfBirth),
                        TimeOfBirthInUtcTime = astrologer.TimeOfBirth.DateTime
                    };

                    var url = await _astroChartService.Execute(getNatalChartRequest);

                    astrologer.NatalChartUrl = url;
                    await UpdateAsyn(astrologer);
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw new ErrorResponse((int)HttpStatusCode.InternalServerError,
                        "Error when creating an astrologer");
                }

                return;
            }


            if (astrologerInDb.DeletedAt == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.BadRequest, "This user has been an astrologer already!");
            }

            astrologer.UpdatedAt = DateTimeOffset.Now;
            await UpdateAsyn(astrologer);
        }

        public async Task UpdateAnAstrologer(long astrologerId, UpdateAstrologerRequest updateAstrologerRequest)
        {
            var astrologerInDb = await Get().FirstOrDefaultAsync(a => a.Id == astrologerId && a.DeletedAt == null);
            if (astrologerInDb == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any astrologer matches with id = {astrologerId}");
            }

            var mapper = _mapper.CreateMapper();
            var astrologerInRequest = mapper.Map<Astrologer>(updateAstrologerRequest);


            astrologerInDb.Name = astrologerInRequest.Name;
            astrologerInDb.PhoneNumber = astrologerInRequest.PhoneNumber;
            astrologerInDb.Gender = astrologerInRequest.Gender;
            astrologerInDb.LatitudeOfBirth = astrologerInRequest.LatitudeOfBirth;
            astrologerInDb.LongitudeOfBirth = astrologerInRequest.LongitudeOfBirth;
            astrologerInDb.TimeOfBirth = astrologerInRequest.TimeOfBirth;
            astrologerInDb.Description = astrologerInRequest.Description;
            astrologerInDb.ImageUrl = astrologerInRequest.ImageUrl;
            
            var getNatalChartRequest = new GetNatalChartRequest
            {
                Coordinates = Coordinates.FromLatLong(astrologerInDb.LongitudeOfBirth, astrologerInDb.LatitudeOfBirth),
                TimeOfBirthInUtcTime = astrologerInDb.TimeOfBirth.DateTime
            };

            var url = await _astroChartService.Execute(getNatalChartRequest);
            astrologerInDb.NatalChartUrl = url;

            await UpdateAsyn(astrologerInDb);
        }

        public async Task DeleteAnAstrologer(long astrologerId)
        {
            var astrologer = await Get().FirstOrDefaultAsync(a => a.Id == astrologerId && a.DeletedAt == null);
            if (astrologer == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any astrologer matches with id = {astrologerId}");
            }

            astrologer.DeletedAt = DateTimeOffset.Now;


            var transaction = await repository.BeginTransaction();
            try
            {
                await UpdateAsyn(astrologer);
                await _userRoleService.DeleteAsyn(new UserRole
                {
                    UserId = astrologer.Id,
                    RoleId = "8888"
                });
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw new ErrorResponse((int)HttpStatusCode.InternalServerError, "Error when deleting an astrologer");
            }
        }
    }
}