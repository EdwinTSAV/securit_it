using System.ComponentModel.DataAnnotations;

namespace ApiBackend.Models
{
    public class Ronda : BaseEntity
    {
        [Key]
        public int RondaId { get; set; }

        [Required]
        public int SupervisorId { get; set; }

        [Required]
        public required string Nombre { get; set; }

        [Required]
        public required DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        [Required]
        public int EstadoId { get; set; }

        public string? Observaciones { get; set; } = string.Empty;

        public int? CoordinadorId { get; set; }

        public DateTime? FechaRevision { get; set; }

        public User? Supervisor { get; set; }

        public User? Coordinador { get; set; }

        public RondaEstado? RondaEstado { get; set; }

        // Puestos de la ronda
        public ICollection<RondaPuesto> RondaPuestos { get; set; } = new List<RondaPuesto>();
    }
}
