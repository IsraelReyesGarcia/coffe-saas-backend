using System;
using Cafeteria.Models;
using Cafeteria.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Cafeteria.Repository;

public class RawMaterialRepository : IRawMaterialRepository
{
    private readonly ApplicationDbContext _db;

    public RawMaterialRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public bool CreateRawMaterial(RawMaterial rawMaterial)
    {
        if(rawMaterial == null)
        {
            return false;
        }

        _db.RawMaterial.Add(rawMaterial);
        return Save();
    }

    public bool DeleteRawMaterial(RawMaterial rawMaterial)
    {
        if(rawMaterial == null)
        {
            return false;
        }

        _db.RawMaterial.Remove(rawMaterial);
        return Save();
    }

    public RawMaterial GetRawMaterial(int id)
    {
        if(id <= 0){
            return null;
        }

        return _db.RawMaterial.Include(r => r.UnitOfMeasurement).FirstOrDefault(r => r.RawMaterialId == id);
    }

    public ICollection<RawMaterial> GetRawMaterials()
    {
        return _db.RawMaterial
            .Include(r => r.UnitOfMeasurement)
            .OrderBy(r => r.Name)
            .ToList();
    }

    public bool RawMaterialExists(int id)
    {
        if(id <= 0)
        {
            return false;
        }

        return _db.RawMaterial.Any(r => r.RawMaterialId == id);
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0 ? true: false;
    }

    public bool UpdateRawMaterial(RawMaterial rawMaterial)
    {
        if(rawMaterial == null)
        {
            return false;
        }

        _db.RawMaterial.Update(rawMaterial);
        return Save();
    }
}
