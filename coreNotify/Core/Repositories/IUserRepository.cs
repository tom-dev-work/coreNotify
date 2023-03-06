using CoreNotify.Areas.Identity.Data;

namespace coreNotify.Core.Repositories
{
    public interface IUserRepository
    {
        ICollection<NotifyUser> GetUsers();
        NotifyUser GetUser(string id);

        NotifyUser UpdateUser(NotifyUser user);
    }
}
