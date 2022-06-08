using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSB.Data.Entity
{
    public class MAUser : IdentityUser
    {
        public string FristName { get; set; }
        public string LastName { get; set; }
        public virtual FavouriteMovies UserFavouriteMovies { get; set; }
    }
}
