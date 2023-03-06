namespace coreNotify.Core.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        IRoleRespository Role { get; }
    }
}
