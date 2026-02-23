using System;
using Cafeteria.Models;

namespace Cafeteria.Repository.IRepository;

public interface IMenuRepository
{
    //Obtener todos los Menus
    ICollection<Menu> GetMenus();

    //Obtener un menú por id
    Menu? GetMenu(int id);

    //Saber si existe un menú por id
    bool MenuExists(int id);

    //Crear un menú
    bool CreateMenu(Menu menu);

    //Actualizar menú
    bool UpdateMenu(Menu menu);

    //Eliminar menú
    bool DeleteMenu(Menu menu);

    //Guardar los cambios
    bool Save();
}
