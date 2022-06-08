using BSB.Data.Dto;
using BSB.Data.Entity;
using BSB.Repository.Interface;
using BSB.Service.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSB.Service.Implementation
{
    public class MoviesService : IMoviesService
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly IUserRepository userRepository;
        private readonly ILogger<MoviesService> logger;

        public MoviesService(IMoviesRepository moviesRepository, ILogger<MoviesService> logger, IUserRepository userRepository)
        {
            this._moviesRepository = moviesRepository;
            this.logger = logger;
            this.userRepository = userRepository;
        }

        public async Task<Movie> AddMovie(Movie product)
        {
            if (product.Id == null ||
                product.Image == null)
                return null;

            return await this._moviesRepository.AddMovie(product);
        }

        public async Task<bool> AddToFavourites(AddToFavouritesDto item, string userID)
        {
            var user = this.userRepository.Get(userID);
            var userFavMovies = user.UserFavouriteMovies;

            if (item.MovieId != null && userFavMovies != null)
            {
                var movie = await this._moviesRepository.GetMovie(item.MovieId);

                if (movie != null)
                {
                    UserFavMovie itemToAdd = new UserFavMovie
                    {
                        Id = Guid.NewGuid(),
                        Movie = movie,
                        MovieId = movie.Id,
                        FavMovies = userFavMovies,
                        FavMoviesId = userFavMovies.Id
                    };

                    this._moviesRepository.Insert(itemToAdd);
                    logger.LogInformation("Movie was succesfully added into Favourites");
                    return true;
                }
                return false;
            }
            logger.LogInformation("Something was wrong. ProductId or UserShoppingCard may be unaveliable!");
            return false;
        }

        public async Task<Movie> DeleteMovie(Guid? id)
        {
            if (id == null)
                return null;

            return await this._moviesRepository.DeleteMovie(id.Value);
        }

        public async Task<Movie> EditMovie(Movie movie)
        {
            if (movie.Id == null)
                return null;

            return await this._moviesRepository.EditMovie(movie);
        }

        public async Task<List<string>> GetAllGenres()
        {
            return await this._moviesRepository.GetAllGenres();
        }

        public async Task<List<Movie>> GetAllMovies(string? SearchString)
        {
            var result = new List<Movie>();

            result= await this._moviesRepository.GetAll();

            if (SearchString == null || SearchString.Equals(""))
                return result;

            var forReturn = new List<Movie>();
            foreach(var movie in result)
            {
                if (movie.Name.ToLower().Contains(SearchString.ToLower()))
                    forReturn.Add(movie);
            }

            return forReturn;
        }

        public async Task<Movie> GetMovie(Guid? id)
        {
            if (id == null)
                return null;

            return await this._moviesRepository.GetMovie(id.Value);
        }

        public async Task<AddToFavouritesDto> GetFavMoivesInfo(Guid? id)
        {
            var movie = await this.GetMovie(id);
            AddToFavouritesDto model = new AddToFavouritesDto
            {
                SelectedMovie = movie,
                MovieId = movie.Id,
            };

            return model;
        }

        public async Task<List<Movie>> GroupByGenres(string? Genre)
        {
            if (Genre == null || Genre.Equals(""))
                return await this._moviesRepository.GetAll();

            return await this._moviesRepository.GroupByGenres(Genre);
        }

    }
}
