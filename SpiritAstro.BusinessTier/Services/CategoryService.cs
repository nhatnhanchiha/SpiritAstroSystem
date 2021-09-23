using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Requests.Category;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.Category;
using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface ICategoryService
    {
        Task<CategoryModel> GetCategoryById(long categoryId);
        Task<long> CreateCategory(CreateCategoryRequest createCategoryRequest);

        Task UpdateCategory(long categoryId, UpdateCategoryRequest updateCategoryRequest);
        Task DeleteCategory(long categoryId);
    }
    
    public partial class CategoryService
    {
        private readonly IConfigurationProvider _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IFieldRepository repository, IMapper mapper) : base(
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

        public async Task<long> CreateCategory(CreateCategoryRequest createCategoryRequest)
        {
            var mapper = _mapper.CreateMapper();
            var category = mapper.Map<Category>(createCategoryRequest);
            await CreateAsyn(category);
            return category.Id;
        }


        public async Task UpdateCategory(long categoryId, UpdateCategoryRequest updateCategoryRequest)
        {
            var categoryInDb = await Get().FirstOrDefaultAsync(c => c.Id == categoryId);
            if (categoryInDb == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any category matches with id = {categoryId}");
            }
            
            var mapper = _mapper.CreateMapper();
            var categoryInRequest = mapper.Map<Category>(updateCategoryRequest);

            categoryInDb.Name = categoryInRequest.Name;

            await UpdateAsyn(categoryInDb);
        }

        public async Task DeleteCategory(long categoryId)
        {
            var categoryInDb = await Get().FirstOrDefaultAsync(c => c.Id == categoryId);
            if (categoryInDb == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any category matches with id = {categoryId}");
            }
            
            await DeleteAsyn(categoryInDb);
        }
    }
}