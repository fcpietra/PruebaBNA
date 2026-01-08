using PruebaBNA.Application.DTOs.Bcra;
using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaBNA.Application.DTOs.Negocio
{
    public class ClienteDetalleDto : ClienteDto
    {
        public List<CuentaDto> Cuentas { get; set; } = new();
        public List<DeudaBcraDto> SituacionBcra { get; set; } = new();
}
