using Microsoft.EntityFrameworkCore;
using PruebaBNA.Domain.Entities;
using PruebaBNA.Application.Common.Interfaces;

namespace PruebaBNA.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Cuenta> Cuentas { get; set; }
    public DbSet<Movimiento> Movimientos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Movimiento>()
        .Property(m => m.Tipo)
        .HasConversion<string>();
    }
}