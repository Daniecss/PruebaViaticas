namespace CinemaAPP.Models
{
    public class PeliculaDto
    {
        public int? IdPelicula { get; set; }
        public  string? Nombre { get; set; }
        public int Duracion { get; set; }
    }
}

//DTO de pelicula para transportar dato