using Cafeteria.Models;
using Cafeteria.Repository.IRepository;

namespace Cafeteria.Repository
{
    public class MenuTimeRepository : IMenuTimeRepository
    {   
        private readonly ApplicationDbContext _db;
        public MenuTimeRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        //MenuTime se refiere a los tiempos de Menú: Desayuno, Almuerzo, Cena, etc.
        public bool CreateMenuTime(MenuTime menuTime)
        {
            if(menuTime == null){
                return false;
            }

            menuTime.CreateAt = DateTime.UtcNow;
            _db.MenuTimes.Add(menuTime);
            return Save();
        }

        public bool DeleteMenuTime(MenuTime menuTime)
        {
            if(menuTime == null)
            {
                return false;
            }

            _db.MenuTimes.Remove(menuTime);
            return Save();
        }

        public MenuTime? GetMenuTime(int id)
        {
            if(id <= 0)
            {
                return null;
            }

            return _db.MenuTimes.FirstOrDefault(m => m.MenuTimeId == id);
        }

        public ICollection<MenuTime> GetMenuTimes()
        {
            return _db.MenuTimes.OrderBy(m => m.Name).ToList();
        }

        public bool MenuTimeExists(int id)
        {
            if(id <= 0){
                return false;
            }

            return _db.MenuTimes.Any(m => m.MenuTimeId == id);
        }

        public bool MenuTimeExists(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            return _db.MenuTimes.Any(m => m.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateMenuTime(MenuTime menuTime)
        {
            if(menuTime == null)
            {
                return false;
            }

            _db.MenuTimes.Update(menuTime);
            return Save();
        }
    }
}