using System;

namespace Cafeteria.Models.Dtos.RawMaterial;

public class RawMaterialDto
{
    public int RawMaterialId { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public int UnitOfMeasurementId { get; set; }
    public string UnitOfMeasurement {get;set;} = string.Empty;
    public float? PriceBase { get; set; }
}
