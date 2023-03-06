using coreNotify.Core.Repositories;

namespace coreNotify.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository User { get; }

        public IRoleRespository Role { get; }


        public UnitOfWork(IUserRepository user, IRoleRespository role)
        {
            User = user;
            Role = role;
        }
    }
}
