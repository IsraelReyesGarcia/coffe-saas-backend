using System;

namespace Cafeteria.Models.Dtos.Product;

public class CreateProductDto
{
    public string Name { get; set; } = string.Empty;
    public float Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public int ProductCategoryId {get; set;}
}
