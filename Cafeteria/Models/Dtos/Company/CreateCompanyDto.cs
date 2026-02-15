using System;

namespace Cafeteria.Models.Dtos.Company;

public class CreateCompanyDto
{
    public string Name {get; set;} = string.Empty;

    public string Rfc {get; set;} = string.Empty;

    public string Address {get; set;} = string.Empty;

    public string Phone {get; set;} = string.Empty;

    public string Email {get; set;} = string.Empty;

    public DateTime CreateAt {get; set;} = DateTime.Now;

    public int UserId {get; set;}
}
