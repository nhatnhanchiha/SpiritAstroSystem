﻿using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Requests.UserRole;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpiritAstro.WebApi.Attributes;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        public UserRolesController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpPost]
        [CasbinAuthorize]
        public async Task<IActionResult> CreateUserRole([FromBody] CreateUserRoleRequest createUserRoleZodiac)
        {
            try
            {
                var postId = await _userRoleService.CreateUserRole(createUserRoleZodiac);
                return Ok(MyResponse<long>.OkWithDetail(postId,
                    $"Created success user role with userId = {postId}"));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }

        [HttpGet]
        [CasbinAuthorize]
        public async Task<IActionResult> GetListUserRole([FromQuery] UserRoleModel userRoleFilter, [FromQuery] string[] fields, string sort, int page, int limit)
        {
            try
            {
                var pzModels = await _userRoleService.GetListUserRole(userRoleFilter, page, limit, sort, fields);
                return Ok(MyResponse<PageResult<UserRoleModel>>.OkWithData(pzModels));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }

        [HttpDelete]
        [CasbinAuthorize]
        public async Task<IActionResult> DeleteUserRole([FromBody] DeleteUserRoleRequest deleteUserRoleRequest)
        {
            try
            {
                await _userRoleService.DeleteUserRole(deleteUserRoleRequest);
                return Ok(MyResponse<long>.OkWithMessage("Deleted success"));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }
    }
}
