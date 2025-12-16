using System.ComponentModel.DataAnnotations;

namespace ApiBackend.Models
{
    public class Puesto : BaseEntity
    {
        [Key]
        public int PuestoId { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public string? UbicacionId { get; set; }

        public bool Activo { get; set; }

        // Puestos de la ronda
        public ICollection<RondaPuesto> RondaPuestos { get; set; } = new List<RondaPuesto>();
    }
}
