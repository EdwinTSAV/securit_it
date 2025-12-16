using System.ComponentModel.DataAnnotations;

namespace ApiBackend.Models
{
    public class RondaEstado
    {
        [Key]
        public int RondaEstadoId { get; set; }

        [Required]
        [MaxLength(25)]
        public required string Nombre { get; set; } = string.Empty;

        public bool Activo { get; set; }

        public ICollection<Ronda> Rondas { get; set; } = new List<Ronda>();
    }
}
