using CinemaAPP.Models;
using CinemaAPP.Repositories;
using CinemaAPP.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaAPP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeliculaSalaCineController : ControllerBase
    {
        private readonly IPeliculaRepository _peliculaRepository;
        private readonly ISalaCineRepository _salaCineRepository;
        private readonly PeliculaSalaCineService _peliculaSalaCineService;

        // Inyectamos las dependencias necesarias
        public PeliculaSalaCineController(
            IPeliculaRepository peliculaRepository, 
            ISalaCineRepository salaCineRepository,
            PeliculaSalaCineService peliculaSalaCineService)
        {
            _peliculaRepository = peliculaRepository;
            _salaCineRepository = salaCineRepository;
            _peliculaSalaCineService = peliculaSalaCineService;
        }

        [HttpGet("lista-simple")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _peliculaSalaCineService.GetAllPeliculaSalaCine();
            return Ok(result);
        }

        // Asignar una película a una sala
        [HttpPost("asignar-multiple")]
        public async Task<IActionResult> AsignarPeliculasASalas([FromBody] List<AsignacionPeliculaSalaCineDto> asignaciones)
        {
            if (asignaciones == null || asignaciones.Count == 0)
                return BadRequest("Debe proporcionar al menos una asignación.");

            foreach (var asignacion in asignaciones)
            {
                try
                {
                    await _peliculaSalaCineService.AsignarPeliculaASalaAsync(
                        asignacion.IdPelicula,
                        asignacion.IdSalaCine,
                        asignacion.FechaPublicacion,
                        asignacion.FechaFin
                    );
                }
                catch (Exception ex)
                {
                    return BadRequest(new
                    {
                        mensaje = $"Error al asignar película ID {asignacion.IdPelicula} a sala ID {asignacion.IdSalaCine}: {ex.Message}"
                    });
                }
            }

            return Ok(new { mensaje = "Todas las películas fueron asignadas correctamente." });
        }

        [HttpGet("buscar-por-fecha")]
        public async Task<IActionResult> BuscarPorFecha([FromQuery] string fechaInicio)
        {
            // Verificar lo que se está recibiendo como fechaInicio
            Console.WriteLine($"Fecha recibida: {fechaInicio}");

            // Validar el formato de la fecha en el formato 'yyyy/MM/dd'
            if (!DateTime.TryParseExact(fechaInicio, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out var fechaInicioParsed))
            {
                return BadRequest("La fecha proporcionada no es válida. Usa el formato yyyy-MM-dd.");
            }

            // Llamar al servicio para obtener las asignaciones por la fecha proporcionada
            var asignaciones = await _peliculaSalaCineService.BuscarPorFecha(fechaInicioParsed);

            // Verificar si se encontraron asignaciones
            if (asignaciones == null || !asignaciones.Any())
            {
                return NotFound("No se encontraron asignaciones para la fecha proporcionada.");
            }

            // Retornar las asignaciones encontradas
            return Ok(asignaciones);
        }

        [HttpGet("buscar-pelicula")]
        public async Task<IActionResult> BuscarPeliculaPorNombre(string nombre)
        {
            try
            {
                var peliculaConSalas = await _peliculaSalaCineService.BuscarPeliculaPorNombreAsync(nombre);

                if (peliculaConSalas == null)
                {
                    return NotFound(new { mensaje = "Película no encontrada." });
                }

                return Ok(peliculaConSalas);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboardData()
        {
            try
            {
                // Llamar al servicio para obtener todas las asignaciones con los detalles completos
                var result = await _peliculaSalaCineService.GetDashboardData();
                
                // Retornar las asignaciones con detalles
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = $"Error al obtener datos para el dashboard: {ex.Message}" });
            }
        }

        [HttpGet("total")]
        public async Task<IActionResult> ObtenerTotalPeliculasSalasCine()
        {
            var totalasignacion = await _peliculaSalaCineService.ObtenerTotalPeliculasSalasCine();
           return Ok(totalasignacion);
        }
        
    }
}
