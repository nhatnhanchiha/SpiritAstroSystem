
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
namespace SpiritAstro.BusinessTier.Generations.DependencyInjection
{
    public static class DependencyInjectionResolverGen
    {
        public static void InitializerDI(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DbContext, SpiritAstroContext>();
        
            services.AddScoped<IAstrologerService, AstrologerService>();
            services.AddScoped<IAstrologerRepository, AstrologerRepository>();
        
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IBookingRepository, BookingRepository>();
        
            services.AddScoped<ICasbinRuleService, CasbinRuleService>();
            services.AddScoped<ICasbinRuleRepository, CasbinRuleRepository>();
        
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        
            services.AddScoped<ICustomerZodiacService, CustomerZodiacService>();
            services.AddScoped<ICustomerZodiacRepository, CustomerZodiacRepository>();
        
            services.AddScoped<IFamousPersonService, FamousPersonService>();
            services.AddScoped<IFamousPersonRepository, FamousPersonRepository>();
        
            services.AddScoped<IFieldService, FieldService>();
            services.AddScoped<IFieldRepository, FieldRepository>();
        
            services.AddScoped<IFieldDetailService, FieldDetailService>();
            services.AddScoped<IFieldDetailRepository, FieldDetailRepository>();
        
            services.AddScoped<IFollowService, FollowService>();
            services.AddScoped<IFollowRepository, FollowRepository>();
        
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
        
            services.AddScoped<IPlanetService, PlanetService>();
            services.AddScoped<IPlanetRepository, PlanetRepository>();
        
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostRepository, PostRepository>();
        
            services.AddScoped<IPostPlanetService, PostPlanetService>();
            services.AddScoped<IPostPlanetRepository, PostPlanetRepository>();
        
            services.AddScoped<IPostZodiacService, PostZodiacService>();
            services.AddScoped<IPostZodiacRepository, PostZodiacRepository>();
        
            services.AddScoped<IPriceTableService, PriceTableService>();
            services.AddScoped<IPriceTableRepository, PriceTableRepository>();
        
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
        
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
        
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
        
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<IWalletRepository, WalletRepository>();
        
            services.AddScoped<IZodiacService, ZodiacService>();
            services.AddScoped<IZodiacRepository, ZodiacRepository>();
        }
    }
}
