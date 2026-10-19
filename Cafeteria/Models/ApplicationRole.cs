using Microsoft.AspNetCore.Identity;
namespace Cafeteria.Models;

public class ApplicationRole : IdentityRole
{
    public float Pay { get; set; }
}
