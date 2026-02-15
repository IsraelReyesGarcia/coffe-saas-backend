using System;
using Cafeteria.Models;
using Cafeteria.Models.Dtos.Company;

namespace Cafeteria.Repository.IRepository;

public interface ICompanyRepository
{
    //Obtener una lista de las compañias
    ICollection<CompanyDto> GetCompanies();

    //Obtener una compañia por id
    CompanyDto GetCompany(int id);

    //Crear compañia
    Task<CompanyDto> Register(CreateCompanyDto createCompanyDto);

    //Actualizar una compañia
    Task<CompanyDto> Update(UpdateCompanyDto updateCompanyDto);

    //Eliminar Compañias
    bool Delete(Company company);

    bool Save();
}
