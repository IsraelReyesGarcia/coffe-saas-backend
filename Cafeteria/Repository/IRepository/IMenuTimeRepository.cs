using Cafeteria.Models;
using Cafeteria.Models.Dtos.MenuTime;

namespace Cafeteria.Repository.IRepository
{
    public interface IMenuTimeRepository
    {
         //Obtener todos los Tiempos de Menú
         ICollection<MenuTime> GetMenuTimes();

         //Obtener un solo tiempo de Menú
         MenuTime? GetMenuTime(int id);

         //Conocer si existe el tiempo de Menú
         bool MenuTimeExists(int id);

         bool MenuTimeExists(string name);

         //Crear un tiempo de Menú
         bool CreateMenuTime(MenuTime menuTime);

         //Eliminar tiempo de menú
         bool DeleteMenuTime(MenuTime menuTime);

         //Actualizar tiempo de Menú
         bool UpdateMenuTime(MenuTime menuTime);

         //Guardar cambios
         bool Save();
    }
}