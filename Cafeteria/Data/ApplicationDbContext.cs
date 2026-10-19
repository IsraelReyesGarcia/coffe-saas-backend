using Cafeteria.Models;
using Cafeteria.Models.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    /* public DbSet<Role> Roles { get; set; } */
    /* public DbSet<User> Users { get; set; } */
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Company> Company {get;set;}
    public DbSet<ProductCategorie> ProductCategories {get;set;}
    public DbSet<Product> Product {get;set;}
    public DbSet<Table> Tables {get;set;}
    public DbSet<Client> Clients {get;set;}
    public DbSet<Order> Orders {get;set;}
    public DbSet<MenuTime> MenuTimes {get;set;}
    public DbSet<Menu> Menu {get;set;}
    public DbSet<Warehouse> Warehouse {get;set;}
    public DbSet<UnitOfMeasurement> UnitOfMeasurements {get; set;}
    public DbSet<RawMaterial> RawMaterial {get;set;}
    public DbSet<OrderProduct> OrderProduct { get; set; } 
    public DbSet<InputMaterialCategory> InputMaterialCategories { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
}