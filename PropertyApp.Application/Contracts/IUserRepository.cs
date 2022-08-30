using PropertyApp.Application.Models;
using PropertyApp.Domain.Entities;
using PropertyApp.Domain.Enums;

namespace PropertyApp.Application.Contracts;

public interface IUserRepository : IBaseRepository<User, Guid>
{
    public Task<User> FindyByEmail(string email);
    public Task<User> FindyByVerificationToken(string token);
    public Task<bool> ChangeUserRole(User userToChange, RoleName Rolename);
    public Task<PaginationHelper<User>> GetAllAsync(string searchPhrase, int PageSize, int PageNumber);

}
