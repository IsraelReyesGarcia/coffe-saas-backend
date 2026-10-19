using System;
using System.ComponentModel.DataAnnotations;

namespace Cafeteria.Models.Dtos;

public class RoleDto
{
    public string Id { get; set; } = string.Empty;

    public string Name {get; set;} = string.Empty;

    public float Pay {get; set;}
}