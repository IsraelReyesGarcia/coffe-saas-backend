using Cafeteria.Models.Dtos.ProductCategorie;
using static System.Net.Mime.MediaTypeNames;

namespace Cafeteria.Repository.IRepository;

public interface IProductCategorieRepository
{

    //Obtener todos los ProductsCategorie
    ICollection<ProductCategorieDto> GetProductCategories();

    //Obtener ProductCategorie por id
    ProductCategorieDto GetProductCategorie(int id);

    // Buscar si existe ProductCategorie por id
    bool ProductCategorieExist(int id);

    // Buscar si existe ProductCategorie por name
    bool ProductCategorieExist(string name);

    // Crear ProductCategorie
    Task<ProductCategorieDto> Register(CreateProductCategorieDto createProductCategorieDto);

    //Actualizar ProductCategorie
    Task<ProductCategorieDto> Update(ProductCategorieDto productCategorieDto);

    // Eliminar ProductCategorie
    bool Delete(ProductCategorieDto productCategorieDto);

    // Guardar Cambios
    bool Save();
    
}
