using Cafeteria.Models;
using Cafeteria.Models.Dtos.Table;
using Cafeteria.Repository.IRepository;

namespace Cafeteria.Repository;

public class TableRepository:ITableRepository
{
    private readonly ApplicationDbContext _db;

    public TableRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<TableDto> CreateTable(CreateTableDto createTableDto)
    {
        if(createTableDto == null){
            return null;
        }

        var table = new Cafeteria.Models.Table
        {
            Description = createTableDto.Description,
            ZoneName = createTableDto.ZoneName,
            IsBusy = createTableDto.IsBusy
        };
       
       _db.Tables.Add(table);
       await _db.SaveChangesAsync();

       return new TableDto
       {
            TableId = table.TableId,
            Description = table.Description,
            ZoneName = table.ZoneName,
            IsBusy = table.IsBusy
       };

    }

    public bool DeleteTable(TableDto tableDto)
    {
        if(tableDto == null)
        {
            return false;
        }

        var table = _db.Tables.FirstOrDefault(t => t.TableId == tableDto.TableId);
        _db.Tables.Remove(table);

        return Save();
    }

    public TableDto getTable(int id)
    {
        if(id <=0)
        {
            return null;
        }
        
        var table = _db.Tables.FirstOrDefault(t => t.TableId == id);

        return new TableDto
        {
            TableId = table.TableId,
            Description = table.Description,
            ZoneName = table.ZoneName,
            IsBusy = table.IsBusy
        };
    }

    public ICollection<TableDto> getTables()
    {
        return _db.Tables
        .OrderBy(t => t.Description)
        .Select(t => new TableDto
        {
            TableId = t.TableId,
            Description = t.Description,
            ZoneName = t.ZoneName,
            IsBusy = t.IsBusy
        }).ToList();
    }

    public bool TableExist(int id)
    {
        if (id <= 0)
        {
            return false;
        }
        
        return _db.Tables.Any(t => t.TableId == id);
    }

    public async Task<TableDto> UpdateTable(TableDto tableDto)
    {
        if(tableDto == null)
        {
            return null;
        }

        var table = new Cafeteria.Models.Table
        {
            TableId = tableDto.TableId,
            Description = tableDto.Description,
            ZoneName = tableDto.ZoneName,
            IsBusy = tableDto.IsBusy
        };

        _db.Tables.Update(table);
        _db.SaveChangesAsync();

        return tableDto;
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0 ? true : false;
    }

}
