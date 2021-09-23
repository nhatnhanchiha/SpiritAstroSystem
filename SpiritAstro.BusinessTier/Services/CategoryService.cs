using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.Category;
using SpiritAstro.DataTier.BaseConnect;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface ICategoryService
    {
        Task<CategoryModel> GetCategoryById(long categoryId);
    }
    
    public partial class CategoryService
    {
        private readonly IConfigurationProvider _mapper;

        public CategoryService(IUnitOfWork unitOfWork, ICategoryRepository repository, IMapper mapper) : base(
            unitOfWork, repository)
        {
            _mapper = mapper.ConfigurationProvider;
        }

        public async Task<CategoryModel> GetCategoryById(long categoryId)
        {
            var categoryModel = await Get().Where(c => c.Id == categoryId).ProjectTo<CategoryModel>(_mapper).FirstOrDefaultAsync();
            if (categoryModel == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any category matches with id = {categoryId}");
            }

            return categoryModel;
        } 
    }
}