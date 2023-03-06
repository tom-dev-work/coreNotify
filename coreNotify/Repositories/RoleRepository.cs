using coreNotify.Core.Repositories;
using coreNotify.Data;
using coreNotify.Models;
using CoreNotify.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace coreNotify.Repositories
{
    public class RoleRepository : IRoleRespository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RoleRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public ICollection<IdentityRole> GetRoles()
        {
            return _applicationDbContext.Roles.ToList();  
        }
    }
}
