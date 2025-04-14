using Microsoft.AspNetCore.Mvc;
using CinemaAPP.Models;
using CinemaAPP.Services;

namespace CinemaAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculaController : ControllerBase
    {
        private readonly PeliculaService _peliculaService;

        public PeliculaController(PeliculaService peliculaService)
        {
            _peliculaService = peliculaService;
        }

        // Crear una nueva película
        [HttpPost]
        public async Task<IActionResult> CrearPeliculas([FromBody] PeliculaDto peliculaDto)
        {
            if (peliculaDto == null)
            {
                return BadRequest("La película no puede estar vacía.");
            }

            var pelicula = new Pelicula
            {
                Nombre = peliculaDto.Nombre,
                Duracion = peliculaDto.Duracion
            };

            var nuevaPelicula = await _peliculaService.AddPelicula(pelicula);

            var peliculaResponseDto = new PeliculaDto
            {
                IdPelicula = nuevaPelicula.IdPelicula, 
                Nombre = nuevaPelicula.Nombre,
                Duracion = nuevaPelicula.Duracion
            };

            return Ok(peliculaResponseDto); 
        }

        // Obtener una película por su ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPeliculaById(int id)
        {
            var pelicula = await _peliculaService.GetPeliculaById(id);
            if (pelicula == null)
            {
                return NotFound(); // Si no se encuentra la película, se retorna 404
            }

            var peliculaDto = new PeliculaDto
            {
                IdPelicula = pelicula.IdPelicula,
                Nombre = pelicula.Nombre,
                Duracion = pelicula.Duracion
            };

            return Ok(peliculaDto); 
        }

        // Obtener todas las películas
        [HttpGet]
        public async Task<IActionResult> GetPeliculas()
        {
            var peliculas = await _peliculaService.GetPeliculas();
            var peliculasDto = peliculas.Select(p => new PeliculaDto
            {
                IdPelicula = p.IdPelicula,
                Nombre = p.Nombre,
                Duracion = p.Duracion
            }).ToList();

            return Ok(peliculasDto); 
        }

        // Buscar por id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePelicula(int id, [FromBody] PeliculaDto peliculaDto)
        {
            var pelicula = await _peliculaService.GetPeliculaById(id);
            if (pelicula == null)
            {
                return NotFound();
            }

            pelicula.Nombre = peliculaDto.Nombre ?? pelicula.Nombre;
            pelicula.Duracion = peliculaDto.Duracion > 0 ? peliculaDto.Duracion : pelicula.Duracion;

            await _peliculaService.UpdatePelicula(pelicula);
            return NoContent();
        }


        // Eliminar una película
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePelicula(int id)
        {
            var result = await _peliculaService.DeletePelicula(id);
            if (!result)
            {
                return NotFound(); 
            }

            return NoContent(); 
        }

        //Total de peliculas
        [HttpGet("total")]
        public async Task<IActionResult> GetTotalPeliculas()
        {
            var total = await _peliculaService.GetTotalPeliculas();
            return Ok(total);
        }

    }
}
