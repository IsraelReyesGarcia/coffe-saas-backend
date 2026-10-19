using System;

namespace Cafeteria.Models.Dtos.UnitOfMeasurement;

public class CreateUnitOfMeasurementDto
{
    public string Name { get; set; } = string.Empty;

    public string Abreviation { get; set; } =string.Empty;
}
