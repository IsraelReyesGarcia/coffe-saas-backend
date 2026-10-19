using System.Data.Common;
using Cafeteria.Models;
using Cafeteria.Repository.IRepository;

namespace Cafeteria.Repository;

public class WarehouseRepository : IWarehouseRepository
{

    private readonly ApplicationDbContext _db;

    public WarehouseRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool CreateWarehouse(Warehouse warehouse)
    {
        if(warehouse == null)
        {
            return false;
        }

        _db.Warehouse.Add(warehouse);

        return Save();
    }

    public bool DeleteWarehouse(Warehouse warehouse)
    {
        if(warehouse == null)
        {
            return false;
        }

        _db.Warehouse.Remove(warehouse);
        return Save();
    }

    public Warehouse? GetWarehouse(int id)
    {
        if(id <= 0)
        {
            return null;
        }

        return _db.Warehouse.FirstOrDefault(w => w.WarehouseId == id);
    }

    public ICollection<Warehouse> GetWarehouses()
    {
        return _db.Warehouse.OrderBy(w => w.Address).ToList();
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0 ? true : false;
    }

    public bool UpdateWarehouse(Warehouse warehouse)
    {
        if(warehouse == null)
        {
            return false;
        }

        _db.Warehouse.Update(warehouse);
        return Save();
    }

    public bool WarehouseExists(int id)
    {
        if(id <= 0)
        {
            return false;
        }

        return _db.Warehouse.Any(w => w.WarehouseId == id);
    }

}
