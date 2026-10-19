using System;
using Microsoft.AspNetCore.Identity;

namespace Cafeteria.Models;

public class ApplicationUser:IdentityUser
{
    public string? Name { get; set; }
    public string? Phone { get; set; }
}