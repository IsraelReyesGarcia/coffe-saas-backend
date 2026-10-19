using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafeteria.Models;

[Table("orderproduct")]
public class OrderProduct
{
    [Key]
    [Column("orderproductid")]
    public int OrderProductId { get; set; }

    [Column("orderid")]
    public int OrderId { get; set; }

    [ForeignKey(nameof(OrderId))]
    public required Order Order { get; set; }

    [Column("productid")]
    public int ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public required Product Product {get; set;}

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

    [Column("subtotal")]
    public decimal Subtotal { get; set; }

}
