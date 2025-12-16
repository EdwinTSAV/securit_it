using System.ComponentModel.DataAnnotations;

namespace ApiBackend.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public int RolId { get; set; }

        public Rol? Rol { get; set; }

        // Rondas de este Supervisor
        public ICollection<Ronda> RondasSupervisores { get; set; } = new List<Ronda>();

        // Rondas revisadas por este coordinador
        public ICollection<Ronda> RondasCoordinadores { get; set; } = new List<Ronda>();
    }
}
