using PruebaBNA.Application.DTOs.Negocio;
using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaBNA.Application.Services
{
    public interface ICuentaService
    {
        Task<CuentaDetalleDto?> ObtenerCuentaConMovimientos(int id);
    }
}