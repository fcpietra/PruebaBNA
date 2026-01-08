using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PruebaBNA.Domain.Common;

namespace PruebaBNA.Domain.Entities;

public class Cliente : BaseEntity
{
    [Required]
    [MaxLength(255)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string Apellido { get; set; } = string.Empty;

    [Required]
    [MaxLength(11)]
    public string Cuit { get; set; } = string.Empty;

    public virtual ICollection<Cuenta> Cuentas { get; set; } = new List<Cuenta>();
}