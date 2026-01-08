using Microsoft.EntityFrameworkCore;
using PruebaBNA.Application.Common.Interfaces;
using PruebaBNA.Application.DTOs.Negocio;

namespace PruebaBNA.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IApplicationDbContext _context;
    private readonly IBcraService _bcraService;

    public ClienteService(IApplicationDbContext context, IBcraService bcraService)
    {
        _context = context;
        _bcraService = bcraService;
    }

    public async Task<List<ClienteDto>> ObtenerTodosAsync()
    {
        return await _context.Clientes
            .Select(c => new ClienteDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Cuit = c.Cuit
            })
            .ToListAsync();
    }

    public async Task<ClienteDetalleDto?> ObtenerPorCuitAsync(string cuit)
    {
        // 1. Busca Cliente y Cuentas en DB local
        var clienteEntity = await _context.Clientes
            .Include(c => c.Cuentas)
            .FirstOrDefaultAsync(c => c.Cuit == cuit);

        if (clienteEntity == null) return null;

        // 2. Busca deudas en API externa (o Caché)
        var deudasBcra = await _bcraService.ObtenerDeudasPorCuitAsync(cuit);

        // 3. Mapea a DTO
        return new ClienteDetalleDto
        {
            Id = clienteEntity.Id,
            Nombre = clienteEntity.Nombre,
            Apellido = clienteEntity.Apellido,
            Cuit = clienteEntity.Cuit,
            Cuentas = clienteEntity.Cuentas.Select(c => new CuentaDto
            {
                Id = c.Id,
                Numero = c.Numero,
                Saldo = c.Saldo,
                CodSucursal = c.CodSucursal
            }).ToList(),
            SituacionBcra = deudasBcra
        };
    }
}