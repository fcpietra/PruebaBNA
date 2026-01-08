using PruebaBNA.Application.DTOs.Negocio;
using System;
using System.Collections.Generic;
using System.Text;

public class CuentaDetalleDto : CuentaDto
{
    public ClienteDto Titular { get; set; } = null!;
    public List<MovimientoDto> Movimientos { get; set; } = new();
}
