using System.ComponentModel.DataAnnotations;

namespace ApiBackend.Models
{
    public class Rol
    {
        [Key]
        public int RolId { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        // Usuarios con este rol
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
