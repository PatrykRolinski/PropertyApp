using PropertyApp.Domain.Common;
using System.Text.Json;

namespace PropertyApp.API.Extensions;

public static class HttpExtension
{
    public static void AddPaginationHeader(this HttpResponse respone, int currentPage, int itemsPerPage, int totalItems, int totalPages, int itemFrom, int itemTo)
    {
        var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages, itemFrom, itemTo);

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,

        };
        respone.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader, options));
        respone.Headers.Add("Access-Control-Expose-Headers", "Pagination");
    }
}

