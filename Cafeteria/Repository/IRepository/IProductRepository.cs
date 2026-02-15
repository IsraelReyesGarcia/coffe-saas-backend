using System;
using Cafeteria.Models.Dtos.Product;

namespace Cafeteria.Repository.IRepository;

public interface IProductRepository
{

    // Obtener productos
    ICollection<ProductResponseDto> GetProducts();

    // Obtener producto por id
    ProductResponseDto GetProduct(int id);

    // Conocer existencia por id
    bool ProductExist(int id);

    // Conocer existencia por nombre
    bool ProductExists(string name);

    // Registrar producto
    Task<ProductDto> Register(CreateProductDto createProductDto);

    // Actualizar producto
    Task<ProductDto> Update(ProductDto productDto);

    // Eliminar producto
    bool Delete(ProductDto productDto);

    // Guardar cambios
    bool Save();
}
