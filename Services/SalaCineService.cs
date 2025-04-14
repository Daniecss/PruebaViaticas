using CinemaAPP.Models;
using CinemaAPP.Repositories;

namespace CinemaAPP.Services
{
    public class SalaCineService
    {
        private readonly ISalaCineRepository _salaCineRepository;
        private readonly IPeliculaSalaCineRepository _peliculaSalaCineRepository;

        public SalaCineService(ISalaCineRepository salaCineRepository, IPeliculaSalaCineRepository peliculaSalaCineRepository)
        {
            _salaCineRepository = salaCineRepository;
            _peliculaSalaCineRepository = peliculaSalaCineRepository;
        }

        // Obtener todas las salas de cine
        public async Task<IEnumerable<SalaCine>> GetSalasCine()
        {
            return await _salaCineRepository.GetSalasCine();
        }

        // Obtener una sala de cine por ID
        public Task<SalaCine?> GetSalaCineById(int id)
        {
            return _salaCineRepository.GetSalaCineById(id);
        }

        // Crear una nueva sala de cine
        public async Task<SalaCine> AddSalaCine(SalaCine salaCine)
        {
            await _salaCineRepository.AddSalaCine(salaCine);
            return salaCine;
        }

        // Obtener la última sala de cine ingresada
        public async Task<SalaCine?> GetUltimaSalaCine()
        {
            return await _salaCineRepository.GetUltimaSalaCine();
        }

        // Actualizar una sala de cine
        public Task UpdateSalaCine(SalaCine salaCine)
        {
            return _salaCineRepository.UpdateSalaCine(salaCine);
        }

        // Eliminar una sala de cine
        public Task<bool> DeleteSalaCine(int id)
        {
            return _salaCineRepository.DeleteSalaCine(id);
        }

        //Obtener estado de una sala de cine
        public async Task<string> ObtenerEstadoSalaPorNombre(string nombreSala)
        {
            var sala = await _salaCineRepository.GetByNombreAsync(nombreSala);
            
            if (sala == null)
            {
                return "Sala no encontrada";
            }

            var peliculasEnSala = await _salaCineRepository.GetPeliculasEnSala(sala.IdSala);
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
        //Cantidad de salas
        public async Task<int> ObtenerTotalSalasCine()
        {
            return await _salaCineRepository.GetTotalSalasCine();
        }

    }
}