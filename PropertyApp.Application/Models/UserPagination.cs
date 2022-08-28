using PropertyApp.Domain.Entities;

namespace PropertyApp.Application.Models
{
    public class UserPagination
    {
        public List<User>? Users { get; set; }
        public int totalCount { get; set; }
    }
}