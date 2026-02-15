using System;

namespace Cafeteria.Models.Dtos.Product;

public class ProductResponseDto
{
    public int ProductId {get; set;}
    public string Name { get; set; } = string.Empty;
    public float Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime CreateAt {get; set;} = DateTime.UtcNow;
    public DateTime? UpdateAt {get; set;} = null;
    public int ProductCategoryId {get; set;}
    public string ProductCategorie {get;set;} = string.Empty;
}