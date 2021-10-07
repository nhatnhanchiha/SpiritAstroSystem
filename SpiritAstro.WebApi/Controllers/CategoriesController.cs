using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Requests.Category;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.Category;
using SpiritAstro.WebApi.Attributes;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet]
        public async Task<IActionResult> GetListCategories([FromQuery]CategoryModel categoryFilter, [FromQuery] string[] fields, string sort, int page, int limit)
        {
            try
            {
                var categoryModels = await _categoryService.GetListCategories(categoryFilter, fields, sort, page, limit);
                return Ok(MyResponse<PageResult<CategoryModel>>.OkWithData(categoryModels));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetCategoryById(long id)
        {
            try
            {
                var categoryModel = await _categoryService.GetCategoryById(id);
                return Ok(MyResponse<CategoryModel>.OkWithData(categoryModel));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    (int)HttpStatusCode.NotFound => Ok(MyResponse<object>.FailWithMessage(e.Error.Message)),
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }

        [HttpPost]
        [CasbinAuthorize]
        public async Task<IActionResult> CreateNewCategory([FromBody] CreateCategoryRequest createCategoryRequest)
        {
            try
            {
                var categoryId = await _categoryService.CreateCategory(createCategoryRequest);
                return Ok(MyResponse<long>.OkWithDetail(categoryId,
                    $"Created success category with id = {categoryId}"));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }


        [HttpPut("{id:long}")]
        [CasbinAuthorize]
        public async Task<IActionResult> UpdateCategory(long id, [FromBody] UpdateCategoryRequest updateCategoryRequest)
        {
            try
            {
                await _categoryService.UpdateCategory(id, updateCategoryRequest);
                return Ok(MyResponse<object>.OkWithMessage("Updated success"));
            }
            catch (ErrorResponse e)
            {
                if (e.Error.Code == (int)HttpStatusCode.NotFound)
                {
                    return Ok(MyResponse<object>.FailWithMessage("Updated fail. " + e.Error.Message));
                }

                return Ok(MyResponse<object>.FailWithMessage("Updated fail. " + e.Error.Message));
            }
        }

        [HttpDelete("{id:long}")]
        [CasbinAuthorize]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            try
            {
                await _categoryService.DeleteCategory(id);
                return Ok(MyResponse<object>.OkWithMessage("Deleted success"));
            }
            catch (ErrorResponse e)
            {
                if (e.Error.Code == (int)HttpStatusCode.NotFound)
                {
                    return Ok(MyResponse<object>.FailWithMessage("Deleted fail. " + e.Error.Message));
                }

                return Ok(MyResponse<object>.FailWithMessage("Deleted fail. " + e.Error.Message));
            }
        }
    }
}