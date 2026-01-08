namespace PruebaBNA.Application.DTOs.Negocio;

public class CuentaDto
{
    public int Id { get; set; }
    public string Numero { get; set; } = string.Empty;
    public decimal Saldo { get; set; }
    public string CodSucursal { get; set; } = string.Empty;
}