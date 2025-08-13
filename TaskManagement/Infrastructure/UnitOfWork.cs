using TaskManagement.Application.Services;

namespace TaskManagement.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserService UserService => throw new NotImplementedException();

        public IProjectService ProjectService => throw new NotImplementedException();

        public Task BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task CommitTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task RollbackTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }

}
