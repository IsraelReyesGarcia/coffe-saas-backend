using System;
using System.ComponentModel.DataAnnotations;

namespace Cafeteria.Models.Dtos.User;

public class UserLoginDto
{


    [Required(ErrorMessage = "El campo UserName es requerido")]
    public string Username {get; set;} = string.Empty;

    [Required(ErrorMessage = "El campo password es requerido")]
    public string Password {get; set;} = string.Empty;

}
