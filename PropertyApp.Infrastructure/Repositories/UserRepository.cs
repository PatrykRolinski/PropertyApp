using Microsoft.EntityFrameworkCore;
using PropertyApp.Application.Contracts;
using PropertyApp.Application.Models;
using PropertyApp.Domain.Entities;
using PropertyApp.Domain.Enums;

namespace PropertyApp.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User, Guid>, IUserRepository
{
    public UserRepository(PropertyAppContext context) : base(context)
    {
    }

    public async Task<User> FindyByEmail(string email)
    {
        User userWithEmail = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower()== email.ToLower());
        return userWithEmail;

    }
    public async Task<User> FindyByVerificationToken(string token)
    {
        User userWithToken = await _context.Users.FirstOrDefaultAsync(x => x.VerificationToken == token);
        return userWithToken;

    }
    public async Task<bool> ChangeUserRole(User userToChange , RoleName Rolename)
    {
      var user= _context.Users.FirstOrDefault(x => x.Id == userToChange.Id);
        var role = await _context.Roles.FirstOrDefaultAsync(x => x.Name == Rolename.ToString());
        if(user ==null || role == null)
        {
            return false;
        }
        user.RoleId = role.Id;
       await _context.SaveChangesAsync();
        return true;
    }
   public async Task<PaginationHelper<User>> GetAllAsync(string searchPhrase, int PageSize, int PageNumber)
    {
        var baseQuery = _context.Users.Where(u => searchPhrase == null || (u.Email.ToLower().Contains(searchPhrase.ToLower())));

        var totalItemsCount = baseQuery.Count();
        var users=await baseQuery.Skip(PageSize *(PageNumber-1)).Take(PageSize).ToListAsync();

        var result = new PaginationHelper<User>() {Items= users, totalCount = totalItemsCount };
        return result;
    }
}
