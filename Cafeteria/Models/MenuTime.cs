using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cafeteria.Models
{
    [Table("menutimes")]
    public class MenuTime
    {   
        [Key]
        [Column("menutimeid")]
        public int MenuTimeId { get; set; }

        [MaxLength(50)]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("timestart")]
        public TimeOnly TimeStart { get; set; }

        [Column("timeend")]
        public TimeOnly TimeEnd { get; set; }

        [Column("createat")]
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
   
   }
}