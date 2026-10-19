using System;

namespace Cafeteria.Models.Dtos.RawMaterial;

public class CreateRawMatetialDto
{
    public string? Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public int UnitOfMeasurementId { get; set; }
    public float? PriceBase { get; set; }
}
