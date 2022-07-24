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
     var user= await _context.Users.FirstOrDefaultAsync(u=> u.Email == email);
     if (user == null) throw new Exception();           
     return user;
    }
}
