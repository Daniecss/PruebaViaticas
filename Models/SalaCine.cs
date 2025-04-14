using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
//modelo de la entidad SalaCine
namespace CinemaAPP.Models
{
    public class SalaCine
    {
        [Key]
        public int IdSala { get; set; }
        public required string Nombre { get; set; }
        public required string Estado { get; set; }
                
                [JsonIgnore]
        public ICollection<PeliculaSalaCine> PeliculasSalasCine { get; set; } = new List<PeliculaSalaCine>();

    }
}

