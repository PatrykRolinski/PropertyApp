using Microsoft.EntityFrameworkCore;
using PropertyApp.Application.Contracts;

namespace PropertyApp.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly PropertyAppContext _appContext;

    public RoleRepository(PropertyAppContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<int> GetRoleId(string roleName)
    {
        var role=await _appContext.Roles.FirstOrDefaultAsync(r => r.Name.ToLower() == roleName.ToLower());
        return role.Id;
    }
}
