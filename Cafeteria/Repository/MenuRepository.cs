using System;
using Cafeteria.Models;
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

    public ICollection<Menu> GetMenus()
    {
        return _db.Menu
            .Include(m => m.MenuTime)
            .OrderBy(m => m.Name)
            .ToList();
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
