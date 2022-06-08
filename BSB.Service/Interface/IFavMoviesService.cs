using BSB.Data.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSB.Service.Interface
{
    public interface IFavMoviesService
    {
        Task<FavouritesDto> GetUserFavMoviesInfo(string userId);
        Task<bool> DeleteMovieFromUserFavMovies(string userId, Guid id);
    }
}
