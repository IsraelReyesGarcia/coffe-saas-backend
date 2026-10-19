using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafeteria.Models;

[Table("warehouse")]
public class Warehouse
{
    [Key]
    [Column("warehouseid")]
    public int WarehouseId { get; set; }
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    [Column("address")]
    public string Address { get; set; } = string.Empty;
    [Column("userid")]
    public string? UserId { get; set; }
    [ForeignKey(nameof(UserId))]

    public ApplicationUser User {get; set;}


}
