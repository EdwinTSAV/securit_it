namespace ApiBackend.DTOs
{
    public class RondaPuestoDto
    {
        public int Id { get; set; }
        public int PuestoId { get; set; }
        public int RondaId { get; set; }
        public string NombreSupervisor { get; set; } = string.Empty;
        public string NombrePuesto { get; set; } = string.Empty;
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public required string Estado { get; set; }
    }
}
