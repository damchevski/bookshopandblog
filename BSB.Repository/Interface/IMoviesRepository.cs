using BSB.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSB.Repository.Interface
{
    public interface IMoviesRepository
    {
    
        Task<List<Movie>> GetAll();
        Task<Movie> GetMovie(Guid id);
        Task<Movie> AddMovie(Movie p);
        Task<Movie> EditMovie(Movie p);
        Task<Movie> DeleteMovie(Guid id);
        Task<List<Movie>> GroupByGenres(string Genre);
        Task<List<string>> GetAllGenres();
        void Insert(UserFavMovie entity);
    }
}
