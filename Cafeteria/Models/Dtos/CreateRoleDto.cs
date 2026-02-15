using System;
using System.ComponentModel.DataAnnotations;

namespace Cafeteria.Models.Dtos;

public class CreateRoleDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(50, ErrorMessage = "El name no puede tener más de 50 caractéres.")]
    [MinLength(3, ErrorMessage = "El name debe tener al menos 3 caractéres.")]
    public string Name {get; set;} = string.Empty;

    public int Pay {get; set;}
}
