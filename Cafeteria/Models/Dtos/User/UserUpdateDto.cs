using System;
using System.ComponentModel.DataAnnotations;
namespace Cafeteria.Models.Dtos.User;

public class UserUpdateDto
{
    [Required(ErrorMessage = "El campo usaername es requerido")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo name es requerido")]
    public string Name { get; set; } = string.Empty;

    public string? Phone {get; set;} = string.Empty;

    public String? Role { get; set; }
}
