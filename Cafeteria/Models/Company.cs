using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafeteria.Models;

[Table("company")]
public class Company
{
    [Key]
    [Column("companyid")]
    public int CompanyId {get;set;}

    [Required]
    [MaxLength(50)]
    [Column("name")]
    public string Name {get; set;} = string.Empty;

    [MaxLength(20)]
    [Column("rfc")]
    public string Rfc {get; set;} = string.Empty;

    [Column("address")]
    [MaxLength(150)]
    public string Address {get; set;} = string.Empty;

    [MaxLength(20)]
    [Column("phone")]
    public string Phone {get; set;} = string.Empty;

    [MaxLength(50)]
    [Column("email")]
    public string Email {get; set;} = string.Empty;

    [Column("createat")]
    public DateTime CreateAt {get; set;} = DateTime.Now;

    [Column("updateat")]
    public DateTime? UpdateAt {get; set;} = DateTime.Now;

    [Column("userid")]
    public int? UserId {get; set;}

    [ForeignKey(nameof(UserId))]

    public required User User {get;set;}

}
