using CinemaAPP.Models;

namespace CinemaAPP.Repositories
{
    public interface ISalaCineRepository
    {
        Task<SalaCine?> GetSalaCineById(int id);
        Task<IEnumerable<SalaCine>> GetSalasCine();
        Task AddSalaCine(SalaCine salaCine);  
        Task UpdateSalaCine(SalaCine salaCine);  
        Task<bool> DeleteSalaCine(int id);
        Task<string> GetEstadoSalaPorNombre(string nombre); 
        Task<SalaCine?> GetByIdAsync(int idSalaCine);
        Task<SalaCine?> GetUltimaSalaCine();
        Task<SalaCine?> GetByNombreAsync(string nombre);
        Task<IEnumerable<Pelicula?>> GetPeliculasEnSala(int salaId);
         Task<int> GetTotalSalasCine();
    }
}
