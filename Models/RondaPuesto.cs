using System.ComponentModel.DataAnnotations;

namespace ApiBackend.Models
{
    public class RondaPuesto : BaseEntity
    {
        [Key]
        public int RondaPuestoId { get; set; }

        public int RondaId { get; set; }
        
        public int PuestoId { get; set; }

        public DateTime? FechaInicio { get; set; }
        
        public DateTime? FechaFin { get; set; }
        
        public int RondaPuestoEstadoId { get; set; }

        public string? Observaciones { get; set; } = string.Empty;

        public Ronda? Ronda { get; set; }

        public Puesto? Puesto { get; set; }

        public RondaPuestoEstado? RondaPuestoEstado { get; set; }

    }
}
