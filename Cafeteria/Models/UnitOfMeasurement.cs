using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cafeteria.Models;

[Table("unitofmeasurements")]
public class UnitOfMeasurement
{   
    [Key]
    [Column("unitofmesurementid")]
    public int UnitOfMeasurementId { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("abreviation")]
    public string Abreviation { get; set; } =string.Empty;
}
