using System.Text.Json.Serialization;

namespace PruebaBNA.Application.DTOs.Bcra;

public class BcraRootResponse
{
    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("results")]
    public BcraResults? Results { get; set; }
}

public class BcraResults
{
    [JsonPropertyName("identificacion")]
    public long Identificacion { get; set; }

    [JsonPropertyName("periodos")]
    public List<BcraPeriodo>? Periodos { get; set; }
}

public class BcraPeriodo
{
    [JsonPropertyName("periodo")]
    public string? Periodo { get; set; }

    [JsonPropertyName("entidades")]
    public List<DeudaBcraDto>? Entidades { get; set; }
}