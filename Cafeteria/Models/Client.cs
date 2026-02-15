using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafeteria.Models;

[Table("clients")]
public class Client
{   
    [Key]
    [Column("clientid")]
    public int ClientId { get; set; }

    [MaxLength(100)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(40)]
    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [MaxLength(20)]
    [Column("phone")]
    public string Phone {get; set;} = string.Empty;

    [MaxLength(20)]
    [Column("rfc")]
    public string? Rfc { get; set; } = string.Empty;

    [Column("createat")]
    public DateTime CreateAt {get;set;} = DateTime.UtcNow;

    [Column("updateat")]
    public DateTime? UpdateAt {get;set;} = null;

    [Column("rating")]
    public int Rating {get;set;}
}
