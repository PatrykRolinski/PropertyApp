namespace PropertyApp.Application.Functions.Users.Queries
{
    public class GetUsersListDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
    }
}