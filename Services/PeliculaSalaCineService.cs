using CinemaAPP.Models;
using CinemaAPP.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaAPP.Services
{
    public class PeliculaSalaCineService
    {
        private readonly IPeliculaSalaCineRepository _peliculaSalaCineRepository;
        private readonly IPeliculaRepository _peliculaRepository;
        private readonly ISalaCineRepository _salaCineRepository;

        public PeliculaSalaCineService(
            IPeliculaSalaCineRepository peliculaSalaCineRepository,
            IPeliculaRepository peliculaRepository,
            ISalaCineRepository salaCineRepository)
        {
            _peliculaSalaCineRepository = peliculaSalaCineRepository;
            _peliculaRepository = peliculaRepository;
            _salaCineRepository = salaCineRepository;
        }

        public async Task<IEnumerable<object>> GetAllPeliculaSalaCine()
        {
            var asignaciones = await _peliculaSalaCineRepository.GetAllAsync();

            return asignaciones.Select(p => new
            {
                IdPelicula = p.IdPelicula,
                NombrePelicula = p.Pelicula?.Nombre,
                IdSalaCine = p.IdSalaCine,
                NombreSalaCine = p.SalaCine?.Nombre,
                FechaPublicacion = p.FechaPublicacion,
                FechaFin = p.FechaFin
            }).ToList();
        }

        public async Task<IEnumerable<PeliculaSalaCineResponseDto>> ObtenerPeliculasPorFecha(DateTime fecha)
        {
            var asignaciones = await _peliculaSalaCineRepository.GetAllAsync();

            var peliculasPorFecha = asignaciones
                .Where(p =>
                {
                    DateTime fechaPubParsed;
                    return DateTime.TryParse(p.FechaPublicacion, out fechaPubParsed) && fechaPubParsed.Date == fecha.Date;
                })
                .Select(p => new PeliculaSalaCineResponseDto
                {
                    IdPelicula = p.IdPelicula,
                    IdSalaCine = p.IdSalaCine,
                    FechaPublicacion = p.FechaPublicacion,
                    FechaFin = p.FechaFin
                })
                .ToList();

            return peliculasPorFecha;
        }

        

        public async Task AsignarPeliculaASalaAsync(int idPelicula, int idSalaCine, string fechaPublicacion, string fechaFin)
        {
            var pelicula = await _peliculaRepository.GetByIdAsync(idPelicula);
            if (pelicula == null)
                throw new Exception("La película no existe.");

            var salaCine = await _salaCineRepository.GetByIdAsync(idSalaCine);
            if (salaCine == null)
                throw new Exception("La sala de cine no existe.");

            if (!DateTime.TryParse(fechaPublicacion, out var fechaPubParsed))
                throw new Exception("La fecha de publicación no es válida.");

            if (!DateTime.TryParse(fechaFin, out var fechaFinParsed))
                throw new Exception("La fecha de fin no es válida.");

            if (fechaFinParsed <= fechaPubParsed)
                throw new Exception("La fecha de fin no puede ser menor o igual a la fecha de publicación.");

            var asignacionExistente = await _peliculaSalaCineRepository
                .GetByPeliculaYFecha(idPelicula, idSalaCine);

            if (asignacionExistente != null)
                throw new Exception("La película ya está asignada a esta sala de cine.");

            var nuevaAsignacion = new PeliculaSalaCine
            {
                IdPelicula = idPelicula,
                IdSalaCine = idSalaCine,
                FechaPublicacion = fechaPublicacion,
                FechaFin = fechaFin
            };

            await _peliculaSalaCineRepository.AddAsync(nuevaAsignacion);
        }

       public async Task<IEnumerable<object>> BuscarPorFecha(DateTime fechaInicio)
        {
            var asignaciones = await _peliculaSalaCineRepository.GetAllAsync();

            // Filtrar las asignaciones que coincidan con la fecha de publicación
            var asignacionesPorFecha = asignaciones
                .Where(p =>
                {
                    DateTime fechaPubParsed;
                    return DateTime.TryParseExact(p.FechaPublicacion, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out fechaPubParsed) &&
                        fechaPubParsed.Date == fechaInicio.Date;
                })
                .Select(p => new
                {
                    IdPelicula = p.IdPelicula,
                    NombrePelicula = p.Pelicula?.Nombre,
                    IdSalaCine = p.IdSalaCine,
                    NombreSalaCine = p.SalaCine?.Nombre,
                    FechaPublicacion = p.FechaPublicacion,
                    FechaFin = p.FechaFin
                })
                .ToList();

            return asignacionesPorFecha;
        }

        public async Task<PeliculaConSalasCineDto> BuscarPeliculaPorNombreAsync(string nombre)
        {
            var pelicula = await _peliculaRepository.GetByNombreAsync(nombre);

            if (pelicula == null)
                throw new Exception("La película no fue encontrada.");

            var peliculasSalasCine = pelicula.PeliculasSalasCine.Select(ps => new PeliculaSalaCineDto
            {
                IdPelicula = ps.IdPelicula,
                IdSalaCine = ps.IdSalaCine,
                FechaPublicacion = ps.FechaPublicacion,
                FechaFin = ps.FechaFin
            }).ToList();

            return new PeliculaConSalasCineDto
            {
                IdPelicula = pelicula.IdPelicula,
                Nombre = pelicula.Nombre,
                Duracion = pelicula.Duracion,
                PeliculasSalasCine = peliculasSalasCine
            };
        }

        public async Task<IEnumerable<object>> GetDashboardData()
        {
            var asignaciones = await _peliculaSalaCineRepository.GetAllAsync();

            return asignaciones.Select(p => new
            {
                IdAsignacion = p.Id,
                IdPelicula = p.IdPelicula,
                NombrePelicula = p.Pelicula?.Nombre,
                IdSalaCine = p.IdSalaCine,
                NombreSalaCine = p.SalaCine?.Nombre,
                FechaPublicacion = p.FechaPublicacion,
                FechaFin = p.FechaFin
            }).ToList();
        }

        public async Task<int> ObtenerTotalPeliculasSalasCine()
        {
            return await _peliculaSalaCineRepository.GetTotalPeliculasSalasCine();
        }

    }
}