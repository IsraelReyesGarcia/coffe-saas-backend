using Cafeteria.Models.Dtos.InputMaterialCategory;

namespace Cafeteria.Repository.IRepository;

public interface IInputMaterialCategoryRepository
{
    // Obtener categorias
    ICollection<InputMaterialCategoryDto> GetInputCategories();

    // Obtener categoría por ID
    InputMaterialCategoryDto? GetInputMaterialCategory(int id);

    // Saber si existe un cliente por id
    bool InputMaterialCategoryExist(int id);

    // Crear un categoría
    Task<InputMaterialCategoryDto> CreateCategory(InputMaterialCategoryDto InputMaterialCategoryDto);

    // Actualizar una categoría
    Task<InputMaterialCategoryDto> UpdateCategory(InputMaterialCategoryDto InputMaterialCategoryDto);

    // Eliminar una categoría
    bool DeleteCategory(InputMaterialCategoryDto InputMaterialCategoryDto);

    // Guardar cambios
    bool Save();
}
