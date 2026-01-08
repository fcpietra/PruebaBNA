using PruebaBNA.Domain.Common;
using PruebaBNA.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaBNA.Domain.Entities;

public class Movimiento : BaseEntity
{
    [ForeignKey("Cuenta")]
    public int CuentaId { get; set; }

    public virtual Cuenta Cuenta { get; set; } = null!;

    public DateTime Fecha { get; set; }

    [Required]
    public TipoMovimiento Tipo { get; set; }

    [MaxLength(50)]
    public string Descripcion { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Importe { get; set; }
}