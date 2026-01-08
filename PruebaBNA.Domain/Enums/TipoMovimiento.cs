namespace PruebaBNA.Domain.Enums;

public enum TipoMovimiento
{
    // --- INGRESOS (Créditos) ---
    HaberesSueldoAcreditado = 1,
    TransferenciaRecibida = 2,
    DepositoEnEfectivo = 3,
    DepositoPorCajeroAutomatico = 4,
    DevolucionDeGastos = 5,
    ReintegroTarjetaDeDebito = 6,
    ReintegroImpuesto = 7,
    AcreditacionDePlazoFijo = 8,
    AcreditacionDePrestamo = 9,

    // --- EGRESOS (Débitos) ---
    ExtraccionEnEfectivoCajero = 100,
    TransferenciaEmitida = 101,
    CompraTarjetaDeDebito = 102,
    PagoDeServiciosDebitoAutomatico = 103,
    DebitoAutomaticoTarjetaDeCredito = 104,
    PagoDePrestamoCuota = 105,
    TransferenciaInmediataSalienteCI = 106
}