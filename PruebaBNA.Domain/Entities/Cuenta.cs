using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PruebaBNA.Domain.Common;

namespace PruebaBNA.Domain.Entities;

public class Cuenta : BaseEntity
{
    [ForeignKey("Cliente")]
    public int ClienteId { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    [Required]
    [MaxLength(12)]
    public string Numero { get; set; } = string.Empty;

    [Required]
    [MaxLength(4)]
    public string CodSucursal { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Saldo { get; set; }

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
}