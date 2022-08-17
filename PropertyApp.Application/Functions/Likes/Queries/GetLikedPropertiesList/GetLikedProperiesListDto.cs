using PropertyApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyApp.Application.Functions.Likes.Queries.GetLikedPropertiesList
{
    public class GetLikedProperiesListDto
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public PropertyType PropertyType { get; set; }
        public ushort PropertySize { get; set; }
        public byte NumberOfRooms { get; set; }
        public PropertyStatus PropertyStatus { get; set; }
        public string? MainPhotoUrl { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
    }
}
