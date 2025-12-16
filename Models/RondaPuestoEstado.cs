using System.ComponentModel.DataAnnotations;

namespace ApiBackend.Models
{
    public class RondaPuestoEstado
    {
        [Key]
        public int RondaPuestoEstadoId { get; set; }

        [Required]
        [MaxLength(25)]
        public required string Nombre { get; set; } = string.Empty;

        public bool Activo { get; set; }

        public ICollection<RondaPuesto> RondaPuestos { get; set; } = new List<RondaPuesto>();
    }
}