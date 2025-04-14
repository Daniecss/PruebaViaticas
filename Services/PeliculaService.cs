using CinemaAPP.Data;
using CinemaAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaAPP.Services
{
    public class PeliculaService
    {
        private readonly CinemaContext _context;

        public PeliculaService(CinemaContext context)
        {
            _context = context;
        }

        // Obtener todas las películas
        public async Task<IEnumerable<Pelicula>> GetPeliculas()
        {
            return await _context.Peliculas.ToListAsync();
        }

        // Obtener una película por su ID
        public async Task<Pelicula?> GetPeliculaById(int id)
        {
            return await _context.Peliculas.FindAsync(id);
        }

        // Agregar una nueva película
        public async Task<Pelicula> AddPelicula(Pelicula pelicula)
        {
            _context.Peliculas.Add(pelicula);
            await _context.SaveChangesAsync();
            return pelicula; 
        }

        // Agregar múltiples películas
        public async Task AddMultiplePeliculas(IEnumerable<Pelicula> peliculas)
        {
            await _context.Peliculas.AddRangeAsync(peliculas);
            await _context.SaveChangesAsync();
        }

        // Actualizar una película
        public async Task UpdatePelicula(Pelicula pelicula)
        {
            _context.Entry(pelicula).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Eliminar una película
        public async Task<bool> DeletePelicula(int id)
        {
            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula == null) return false;

            _context.Peliculas.Remove(pelicula);
            await _context.SaveChangesAsync();
            return true;
        }
        // Guardar los cambios en la base de datos
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        //Total de peliculas
        public async Task<int> GetTotalPeliculas()
        {
            return await _context.Peliculas.CountAsync();
        }

    }
}
