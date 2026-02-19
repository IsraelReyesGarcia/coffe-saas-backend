using System;
using Cafeteria.Models;
using Cafeteria.Models.Dtos.Company;
using Cafeteria.Repository.IRepository;

namespace Cafeteria.Repository;

public class CompanyRepository : ICompanyRepository
{
    private readonly ApplicationDbContext _db;

    public CompanyRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public bool Delete(Company company)
    {
        _db.Company.Remove(company);
        return Save();
    }

    public ICollection<CompanyDto> GetCompanies()
    {
        return _db.Company
            .OrderBy(c => c.Name)
            .Select(c => new CompanyDto
            {
                CompanyId = c.CompanyId,
                Name = c.Name,
                Rfc = c.Rfc,
                Address = c.Address,
                Phone = c.Phone,
                Email = c.Email,
                CreateAt = c.CreateAt,
                UpdateAt = c.UpdateAt,
                UserId = c.UserId ?? 0
            })
            .ToList();
    }

    public CompanyDto GetCompany(int id)
    {
        var result = _db.Company.FirstOrDefault(c => c.CompanyId == id);

        if (result == null)
        {
            return null!;
        }

        return new CompanyDto
        {
            CompanyId = result.CompanyId,
            Name = result.Name,
            Rfc = result.Rfc,
            Address = result.Address,
            Phone = result.Phone,
            CreateAt = result.CreateAt,
            UpdateAt = result.UpdateAt,
            UserId = result.UserId ?? 0
        };
    }

    public async Task<CompanyDto> Register(CreateCompanyDto createCompanyDto)
    {
        var user = await _db.Users.FindAsync(createCompanyDto.UserId);
        if (user == null)
        {
            return null!;
        }

        var company = new Cafeteria.Models.Company
        {
            Name = createCompanyDto.Name,
            Rfc = createCompanyDto.Rfc,
            Address = createCompanyDto.Address,
            Phone = createCompanyDto.Phone,
            Email = createCompanyDto.Email,
            CreateAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
            UpdateAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
            UserId = createCompanyDto.UserId,
            User = user
        };
        _db.Company.Add(company);
        await _db.SaveChangesAsync();

        return new CompanyDto
        {
            CompanyId = company.CompanyId,
            Name = company.Name,
            Rfc = company.Rfc,
            Address = company.Address,
            Phone = company.Phone,
            Email = company.Email,
            CreateAt = company.CreateAt,
            UpdateAt = company.UpdateAt,
            UserId = user.UserId
        };
    }

    public bool Save()
    {
        return _db.SaveChanges() >= 0 ? true : false; 
    }

    public async Task<CompanyDto> Update(UpdateCompanyDto updateCompanyDto)
    {
        if(updateCompanyDto == null){
            return null!;
        }

        var user = await _db.Users.FindAsync(updateCompanyDto.UserId);
        if (user == null)
        {
            return null!;
        }

        var company = new Cafeteria.Models.Company 
        {
            CompanyId = updateCompanyDto.CompanyId,
            Name = updateCompanyDto.Name,
            Rfc = updateCompanyDto.Rfc,
            Address  = updateCompanyDto.Address,
            Phone = updateCompanyDto.Phone,
            UpdateAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
            UserId = updateCompanyDto.UserId,
            User = user
        };

        _db.Company.Update(company);
        await _db.SaveChangesAsync();

        return new CompanyDto {
            CompanyId = company.CompanyId,
            Name = company.Name,
            Rfc = company.Rfc,
            Address = company.Address,
            Phone = company.Phone,
            CreateAt = company.CreateAt,
            UpdateAt = company.UpdateAt,
            UserId = company.UserId ?? 0
        };
    }
}
