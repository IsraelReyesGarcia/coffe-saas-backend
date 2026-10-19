using Cafeteria.Models;
using Cafeteria.Models.Dtos.ProductImage;

namespace Cafeteria.Repository.IRepository;

public interface IProductImageRepository
{
    // Obtener registros de imágenes de productos
    ICollection<ProductImage> GetProductImages();

    // Devolver registros paginados
    ICollection<ProductImage> GetProductImagesInPages(int pageNumber, int pageSize);

    // Obtener el total de productos
    int GetTotalProductImages();

    // Obtener imágen por Id
    ProductImage? GetProductImages(int id);

    // Obtener imágenes de un producto
    ICollection<ProductImage> GetImagesByProduct(int productId);

    // Conocer existencia por id
    bool ProductImageExits(int id);

    // Registrar imágen
    bool CreateProductImage(ProductImage productImage);

    //Eliminar imágen
    bool DeleteProductImage(ProductImage productImage);

    // Guardar imágen
    bool Save();
}
