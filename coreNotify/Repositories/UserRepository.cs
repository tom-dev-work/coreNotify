using coreNotify.Core.Repositories;
using coreNotify.Data;
using CoreNotify.Areas.Identity.Data;

namespace coreNotify.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public ICollection<NotifyUser> GetUsers()
        {
            return _applicationDbContext.Users.ToList();
        }
    }
}
