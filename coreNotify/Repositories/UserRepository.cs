using coreNotify.Core.Repositories;
using coreNotify.Data;
using CoreNotify.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace coreNotify.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public NotifyUser GetUser(string id)
        {
            return _applicationDbContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public ICollection<NotifyUser> GetUsers()
        {
            return _applicationDbContext.Users.ToList();
        }

		public NotifyUser UpdateUser(NotifyUser user)
		{
            _applicationDbContext.Update(user);
            _applicationDbContext.SaveChanges();

            return user;
		}
	}
}
