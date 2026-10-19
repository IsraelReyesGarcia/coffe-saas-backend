using System;

namespace Cafeteria.Models.Dtos.OrderProduct;

public class CreateOrderProductDto
{
    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal Subtotal { get; set; }
}
