using Cafeteria.Models.Dtos.Table;

namespace Cafeteria.Repository.IRepository;

public interface ITableRepository
{
    // Obtener todas las mesas
    ICollection<TableDto> getTables();

    // Obtener mesa por id
    TableDto getTable(int id);

    // Obtener existencia

    bool TableExist(int id);
    // Crear mesa
    Task<TableDto> CreateTable(CreateTableDto createTableDto);

    // Editar mesa
    Task<TableDto> UpdateTable(TableDto tableDto);

    // Eliminar mesa
    bool DeleteTable(TableDto tableDto);

    //Guardar cambios
    bool Save();
}
