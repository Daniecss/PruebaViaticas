using CinemaAPP.Models;

 public interface IPeliculaSalaCineRepository
    {
        Task<IEnumerable<PeliculaSalaCine>> GetAll();
        Task<PeliculaSalaCine?> GetByIdAsync(int id);
        Task<IEnumerable<PeliculaSalaCine>> GetAllAsync();
        Task<IEnumerable<PeliculaSalaCine>> GetByPeliculaAsync(int idPelicula);
        Task<PeliculaSalaCine?> GetByPeliculaYFecha(int idPelicula, int idSalaCine);
        Task AddAsync(PeliculaSalaCine peliculaSalaCine);
        Task<Pelicula?> GetByNombreAsync(string nombre);
         Task<int> GetTotalPeliculasSalasCine();

    }