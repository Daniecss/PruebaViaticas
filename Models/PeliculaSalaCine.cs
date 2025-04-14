using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
//modelo de la entidad PeliculaSalaCine.
namespace CinemaAPP.Models
{
    public class PeliculaSalaCine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [Required]
        public int IdPelicula { get; set; }

        [Required]
        public int IdSalaCine { get; set; }

        public required string FechaPublicacion { get; set; }
        public required string FechaFin { get; set; }
        //Relacion con las otras entidades
        [ForeignKey("IdPelicula")]
        [JsonIgnore]
        public virtual Pelicula? Pelicula { get; set; }

        [ForeignKey("IdSalaCine")]
        [JsonIgnore]
        public virtual SalaCine? SalaCine { get; set; }
    }
}
