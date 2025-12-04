using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medigroup.API.Models
{
    public class Medicamento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Column("categoria")]
        public string Categoria { get; set; } = string.Empty;

        [Required]
        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Required]
        [Column("fecha_expiracion")]
        public DateTime FechaExpiracion { get; set; }
    }
}
