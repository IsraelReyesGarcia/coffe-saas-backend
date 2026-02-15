using Cafeteria.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }

    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Company> Company {get;set;}
    public DbSet<ProductCategorie> ProductCategories {get;set;}
    public DbSet<Product> Product {get;set;}
    public DbSet<Table> Tables {get;set;}
    public DbSet<Client> Clients {get;set;}
}