using ConsignadoPrivado.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ConsignadoPrivado.Identity.ORM;

public class IdentityContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
