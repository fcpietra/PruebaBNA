using Microsoft.EntityFrameworkCore;
using PruebaBNA.Domain.Entities;

namespace PruebaBNA.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Cliente> Clientes { get; }
    DbSet<Cuenta> Cuentas { get; }
    DbSet<Movimiento> Movimientos { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}