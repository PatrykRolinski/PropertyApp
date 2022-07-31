using Microsoft.EntityFrameworkCore;
using PropertyApp.Application.Contracts;
using PropertyApp.Domain.Entities;


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
}
