using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PruebaBNA.Application.Common.Interfaces;
using PruebaBNA.Application.Common.Settings;
using PruebaBNA.Application.DTOs.Bcra;

namespace PruebaBNA.Infrastructure.Services.Bcra;

public class BcraService : IBcraService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _memoryCache;
    private readonly CacheSettings _cacheSettings;
    private readonly ILogger<BcraService> _logger;

    public BcraService(
        HttpClient httpClient,
        IMemoryCache memoryCache,
        IOptions<CacheSettings> cacheSettings,
        ILogger<BcraService> logger)
    {
        _httpClient = httpClient;
        _memoryCache = memoryCache;
        _cacheSettings = cacheSettings.Value;
        _logger = logger;
    }

    public async Task<List<DeudaBcraDto>> ObtenerDeudasPorCuitAsync(string cuit)
    {
        string cacheKey = $"BCRA_DEUDAS_{cuit}";
        if (_memoryCache.TryGetValue(cacheKey, out List<DeudaBcraDto>? deudasCache))
        {
            return deudasCache!;
        }

        try
        {
            // 1. Deserializamos la estructura COMPLETA (Root -> Results -> Periodos -> Entidades)
            var wrapper = await _httpClient.GetFromJsonAsync<BcraRootResponse>($"deudas/{cuit}");

            // 2. Navegamos hasta los datos que te importan
            var listaDeudas = new List<DeudaBcraDto>();

            if (wrapper?.Results?.Periodos != null)
            {
                listaDeudas = wrapper.Results.Periodos
                    .Where(p => p.Entidades != null)
                    .SelectMany(p => p.Entidades!)
                    .ToList();
            }

            // 3. Guardamos en caché y devolvemos
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(_cacheSettings.BcraExpirationSeconds));

            _memoryCache.Set(cacheKey, listaDeudas, cacheEntryOptions);

            return listaDeudas;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error parseando BCRA para {Cuit}", cuit);
            return new List<DeudaBcraDto>();
        }
    }
}