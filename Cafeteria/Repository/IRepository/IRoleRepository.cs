using System;
using Cafeteria.Models;

namespace Cafeteria.Repository.IRepository;

public interface IRoleRepository
{
    List<ApplicationRole> GetRoles();
    Task<ApplicationRole?> GetRole(string id);
    bool RoleExists(string Name);
    Task<bool> CreateRole(ApplicationRole role);
    Task<bool> UpdateRole(ApplicationRole role);
    Task<bool> DeleteRole(ApplicationRole role);
    /* Role? GetRole(int id);

    bool RoleExists(int id);

    bool RoleExists(string Name);

    bool CreateRole(Role role);

    bool UpdateRole(Role role);

    bool DeleteRole(Role role);

    bool Save();  */
}
