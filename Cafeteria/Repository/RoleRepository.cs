using System;
using Cafeteria.Models;
using Cafeteria.Repository.IRepository;
using static System.Net.Mime.MediaTypeNames;

namespace Cafeteria.Repository;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _db; 

    public RoleRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool CreateRole(Role role)
    {
        _db.Roles.Add(role);
        return Save();
    }

    public bool DeleteRole(Role role)
    {
        _db.Roles.Remove(role);
        return Save();
    }

    public ICollection<Role> getRoles()
    {
        return _db.Roles.OrderBy(r => r.name).ToList();
    }
    public Role? GetRole(int id)
    {
        return _db.Roles.FirstOrDefault(r => r.roleid == id);
    }

    public bool RoleExists(int id)
    {
        return _db.Roles.Any(r => r.roleid == id);
    }

    public bool RoleExists(string name)
    {
        return _db.Roles.Any(r => r.name.ToLower().Trim() == name.ToLower().Trim());
    }

    public bool Save()
    {
        return _db.SaveChanges()>= 0 ? true : false; 
    }

    public bool UpdateRole(Role role)
    {
        _db.Roles.Update(role);
        return Save();
    }
}
