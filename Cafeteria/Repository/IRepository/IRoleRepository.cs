using System;
using Cafeteria.Models;

namespace Cafeteria.Repository.IRepository;

public interface IRoleRepository
{
    ICollection<Role> getRoles();

    Role? GetRole(int id);

    bool RoleExists(int id);

    bool RoleExists(string Name);

    bool CreateRole(Role role);

    bool UpdateRole(Role role);

    bool DeleteRole(Role role);

    bool Save(); 
}
