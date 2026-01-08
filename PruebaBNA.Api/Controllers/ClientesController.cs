using Microsoft.AspNetCore.Mvc;
using PruebaBNA.Application.Common.Interfaces;

namespace PruebaBNA.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    // 1. Listado de clientes
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _clienteService.ObtenerTodosAsync());
    }

    // 2. Cliente por CUIT (con todo)
    [HttpGet("{cuit}")]
    public async Task<IActionResult> GetByCuit(string cuit)
    {
        var resultado = await _clienteService.ObtenerPorCuitAsync(cuit);
        if (resultado == null) return NotFound("Cliente no encontrado");

        return Ok(resultado);
    }
}