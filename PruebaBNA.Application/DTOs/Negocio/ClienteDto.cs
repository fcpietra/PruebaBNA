using PruebaBNA.Application.DTOs.Bcra;

namespace PruebaBNA.Application.DTOs.Negocio;

public class ClienteDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Cuit { get; set; } = string.Empty;
}