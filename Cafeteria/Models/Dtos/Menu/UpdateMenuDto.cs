using System;

namespace Cafeteria.Models.Dtos.Menu;

public class UpdateMenuDto
{
    public string Name {get;set;} = string.Empty;

    public string Description { get; set; } = string.Empty;
    
    public int MenuTimeId {get;set;}

}
