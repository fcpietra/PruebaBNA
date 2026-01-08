using PruebaBNA.Application.DTOs.Negocio;

namespace PruebaBNA.Application.Common.Interfaces;

public interface IClienteService
{
    Task<List<ClienteDto>> ObtenerTodosAsync();
    Task<ClienteDetalleDto?> ObtenerPorCuitAsync(string cuit);
}