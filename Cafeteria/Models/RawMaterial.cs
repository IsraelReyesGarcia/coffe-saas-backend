using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafeteria.Models;

[Table("rawmaterial")]
public class RawMaterial
{
    [Key]
    [Column("rawmaterialid")]
    public int RawMaterialId { get; set; }

    [MaxLength(40)]
    [Column("name")]
    public string? Name { get; set; } = string.Empty;

    [MaxLength(100)]
    [Column("description")]
    public string? Description { get; set; } = string.Empty;

    [Column("unitofmesurementid")]
    public int UnitOfMeasurementId { get; set; }
    [ForeignKey(nameof(UnitOfMeasurementId))]
    public required UnitOfMeasurement UnitOfMeasurement {get; set;}

    [Column("pricebase")]
    public float? PriceBase { get; set; }
}
