using Cafeteria.Models;
using Cafeteria.Models.Dtos.InputMaterialCategory;
using Cafeteria.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Cafeteria.Repository;

public class InputMaterialCategoryRepository : IInputMaterialCategoryRepository
{
    private readonly ApplicationDbContext _db;
    public InputMaterialCategoryRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public ICollection<InputMaterialCategoryDto> GetInputCategories()
    {
        return _db.InputMaterialCategories
            .OrderBy(c => c.Name)
            .Select(c => new InputMaterialCategoryDto
            {
                InputMaterialCategoryId = c.InputMaterialCategoryId,
                Name = c.Name,
                SuggestionUssageType = c.SuggestionUssageType
            })
            .ToList();
    }

    public InputMaterialCategoryDto? GetInputMaterialCategory(int id)
    {
        if (id <= 0)
        {
            return null;
        }

        var InputMaterialCategory = _db.InputMaterialCategories.FirstOrDefault(c => c.InputMaterialCategoryId == id);
        if (InputMaterialCategory == null) return null;

        return new InputMaterialCategoryDto
        {
            InputMaterialCategoryId = InputMaterialCategory.InputMaterialCategoryId,
            Name = InputMaterialCategory.Name,
            SuggestionUssageType = InputMaterialCategory.SuggestionUssageType
        };
    }

    public bool InputMaterialCategoryExist(int id)
    {
        if (id <= 0)
        {
            return false;
        }

        return _db.InputMaterialCategories.Any(c => c.InputMaterialCategoryId == id);
    }

    public async Task<InputMaterialCategoryDto> CreateCategory(InputMaterialCategoryDto InputMaterialCategoryDto)
    {
        if (InputMaterialCategoryDto == null)
        {
            return null;
        }

        var inputMaterialCategory = new InputMaterialCategory
        {
            Name = InputMaterialCategoryDto.Name,
            SuggestionUssageType = InputMaterialCategoryDto.SuggestionUssageType,
        };

        _db.InputMaterialCategories.Add(inputMaterialCategory);
        await _db.SaveChangesAsync();

        InputMaterialCategoryDto.InputMaterialCategoryId = inputMaterialCategory.InputMaterialCategoryId;
        return InputMaterialCategoryDto;
    }

    public async Task<InputMaterialCategoryDto> UpdateCategory(InputMaterialCategoryDto InputMaterialCategoryDto)
    {
        if (InputMaterialCategoryDto == null)
        {
            return null;
        }

        var category = new InputMaterialCategory
        {
            InputMaterialCategoryId = InputMaterialCategoryDto.InputMaterialCategoryId,
            Name = InputMaterialCategoryDto.Name,
            SuggestionUssageType = InputMaterialCategoryDto.SuggestionUssageType,
        };

        _db.InputMaterialCategories.Update(category);
        await _db.SaveChangesAsync();

        return new InputMaterialCategoryDto
        {
            InputMaterialCategoryId = category.InputMaterialCategoryId,
            Name = category.Name,
            SuggestionUssageType = category.SuggestionUssageType,
        };
    }

    public bool DeleteCategory(InputMaterialCategoryDto InputMaterialCategoryDto)
    {
        if (InputMaterialCategoryDto == null)
        {
            return false;
        }

        var category = _db.InputMaterialCategories.FirstOrDefault(c => c.InputMaterialCategoryId == InputMaterialCategoryDto.InputMaterialCategoryId);
        _db.InputMaterialCategories.Remove(category);
        return Save();
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0 ? true : false;
    }
}
