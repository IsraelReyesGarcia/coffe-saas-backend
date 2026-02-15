using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cafeteria.Models
{
    [Table("roles")]
    public class Role
    {
        [Key]
        public int roleid {get; set;}

        [Required]
        public string name {get; set;} = string.Empty;

        public int pay {get; set;}
    }
}