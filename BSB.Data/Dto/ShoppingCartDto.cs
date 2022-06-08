using BSB.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSB.Data.Dto
{
    public class FavouritesDto
    {
        public List<UserFavMovie> Movies { get; set; }
    }
}
