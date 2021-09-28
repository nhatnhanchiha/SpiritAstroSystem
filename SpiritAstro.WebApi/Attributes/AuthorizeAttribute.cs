using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using SpiritAstro.BusinessTier.Entities;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.Services;

namespace SpiritAstro.WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var claims = context.HttpContext.Items["claims"];
            if (claims == null)
            {
                context.Result = new JsonResult(MyResponse<dynamic>.FailWithDetail(new
                {
                    IsLogined = false
                }, "No Login")) { StatusCode = StatusCodes.Status200OK };
            }
        }
    }


    [AttributeUsage(AttributeTargets.Method)]
    public class CasbinAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var claims = (CustomClaims)context.HttpContext.Items["claims"];
            if (claims == null)
            {
                context.Result = new JsonResult(MyResponse<dynamic>.FailWithDetail(new
                {
                    IsLogined = false
                }, "No login")) { StatusCode = StatusCodes.Status200OK };
                return;
            }

            var casbinService = context.HttpContext.RequestServices.GetService<ICasbinService>();
            
            var subs = claims.Roles;
            var obj = context.HttpContext.Request.Path;
            var act = context.HttpContext.Request.Method;

            if (!casbinService!.Enforce(subs, obj, act))
            {
                context.Result = new JsonResult(MyResponse<dynamic>.FailWithMessage("Insufficient permissions"))
                    { StatusCode = StatusCodes.Status200OK };
            }
        }
    }
}