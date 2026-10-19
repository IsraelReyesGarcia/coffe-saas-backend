using System;

namespace Cafeteria.Models.Dtos.Responses;

public class PaginationResponse<T> // La <T> significa que esta clase 
// será genérica (puede usarse en donde sea)
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int Total { get; set; }
    public ICollection<T> Data { get; set; } = new List<T>();
}
