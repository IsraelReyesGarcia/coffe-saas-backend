using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cafeteria.Models;

[Table("users")]
public class User
{
    [Key]
    [Column("userid")]
    public int UserId {get; set;}

    [Required]
    [MaxLength(50)]
    [Column("name")]
    public  string Name {get; set;} = string.Empty;

    [MaxLength(20)]
    [Column("phone")]
    public string Phone {get; set;} = string.Empty;

    [Required]
    [MaxLength(30)]
    [Column("email")]
    public string Email {get; set;} = string.Empty;

    [Required]
    [MaxLength(200)]
    [Column("passwordhash")]
    public string? PasswordHash {get; set;} = string.Empty;

    [Column("createat")]
    public DateTime CreateAt {get; set;} = DateTime.UtcNow;

    [Column("updateat")]
    public DateTime? UpdateAt {get; set;} = null;

    [Column("roleid")]
    public int RoleId {get; set; }

    [ForeignKey(nameof(RoleId))]
    public required Role Role {get; set;}

}
