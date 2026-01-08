using Microsoft.EntityFrameworkCore;
using PruebaBNA.Application.Common.Interfaces;
using PruebaBNA.Application.DTOs.Negocio;
using PruebaBNA.Application.Services;

public class CuentaService : ICuentaService
{
    private readonly IApplicationDbContext _context;

    public CuentaService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CuentaDetalleDto?> ObtenerCuentaConMovimientos(int id)
    {
        var cuenta = await _context.Cuentas
            .Include(c => c.Cliente)
            .Include(c => c.Movimientos)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cuenta == null) return null;

        return new CuentaDetalleDto
        {
            Id = cuenta.Id,
            Numero = cuenta.Numero,
            Saldo = cuenta.Saldo,
            CodSucursal = cuenta.CodSucursal,
            Titular = new ClienteDto { Nombre = cuenta.Cliente.Nombre, Apellido = cuenta.Cliente.Apellido, Cuit = cuenta.Cliente.Cuit },
            Movimientos = cuenta.Movimientos.OrderByDescending(m => m.Fecha).Select(m => new MovimientoDto
            {
                Fecha = m.Fecha,
                Tipo = m.Tipo.ToString(),
                Descripcion = m.Descripcion,
                Importe = m.Importe,
            }).ToList()
        };
    }
}