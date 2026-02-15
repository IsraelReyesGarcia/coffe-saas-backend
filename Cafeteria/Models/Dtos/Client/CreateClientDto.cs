namespace Cafeteria.Models.Dtos.Client;

public class CreateClientDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone {get; set;} = string.Empty;
    public string? Rfc { get; set; } = string.Empty;
    public int Rating {get;set;}
}
