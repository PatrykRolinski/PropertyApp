namespace PropertyApp.Application.Contracts.IServices;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? UserRole { get; }
}
