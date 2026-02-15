using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafeteria.Models;

[Table("tables")]
public class Table
{   
    [Key]
    [Column("tableid")]
    public int TableId {get;set;}

    [MaxLength(100)]
    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Column("zonename")]
    public int ZoneName {get;set;}

    [Column("isbusy")]
    public bool IsBusy {get;set;}
}
