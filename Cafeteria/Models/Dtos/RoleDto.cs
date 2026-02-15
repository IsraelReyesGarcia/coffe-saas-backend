using System;
using System.ComponentModel.DataAnnotations;

namespace Cafeteria.Models.Dtos;

public class RoleDto
{
    public int roleid {get; set;}

    public string name {get; set;} = string.Empty;

    public int pay {get; set;}
}
