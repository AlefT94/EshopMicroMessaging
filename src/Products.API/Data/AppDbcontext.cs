using Microsoft.EntityFrameworkCore;
using Products.API.Entities;

namespace Products.API.Data;

public class AppDbcontext : DbContext
{
    public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options){  }

    public DbSet<Product> Products { get; set; }
}
