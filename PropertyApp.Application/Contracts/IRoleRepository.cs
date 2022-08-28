namespace PropertyApp.Application.Contracts;

public interface IRoleRepository
{
    public Task<int> GetRoleId(string roleName);
}
