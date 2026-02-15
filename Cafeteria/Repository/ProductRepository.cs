using System;
using Cafeteria.Models.Dtos.Product;
using Cafeteria.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Cafeteria.Models;

namespace Cafeteria.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _db;
    public ProductRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public bool Delete(ProductDto productDto)
    {
        if(productDto == null)
        {
            return false;
        }

        var product = _db.Product.FirstOrDefault(p => p.ProductId == productDto.ProductId);

        _db.Product.Remove(product);

        return Save();
    }

    public ProductResponseDto GetProduct(int id)
    {
        var product = _db.Product.Include(p => p.ProductCategorie).FirstOrDefault(p => p.ProductId == id);

        if(product == null)
        {
            return null;
        }

        return new ProductResponseDto
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            CreateAt = product.CreateAt,
            UpdateAt = product.UpdateAt,
            ProductCategoryId = product.ProductCategoryId,
            ProductCategorie = product.ProductCategorie.Category
        };
    }

    public ICollection<ProductResponseDto> GetProducts()
    {
        return _db.Product
            .Include(p => p.ProductCategorie)
            .OrderBy(p => p.Name)
            .Select(p => new ProductResponseDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                CreateAt = p.CreateAt,
                UpdateAt = p.UpdateAt,
                ProductCategoryId = p.ProductCategoryId,
                ProductCategorie = p.ProductCategorie.Category
            }).ToList();
    }

    public bool ProductExist(int id)
    {
        if(id <= 0)
        {
            return false;
        }

        return _db.Product.Any(p =>p.ProductId == id);
    }

    public bool ProductExists(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        return _db.Product.Any(p => p.Name == name);
    }

    public async Task<ProductDto> Register(CreateProductDto createProductDto)
    {
        if(createProductDto == null)
        {
            return null;
        }

        var productCategory = await _db.ProductCategories.FindAsync(createProductDto.ProductCategoryId);

        var product = new Cafeteria.Models.Product
        {
            Name = createProductDto.Name,
            Price = createProductDto.Price,
            Description = createProductDto.Description,
            CreateAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
            UpdateAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
            ProductCategoryId = createProductDto.ProductCategoryId,
            ProductCategorie = productCategory!
        };

        _db.Add(product);
        await _db.SaveChangesAsync();

        return new ProductDto
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            CreateAt = product.CreateAt,
            UpdateAt = product.UpdateAt,
            ProductCategoryId = product.ProductCategoryId,
        };
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0 ? true : false;
    }

    public async Task<ProductDto> Update(ProductDto productDto)
    {
        if (productDto == null)
        {
            return null;
        }

        var productCategory = await _db.ProductCategories.FindAsync(productDto.ProductCategoryId);

        var product = new Cafeteria.Models.Product
        {
            ProductId = productDto.ProductId,
            Name = productDto.Name,
            Price = productDto.Price,
            Description = productDto.Description,
            CreateAt = productDto.CreateAt,
            UpdateAt = productDto.UpdateAt,
            ProductCategoryId = productDto.ProductCategoryId,
            ProductCategorie = productCategory!
        };

        _db.Product.Update(product);
        await _db.SaveChangesAsync();

        return productDto;
    }
}