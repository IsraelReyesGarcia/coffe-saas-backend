namespace Cafeteria.Models.Dtos.Table;

public class TableDto
{
    public int TableId {get;set;}
    public string Description { get; set; } = string.Empty;
    public int ZoneName {get;set;}
    public bool IsBusy {get;set;}
}
