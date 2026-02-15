using System;

namespace Cafeteria.Models.Dtos.User;

public class UserResponseDto
{

    public int UserId {get; set;}
    public  string Name {get; set;} = string.Empty;

    public string? Phone {get; set;} = string.Empty;

    public string Email {get; set;} = string.Empty;

    public int RoleId {get; set; }

    public string RoleName {get; set;} = string.Empty;

    public DateTime? CreateAt {get; set;}

    public DateTime? UpdateAt {get; set;}
}
