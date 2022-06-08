using BSB.Data;
using BSB.Data.Entity;
using BSB.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSB.Repository.Implementation
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly ApplicationDbContext _context;

        public MoviesRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<Movie> AddMovie(Movie p)
        {
            this._context.Add(p);
            await _context.SaveChangesAsync();

            return p;
        }

        public async Task<Movie> DeleteMovie(Guid id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return movie;
        }

        public async Task<Movie> EditMovie(Movie p)
        {
            this._context.Update(p);
            await _context.SaveChangesAsync();

            return p;
        }

        public async Task<List<Movie>> GetAll()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<List<string>> GetAllGenres()
        {
            List<string> res = new List<string>();

            foreach(var movie in await this.GetAll())
            {
                if (!res.Contains(movie.Genre))
                    res.Add(movie.Genre);
            }

            return res;
        }

        public async Task<Movie> GetMovie(Guid id)
        {
            return await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Movie>> GroupByGenres(string Genre)
        {
            return await _context.Movies
                .Where(x => x.Genre.Equals(Genre)).ToListAsync();
        }

        public void Insert(UserFavMovie entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");  
            }
            _context.Add(entity);
            _context.SaveChanges();
        }

    }
}
