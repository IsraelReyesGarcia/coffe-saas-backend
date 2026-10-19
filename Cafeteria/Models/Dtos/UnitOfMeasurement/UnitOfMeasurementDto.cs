using System;

namespace Cafeteria.Models.Dtos.UnitOfMeasurement;

public class UnitOfMeasurementDto
{
    public int UnitOfMeasurementId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Abreviation { get; set; } =string.Empty;
}
