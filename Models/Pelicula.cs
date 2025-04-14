using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
//componentes de la entidad Pelicula
namespace CinemaAPP.Models
{
    public class Pelicula
    {
        [Key]
        public int IdPelicula { get; set; }
        public string? Nombre { get; set; }
        public int Duracion { get; set; }

        public ICollection<PeliculaSalaCine> PeliculasSalasCine { get; set; } = new List<PeliculaSalaCine>(); 

    }
}
