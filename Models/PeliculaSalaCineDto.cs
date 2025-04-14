namespace CinemaAPP.Models
{
 public class PeliculaSalaCineResponseDto
{
    public int IdPelicula { get; set; }
    public int IdSalaCine { get; set; }
    public required string FechaPublicacion { get; set; }
    public required string FechaFin { get; set; }
}
}

//DTO para transportar datos