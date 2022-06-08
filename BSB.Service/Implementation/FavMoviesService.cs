using BSB.Data.Dto;
using BSB.Data.Entity;
using BSB.Repository.Interface;
using BSB.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSB.Service.Implementation
{
    public class FavMoviesService : IFavMoviesService
    {
        private readonly IMoviesRepository movieRepository;
        private readonly IUserRepository userRepository;
        private readonly IFavMoviesRepository userFavMoviesRepository;
        public FavMoviesService(IMoviesRepository movieRepository, 
            IUserRepository userRepository,
            IFavMoviesRepository userFavMoviesRepository)
        {
            this.userFavMoviesRepository = userFavMoviesRepository;
            this.movieRepository = movieRepository;
            this.userRepository = userRepository;
        }
        public async Task<bool> DeleteMovieFromUserFavMovies(string userId, Guid id)
        {
            if (!string.IsNullOrEmpty(userId) && id != null)
            {
                //Select * from Users Where Id LIKE userId

                var loggedInUser = this.userRepository.Get(userId);

                var useruserFavMovies = loggedInUser.UserFavouriteMovies;

                var itemToDelete = useruserFavMovies.MovieInUserFavourites.Where(z => z.MovieId.Equals(id)).FirstOrDefault();

                useruserFavMovies.MovieInUserFavourites.Remove(itemToDelete);

                this.userFavMoviesRepository.Update(useruserFavMovies);

                return true;
            }

            return false;
        }

        public async Task<FavouritesDto> GetUserFavMoviesInfo(string userId)
        {
            var loggedInUser = this.userRepository.Get(userId);

            var useruserFavMovies = loggedInUser.UserFavouriteMovies;

            var Allmovies = useruserFavMovies.MovieInUserFavourites.ToList();

            FavouritesDto scDto = new FavouritesDto
            {
                Movies = Allmovies,
            };

            return scDto;
        }
    }
}
