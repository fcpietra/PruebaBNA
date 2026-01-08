using PruebaBNA.Application.DTOs.Bcra;

namespace PruebaBNA.Application.Common.Interfaces;

public interface IBcraService
{
    Task<List<DeudaBcraDto>> ObtenerDeudasPorCuitAsync(string cuit);
}