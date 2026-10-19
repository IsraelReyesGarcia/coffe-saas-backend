using System;
using Cafeteria.Models;

namespace Cafeteria.Repository.IRepository;

public interface IUnitOfMeasurementRepository
{
    // Obtener todas las unidades
    ICollection<UnitOfMeasurement> GetUnitOfMeasurements();

    // Obtener una sola unidad
    UnitOfMeasurement GetUnitOfMeasurement(int id);

    // Saber si existe una unidad
    bool UnitOfMeasurementExists(int id);

    // Crear una unidad
    bool CreateUnitOfMeasurement(UnitOfMeasurement unitOfMeasurement);

    // Editar una unidad
    bool UpdateUnitOfMeasurement(UnitOfMeasurement unitOfMeasurement);

    // Eliminar una unidad 
    bool DeleteUnitOfMeasurement(UnitOfMeasurement unitOfMeasurement);
    
    // Guardar cambios
    bool Save();
}
