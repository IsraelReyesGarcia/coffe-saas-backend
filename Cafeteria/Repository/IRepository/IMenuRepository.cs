using System;
using Cafeteria.Models;
using Cafeteria.Models.Dtos.Menu;

namespace Cafeteria.Repository.IRepository;

public interface IMenuRepository
{
    //Obtener todos los Menus
    ICollection<Menu> GetMenus(string? searchText = null);

    //Obtener los Menus paginados
    ICollection<MenuResponseDto> GetMenusInPage(int pageNumber, int pageSize, string? searchText = null);

    // Obtener el total de Menus
    int GetTotalMenus(string? searchText = null);

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
