using System;
using System.ComponentModel.DataAnnotations;

namespace Cafeteria.Models.Dtos.User;

public class CreateUserDto
{
    /* public  string Name {get; set;} = string.Empty;

    public string Phone {get; set;} = string.Empty;

    public string Email {get; set;} = string.Empty;

    public string? Password {get; set;} = string.Empty;

    public string? Role {get;set;} = string.Empty;
    public int RoleId {get; set; } */

    [Required(ErrorMessage = "El campo username es requerido")]
    public string? Username { get; set; }
    [Required(ErrorMessage = "El campo name es requerido")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "El campo password es requerido")]
    public string? Password { get; set; }
    [Required(ErrorMessage = "El campo email es requerido")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "El campo role es requerido")]
    public string? Role { get; set; }
}