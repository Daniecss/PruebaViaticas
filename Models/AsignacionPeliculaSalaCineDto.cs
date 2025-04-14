namespace CinemaAPP.Models
{
    public class AsignacionPeliculaSalaCineDto
    {
        public int IdPelicula { get; set; }
        public int IdSalaCine { get; set; }
        public string FechaPublicacion { get; set; } = string.Empty;
        public string FechaFin { get; set; } = string.Empty;
    }
}

// DTO ousado para la asignacion de peliculas y salas