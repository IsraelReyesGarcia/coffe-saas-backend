using System;
using Cafeteria.Models;
using Cafeteria.Models.Dtos.Menu;
using Cafeteria.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Cafeteria.Repository;

public class MenuRepository : IMenuRepository
{
    private readonly ApplicationDbContext _db;

    public MenuRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool CreateMenu(Menu menu)
    {
        if(menu == null)
        {
            return false;
        }

        _db.Menu.Add(menu);
        return Save();
    }

    public bool DeleteMenu(Menu menu)
    {
        if(menu == null)
        {
            return false;
        }

        _db.Menu.Remove(menu);
        return Save();
    }

    public Menu? GetMenu(int id)
    {
        if(id <= 0){
            return null;
        }

        return _db.Menu.Include(m => m.MenuTime).FirstOrDefault(m => m.MenuId == id);
    }

    public ICollection<Menu> GetMenus(string? searchText = null)
    {
        var query = _db.Menu.AsQueryable();
        if (!string.IsNullOrWhiteSpace(searchText))
            query = query.Where(m=> m.Name.Contains(searchText)
            || m.MenuTime.Name.Contains(searchText));

        return query
            .Include(m => m.MenuTime)
            .OrderBy(m => m.Name)
            .ToList();
    }

    public ICollection<MenuResponseDto> GetMenusInPage(int pageNumber, int pageSize, string? searchText = null)
    {
        var query = _db.Menu.AsQueryable();
        if(!string.IsNullOrWhiteSpace(searchText))
            query = query.Where(m => m.Name.Contains(searchText)
            || m.MenuTime.Name.Contains(searchText));
        
        return query
        .Include(m => m.MenuTime)
        .Skip((pageNumber -1) * pageSize).Take(pageSize)
        .OrderBy(m => m.Name)
        .Select(m => new MenuResponseDto
        {
            MenuId = m.MenuId,
            Name = m.Name,
            Description = m.Description,
            DateRegister = m.DateRegister,
            MenuTimeId = m.MenuTimeId,
            MenuType = m.MenuTime.Name
        })
        .ToList();
    }

    public int GetTotalMenus(string? searchText = null)
    {
        var query = _db.Menu.AsQueryable();
        if (!string.IsNullOrWhiteSpace(searchText))
            query = query.Where(m => m.Name.Contains(searchText)
            || m.MenuTime.Name.Contains(searchText));

        return query.Count();
    }

    public bool MenuExists(int id)
    {
        if(id <= 0 ){
            return false;
        }

        return _db.Menu.Any(m => m.MenuId == id);
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0 ? true : false;
    }

    public bool UpdateMenu(Menu menu)
    {
        if(menu == null){
            return false;
        }

        _db.Menu.Update(menu);

        return Save();
    }
    
}
