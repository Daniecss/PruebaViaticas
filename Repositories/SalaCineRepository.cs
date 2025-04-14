using CinemaAPP.Data;
using CinemaAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaAPP.Repositories
{
    public class SalaCineRepository : ISalaCineRepository
    {
        private readonly CinemaContext _context;

        public SalaCineRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SalaCine>> GetSalasCine()
        {
            return await _context.SalasCine.ToListAsync();  // Aquí se usa ToListAsync
        }

        // Obtener una sala de cine por ID
        public async Task<SalaCine?> GetSalaCineById(int id)
        {
            return await _context.SalasCine.FindAsync(id);
        }

        // Agregar una nueva sala de cine
        public async Task AddSalaCine(SalaCine salaCine)
        {
            await _context.SalasCine.AddAsync(salaCine);  // Agrega la sala de cine al contexto
            await _context.SaveChangesAsync();  // Guarda los cambios en la base de datos
        }

         public async Task<SalaCine?> GetByIdAsync(int idSalaCine)
        {
            return await _context.SalasCine
                .FirstOrDefaultAsync(s => s.IdSala == idSalaCine);
        }

        public async Task<SalaCine?> GetByNombreAsync(string nombreSala)
            {
                return await _context.SalasCine
                                    .FirstOrDefaultAsync(s => s.Nombre == nombreSala);
            }
        //  obtener la última sala
        public async Task<SalaCine?> GetUltimaSalaCine()
        {
            return await _context.SalasCine
                                 .OrderByDescending(s => s.IdSala) 
                                 .FirstOrDefaultAsync();  
        }
        // Actualizar una sala de cine existente
        public async Task UpdateSalaCine(SalaCine salaCine)
        {
            _context.Entry(salaCine).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Eliminar una sala de cine
        public async Task<bool> DeleteSalaCine(int id)
        {
            var salaCine = await _context.SalasCine.FindAsync(id);
            if (salaCine == null) return false;

            _context.SalasCine.Remove(salaCine);
            await _context.SaveChangesAsync();
            return true;
        }

        // Buscar una sala de cine por nombre y obtener su estado
        public async Task<string> GetEstadoSalaPorNombre(string nombre)
        {
            var salaCine = await _context.SalasCine
                                        .Where(s => s.Nombre.Contains(nombre))
                                        .FirstOrDefaultAsync();

            if (salaCine == null)
            {
                return "Sala no encontrada";
            }

            var peliculasEnSala = await GetPeliculasEnSala(salaCine.IdSala);
            int numPeliculas = peliculasEnSala.Count();

            if (numPeliculas < 3)
            {
                return "Sala disponible";
            }
            else if (numPeliculas >= 3 && numPeliculas <= 5)
            {
                return $"Sala con {numPeliculas} películas asignadas";
            }
            else
            {
                return "Sala no disponible";
            }
        }

        // Método para obtener las películas asociadas con una sala de cine
        public async Task<IEnumerable<Pelicula?>> GetPeliculasEnSala(int salaId)
    {
        return await _context.PeliculasSalasCine
            .Where(ps => ps.IdSalaCine == salaId)
            .Select(ps => ps.Pelicula)
            .ToListAsync();
    }

    public async Task<int> GetTotalSalasCine()
    {
        return await _context.SalasCine.CountAsync();
    }
    }
}
