using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Contracts;

public interface IUserRepository : IBaseRepository<User, Guid>
{
    public Task<User> FindyByEmail(string email);
}
