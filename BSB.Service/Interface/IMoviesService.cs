using BSB.Data.Dto;
using BSB.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSB.Service.Interface
{
    public interface IMoviesService
    {
        Task<List<Movie>> GetAllMovies(string? SearchString);
        Task<Movie> GetMovie(Guid? id);
        Task<Movie> AddMovie(Movie product);
        Task<Movie> EditMovie(Movie product);
        Task<Movie> DeleteMovie(Guid ?id);
        Task<List<Movie>> GroupByGenres(string? Genre);
        Task<List<string>> GetAllGenres();

        Task<AddToFavouritesDto> GetFavMoivesInfo(Guid? id);

        Task<bool> AddToFavourites(AddToFavouritesDto item, string userID);
    }
}
