using Microsoft.EntityFrameworkCore;
using PropertyApp.Application.Contracts;
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
}
