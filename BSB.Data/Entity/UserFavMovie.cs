using System;
using System.Collections.Generic;
using System.Text;

namespace BSB.Data.Entity
{
    public class UserFavMovie : Base
    {
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
        public Guid FavMoviesId { get; set; }
        public FavouriteMovies FavMovies { get; set; }
    }
}
