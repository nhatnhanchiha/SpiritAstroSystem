
/////////////////////////////////////////////////////////////////
//
//              AUTO-GENERATED
//
/////////////////////////////////////////////////////////////////

using SpiritAstro.DataTier.Models;
using Microsoft.Extensions.DependencyInjection;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Generations.Repositories;
using Microsoft.EntityFrameworkCore;
using SpiritAstro.DataTier.BaseConnect;
using Test.DataTier.BaseConnect;

namespace SpiritAstro.BusinessTier.Generations.DependencyInjection
{
    public static class DependencyInjectionResolverGen
    {
        public static void InitializerDI(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DbContext, SpiritAstroSystemContext>();
        
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostRepository, PostRepository>();
        
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
        
            services.AddScoped<ISubCategoryService, SubCategoryService>();
            services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
        
            services.AddScoped<ISubCategoryPostService, SubCategoryPostService>();
            services.AddScoped<ISubCategoryPostRepository, SubCategoryPostRepository>();
        
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
        
            services.AddScoped<IUserPostService, UserPostService>();
            services.AddScoped<IUserPostRepository, UserPostRepository>();
        
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        }
    }
}
