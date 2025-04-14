using CinemaAPP.Data;
using CinemaAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaAPP.Repositories
{
    public class PeliculaRepository : IPeliculaRepository
    {
        private readonly CinemaContext _context;

        public PeliculaRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<Pelicula?> GetByIdAsync(int idPelicula)
        {
            return await _context.Peliculas.FirstOrDefaultAsync(p => p.IdPelicula == idPelicula);
        }

        public async Task<Pelicula?> GetPeliculaById(int id)
        {
            return await _context.Peliculas.FindAsync(id);
        }

        public async Task<IEnumerable<Pelicula>> GetPeliculas()
        {
            return await _context.Peliculas.ToListAsync();
        }

        public async Task<Pelicula> AddPelicula(Pelicula pelicula)
        {
            _context.Peliculas.Add(pelicula);
            await _context.SaveChangesAsync();
            return pelicula;
        }
        

        public async Task<Pelicula> UpdatePelicula(Pelicula pelicula)
        {
            _context.Peliculas.Update(pelicula);
            await _context.SaveChangesAsync();
            return pelicula;
        }

        public async Task<bool> DeletePelicula(int id)
        {
            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula == null)
            {
                return false;
            }

            _context.Peliculas.Remove(pelicula);
            await _context.SaveChangesAsync();
            return true;
        }

         public async Task<Pelicula?> GetByNombreAsync(string nombrePelicula)
        {
            return await _context.Peliculas
                                .FirstOrDefaultAsync(p => p.Nombre == nombrePelicula);
        }
    }
}
