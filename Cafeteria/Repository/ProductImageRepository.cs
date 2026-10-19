using Cafeteria.Models;
using Cafeteria.Models.Dtos.ProductImage;
using Cafeteria.Repository.IRepository;

namespace Cafeteria.Repository
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly ApplicationDbContext _db;
        
        public ProductImageRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateProductImage(ProductImage productImage)
        {
            if (productImage == null)
            {
                return false;
            }

            _db.ProductImages.Add(productImage);

            return Save();
        }

        public bool DeleteProductImage(ProductImage productImage)
        {
            if (productImage == null)
            {
                return false;
            }

            _db.ProductImages.Remove(productImage);
            return Save();
        }

        public ICollection<ProductImage> GetProductImages()
        {
            return _db.ProductImages.OrderBy(p => p.OriginalFileName).ToList();
        }

        public ProductImage? GetProductImages(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return _db.ProductImages.FirstOrDefault(p => p.ProductImageId == id);
        }

        public ICollection<ProductImage> GetProductImagesInPages(int pageNumber, int pageSize)
        {
            var query = _db.ProductImages.AsQueryable();

            return query
                .OrderBy(p => p.ProductImageId)
                .Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .ToList();
        }

        public int GetTotalProductImages()
        {
            return _db.ProductImages.Count();
        }

        public ICollection<ProductImage> GetImagesByProduct(int productId)
        {
            return _db.ProductImages
                .Where(i => i.ProductId == productId)
                .OrderBy(i => i.CreatedAt)
                .ToList();
        }

        public bool ProductImageExits(int id)
        {
            if (id <= 0)
            {
                return false;
            }

            return _db.ProductImages.Any(p => p.ProductImageId == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

    }
}