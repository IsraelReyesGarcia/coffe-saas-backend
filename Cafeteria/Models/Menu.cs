using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafeteria.Models
{
    [Table("menu")]
    public class Menu
    {
        [Key]
        [Column("menuid")]
        public int MenuId {get;set;}

        [MaxLength(50)]
        [Column("name")]
        public string Name {get;set;} = string.Empty;

        [MaxLength(30)]
        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("dateregister")]
        public DateTime DateRegister { get; set; } = DateTime.UtcNow;
        
        [Column("menutimeid")]
        public int MenuTimeId {get;set;}
        [ForeignKey(nameof(MenuTimeId))]

        public required MenuTime MenuTime {get;set;}
    }
}