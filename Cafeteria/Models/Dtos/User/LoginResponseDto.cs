using System;

namespace Cafeteria.Models.Dtos.User;

public class LoginResponseDto
{
    public  int UserId {get; set;}

    public string Name {get; set;} = string.Empty;

    public string Email {get; set;} = string.Empty;

    public string RoleName {get; set;} = string.Empty;

    public string Token {get; set;} = string.Empty;

    public DateTime TokenExpiration {get; set; }
}
