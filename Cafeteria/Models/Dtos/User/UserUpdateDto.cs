using System;

namespace Cafeteria.Models.Dtos.User;

public class UserUpdateDto
{

    public  string Name {get; set;} = string.Empty;

    public string Phone {get; set;} = string.Empty;

    public string Email {get; set;} = string.Empty;

    public int RoleId {get; set; }
}
