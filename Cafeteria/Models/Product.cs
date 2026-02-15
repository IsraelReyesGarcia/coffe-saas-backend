using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafeteria.Models;

[Table("product")]
public class Product
{
    [Key]
    [Column("productid")]
    public int ProductId {get; set;}

    [MaxLength(50)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("price")]
    public float Price { get; set; }

    [MaxLength(200)]
    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Column("createat")]
    public DateTime CreateAt {get; set;} = DateTime.UtcNow;

    [Column("updateat")]
    public DateTime? UpdateAt {get; set;} = null;

    [Column("productcategoryid")]
    public int ProductCategoryId {get; set;}
    [ForeignKey(nameof(ProductCategoryId))]

    public required ProductCategorie ProductCategorie {get;set;}

}
