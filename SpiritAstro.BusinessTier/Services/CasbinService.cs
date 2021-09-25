using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Casbin.Adapter.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetCasbin;
using SpiritAstro.BusinessTier.Requests.Casbin;

namespace SpiritAstro.BusinessTier.Services
{
    public interface ICasbinService
    {
        bool Enforce(string subs, string obj, string act);
        List<List<string>> GetAllPermissions(string role);
        Task AddPermission(AddPolicyRequest addPolicyRequest);
        Task RemovePermission(RemovePolicyRequest removePolicyRequest);
    }
    
    public class CasbinService: ICasbinService
    {
        private readonly Enforcer _enforcer;
        public CasbinService(IConfiguration configuration)
        {
            var options = new DbContextOptionsBuilder<CasbinDbContext<int>>()
                .UseSqlServer(configuration["ConnectionStrings:DbContext"])
                .Options;
            var dbContext = new CasbinDbContext<int>(options);
            var efCoreAdapter = new EFCoreAdapter<int>(dbContext);
            _enforcer = new Enforcer("Resources/rbac_model.conf", efCoreAdapter);
        }

        public bool Enforce(string subs, string obj, string act)
        {
            var subjects = new List<string>(subs.Split(","));
            return subjects.Any(subject => _enforcer.Enforce(subject, obj, act));
        }

        public List<List<string>> GetAllPermissions(string role)
        {
            var allRoles = _enforcer.GetPermissionsForUser(role);
            return allRoles;
        }

        public async Task AddPermission(AddPolicyRequest addPolicyRequest)
        {
            await _enforcer.AddPolicyAsync(addPolicyRequest.Subject, addPolicyRequest.Object, addPolicyRequest.Action);
        }

        public async Task RemovePermission(RemovePolicyRequest removePolicyRequest)
        {
            await _enforcer.RemovePolicyAsync(removePolicyRequest.Subject, removePolicyRequest.Object,
                removePolicyRequest.Action);
        }
    }
}