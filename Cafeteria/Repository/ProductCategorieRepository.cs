using Cafeteria.Models.Dtos.ProductCategorie;
using Cafeteria.Repository.IRepository;

namespace Cafeteria.Repository;

public class ProductCategorieRepository : IProductCategorieRepository
{
    private readonly ApplicationDbContext _db;

    public ProductCategorieRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public bool Delete(ProductCategorieDto productCategorieDto)
    {
        if(productCategorieDto == null)
        {
            return false;
        }

        var productCategorie = _db.ProductCategories.FirstOrDefault(p => p.ProductCategorieId == productCategorieDto.ProductCategorieId);
        if(productCategorie == null)
        {
            return false;
        }

        _db.ProductCategories.Remove(productCategorie);

        return Save();
    }

    public ProductCategorieDto GetProductCategorie(int id)
    {
        if(id <= 0)
        {
            return null;
        }

        var productCategorie = _db.ProductCategories.FirstOrDefault(p => p.ProductCategorieId == id);
        if(productCategorie == null)
        {
            return null;
        }

        var productCagorieDto = new ProductCategorieDto
        {
          ProductCategorieId = productCategorie.ProductCategorieId,
          Category = productCategorie.Category  
        };

        return productCagorieDto;
    }

    public ICollection<ProductCategorieDto> GetProductCategories()
    {
        return _db.ProductCategories
        .OrderBy(p=>p.ProductCategorieId)
        .Select(p => new ProductCategorieDto
        {
            ProductCategorieId = p.ProductCategorieId,
            Category = p.Category
        })
        .ToList();
    }

    public bool ProductCategorieExist(int id)
    {
        if(id <= 0)
        {
            return false;
        }

        return _db.ProductCategories.Any(p => p.ProductCategorieId == id);
    }

    public bool ProductCategorieExist(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        return _db.ProductCategories.Any(p => p.Category.ToLower().Trim() == name.ToLower().Trim());
    }

    public async Task<ProductCategorieDto> Register(CreateProductCategorieDto createProductCategorieDto)
    {
        if(createProductCategorieDto == null)
        {
            return new ProductCategorieDto
            {
                ProductCategorieId = 0,
                Category = "--"
            };
        }

        var productCategorie = new Cafeteria.Models.ProductCategorie
        {
          Category = createProductCategorieDto.Category
        };

        _db.ProductCategories.Add(productCategorie);
        await _db.SaveChangesAsync();

        return new ProductCategorieDto
        {
          ProductCategorieId = productCategorie.ProductCategorieId,
          Category = productCategorie.Category  
        };
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0 ? true: false;
    }

    public async Task<ProductCategorieDto> Update(ProductCategorieDto productCategorieDto)
    {
        if(productCategorieDto == null)
        {
            return null;
        }

        var productCategorie = new Cafeteria.Models.ProductCategorie
        {
            ProductCategorieId = productCategorieDto.ProductCategorieId,
            Category = productCategorieDto.Category
        };

        _db.ProductCategories.Update(productCategorie);

        await _db.SaveChangesAsync();

        return new ProductCategorieDto
        {
          ProductCategorieId = productCategorie.ProductCategorieId,
          Category = productCategorie.Category  
        };

    }

}
