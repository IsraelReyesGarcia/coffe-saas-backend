using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cafeteria.Models.Dtos.Client;

public class ClientDto
{
    public int ClientId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone {get; set;} = string.Empty;
    public string? Rfc { get; set; } = string.Empty;
    public DateTime CreateAt {get;set;} = DateTime.UtcNow;
    public DateTime? UpdateAt {get;set;} = null;
    public int Rating {get;set;}
}
