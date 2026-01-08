using Microsoft.AspNetCore.Mvc;
using PruebaBNA.Application.Services;

namespace PruebaBNA.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController : ControllerBase
    {
        private readonly ICuentaService _cuentaService;

        public CuentasController(ICuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCuenta(int id)
        {
            var cuenta = await _cuentaService.ObtenerCuentaConMovimientos(id);
            if (cuenta == null) return NotFound("Cuenta no encontrada");
            return Ok(cuenta);
        }
    }
}