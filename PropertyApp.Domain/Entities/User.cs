using PropertyApp.Domain.Common;

namespace PropertyApp.Domain.Entities;


public class User : AuditableEntity
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public Role? Role { get; set; }
    public int RoleId { get; set; }


}
