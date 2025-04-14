public class PeliculaConSalasCineDto
{
    public int IdPelicula { get; set; }
    public required string Nombre { get; set; }
    public int Duracion { get; set; }
    public List<PeliculaSalaCineDto>? PeliculasSalasCine { get; set; }
}

public class PeliculaSalaCineDto
{
    public int IdPelicula { get; set; }
    public int IdSalaCine { get; set; }
    public required string FechaPublicacion { get; set; }
    public required string FechaFin { get; set; }
}

//Modelo para buscar datos