using System;

namespace Cafeteria.Models.Dtos.Company;

public class UpdateCompanyDto
{
    public int CompanyId {get;set;}
    public string Name {get; set;} = string.Empty;

    public string Rfc {get; set;} = string.Empty;

    public string Address {get; set;} = string.Empty;

    public string Phone {get; set;} = string.Empty;

    public int UserId {get; set;}
}
