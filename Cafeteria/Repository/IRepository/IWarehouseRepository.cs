using Cafeteria.Models;

namespace Cafeteria.Repository.IRepository;

public interface IWarehouseRepository
{
    // Obtener todos los Warehouse
    ICollection<Warehouse> GetWarehouses();

    // Obtener un solo Warehouse
    Warehouse? GetWarehouse(int id);

    // Saber Si existe un Warehouse
    bool WarehouseExists(int id);

    // Crear un Warehouse
    bool CreateWarehouse(Warehouse warehouse);

    // Actualizar un Warehouse
    bool UpdateWarehouse(Warehouse warehouse);

    // Eliminar Warehouse
    bool DeleteWarehouse(Warehouse warehouse);

    // Guardar cambios
    bool Save();
}
