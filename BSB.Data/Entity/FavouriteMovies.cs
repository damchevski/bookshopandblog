using System;
using System.Collections.Generic;
using System.Text;

namespace BSB.Data.Entity
{
    public class FavouriteMovies : Base
    {
        public string UserId { get; set; }
        public MAUser User { get; set; }
        public virtual ICollection<UserFavMovie> MovieInUserFavourites { get; set; }
    }
}
