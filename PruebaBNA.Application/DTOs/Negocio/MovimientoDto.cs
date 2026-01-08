namespace PruebaBNA.Application.DTOs.Negocio;

public class MovimientoDto
{
    public DateTime Fecha { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Importe { get; set; }
    public bool EsIngreso { get; set; }
}