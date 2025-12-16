namespace ApiBackend.DTOs
{
    public class RondaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public required string Estado { get; set; }
    }
}
