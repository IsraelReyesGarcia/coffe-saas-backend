using System;

namespace Cafeteria.Models.Dtos.Menu;

public class MenuDto
{
    public int MenuId {get;set;}

    public string Name {get;set;} = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime DateRegister { get; set; } = DateTime.UtcNow;
    
    public int MenuTimeId {get;set;}

    public string MenuType {get;set;} = string.Empty;
}
