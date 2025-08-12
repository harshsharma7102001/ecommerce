using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Data.StoreContext;

public class StoreContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}
