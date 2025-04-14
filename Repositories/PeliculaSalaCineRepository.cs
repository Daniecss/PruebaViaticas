using CinemaAPP.Data;
using CinemaAPP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PeliculaSalaCineRepository : IPeliculaSalaCineRepository
{
    private readonly CinemaContext _context;

    public PeliculaSalaCineRepository(CinemaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PeliculaSalaCine>> GetAll()
    {
        return await _context.PeliculasSalasCine
            .Include(ps => ps.Pelicula)  // Incluir la relación con Pelicula
            .Include(ps => ps.SalaCine)  // Incluir la relación con SalaCine
            .ToListAsync(); // Ejecutar la consulta asincrónica
    }

    public async Task<PeliculaSalaCine?> GetByIdAsync(int id)
    {
        return await _context.PeliculasSalasCine.FindAsync(id);
    }

    public async Task<IEnumerable<PeliculaSalaCine>> GetAllAsync()
    {
        return await _context.PeliculasSalasCine
            .Include(p => p.Pelicula)  // Incluir la relación con Pelicula
            .Include(p => p.SalaCine)  // Incluir la relación con SalaCine
            .ToListAsync();
    }

    public async Task<IEnumerable<PeliculaSalaCine>> GetByPeliculaAsync(int idPelicula)
    {
        return await _context.PeliculasSalasCine
            .Where(ps => ps.IdPelicula == idPelicula)
            .ToListAsync();
    }

    public async Task<PeliculaSalaCine?> GetByPeliculaYFecha(int idPelicula, int idSalaCine)
    {
        return await _context.PeliculasSalasCine
            .FirstOrDefaultAsync(ps => ps.IdPelicula == idPelicula && ps.IdSalaCine == idSalaCine);
    }

    public async Task AddAsync(PeliculaSalaCine peliculaSalaCine)
    {
        await _context.PeliculasSalasCine.AddAsync(peliculaSalaCine);
        await _context.SaveChangesAsync();
    }

    public async Task<Pelicula?> GetByNombreAsync(string nombre)
    {
        return await _context.Peliculas
            .Include(p => p.PeliculasSalasCine) // Incluimos la relación
            .ThenInclude(ps => ps.SalaCine) // También incluimos la relación con SalaCine
            .FirstOrDefaultAsync(p => p.Nombre.Contains(nombre)); // Buscamos la película por nombre
    }

    public Task<IEnumerable<Pelicula>> ObtenerPeliculasPorFecha(string fecha)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetTotalPeliculasSalasCine()
    {
        return await _context.PeliculasSalasCine.CountAsync();
    }
    }