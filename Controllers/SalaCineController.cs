using CinemaAPP.Models;
using CinemaAPP.Services;
using Microsoft.AspNetCore.Mvc;

namespace CinemaAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaCineController : ControllerBase
    {
        private readonly SalaCineService _salaCineService;

        public SalaCineController(SalaCineService salaCineService)
        {
            _salaCineService = salaCineService;
        }

        // Obtener todas las salas de cine
        [HttpGet]
        public async Task<IActionResult> GetAllSalasCine()
        {
            var salasCine = await _salaCineService.GetSalasCine();

            // Convertir a un DTO para devolver solo los campos deseados
            var salasCineDto = salasCine.Select(s => new SalaCineDto
            {
                IdSala = s.IdSala,
                Nombre = s.Nombre,
                Estado = s.Estado
            }).ToList();

            return Ok(salasCineDto);
        }

        // Obtener una sala de cine por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalaCineById(int id)
        {
            var salaCine = await _salaCineService.GetSalaCineById(id);
            if (salaCine == null)
            {
                return NotFound();
            }

            // Convertir a un DTO para evitar películas relacionadas
            var salaCineDto = new SalaCineDto
            {
                IdSala = salaCine.IdSala,
                Nombre = salaCine.Nombre,
                Estado = salaCine.Estado
            };

            return Ok(salaCineDto);
        }

        // Buscar sala de cine por nombre y obtener estado
        [HttpGet("buscar/{nombre}")]
        public async Task<IActionResult> ObtenerEstadoSalaPorNombre(string nombre)
        {
            var estado = await _salaCineService.ObtenerEstadoSalaPorNombre(nombre);
            return Ok(new { Estado = estado });
        }

        // Crear una nueva sala de cine
        [HttpPost]
        public async Task<IActionResult> CrearSalaCine([FromBody] SalaCineDto salaCineDto)
        {
            if (salaCineDto == null)
            {
                return BadRequest("La sala de cine no puede estar vacía.");
            }

            var salaCine = new SalaCine
            {
                Nombre = salaCineDto.Nombre,
                Estado = salaCineDto.Estado
            };

            var nuevaSalaCine = await _salaCineService.AddSalaCine(salaCine);

            // Obtener la última sala de cine ingresada
            var ultimaSalaCine = await _salaCineService.GetUltimaSalaCine();

            // Mapea el objeto SalaCine a un SalaCineDto
            var salaCineResponseDto = new SalaCineDto
            {
                IdSala = ultimaSalaCine.IdSala,
                Nombre = ultimaSalaCine.Nombre,
                Estado = ultimaSalaCine.Estado
            };

            return Ok(salaCineResponseDto); 
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalaCine(int id, [FromBody] SalaCineDto salaCineUpdateDto)
        {
            if (salaCineUpdateDto == null)
            {
                return BadRequest("No se ha proporcionado ningún dato para actualizar.");
            }

            var salaCine = await _salaCineService.GetSalaCineById(id);
            if (salaCine == null)
            {
                return NotFound($"No se encontró la sala con ID {id}");
            }

            // Actualizamos solo los campos que han sido enviados
            salaCine.Nombre = salaCineUpdateDto.Nombre ?? salaCine.Nombre;
            salaCine.Estado = salaCineUpdateDto.Estado ?? salaCine.Estado;

            await _salaCineService.UpdateSalaCine(salaCine);
            return NoContent();
        }


        // Eliminar una sala de cine
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalaCine(int id)
        {
            var result = await _salaCineService.DeleteSalaCine(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("total")]
        public async Task<IActionResult> ObtenerTotalSalasCine()
        {
            var totalSalas = await _salaCineService.ObtenerTotalSalasCine();
            return Ok(totalSalas);
        }

    }
}
