namespace Cafeteria.Models.Dtos.Warehouse;

public class WarehouseDto
{
    public int WarehouseId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? UserId { get; set; }
}
