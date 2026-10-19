using System;
using Cafeteria.Models;
using Cafeteria.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using static System.Net.Mime.MediaTypeNames;

namespace Cafeteria.Repository;

public class RoleRepository : IRoleRepository
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RoleRepository(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }


    public List<ApplicationRole> GetRoles() =>
        _roleManager.Roles.OrderBy(r => r.Name).ToList();

    public async Task<ApplicationRole?> GetRole(string id) =>
        await _roleManager.FindByIdAsync(id);

    public bool RoleExists(string name) =>
        _roleManager.Roles.Any(r => r.Name != null && r.Name.ToLower().Trim() == name.ToLower().Trim());

    public async Task<bool> CreateRole(ApplicationRole role) =>
        (await _roleManager.CreateAsync(role)).Succeeded;

    public async Task<bool> UpdateRole(ApplicationRole role) =>
        (await _roleManager.UpdateAsync(role)).Succeeded;

    public async Task<bool> DeleteRole(ApplicationRole role) =>
        (await _roleManager.DeleteAsync(role)).Succeeded;

    /* public bool CreateRole(Role role)
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
    } */
}
