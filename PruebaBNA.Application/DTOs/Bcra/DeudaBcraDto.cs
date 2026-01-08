using System.Text.Json.Serialization;

namespace PruebaBNA.Application.DTOs.Bcra;

public class DeudaBcraDto
{
    [JsonPropertyName("entidad")]
    public string? Entidad { get; set; }

    [JsonPropertyName("periodo")]
    public int Periodo { get; set; }

    [JsonPropertyName("situacion")]
    public int Situacion { get; set; }

    [JsonPropertyName("monto")]
    public decimal Monto { get; set; }

    [JsonPropertyName("diasAtrasoPago")]
    public int? DiasAtrasoPago { get; set; }
}