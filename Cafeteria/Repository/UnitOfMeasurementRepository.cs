using System;
using Cafeteria.Models;
using Cafeteria.Repository.IRepository;

namespace Cafeteria.Repository;

public class UnitOfMeasurementRepository : IUnitOfMeasurementRepository
{
    private readonly ApplicationDbContext _db;

    public UnitOfMeasurementRepository( ApplicationDbContext db)
    {
        _db = db;
    }

    public bool CreateUnitOfMeasurement(UnitOfMeasurement unitOfMeasurement)
    {
        if(unitOfMeasurement == null)
        {
            return false;
        }

        _db.UnitOfMeasurements.Add(unitOfMeasurement);
        return Save();
    }

    public bool DeleteUnitOfMeasurement(UnitOfMeasurement unitOfMeasurement)
    {
        if(unitOfMeasurement == null)
        {
            return false;
        }

        _db.UnitOfMeasurements.Remove(unitOfMeasurement);
        return Save();
    }

    public UnitOfMeasurement GetUnitOfMeasurement(int id)
    {
        if(id <= 0)
        {
            return null;
        }

        return _db.UnitOfMeasurements.FirstOrDefault(u => u.UnitOfMeasurementId == id);

    }

    public ICollection<UnitOfMeasurement> GetUnitOfMeasurements()
    {
        return _db.UnitOfMeasurements.OrderBy(u => u.Name).ToList();
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0 ? true : false;
    }

    public bool UnitOfMeasurementExists(int id)
    {
        if(id <= 0)
        {
            return false;
        }

        return _db.UnitOfMeasurements.Any(u => u.UnitOfMeasurementId == id);
    }

    public bool UpdateUnitOfMeasurement(UnitOfMeasurement unitOfMeasurement)
    {
        if(unitOfMeasurement == null)
        {
            return false;
        }

        _db.UnitOfMeasurements.Update(unitOfMeasurement);
        return Save();
    }
}
