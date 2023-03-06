using CoreNotify.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace coreNotify.Core.Repositories
{
    public interface IRoleRespository
    {
        ICollection<IdentityRole> GetRoles();
    }
}
