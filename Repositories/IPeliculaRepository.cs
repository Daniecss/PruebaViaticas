using CinemaAPP.Models;

namespace CinemaAPP.Repositories
{
    public interface IPeliculaRepository
    {
        Task<Pelicula?> GetByIdAsync(int idPelicula);
        Task<Pelicula?> GetPeliculaById(int id);
        Task<Pelicula?> GetByNombreAsync(string nombre);
        Task<IEnumerable<Pelicula>> GetPeliculas();
        Task<Pelicula> AddPelicula(Pelicula pelicula);
        Task<Pelicula> UpdatePelicula(Pelicula pelicula);
        Task<bool> DeletePelicula(int id);
    }
}
