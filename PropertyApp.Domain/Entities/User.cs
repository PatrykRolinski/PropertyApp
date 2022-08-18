using PropertyApp.Domain.Common;

namespace PropertyApp.Domain.Entities;
public class User : AuditableEntity
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public string? VerificationToken { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public string? PasswordResetToken { get; set; }
    public DateTime? ResetTokenExpires { get; set; }

    public virtual Role? Role { get; set; }
    public int RoleId { get; set; }
    public virtual ICollection<Property>? CreatedProperties { get; set; }
    public virtual ICollection<LikeProperty>? LikedProperties { get; set; }

    public virtual ICollection<Message>? MessageRecived { get; set;}
    public virtual ICollection<Message>? MessageSent { get; set; }


}
