using TaskManagement.Application.Services;

namespace TaskManagement.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IUserService UserService { get; }
        IProjectService ProjectService { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }

}
